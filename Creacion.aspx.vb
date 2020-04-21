Imports System.Data
Imports System.Net.Mail

Partial Class Creacion
    Inherits System.Web.UI.Page
    Dim ituser, sql, U, catPre, var, correo As String
    Dim cn As New Conn

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        U = Session("U")
        If (IsPostBack = False) Then
            ValidarCasos()
        End If
    End Sub
    Private Sub ValidarCasos()
        var = Request.QueryString("v")
        If (String.IsNullOrEmpty(var)) Then
            Response.Redirect("Historial.aspx")
        Else
            Dim tipoDeCuenta As String = Session("M")
            txtdescripcion.Attributes.Add("maxlength", "497")
            sql = "SELECT Tipo, IdTipo FROM dbo.CdTipoSol WHERE Activo=1 AND IdCategoria='" + var + "'  ORDER BY Tipo ASC"
            cn.llenarCombo(sql, ddlservicio, "IdTipo", "Tipo")
            sql = "SELECT Categoria FROM dbo.CdCategoriaIT WHERE IdCategoria='" + var + "'"
            Dim dt As DataTable
            cn.llenarDataTable(sql, dt)
            Dim rrow As DataRow = dt.Rows(0)
            lblcategoria.Text = " " + HttpUtility.HtmlDecode(rrow(0).ToString.Replace("&nbsp;", ""))
            lblestado.Text = " Estado: Solicitud"
            sql = "SELECT CASE WHEN CambioaAuxiliar=1 THEN CASE WHEN Auxiliar IS NULL THEN Asignado ELSE Auxiliar END  ELSE Asignado END Usuario FROM dbo.CdCategoriaIT WHERE Activo='1' AND IdCategoria='" + var + "'"
            Dim asignado As String = cn.obtienePalabra(sql, "Usuario")
            correo = HttpUtility.HtmlDecode(asignado) + "@guate.commonhope.org"
            lblVdesc.Text = correo
            txttitulo.Text = HttpUtility.HtmlDecode(rrow(0).ToString.Replace("&nbsp;", ""))
            lblhora.Text = " -   "
            lblfecha.Text = " -   "
            lblticket.Text = " -   "
            txttitulo.Enabled = False
            btnAceptar.Text = "Guardar"
            pnlopcionIT.Visible = False
            Dim confirmartipoUsuario As String = Session("M")
            sql = "SELECT EmployeeId,CompleteName FROM dbo.FwEmployee WHERE  Email IS NOT NULL ORDER BY CompleteName ASC"

            If (tipoDeCuenta = "IT") Then
                cn.llenarComboFam(sql, ddlSolicitante, "EmployeeId", "CompleteName")
                cn.llenarComboFam(sql, ddlparausuario, "EmployeeId", "CompleteName")
            Else
                cn.llenarComboEmp(ddlSolicitante, U)
                cn.llenarComboEmp(ddlparausuario, U)
            End If
            sql = "SELECT * FROM dbo.CdPrioridad ORDER BY Prioridad ASC"
            cn.llenarCombo(sql, ddlprioridad, "IdPrioridad", "Prioridad")
            sql = "SELECT * FROM dbo.USUARIO_IT WHERE CategoriaUsuario !='EN'"
            cn.llenarCombo(sql, ddlusuarioIT, "Usuario", "Usuario")
            ddlSolicitante.SelectedValue = U
            If (tipoDeCuenta = "NORM") Then
                sql = "SELECT COUNT(*) categoria FROM dbo.USUARIO_IT WHERE Activo=1 AND Usuario='" + U + "'"
                Dim tipou = cn.obtieneEntero(sql, "categoria")

                If (tipou > 0) Then
                    pnlopcionIT.Visible = True
                    pnlprioridad.Visible = True
                    pnlservicio.Visible = False
                    pnlsolicitante.Visible = True
                    ddlSolicitante.SelectedValue = U
                    pnlasignado.Visible = False
                    pnlparausuario.Visible = True
                Else
                    pnlopcionIT.Visible = True
                    pnlprioridad.Visible = True
                    pnlservicio.Visible = False
                    pnlsolicitante.Visible = False
                    pnlasignado.Visible = False
                    pnlparausuario.Visible = False
                End If
            Else
                pnlopcionIT.Visible = True
                pnlprioridad.Visible = True
                pnlservicio.Visible = True
                pnlsolicitante.Visible = True
                pnlasignado.Visible = True
                pnlparausuario.Visible = True
                ddlSolicitante.SelectedValue = U
            End If
            BuscarCorrelativo()
        End If

    End Sub


    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        VerificarNumero()
    End Sub
    Private Sub VerificarNumero()
        Dim nosol = Convert.ToInt32(lblticket.Text)
        Dim conteo As Int32
        sql = "SELECT COUNT(*) conteo FROM dbo.SOLICITUD WHERE NoSolicitud = '" + lblticket.Text + "'"
        conteo = cn.obtieneEntero(sql, "conteo")
        Dim tipo As String = ddlservicio.SelectedValue
        Dim soli As String = ddlSolicitante.SelectedValue
        Dim Prio As String = ddlprioridad.SelectedValue
        Dim it As String = ddlusuarioIT.SelectedValue
        If conteo = 0 Then
            Dim confirmartipoUsuario As String = Session("M")
            If (confirmartipoUsuario = "NORM") Then
                sql = "SELECT CategoriaUsuario categoria FROM dbo.USUARIO_IT WHERE Activo = 1 AND Usuario = '" + U + "'"
                Dim tipou = cn.obtienePalabra(sql, "categoria")
                If (tipou = "EN") Then
                    If (ddlprioridad.SelectedIndex = 0 Or ddlSolicitante.SelectedIndex = 0 Or ddlparausuario.SelectedIndex = 0) Then
                        btnOk.Text = "Ok"
                        Mostrarmsj("Debe llenar los demas campos")
                    Else
                        ProcesoGuardar("NULL", "'" + ddlSolicitante.SelectedValue + "'", "'" + ddlprioridad.SelectedValue + "'", "365", "NULL")
                    End If
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Else
                    If (ddlprioridad.SelectedIndex = 0) Then
                        btnOk.Text = "Ok"
                        Mostrarmsj("Debe llenar los demas campos")
                    Else
                        ProcesoGuardar("NULL", "'" + U + "'", "'" + Prio + "'", "365", "NULL")
                    End If
                End If
            Else
                If (ddlprioridad.SelectedIndex = 0 Or ddlservicio.SelectedIndex = 0 Or ddlSolicitante.SelectedIndex = 0 Or ddlusuarioIT.SelectedIndex = 0) Then
                    btnOk.Text = "Ok"
                    Mostrarmsj("Debe llenar los demas campos")
                Else
                    If (ddlparausuario.SelectedIndex = 0) Then

                        ProcesoGuardar("'" + tipo + "'", "'" + soli + "'", "'" + Prio + "'", "'" + it + "'", "NULL")
                    Else
                        ProcesoGuardar("'" + tipo + "'", "'" + soli + "'", "'" + Prio + "'", "'" + it + "'", "'" + ddlparausuario.SelectedValue + "'")
                    End If

                End If

            End If


        Else
            nosol = nosol + 1
            lblticket.Text = nosol.ToString()
            VerificarNumero()
        End If

    End Sub
    Private Sub ProcesoGuardar(ByVal tipo As String, ByVal Soli As String, ByVal prio As String, ByVal IT As String, ByVal parausuario As String)
        Try
            Dim titulo, descripcion, ticket, solicitante, categoria, asignado, correoasignado As String
            titulo = Request.QueryString("v")
            descripcion = txtdescripcion.Text
            ticket = lblticket.Text

            sql = "SELECT IdCategoria FROM dbo.CdCategoriaIT WHERE Categoria='" + HttpUtility.HtmlDecode(txttitulo.Text) + "'"
            categoria = cn.obtienePalabra(sql, "IdCategoria")
            sql = "SELECT CASE WHEN CambioaAuxiliar=1 THEN CASE WHEN Auxiliar IS NULL THEN Asignado ELSE Auxiliar END  ELSE Asignado END Usuario FROM dbo.CdCategoriaIT WHERE Activo='1' AND IdCategoria='" + categoria + "'"
            asignado = cn.obtienePalabra(sql, "Usuario")

            If (IT = "365") Then
                asignado = "'" + asignado + "'"
            Else
                asignado = IT
            End If

            sql = "INSERT INTO dbo.SOLICITUD( NoSolicitud, FechaCreacion, Solicitante, FechaSolicitud, Tipo, Descripcion, UsuarioIT, Estado, Finalizacion, Hrs, Minutos, Sitio, Solucion, Registro, Activo, Prioridad, Inventario, Meta, NoPresencial, Categoria, FechaEstado, Calificacion, NotasCalificacion, ParaUsuario) VALUES('" + ticket + "',GETDATE()," + Soli + ",GETDATE()," + tipo + ",'" + descripcion + "'," + asignado + ",'SOLI', NULL, NULL, NULL, NULL, NULL,'" + U + "',1," + prio + ",0,NULL,0,'" + categoria + "', GETDATE(), NULL, NULL," + parausuario + ")"


            cn.ejecutarSQL(sql)
            Dim mensaje As String = "<h1>!Gracias por Utilizar SolicitudesIT¡</h1>" +
                                    "<p>Su <b>ticket #" + ticket + " " + txttitulo.Text + "</b> ha sido recibido</p>"
            Dim asunto2 As String = "IT-" + txttitulo.Text + "( " + Soli + " )"
            Dim asunto As String = "Ticket #" + ticket + " " + txttitulo.Text
            Dim mensaje2 As String = "<b>Número de Ticket : </b>" + ticket + "</br>" +
                                    "<b>Tipo de Solicitud : </b>" + txttitulo.Text + "</br>" +
                                    "<b>Usuario: </b>" + U + "</br>" +
                                    "<b>Mensaje del Usuario: </b>" + descripcion + "</br>" +
                                    "<b>Fecha de Solicitud: </b>" + Date.Now
            Soli = Soli.Replace("'", "")
            sql = "SELECT Email FROM dbo.FwEmployee WHERE EmployeeId='" + Soli + "'"
            solicitante = cn.obtienePalabraFam(sql, "Email")
            Response.Write(solicitante)


            'sql = "SELECT CASE WHEN EstadoAuxiliar=1 THEN CASE WHEN Auxiliar IS NULL THEN Responsable ELSE Auxiliar END  ELSE Responsable END Usuario FROM dbo.CdTipoSol WHERE Activo=1 AND IdTipo='" + titulo + "'"
            'asignado = cn.obtienePalabra(sql, "Usuario")
            sql = "SELECT Email FROM dbo.FwEmployee WHERE EmployeeId=" + asignado + ""
            correoasignado = cn.obtienePalabraFam(sql, "Email")
            enviar(solicitante, asunto, mensaje)
            enviar(correoasignado, asunto2, mensaje2)
            btnOk.Text = "Aceptar"
            Mostrarmsj("Ticket #" + ticket + " Ingresado Exitosamente")
        Catch ex As Exception
            btnOk.Text = "Ok"
            Mostrarmsj(ex.Message)
        End Try

    End Sub

    Private Sub Mostrarmsj(msj As String)

        btnOk.Visible = True
        lblAdv.Text = "Mensaje:"
        lblWarning.Text = msj
        pnlWarning.Visible = False
        pnlAdv.Visible = True
    End Sub
    Private Sub BuscarCorrelativo()
        Try
            Dim Vsolicitud As Integer

            sql = "SELECT NoSolicitud  FROM dbo.SOLICITUD WHERE Activo=1  ORDER BY NoSolicitud DESC"
            Vsolicitud = cn.obtieneEntero(sql, "NoSolicitud")
            If Vsolicitud = 0 Then
                Vsolicitud = 1
                lblticket.Text = Vsolicitud
            Else
                Vsolicitud = Vsolicitud + 1
                lblticket.Text = Vsolicitud
            End If

        Catch ex As Exception
            btnOk.Text = "Okr"
            Mostrarmsj(ex.Message)
        End Try
    End Sub
    Private Sub enviar(correo As String, asunto As String, mensaje As String)
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

    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If btnOk.Text = "Aceptar" Then
            Dim tipoDeCuenta As String = Session("M")
            If (tipoDeCuenta = "IT") Then
                Response.Redirect("SeguimientoS.aspx")
            Else
                Response.Redirect("Historial.aspx?v=2")
            End If

        Else
            btnOk.Visible = False
            pnlWarning.Visible = True
            pnlAdv.Visible = False
        End If
    End Sub
End Class
