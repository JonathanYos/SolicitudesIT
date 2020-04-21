Imports Microsoft.VisualBasic
Imports Microsoft.SqlServer
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.DataTable

Partial Class SiteMaster
    Inherits MasterPage
    Dim cn As New Conn
    Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Dim _antiXsrfTokenValue As String

    Protected Sub Page_Init(sender As Object, e As System.EventArgs)
        ' The code below helps to protect against XSRF attacks
        Dim requestCookie As HttpCookie = Request.Cookies(AntiXsrfTokenKey)
        Dim requestCookieGuidValue As Guid
        If ((Not requestCookie Is Nothing) AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue)) Then
            ' Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value
            Page.ViewStateUserKey = _antiXsrfTokenValue
        Else
            ' Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
            Page.ViewStateUserKey = _antiXsrfTokenValue

            Dim responseCookie As HttpCookie = New HttpCookie(AntiXsrfTokenKey) With {.HttpOnly = True, .Value = _antiXsrfTokenValue}
            If (FormsAuthentication.RequireSSL And Request.IsSecureConnection) Then
                responseCookie.Secure = True
            End If
            Response.Cookies.Set(responseCookie)
        End If

        AddHandler Page.PreLoad, AddressOf master_Page_PreLoad
    End Sub

    Private Sub master_Page_PreLoad(sender As Object, e As System.EventArgs)
        If (Not IsPostBack) Then
            ' Set Anti-XSRF token
            ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
            ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, String.Empty)
        Else
            ' Validate the Anti-XSRF token
            If (Not DirectCast(ViewState(AntiXsrfTokenKey), String) = _antiXsrfTokenValue _
                Or Not DirectCast(ViewState(AntiXsrfUserNameKey), String) = If(Context.User.Identity.Name, String.Empty)) Then
                Throw New InvalidOperationException("Validation of Anti-XSRF token failed.")
            End If
        End If
    End Sub
    Private Sub Mostarmsj(msj As String)
        btnOk.Visible = True
        MainContent.Visible = False
        FeaturedContent.Visible = False
        lblAdv.Text = "Mensaje:"
        lblWarning.Text = msj
        pnlWarning.Visible = True
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Inicio()
    End Sub
    Private Sub Inicio()
        Dim U As String = Session("U")
        If (String.IsNullOrEmpty(U)) Then
            Response.Redirect("Login.aspx")
        End If
        Dim menu As String = Session("M")
        llenarlabels()
        OcultarLinks()
        VerificarUsuario(U)
        VerificarCuentaUsuario(menu)
    End Sub
    Private Sub llenarlabels()
        lbltitbus.Text = "Buscar No.Ticket"
        btnbuscar.Text = "Buscar Ticket"
    End Sub
    Private Sub OcultarLinks()
        lnkSegui.Visible = False
        lnkCrearT.Visible = False
        lnkLogout.Visible = False
        lnkRecord.Visible = False
        lnkCali.Visible = False
        lnkhistorial.Visible = False
    End Sub
    Private Sub VerificarUsuario(U As String)
        If String.IsNullOrEmpty(U) Or String.IsNullOrWhiteSpace(U) Then
            Response.Redirect("Login.aspx")
        Else
            lbltitulo.Text = "Solicitudes IT"
            lblUsuario.Text = U
        End If
    End Sub
    Private Sub VerificarCuentaUsuario(M As String)
        If String.IsNullOrEmpty(M) Or String.IsNullOrWhiteSpace(M) Then
            Response.Redirect("Login.aspx")
        Else
            If M = "IT" Then
                Dim Sql = "SELECT CategoriaUsuario cuenta FROM dbo.USUARIO_IT WHERE Usuario='" + Session("U") + "'"
                Dim cuenta As String = cn.obtienePalabra(Sql, "cuenta")
                If cuenta = "IT" Then
                    lnkhistorial.Visible = True
                    lnkSegui.Visible = True
                    lnkLogout.Visible = True
                    lnkCrearT.Visible = True
                    lnkCali.Visible = True
                    lnkRecord.Visible = True
                    lnkhistorial.Visible = True
                ElseIf cuenta = "SU" Then
                    lnkhistorial.Visible = True
                    lnkSegui.Visible = True
                    lnkLogout.Visible = True
                    'lnkCrearT.Visible = True
                    lnkCali.Visible = True
                    lnkRecord.Visible = True
                    lnkhistorial.Visible = True
                ElseIf cuenta = "EN" Then
                    lnkhistorial.Visible = True
                    lnkLogout.Visible = True
                    lnkCali.Visible = True
                End If
            Else
                If M = "NORM" Then
                    lnkhistorial.Visible = True
                    lnkLogout.Visible = True
                    lnkCali.Visible = True

                End If
            End If

        End If
    End Sub

    Protected Sub btnbuscar_Click(sender As Object, e As EventArgs) Handles btnbuscar.Click
        Dim resultado As Integer
        Dim numero, sql As String
        numero = (txttiecket.Text)
        sql = "SELECT COUNT(*) conteo FROM dbo.SOLICITUD WHERE Activo=1 AND NoSolicitud='" + numero + "'"
        resultado = cn.obtieneEntero(sql, "conteo")
        If (resultado = 0) Then
            Mostarmsj("No se ha encontrado resultados")
        Else
            Session("T") = numero
            Response.Redirect("Historial.aspx?v=1")
        End If
    End Sub

    Public Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        pnlWarning.Visible = False
        lblWarning.Text = ""
        MainContent.Visible = True
    End Sub
End Class