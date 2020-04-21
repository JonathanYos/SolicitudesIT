Imports System.Net.Mail

Partial Class Recordatorio
    Inherits System.Web.UI.Page
    Dim ituser, sql, U, catPre, opcion As String
    Dim cn As New Conn
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        U = Session("U")
        If (IsPostBack = False) Then
            sql = "SELECT COUNT(*) conteo FROM dbo.USUARIO_IT WHERE Activo =1 AND Usuario='" + U + "'"
            Dim conteo = cn.obtieneEntero(sql, "conteo")
            If conteo = 0 Then
                Response.Redirect("Login.aspx")
            End If
            valoresIniciales()
        End If
    End Sub
    Private Sub valoresIniciales()
        llenarLabels()
        llenarCombos()
    End Sub
    Private Sub llenarLabels()
        btnguardar.Text = "Enviar Recordatorio"
        lblSolicitante.Text = "Solicitante"
        lblservicio.Text = "Servicio"
    End Sub
    Private Sub llenarCombos()
        sql = "SELECT IdTipo,Tipo FROM dbo.CdTipoSol WHERE Activo=1 AND Responsable='LizandroC'"
        cn.llenarCombo(sql, ddlservicior, "IdTipo", "Tipo")
        sql = "SELECT EmployeeId,CompleteName FROM dbo.FwEmployee WHERE  Email IS NOT NULL ORDER BY CompleteName ASC"
        cn.llenarComboFam(sql, ddlSolicitante, "EmployeeId", "CompleteName")
    End Sub
    Protected Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        If (ddlSolicitante.SelectedIndex = 0 Or ddlservicior.SelectedIndex = 0) Then
            Response.Write("Campos Obligatorios Vacios")
        Else
            Try
                Dim mensaje, asunto, tipo, codtipo, solicitante, categoria As String
                tipo = ddlservicior.SelectedItem.Text
                codtipo = ddlservicior.SelectedValue
                sql = "SELECT Email FROM dbo.FwEmployee WHERE  EmployeeId='" + ddlSolicitante.SelectedValue + "'"
                solicitante = cn.obtienePalabraFam(sql, "Email")
                sql = "SELECT Categoria FROM dbo.CdCategoriaIT WHERE IdCategoria=(SELECT IdCategoria FROM dbo.CdTipoSol WHERE IdTipo='" + codtipo + "')"
                categoria = cn.obtienePalabra(sql, "Categoria")
                mensaje = "<h1>!Solicitud Pendiente¡</h1><br/>" +
                            "<p>El usuario " + U + " ha enviado un recordatorio de solicitud pendiente para que ingrese una Solicitud con el servicio" + _
                            " <b>" + tipo + "</b> de la categoria <b>" + categoria + "</b></p>"
                asunto = "Recordatorio para Ticket"
                enviar(solicitante, mensaje, asunto)

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If
    End Sub
    Private Sub enviar(correo As String, mensaje As String, asunto As String)
        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        Dim eMensaje1 As String
        Dim eTo, eNuestroCorreo, eNuestraContraseña As String
        Try
            eNuestroCorreo = "SolicitudesIT@fundacionfde.onmicrosoft.com"
            eNuestraContraseña = "S0l1c1tud3s.IT"
            eMensaje1 = mensaje
            eTo = correo
            SmtpServer.Port = 587
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network

            SmtpServer.Host = "smtp-mail.outlook.com"
            SmtpServer.EnableSsl = True
            SmtpServer.Credentials = New Net.NetworkCredential _
            (eNuestroCorreo, eNuestraContraseña)
            mail = New MailMessage()
            mail.From = New MailAddress(eNuestroCorreo)
            mail.To.Add(eTo)
            mail.Subject = asunto
            mail.Body = eMensaje1
            mail.IsBodyHtml = True
            SmtpServer.Send(mail)
        Catch ex As SmtpException
            Throw New SmtpException(ex.ToString)
        End Try
    End Sub
End Class
