Imports System.Net.Mail

Partial Class CrearT
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
        tblServicios.Visible = False
        'sql = "SELECT COUNT(*) conteo FROM dbo.CdCategoriaIT WHERE Activo=1 AND Asignado='" + U + "'"
        'Dim mostrartareas = cn.obtieneEntero(sql, "conteo")
        'If mostrartareas = 0 Then
        '    tblTareas.Visible = False
        tblAsignacion.Visible = False
        'Else
        sql = "SELECT T.IdTipo,C.IdCategoria+'-'+T.Tipo 'Tipo' FROM dbo.CdTipoSol T INNER JOIN CdCategoriaIT C ON  C.IdCategoria=T.IdCategoria AND C.Activo=T.Activo WHERE T.Activo=1 AND T.Activo=1 ORDER BY C.IdCategoria ASC, T.Tipo ASC"
        cn.llenarCombo(sql, ddlservicio, "IdTipo", "Tipo")
        ' End If
        llenarLabels()
        llenarCombos()
        ValidarChequeo()
    End Sub
    Private Sub llenarLabels()
        txbtipo.Attributes.Add("maxlength", "70")
        txbtarea.Attributes.Add("maxlength", "50")
        btnaceptarser.Text = "Guardar Tipo"
        lblcategoria.Text = "Categoria"
        lbltipo.Text = "Servicio"
        
        lbltiposol.Text = "Tipo"
        lbltarea.Text = "Tarea"
        btnaceptartar.Text = "Guardar Tarea"
        lblasignacion.Text = "Asignar servicios a Auxiliar"
        btnguardarasig.Text = "Guardar Asignacion"
    End Sub
    Private Sub llenarCombos()
        sql = "SELECT * FROM dbo.CdCategoriaIT C-- WHERE 0<(SELECT COUNT(*) FROM dbo.CdTipoSol T WHERE T.IdCategoria=C.IdCategoria AND T.Activo=1 AND T.Responsable='" + U + "') "
        cn.llenarCombo(sql, ddlcategoria, "IdCategoria", "Categoria")
    End Sub

    Protected Sub btnaceptarser_Click(sender As Object, e As EventArgs) Handles btnaceptarser.Click
        If (ddlcategoria.SelectedIndex = 0 Or String.IsNullOrEmpty(txbtipo.Text)) Then
        Else
            Try
                Dim categoria, descripcion As String
                categoria = ddlcategoria.SelectedValue
                descripcion = txbtipo.Text
                sql = "SELECT MAX(IdTipo) conteo FROM dbo.CdTipoSol"
                Dim conteo As Integer = cn.obtieneEntero(sql, "conteo")
                Dim aumento = conteo + 1
                lblconteo.Text = aumento

                sql = " INSERT INTO dbo.CdTipoSol VALUES('" + lblconteo.Text + "','" + descripcion + "','" + categoria + "',1,NULL,NULL,NULL,NULL)"
                cn.ejecutarSQL(sql)
                limpiarsol()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End If
    End Sub
    Private Sub limpiarsol()
        txbtipo.Text = ""
        ddlcategoria.SelectedIndex = 0
    End Sub
    Protected Sub btnaceptartar_Click(sender As Object, e As EventArgs) Handles btnaceptartar.Click
        If (ddlservicio.SelectedIndex = 0 Or String.IsNullOrEmpty(txbtarea.Text)) Then
        Else
            Try
                Dim tipo, tarea As String
                tipo = ddlservicio.SelectedValue
                tarea = txbtarea.Text
                sql = "SELECT MAX(IdTarea) conteo FROM dbo.CdTipoTarea"
                Dim conteo = cn.obtieneEntero(sql, "conteo")
                Dim aumento = conteo + 1

                sql = "INSERT INTO dbo.CdTipoTarea(IdTarea,Tipo,TipoSol,Activo,Usuario) VALUES('" + aumento.ToString() + "','" + tarea + "','" + tipo + "',1,'" + U + "')"
                'Response.Write(sql)
                cn.ejecutarSQL(sql)
                ddlservicio.SelectedIndex = 0
                txbtarea.Text = ""
            Catch ex As Exception

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

    Protected Sub btnguardarasig_Click(sender As Object, e As EventArgs) Handles btnguardarasig.Click
        If (chkasignacion.Checked = True) Then
            sql = "SELECT COUNT(*) conteo FROM dbo.CdCategoriaIT WHERE Asignado='" + U + "' AND Activo=1 AND Auxiliar IS NOT NULL AND CambioaAuxiliar =0"
            Dim conteo = cn.obtieneEntero(sql, "conteo")
            If (conteo > 0) Then
                sql = "UPDATE DBO.CdCategoriaIT SET CambioaAuxiliar=1 WHERE Activo=1 AND Asignado='" + U + "' AND Auxiliar IS NOT NULL AND CambioaAuxiliar=0"
                cn.ejecutarSQL(sql)
                ValidarChequeo()
            End If

        Else
            sql = "SELECT COUNT(*) conteo FROM CdCategoriaIT WHERE Asignado='" + U + "' AND Activo=1 AND Auxiliar IS NOT NULL AND CambioaAuxiliar=1"
            Dim conteo = cn.obtieneEntero(sql, "conteo")
            If (conteo > 0) Then
                sql = "UPDATE DBO.CdCategoriaIT SET CambioaAuxiliar=0 WHERE Activo=1 AND Asignado='" + U + "' AND CambioaAuxiliar=1 AND Auxiliar IS NOT NULL "
                cn.ejecutarSQL(sql)
                ValidarChequeo()
            End If
        End If
    End Sub
    Private Sub ValidarChequeo()
        sql = "SELECT COUNT(*) conteo FROM dbo.CdCategoriaIT WHERE Asignado='" + U + "' AND Activo=1 AND Auxiliar IS NOT NULL AND CambioaAuxiliar =0"
        Dim conteo = cn.obtieneEntero(sql, "conteo")

        sql = "SELECT COUNT(*) conteo FROM CdCategoriaIT WHERE Asignado='" + U + "' AND Activo=1 AND Auxiliar IS NOT NULL AND CambioaAuxiliar=1"
        Dim conteo2 = cn.obtieneEntero(sql, "conteo")
        If (conteo < conteo2) Then
            chkasignacion.Checked = True
        Else
            chkasignacion.Checked = False
        End If
    End Sub
End Class
