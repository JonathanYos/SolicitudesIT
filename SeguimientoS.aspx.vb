Imports System.Net.Mail
Imports System.Data

Partial Class SeguimientoS
    Inherits System.Web.UI.Page
    Dim ituser, sql, U, catPre, opcion, ticket3 As String
    Dim cn As New Conn
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        U = Session("U")
        If Page.IsPostBack Then
        Else
            sql = "SELECT COUNT(*) conteo FROM dbo.USUARIO_IT WHERE Activo =1 AND Usuario='" + U + "'"
            Dim conteo = cn.obtieneEntero(sql, "conteo")
            If conteo = 0 Then
                Response.Redirect("Login.aspx")
            End If
            campo2.Visible = False
            pnlfiltros.Visible = False
            pnlWarning.Visible = False
            pnltarea.Visible = False
            pnlAdv.Visible = False
            gvhistorial.Visible = True
            campo1.Visible = True
            LLenarHistorialSolicitud()
            VerificarRegistrosHistorial()
            sql = "SELECT S.NoSolicitud,  CAT.Categoria, CDT.Tipo, cdE.Estado,Convert(VARCHAR, S.FechaSolicitud, 105) 'Fecha de Solicitud',SUBSTRING(S.Descripcion,1,30)+'...' Descripcion, S.Solicitante FROM Solicitud S LEFT JOIN CdPrioridad cdP ON S.Prioridad = cdP.IdPrioridad LEFT JOIN CdEstadoSol cdE ON S.Estado = cdE.IdEstado INNER JOIN CdCategoriaIT CAT ON S.Categoria=CAT.IdCategoria AND CAT.Activo=1 LEFT JOIN CdTipoSol CDT ON CDT.IdTipo= S.Tipo AND CDT.Activo=1  WHERE S.Activo = 1 AND UsuarioIT='" + U + "' AND S.Estado IN('FINA','DUPL') ORDER BY NoSolicitud DESC"
            LlenarSolicitudInact(sql)
            btnVerInactivos.Text = "Historial"
        End If
    End Sub


    Private Sub VerificarCuenta()
        Dim cuenta As String
        cuenta = Session("M")
        If cuenta = "NORM" Or String.IsNullOrEmpty(cuenta) Then
            Response.Redirect("Solicitante.aspx")
        End If
    End Sub
    Private Sub LlenarSolicitudInact(ByVal sql As String)
        cn.llenarGrid(sql, gvhistorialinac)
    End Sub
    Private Sub ValoresIniciales(ticket As String)
        campo2.Visible = False
        campo1.Visible = False
        gvhistorial.Visible = False
        pnltarea.Visible = True
        pnlWarning.Visible = True
        Llenarlabels()
        LlenarCombos(ticket)
        LLenarInfoSolicitud(ticket)
        ticket3 = lblticket.Text
        VerificarCuenta()
        BuscarCorrelativo(ticket)
        LLenarHistorialTarea()
        LLenarHistorialSolicitud()
    End Sub
    Private Sub Llenarlabels()
        txbmeta.Attributes.Add("maxlength", "10")
        txbdescriptarea.Attributes.Add("maxlength", "97")
        txtsolucion.Attributes.Add("maxlength", "297")
        lblasignado.Text = "Asignado"
        lblestadosol.Text = "Estado"
        lblprioridad.Text = "Prioridad"
        lblmeta.Text = "Meta"
        lblsitio.Text = "Sitio"
        btnguardarsol.Text = "Guardar Solicitud"
        lbltarea.Text = "Tareas"
        btnguardartarea.Text = "Guardar Tarea"
        btnVerInactivos.Text = "Historial"
        'ddltipotarea.Enabled = False
    End Sub

    Private Sub LLenarHistorialTarea()
        sql = "SELECT T.NoTarea,T.Asignado,TT.Tipo,E.Estado,CONVERT(VARCHAR,T.FechaEstado,106)+' '+CONVERT(VARCHAR,T.FechaEstado,108) 'Fecha de Estado' FROM dbo.TAREA T INNER JOIN dbo.CdTipoTarea TT ON T.TipoTarea=TT.IdTarea INNER JOIN dbo.CdEstadoSol E ON T.Estado=E.IdEstado AND E.Activo=1 WHERE T.Activo=1 AND T.Solicitud='" + lblticket.Text + "' AND T.Asignado='" + U + "'"
        cn.llenarGrid(sql, gvhistorialtarea)
    End Sub
    Private Sub LLenarHistorialSolicitud()
        sql = "SELECT S.NoSolicitud,  CAT.Categoria, CDT.Tipo, cdE.Estado,SUBSTRING(S.Descripcion,1,30)+'...' Descripcion,Convert(VARCHAR, S.FechaSolicitud, 105) + ' ' + CONVERT(VARCHAR, DATEDIFF(dd, FechaSolicitud, GETDATE()))+' dias' AS 'Fecha de Solicitud', S.Solicitante FROM Solicitud S LEFT JOIN CdPrioridad cdP ON S.Prioridad = cdP.IdPrioridad LEFT JOIN CdEstadoSol cdE ON S.Estado = cdE.IdEstado INNER JOIN CdCategoriaIT CAT ON S.Categoria=CAT.IdCategoria AND CAT.Activo=1 LEFT JOIN CdTipoSol CDT ON CDT.IdTipo= S.Tipo AND CDT.Activo=1  WHERE S.Activo = 1 AND UsuarioIT='" + U + "' AND S.Estado IN('SOLI','PROC') ORDER BY S.FechaSolicitud DESC,cdE.Estado ASC"
        cn.llenarGrid(sql, gvhistorial)
    End Sub
    Private Sub LLenarInfoSolicitud(valor As String)
        sql = "SELECT NoSolicitud,Solicitante ,Tipo ,Descripcion ,FORMAT(FechaEstado,'dd/MM/yyyy') FechaEstado, UsuarioIT, Prioridad, Sitio , Estado, CONVERT(VARCHAR,FechaCreacion,105),CONVERT(VARCHAR,FechaCreacion,108),Meta,Categoria,Solucion,ParaUsuario FROM dbo.SOLICITUD WHERE NoSolicitud='" + valor + "' AND Activo=1"
        Dim Historial As DataTable

        cn.llenarDataTable(sql, Historial)

        Dim rrow As DataRow = Historial.Rows(0)
        lblticket.Text = HttpUtility.HtmlDecode(rrow(0).ToString.Replace("&nbsp;", ""))
        lblsolicitante.Text = rrow(1).ToString()
        lblusuarioit.Text = rrow(5).ToString()
        sql = "SELECT C.Categoria FROM dbo.SOLICITUD S INNER JOIN dbo.CdCategoriaIT C ON S.Categoria=C.IdCategoria WHERE S.Activo=1 AND C.IdCategoria='" + rrow(12).ToString() + "'"
        lblcategoria.Text = cn.obtienePalabra(sql, "Categoria")
        sql = "SELECT COUNT(*) conteo FROM dbo.CdTipoSol TS INNER JOIN SOLICITUD S ON TS.IdTipo=S.Tipo WHERE S.Activo=1 AND S.NoSolicitud=" + valor + ""
        If (cn.obtieneEntero(sql, "conteo") = 0) Then
            txttitulo.Text = ""
        Else
            sql = "SELECT CASE WHEN TS.Tipo IS NULL THEN '' ELSE TS.Tipo END Tipo FROM dbo.CdTipoSol TS INNER JOIN SOLICITUD S ON TS.IdTipo=S.Tipo WHERE S.Activo=1 AND S.NoSolicitud=" + valor + ""
            txttitulo.Text = cn.obtienePalabra(sql, "Tipo")
        End If

        llenartareas(rrow(2).ToString())
        sql = "SELECT Estado FROM dbo.CdEstadoSol WHERE Activo=1 AND IdEstado='" + rrow(8).ToString() + "'"
        lblestado.Text = "Estado: " + cn.obtienePalabra(sql, "Estado")
        lblfecha.Text = rrow(9).ToString()
        lblhora.Text = rrow(10).ToString()
        If (rrow(8).ToString() = "SOLI") Then
        Else
            ddlestado.SelectedValue = rrow(8).ToString()
        End If
        txbdescripcion.Text = HttpUtility.HtmlDecode(rrow(3).ToString.Replace("&nbsp;", ""))
        ddlasignado.SelectedValue = HttpUtility.HtmlDecode(rrow(5).ToString.Replace("&nbsp;", ""))
        ddlprioridad.Enabled = True
        ddlprioridad.SelectedValue = HttpUtility.HtmlDecode(rrow(6).ToString.Replace("&nbsp;", ""))
        ddlprioridad.Enabled = False
        ddlsitio.SelectedValue = HttpUtility.HtmlDecode(rrow(7).ToString.Replace("&nbsp;", ""))
        txbmeta.Text = HttpUtility.HtmlDecode(rrow(11).ToString.Replace("&nbsp;", ""))
        txtsolucion.Text = HttpUtility.HtmlDecode(rrow(13).ToString.Replace("&nbsp;", ""))
        Dim parausuario As String = HttpUtility.HtmlDecode(rrow(14).ToString.Replace("&nbsp;", ""))
        If (String.IsNullOrEmpty(parausuario)) Then
            lblparausuario.Text = " - "
        Else
            lblparausuario.Text = parausuario
        End If
        txbdescripcion.Enabled = False
        txttitulo.Enabled = False
    End Sub
    Private Sub LlenarCombos(tipo As String)
        Dim sql As String
        sql = "SELECT * FROM dbo.CdSitio"
        cn.llenarCombo(sql, ddlsitio, "IdSitio", "Sitio")
        sql = "SELECT * FROM dbo.CdEstadoSol WHERE Activo=1 AND IdEstado NOT LIKE 'SOLI' ORDER BY Orden"
        cn.llenarCombo(sql, ddlestado, "IdEstado", "Estado")
        sql = "SELECT * FROM dbo.USUARIO_IT WHERE Activo=1 AND CategoriaUsuario!='EN'"
        cn.llenarCombo(sql, ddlasignado, "Usuario", "Usuario")
        sql = "SELECT * FROM dbo.CdPrioridad"
        cn.llenarCombo(sql, ddlprioridad, "IdPrioridad", "Prioridad")
    End Sub
    Private Sub llenartareas(tipo As String)
        If (String.IsNullOrEmpty(tipo)) Then
        Else
            sql = "SELECT IdTarea, Tipo FROM dbo.CdTipoTarea WHERE TipoSol='" + tipo + "' AND Activo=1"
            cn.llenarCombo(sql, ddltipotarea, "IdTarea", "Tipo")
        End If
    End Sub
    Private Sub BuscarCorrelativo(valor As String)
        Try
            Dim Vsolicitud As Integer

            sql = "SELECT CASE WHEN NoTarea IS NULL THEN 0 ELSE NoTarea END NoTarea FROM dbo.TAREA WHERE Activo=1 AND Solicitud='" + valor + "' ORDER BY NoTarea DESC "
            Vsolicitud = cn.obtieneEntero(sql, "NoTarea")
            If Vsolicitud = 0 Then
                Vsolicitud = 1
                lblVdesc.Text = Vsolicitud
            Else
                Vsolicitud = Vsolicitud + 1
                lblVdesc.Text = Vsolicitud
            End If

        Catch ex As Exception
            lblVdesc.Text = "ERROR:" + ex.Message
        End Try
    End Sub
    Private Sub enviar(correo As String, mensaje As String, asunto As String)
        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        Dim eMensaje1, eMensaje2, ePie As String
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

    Protected Sub gvhistorialtarea_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvhistorialtarea.RowDataBound
        For Each gvrow As GridViewRow In gvhistorialtarea.Rows
            Dim me2 As String = gvrow.Cells(4).Text

            Dim btn As Button = gvrow.FindControl("btnfinalizado")
            Dim btn2 As Button = gvrow.FindControl("btnselect")
            If (me2.Contains("Resuelto")) Then
                btn.Visible = False
                btn2.Visible = False
            End If
            'LLenarHistorialTarea()
        Next
    End Sub

    Protected Sub btnguardartarea_Click(sender As Object, e As EventArgs) Handles btnguardartarea.Click
        sql = "SELECT CASE WHEN MAX(NoTarea) IS NULL THEN 0 ELSE MAX(NoTarea) END conteo FROM dbo.TAREA WHERE Solicitud='" + lblticket.Text + "'"
        Dim notareactual, nuevonotarea As Integer
        notareactual = cn.obtieneEntero(sql, "conteo")
        nuevonotarea = notareactual + 1
        lblVdesc.Text = nuevonotarea
        IngresarTarea()
        LLenarHistorialTarea()
    End Sub
    Private Sub VerificarRegistrosHistorial()
        Dim conteoHistorial As String = gvhistorial.Rows.Count.ToString()
        If (conteoHistorial = "0") Then
            lblhistorialV.Text = "No tiene servicios para dar seguimiento"
            pnlhaysolicitu.Visible = True
            campo1.Visible = False
        Else
            pnlhaysolicitu.Visible = False
            campo1.Visible = True
        End If
    End Sub

    Private Sub IngresarTarea()
        Dim ticket As String
        ticket = lblticket.Text
        Try
            If (ddltipotarea.SelectedIndex = 0) Then
                btnOk.Text = "Aceptar"
                mostrarmsj("Campos requeridos vacios")
            Else
                Dim tarea, solicitud, tipo, presencial, asignado, estado, comentario As String
                tarea = lblVdesc.Text
                solicitud = lblticket.Text
                tipo = ddltipotarea.SelectedValue
                asignado = U
                comentario = txbdescriptarea.Text
                estado = "SOLI"
                If (cknopresente.Checked = True) Then
                    presencial = "1"
                Else
                    presencial = "0"
                End If

                Dim consulta As String = "INSERT INTO dbo.TAREA(NoTarea,Solicitud,FechaCreacion,TipoTarea,Comentario,Presencial,Asignado,Activo,Estado,FechaEstado) VALUES('" + tarea + "','" + solicitud + "',GETDATE(),'" + tipo + "','" + comentario + "','" + presencial + "','" + asignado + "',1,'" + estado + "',GETDATE())"
                cn.ejecutarSQL(consulta)
                If (lblVdesc.Text = 1) Then
                    Dim query As String = "INSERT INTO dbo.SOLICITUD(NoSolicitud,FechaCreacion,Solicitante,FechaSolicitud,Tipo,Descripcion,UsuarioIT,Estado,Finalizacion,Hrs,Minutos,Sitio,Solucion,Registro,Activo,Prioridad,Inventario,Meta,NoPresencial,Categoria,FechaEstado,Calificacion,NotasCalificacion,ParaUsuario) SELECT NoSolicitud,GETDATE() FechaCreacion,Solicitante,FechaSolicitud,Tipo,Descripcion, UsuarioIT,'PROC' Estado,Finalizacion,Hrs ,Minutos,Sitio,Solucion,'" + U + "' Registro,Activo,Prioridad,Inventario,Meta,NoPresencial,Categoria,GETDATE() FechaEstado,Calificacion,NotasCalificacion,ParaUsuario FROM dbo.SOLICITUD WHERE Activo = 1 And NoSolicitud = '" + solicitud + "'"
                    cn.ejecutarSQL(query)
                    sql = "SELECT E.Estado FROM dbo.SOLICITUD S INNER JOIN dbo.CdEstadoSol E ON E.IdEstado = S.Estado WHERE S.NoSolicitud = " + solicitud + " And S.Activo = 1"
                    Dim EstadoActual As String = cn.obtienePalabra(sql, "Estado")
                    lblestado.Text = "Estado: " + EstadoActual
                End If
                ddltipotarea.SelectedIndex = 0
                txbdescriptarea.Text = ""
                cknopresente.Checked = False
            End If
        Catch ex As Exception
            btnOk.Text = "Aceptar"
            mostrarmsj("Por favor envie este mensaje a Sistemas: " + ex.Message + ".")
        End Try
    End Sub

    Protected Sub gvhistorialtarea_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvhistorialtarea.RowCommand
        Dim index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
        Dim notarea As String = gvhistorialtarea.Rows(index).Cells(1).Text.ToString()
        If (e.CommandName = "cmdfinalizado") Then
            Dim tipo As String = gvhistorialtarea.Rows(index).Cells(3).Text.ToString()
            Dim sql2 As String = "SELECT TipoTarea FROM dbo.TAREA WHERE NoTarea='" + notarea + "' AND Solicitud='" + lblticket.Text + "' AND Activo = 1"
            Dim codigo As String
            codigo = cn.obtienePalabra(sql2, "TipoTarea")
            Dim sql3 As String = "INSERT INTO dbo.TAREA(NoTarea,Solicitud,FechaCreacion,TipoTarea,Comentario,Presencial,Asignado,Activo,Estado,FechaEstado) SELECT NoTarea,Solicitud,GETDATE() FechaCreacion,TipoTarea,Comentario,Presencial,'" + U + "' Asignado,Activo,'FINA' Estado,GETDATE() FechaEstado FROM dbo.TAREA WHERE NoTarea='" + notarea + "' AND Solicitud='" + lblticket.Text + "' AND TipoTarea = '" + codigo + "' And Activo =1"
            cn.ejecutarSQL(sql3)
            Dim sql4 As String = "UPDATE dbo.TAREA SET Activo=0 WHERE NoTarea='" + notarea + "' AND  Solicitud='" + lblticket.Text + "' AND Estado='SOLI'"
            cn.ejecutarSQL(sql4)
            LLenarHistorialTarea()
        ElseIf (e.CommandName = "cmdselect") Then
            sql = "SELECT TipoTarea FROM dbo.TAREA WHERE Activo=1 AND NoTarea='" + notarea + "' AND Solicitud='" + lblticket.Text + "'"
            Dim tipo = cn.obtienePalabra(sql, "TipoTarea")
            sql = "SELECT Comentario FROM dbo.TAREA WHERE Activo=1 AND NoTarea='" + notarea + "' AND Solicitud='" + lblticket.Text + "'"
            Dim comentario = cn.obtienePalabra(sql, "Comentario")
            sql = "SELECT Presencial FROM dbo.TAREA WHERE Activo=1 AND NoTarea='" + notarea + "' AND Solicitud='" + lblticket.Text + "'"
            Dim presencial = cn.obtienePalabra(sql, "Presencial")
            ddltipotarea.SelectedValue = tipo
            txbdescriptarea.Text = comentario
            If presencial = "1" Then
                cknopresente.Checked = True
            ElseIf presencial = "0" Then
                cknopresente.Checked = False
            End If
            ddltipotarea.Enabled = False
            cknopresente.Enabled = False
            btneliminar.Visible = True
            btnmodificar.Visible = True
            btncancelar.Visible = True
            btnguardartarea.Visible = False
            lblguardarvalor.Text = notarea
        End If
    End Sub

    Protected Sub btnguardarsol_Click(sender As Object, e As EventArgs) Handles btnguardarsol.Click
        Dim asignado, prioridad, estado, sitio, meta, fecha As String
        asignado = ddlasignado.SelectedValue
        estado = ddlestado.SelectedValue
        sitio = ddlsitio.SelectedValue
        meta = txbmeta.Text
        sql = "SELECT COUNT(*) FROM dbo.TAREA WHERE Activo=1  AND Solicitud='905' AND ESTADO!='FINA'"

        If (ddlsitio.SelectedIndex = 0) Or ddlasignado.SelectedIndex = 0 Then
            btnOk.Text = "Aceptar"
            mostrarmsj("Llene los campos obligatorios")
        Else
            sql = "SELECT COUNT(*) conteo FROM dbo.TAREA WHERE Activo=1  AND Solicitud='" + lblticket.Text + "' AND ESTADO!='FINA'"
            If ddlestado.SelectedIndex = 0 Then
                estado = ""
                fecha = ""
                ProcesoG(estado, fecha, asignado, meta, sitio)
            Else
                estado = "'" + ddlestado.SelectedValue + "' "
                fecha = "GETDATE() "
                Dim valor5 = cn.obtieneEntero(sql, "conteo")

                If valor5 > 0 Then
                    btnOk.Text = "Aceptar"
                    mostrarmsj("Ha dejado tareas incompletas")
                Else
                    ProcesoG(estado, fecha, asignado, meta, sitio)
                End If
            End If

        End If
    End Sub
    Private Sub ProcesoG(estado As String, fecha As String, asignado As String, meta As String, sitio As String)
        sql = "SELECT COUNT(*) conteo FROM dbo.SOLICITUD WHERE Activo=1 AND NoSolicitud='" + lblticket.Text + "' AND UsuarioIT='" + asignado + "'"
        Dim CorreoAotraPersona As Integer = cn.obtieneEntero(sql, "conteo")
        Dim solucion As String = txtsolucion.Text
        If (CorreoAotraPersona = 0) Then
            Dim solic = lblsolicitante.Text
            Dim Solicitante, Asunto, Mensaje As String
            sql = "SELECT Email FROM dbo.FwEmployee WHERE EmployeeId='" + solic + "'"
            Solicitante = cn.obtienePalabraFam(sql, "Email")
            Asunto = "Delegacion de Solicitud"
            Mensaje = "<h1>Se le ha delegado una Solicitud</h1><br/>" +
                "<p>El usuario " + U + " le ha delegado la solicitud con numero de ticket #" + lblticket.Text + " para que pueda darle seguimiento</p>"
            enviar(Solicitante, Mensaje, Asunto)
        End If
        If (ddlestado.SelectedValue = "FINA") Then
            Dim solic = lblsolicitante.Text
            sql = "SELECT Email FROM dbo.FwEmployee WHERE EmployeeId='" + solic + "'"
            Dim solicitante = cn.obtienePalabraFam(sql, "Email")
            Dim mensaje = "<h1>!Su solicitud ha sido completada¡</h1><br/>" +
                        "<p>Su ticket #" + lblticket.Text + " ha sido completado por favor califique nuestro servicio</p>"
            Dim Asunto As String = "Ticket #" + lblticket.Text + " -Resuelto-"
            enviar(solicitante, mensaje, Asunto)
        End If
        If (ddlestado.SelectedValue = "DUPL") Then
            Dim solic = lblsolicitante.Text
            sql = "SELECT Email FROM dbo.FwEmployee WHERE EmployeeId='" + solic + "'"
            Dim solicitante = cn.obtienePalabraFam(sql, "Email")
            Dim mensaje = "<h1>!Su solicitud fue marcada como duplicado¡</h1><br/>" +
                        "<p>Alguien mas reporto esta solicitud y se esta dando seguimiento, para mas informacion consulte con " + U + "</p>"
            Dim Asunto As String = "Ticket #" + lblticket.Text + " -Duplicado-"
            enviar(solicitante, mensaje, Asunto)
        End If
        If (ddlestado.SelectedValue = "CERR") Then
            Dim solic = lblsolicitante.Text
            sql = "SELECT Email FROM dbo.FwEmployee WHERE EmployeeId='" + solic + "'"
            Dim solicitante = cn.obtienePalabraFam(sql, "Email")
            Dim mensaje = "<h1>!Su solicitud fue marcada como Cerreda¡</h1><br/>" +
                        "<p>Se ha marcado su solicitud como cerrada para mas información consultar con " + U + ".</p>"
            Dim Asunto As String = "Ticket #" + lblticket.Text + " -Cerrado-"
            enviar(solicitante, mensaje, Asunto)
        End If
        sql = "INSERT INTO dbo.SOLICITUD(NoSolicitud,FechaCreacion,Solicitante,FechaSolicitud,Tipo,Descripcion,UsuarioIT,Estado,Finalizacion,Hrs,Minutos,Sitio,Solucion,Registro,Activo,Prioridad,Inventario,Meta,NoPresencial,Categoria,FechaEstado,Calificacion,NotasCalificacion,ParaUsuario) SELECT NoSolicitud,GETDATE() FechaCreacion,Solicitante,FechaSolicitud,Tipo,Descripcion,'" + asignado + "' UsuarioIT," + estado + " Estado,Finalizacion,Hrs ,Minutos,'" + sitio + "' Sitio,'" + solucion + "' Solucion,'" + U + "' Registro,Activo,Prioridad, Inventario,'" + meta + "' Meta,NoPresencial, Categoria," + fecha + " FechaEstado,Calificacion,NotasCalificacion,ParaUsuario FROM dbo.SOLICITUD WHERE Activo = 1 And NoSolicitud = '" + lblticket.Text + "'"
        cn.ejecutarSQL(sql)
        LLenarHistorialSolicitud()
        Response.Redirect("SeguimientoS.aspx")
    End Sub
    Private Sub mostrarmsj(msj As String)
        btnOk.Visible = True
        lblAdv.Text = "Mensaje:"
        lblWarning.Text = msj
        pnlAdv.Visible = True

        pnltarea.Visible = False
        campo1.Visible = False
        gvhistorial.Visible = False
        pnlWarning.Visible = False
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If (btnOk.Text = "Aceptar") Then
            pnlWarning.Visible = True
            pnltarea.Visible = True
            pnlAdv.Visible = False
            campo1.Visible = False
            gvhistorial.Visible = False
        End If
        If (btnOk.Text = "Ok") Then
            pnlWarning.Visible = False
            pnltarea.Visible = False
            pnlAdv.Visible = False
            campo1.Visible = False
            gvhistorial.Visible = True
            LLenarHistorialSolicitud()
            ticket3 = ""
            VerificarRegistrosHistorial()
        End If

    End Sub

    Protected Sub gvhistorial_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvhistorial.RowCommand
        Dim index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
        Dim notarea As String = gvhistorial.Rows(index).Cells(0).Text.ToString()
        ValoresIniciales(notarea)
        btnVerInactivos.Visible = False
        gvhistorialinac.Visible = False
        pnlfiltros.Visible = False

    End Sub

    Protected Sub btnguardarser_Click(sender As Object, e As EventArgs) Handles btnguardarser.Click
        Dim valor = hdfvalor2.Value
        If (String.IsNullOrEmpty(valor) And String.IsNullOrEmpty(txttitulo.Text)) Then
            btnOk.Text = "Aceptar"
            mostrarmsj("Por fabor seleccione un servicio")
            ddltipotarea.Enabled = False
        Else
            Try
                U = Session("U")
                ' sql = "SELECT IdTipo, Tipo FROM dbo.CdTipoTarea WHERE TipoSol='" + valor + "' AND Activo=1"
                'cn.llenarCombo(sql, ddltipotarea, "IdTipo", "Tipo")
                sql = "SELECT IdCategoria FROM dbo.CdTipoSol WHERE Activo=1 AND IdTipo='" + valor + "'"
                Dim categoria As String = cn.obtienePalabra(sql, "IdCategoria")
                sql = "INSERT INTO dbo.SOLICITUD(NoSolicitud,FechaCreacion,Solicitante,FechaSolicitud,Tipo,Descripcion,UsuarioIT,Estado,Finalizacion,Hrs,Minutos,Sitio,Solucion,Registro,Activo,Prioridad,Inventario,Meta,NoPresencial,Categoria,FechaEstado,Calificacion,NotasCalificacion) SELECT NoSolicitud,GETDATE() FechaCreacion,Solicitante,FechaSolicitud,'" + valor + "' Tipo,Descripcion, UsuarioIT, Estado,Finalizacion,Hrs ,Minutos,Sitio,Solucion,'" + U + "' Registro,Activo,Prioridad,Inventario, Meta,NoPresencial,'" + categoria + "' Categoria,FechaEstado,Calificacion,NotasCalificacion FROM dbo.SOLICITUD WHERE Activo = 1 And NoSolicitud = '" + lblticket.Text + "'"
                cn.ejecutarSQL(sql)
                Dim ticket As String = lblticket.Text
                ValoresIniciales(ticket)
                ddltipotarea.Enabled = True
            Catch ex As Exception
                btnOk.Text = "Aceptar"
                mostrarmsj("ERROR - " + ex.Message)
            End Try

        End If
    End Sub

    Protected Sub btnVerInactivos_Click(sender As Object, e As EventArgs) Handles btnVerInactivos.Click
        If (btnVerInactivos.Text = "Historial") Then
            gvhistorialinac.Visible = True
            campo2.Visible = True
            btnVerInactivos.Text = "Ocultar Historial"
            pnlfiltros.Visible = True
            Filtros()
        Else
            gvhistorialinac.Visible = False
            campo2.Visible = False
            btnVerInactivos.Text = "Historial"
            pnlfiltros.Visible = False
            pnlfiltros.Visible = False
        End If

    End Sub

    Protected Sub gvhistorialinac_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvhistorialinac.RowCommand
        Dim index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
        Dim notarea As String = gvhistorialinac.Rows(index).Cells(1).Text.ToString()
        ValoresIniciales(notarea)
        btnguardarser.Visible = False
        btnguardarsol.Visible = False
        btnguardartarea.Visible = False
        pnlhaysolicitu.Visible = False
        gvhistorialinac.Visible = False
        campo2.Visible = False
        btnVerInactivos.Visible = False
        pnlfiltros.Visible = False
    End Sub
    Protected Sub Filtros()
        sql = "SELECT * FROM dbo.CdPrioridad"
        cn.llenarCombo(sql, ddlprioridadfil, "IdPrioridad", "Prioridad")
        sql = " SELECT IdEstado,Estado FROM CdEstadoSol WHERE Activo=1 ORDER BY Orden ASC"
        cn.llenarCombo(sql, ddlestadofil, "IdEstado", "Estado")
        sql = "SELECT * FROM dbo.USUARIO_IT WHERE Activo=1 AND CategoriaUsuario!='EN'"
        cn.llenarCombo(sql, ddlusuarioitfil, "Usuario", "Usuario")
        sql = "select * from CdCategoriaIT WHERE Activo=1"
        cn.llenarCombo(sql, ddlcategoriafil, "IdCategoria", "Categoria")
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        If (ddlprioridadfil.SelectedIndex = 0 And ddlcategoriafil.SelectedIndex = 0 And ddlusuarioitfil.SelectedIndex = 0 And ddlestadofil.SelectedIndex = 0) Then
            sql = "SELECT S.NoSolicitud,  CAT.Categoria, CDT.Tipo, cdE.Estado,S.UsuarioIT,Convert(VARCHAR, S.FechaSolicitud, 105) 'Fecha de Solicitud',SUBSTRING(S.Descripcion,1,30)+'...' Descripcion, S.Solicitante FROM Solicitud S LEFT JOIN CdPrioridad cdP ON S.Prioridad = cdP.IdPrioridad LEFT JOIN CdEstadoSol cdE ON S.Estado = cdE.IdEstado INNER JOIN CdCategoriaIT CAT ON S.Categoria=CAT.IdCategoria AND CAT.Activo=1 LEFT JOIN CdTipoSol CDT ON CDT.IdTipo= S.Tipo AND CDT.Activo=1  WHERE S.Activo = 1  ORDER BY S.FechaSolicitud DESC,cdE.Estado ASC"
            LlenarSolicitudInact(sql)
        Else
            Dim prioridad, categoria, usuarioIT, sitio As String
            If (ddlprioridadfil.SelectedIndex = 0) Then
                prioridad = "1=1"
            Else
                prioridad = "S.Prioridad='" + ddlprioridadfil.SelectedValue + "'"
            End If
            If (ddlcategoriafil.SelectedIndex = 0) Then
                categoria = "1=1"
            Else
                categoria = "S.Categoria = '" + ddlcategoriafil.SelectedValue + "'"
            End If
            If (ddlusuarioitfil.SelectedIndex = 0) Then
                usuarioIT = "1=1"
            Else
                usuarioIT = "S.UsuarioIT='" + ddlusuarioitfil.SelectedValue + "'"
            End If
            If (ddlestadofil.SelectedIndex = 0) Then
                sitio = "1 = 1"
            Else
                sitio = "S.Estado = '" + ddlestadofil.SelectedValue + "'"
            End If

            sql = "SELECT S.NoSolicitud,  CAT.Categoria, CDT.Tipo, cdE.Estado,Convert(VARCHAR, S.FechaSolicitud, 105) 'Fecha de Solicitud',SUBSTRING(S.Descripcion,1,30)+'...' Descripcion, S.Solicitante FROM Solicitud S LEFT JOIN CdPrioridad cdP ON S.Prioridad = cdP.IdPrioridad LEFT JOIN CdEstadoSol cdE ON S.Estado = cdE.IdEstado INNER JOIN CdCategoriaIT CAT ON S.Categoria=CAT.IdCategoria AND CAT.Activo=1 LEFT JOIN CdTipoSol CDT ON CDT.IdTipo= S.Tipo AND CDT.Activo=1  WHERE S.Activo = 1 AND " + prioridad + " AND " + usuarioIT + " AND " + sitio + " AND " + categoria + "  ORDER BY S.FechaSolicitud DESC,cdE.Estado ASC"
            LlenarSolicitudInact(sql)
        End If
    End Sub

    Protected Sub btnmodificar_Click(sender As Object, e As EventArgs) Handles btnmodificar.Click
        Dim descripcion = txbdescriptarea.Text
        Dim notarea = lblguardarvalor.Text
        sql = "SELECT CONVERT(VARCHAR,FechaEstado,20) FechaEstado FROM dbo.TAREA WHERE Activo=1 AND NoTarea='" + notarea + "' AND Solicitud='" + lblticket.Text + "'"
        Dim fecha = cn.obtienePalabra(sql, "FechaEstado")
        Dim sql3 As String = "INSERT INTO dbo.TAREA(NoTarea,Solicitud,FechaCreacion,TipoTarea,Comentario,Presencial,Asignado,Activo,Estado,FechaEstado) SELECT NoTarea,Solicitud,GETDATE() FechaCreacion,TipoTarea,'" + descripcion + "' Comentario,Presencial,'" + U + "' Asignado,Activo, Estado,GETDATE() FechaEstado FROM dbo.TAREA WHERE NoTarea='" + notarea + "' AND Solicitud='" + lblticket.Text + "'  And Activo =1"
        cn.ejecutarSQL(sql3)
        Dim sql4 As String = "UPDATE dbo.TAREA SET Activo=0 WHERE NoTarea='" + notarea + "' AND  Solicitud='" + lblticket.Text + "' AND Estado='SOLI' AND CONVERT(VARCHAR,FechaEstado,20)='" + fecha + "'"
        cn.ejecutarSQL(sql4)
        ddltipotarea.SelectedIndex = 0
        cknopresente.Checked = False
        ddltipotarea.Enabled = True
        cknopresente.Enabled = True
        btneliminar.Visible = False
        btnmodificar.Visible = False
        txbdescriptarea.Text = ""
        btnguardartarea.Visible = True
        btncancelar.Visible = False
        LLenarHistorialTarea()
    End Sub

    Protected Sub btneliminar_Click(sender As Object, e As EventArgs) Handles btneliminar.Click
        Dim descripcion = txbdescriptarea.Text
        Dim notarea = lblguardarvalor.Text
        sql = "SELECT CONVERT(VARCHAR,FechaEstado,20) FechaEstado FROM dbo.TAREA WHERE Activo=1 AND NoTarea='" + notarea + "' AND Solicitud='" + lblticket.Text + "'"
        Dim fecha = cn.obtienePalabra(sql, "FechaEstado")
        Dim sql3 As String = "INSERT INTO dbo.TAREA(NoTarea,Solicitud,FechaCreacion,TipoTarea,Comentario,Presencial,Asignado,Activo,Estado,FechaEstado) SELECT NoTarea,Solicitud,GETDATE() FechaCreacion,TipoTarea, Comentario,Presencial,'" + U + "' Asignado,0 Activo, Estado,GETDATE() FechaEstado FROM dbo.TAREA WHERE NoTarea='" + notarea + "' AND Solicitud='" + lblticket.Text + "'  And Activo =1"
        cn.ejecutarSQL(sql3)
        Dim sql4 As String = "UPDATE dbo.TAREA SET Activo=0 WHERE NoTarea='" + notarea + "' AND  Solicitud='" + lblticket.Text + "' AND Estado='SOLI' AND CONVERT(VARCHAR,FechaEstado,20)='" + fecha + "'"
        cn.ejecutarSQL(sql4)
        ddltipotarea.SelectedIndex = 0
        cknopresente.Checked = False
        ddltipotarea.Enabled = True
        cknopresente.Enabled = True
        btneliminar.Visible = False
        btnmodificar.Visible = False
        btnguardartarea.Visible = True
        btncancelar.Visible = False
        txbdescriptarea.Text = ""
        LLenarHistorialTarea()
    End Sub

    Protected Sub btncancelar_Click(sender As Object, e As EventArgs) Handles btncancelar.Click
        ddltipotarea.SelectedIndex = 0
        cknopresente.Checked = False
        ddltipotarea.Enabled = True
        cknopresente.Enabled = True
        btneliminar.Visible = False
        btnmodificar.Visible = False
        txbdescriptarea.Text = ""
        btnguardartarea.Visible = True
        btncancelar.Visible = False
        LLenarHistorialTarea()
    End Sub
End Class
