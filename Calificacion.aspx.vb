Imports System.Data
Imports System.Net.Mail

Partial Class Calificacion
    Inherits System.Web.UI.Page
    Dim ituser, sql, U, catPre, opcion, ticket3 As String
    Dim cn As New Conn
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        U = Session("U")
        'Response.Redirect("Historial.aspx?v=2")
        If (IsPostBack = False) Then
            Dim var = Request.QueryString("v")
            If var = 1 Then
                If String.IsNullOrEmpty(Session("Cal")) Then
                    Response.Redirect("Historial.aspx?v=2")
                Else

                End If
                LlenarLabels()
                llenarinformacion(Session("Cal"))
            ElseIf var = 2 Then
                lblavisono.Visible = False
                LlenarLabels()
                llenarhistorial()
                VerificarRegistrosHistorial()
                pnlWarning.Visible = False
                pnlhistorial.Visible = True
            Else
                Response.Redirect("Historial.aspx?v=2")
            End If
        End If

    End Sub
    Private Sub VerificarRegistrosHistorial()
        Dim conteoHistorial As String = gvhistorial.Rows.Count.ToString()
        If (conteoHistorial = "0") Then
            lblavisono.Text = "No tiene servicios que calificar"
            lblavisono.Visible = True
        Else
            lblavisono.Visible = False
        End If
    End Sub
    Private Sub LlenarLabels()
        txtcomentarios.Attributes.Add("maxlength", "300")
        btnaceptar.Text = "Aceptar"
        lblcalificacion.Text = "Califique Nuestro Servicio"
        lblcomentarios.Text = "Comentarios"
    End Sub
    Private Sub llenarhistorial()
        sql = "SELECT S.NoSolicitud, T.Tipo,S.UsuarioIT,E.Estado,P.Prioridad FROM dbo.SOLICITUD S INNER JOIN dbo.CdTipoSol T ON S.Tipo = T.IdTipo AND T.Activo=1 INNER JOIN dbo.CdEstadoSol E ON E.IdEstado=S.Estado AND E.Activo=1 INNER JOIN dbo.CdPrioridad P ON P.IdPrioridad=S.Prioridad WHERE S.Activo=1 AND S.Estado IN('FINA') AND S.Calificacion IS NULL AND S.Solicitante='" + U + "' AND S.FechaCreacion BETWEEN CONVERT(varchar,'2020-02-15',110) AND GETDATE()"
        cn.llenarGrid(sql, gvhistorial)

    End Sub
    Protected Sub btnaceptar_Click(sender As Object, e As EventArgs) Handles btnaceptar.Click
        Dim valor = hdfvalor.Value
        If (String.IsNullOrEmpty(valor)) Then
            btnOk.Text = "Aceptar"
            Mostrarmsj("Favor de calificar la solicitud")
        Else
            Try
                Dim mensaje, asunto, asignado, correo As String
                sql = "SELECT UsuarioIT FROM dbo.SOLICITUD WHERE NoSolicitud=" + lblticket.Text + " AND Activo=1"
                asignado = cn.obtienePalabra(sql, "UsuarioIT")
                sql = "SELECT Email FROM dbo.FwEmployee WHERE EmployeeId='" + asignado + "'"
                correo = cn.obtienePalabraFam(sql, "Email")
                Dim desc As String = txtcomentarios.Text
                sql = "INSERT INTO dbo.SOLICITUD(NoSolicitud,FechaCreacion,Solicitante,FechaSolicitud,Tipo,Descripcion,UsuarioIT,Estado,Finalizacion,Hrs,Minutos,Sitio,Solucion,Registro,Activo,Prioridad,Inventario,Meta,NoPresencial,Categoria,FechaEstado,Calificacion,NotasCalificacion,ParaUsuario) SELECT NoSolicitud,GETDATE() FechaCreacion,Solicitante,FechaSolicitud,Tipo,Descripcion, UsuarioIT, Estado,Finalizacion,Hrs ,Minutos,Sitio, Solucion, '" + U + "' Registro,Activo,Prioridad, Inventario, Meta,NoPresencial,Categoria,FechaEstado,'" + valor + "' Calificacion,'" + desc + "' NotasCalificacion,ParaUsuario FROM dbo.SOLICITUD WHERE Activo = 1 And NoSolicitud = '" + lblticket.Text + "'"
                cn.ejecutarSQL(sql)
                mensaje = "<h1>!Han calificado su servicio¡</h1><br/>" +
                            "<p>El ticket <b>#" + lblticket.Text + "</b> ha sido calificado</p>"
                asunto = "Solicitud Calificada"

                ' enviar(correo, mensaje, asunto)
                llenarhistorial()
                VerificarRegistrosHistorial()
                btnOk.Text = "Ok"
                Mostrarmsj("Calificacion Ingresada exitosamente")

            Catch ex As Exception
                btnOk.Text = "Aceptar"
                Mostrarmsj("Por favor envie esta informacion a sistemas: " + ex.Message + ".")
            End Try
        End If

    End Sub

    Protected Sub gvhistorial_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvhistorial.RowCommand
        Dim index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
        Dim notarea As String = gvhistorial.Rows(index).Cells(1).Text.ToString()
        llenarinformacion(notarea)
    End Sub
    Private Sub llenarinformacion(valor As String)
        sql = "SELECT NoSolicitud,Solicitante,Tipo ,Descripcion ,FORMAT(FechaEstado,'dd/MM/yyyy') FechaEstado, UsuarioIT, Prioridad, Sitio , Estado, CONVERT(VARCHAR,FechaCreacion,106),CONVERT(VARCHAR,FechaCreacion,108),Meta FROM dbo.SOLICITUD WHERE NoSolicitud='" + valor + "' AND Activo=1"
        Dim Historial As DataTable

        cn.llenarDataTable(sql, Historial)

        Dim rrow As DataRow = Historial.Rows(0)
        lblticket.Text = HttpUtility.HtmlDecode(rrow(0).ToString.Replace("&nbsp;", ""))
        lblsolicitante.Text = rrow(1).ToString()
        sql = "SELECT C.Categoria FROM dbo.CdTipoSol T INNER JOIN dbo.CdCategoriaIT C ON T.IdCategoria=C.IdCategoria WHERE T.Activo=1 AND T.IdTipo='" + rrow(2).ToString() + "'"
        lblcategoria.Text = cn.obtienePalabra(sql, "Categoria")
        sql = "SELECT Estado FROM dbo.CdEstadoSol WHERE Activo=1 AND IdEstado='" + rrow(8).ToString() + "'"
        lblestado.Text = "Estado: " + cn.obtienePalabra(sql, "Estado")
        lblfecha.Text = rrow(9).ToString()
        lblhora.Text = rrow(10).ToString()
        lblusuarioit.Text = HttpUtility.HtmlDecode(rrow(5).ToString.Replace("&nbsp;", ""))
        Response.Write(HttpUtility.HtmlDecode(rrow(5).ToString.Replace("&nbsp;", "")))
        pnlhistorial.Visible = False
        pnlWarning.Visible = True
    End Sub
    Private Sub Mostrarmsj(mensaje As String)
        pnlAdv.Visible = True
        lblAdv.Text = "Mensaje"
        lblWarning.Text = mensaje
        pnlWarning.Visible = False
        pnlhistorial.Visible = False
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If btnOk.Text = "Aceptar" Then
            pnlhistorial.Visible = False
            pnlWarning.Visible = True
            pnlAdv.Visible = False
        Else
            If btnOk.Text = "Ok" Then
                pnlhistorial.Visible = True
                pnlAdv.Visible = False
                pnlWarning.Visible = False
            End If

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
