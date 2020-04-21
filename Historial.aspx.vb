Imports System.Data

Partial Class Historial
    Inherits System.Web.UI.Page
    Dim ituser, sql, U, catPre, var As String
    Dim cn As New Conn
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        U = Session("U")
        If (IsPostBack = False) Then
            var = Request.QueryString("v")
            If (var = "1") Then
                Dim s = Session("T")
                If (String.IsNullOrEmpty(s)) Then
                    Response.Redirect("Calificacion.aspx")
                Else
                    pnlWarning.Visible = True
                    sql = "SELECT S.NoSolicitud Ticket,CONVERT(VARCHAR,S.FechaCreacion,105), CONVERT(VARCHAR,S.FechaCreacion,108),C.Categoria, S.Descripcion, e.Estado, Convert(VARCHAR, s.FechaEstado, 105) 'Fecha Estado', S.UsuarioIT, S.Solicitante,S.Solucion FROM dbo.SOLICITUD S INNER JOIN dbo.CdCategoriaIT C ON S.Categoria=C.IdCategoria INNER JOIN dbo.CdEstadoSol E ON S.Estado = E.IdEstado  WHERE S.Activo=1 AND S.NoSolicitud='" + s + "' ORDER BY S.Estado DESC"
                    Dim dt As DataTable
                    cn.llenarDataTable(sql, dt)

                    Dim rrow As DataRow = dt.Rows(0)
                    lblVdesc.Text = HttpUtility.HtmlDecode(rrow(4).ToString.Replace("&nbsp;", ""))
                    lblVservicio.Text = HttpUtility.HtmlDecode(rrow(3).ToString.Replace("&nbsp;", ""))
                    sql = "SELECT  C.Categoria, e.Estado FROM dbo.SOLICITUD S INNER JOIN dbo.CdCategoriaIT C ON S.Categoria=C.IdCategoria INNER JOIN dbo.CdEstadoSol E ON S.Estado = E.IdEstado AND E.Activo=1 WHERE S.Activo=1 AND S.NoSolicitud='" + s + "' ORDER BY S.Estado DESC"
                    Dim td As DataTable
                    cn.llenarDataTable(sql, td)
                    Dim row As DataRow = td.Rows(0)
                    lblcategoria.Text = HttpUtility.HtmlDecode(row(0).ToString.Replace("&nbsp;", ""))
                    lblestado.Text = "Estado: " + HttpUtility.HtmlDecode(row(1).ToString.Replace("&nbsp;", ""))
                    lblticket.Text = HttpUtility.HtmlDecode(rrow(0).ToString.Replace("&nbsp;", "")) + "              "
                    lblfecha.Text = HttpUtility.HtmlDecode(rrow(1).ToString.Replace("&nbsp;", "")) + "  "
                    lblhora.Text = HttpUtility.HtmlDecode(rrow(2).ToString.Replace("&nbsp;", ""))
                    lblsolicitante.Text = HttpUtility.HtmlDecode(rrow(8).ToString.Replace("&nbsp;", ""))
                    sql = "SELECT COUNT(*) conteo FROM dbo.SOLICITUD WHERE Activo=1 AND NoSolicitud='" + s + "' AND Calificacion IS NOT NULL"
                    Dim ver = cn.obtieneEntero(sql, "conteo")
                    If (ver = 1) Then
                        sql = "SELECT Calificacion FROM dbo.SOLICITUD WHERE Activo=1 AND NoSolicitud='" + s + "'"
                        Dim valor2 = cn.obtienePalabra(sql, "Calificacion")
                        sql = "SELECT CASE WHEN NotasCalificacion IS NULL THEN '' ELSE NotasCalificacion END NotasCalificacion FROM dbo.SOLICITUD WHERE Activo=1 AND NoSolicitud='" + s + "'"
                        Dim comentario = cn.obtienePalabra(sql, "NotasCalificacion")
                        lblcomentarios.Text = HttpUtility.HtmlDecode(rrow(8).ToString.Replace("&nbsp;", "")) + ": " + comentario + "."
                        lblnota.Text = valor2
                    Else
                        tblcalificacion.Visible = False
                    End If
                    If (String.IsNullOrEmpty(HttpUtility.HtmlDecode(rrow(7).ToString.Replace("&nbsp;", "")))) Then
                        lblusuarioit.Text = " - "
                    Else
                        lblusuarioit.Text = HttpUtility.HtmlDecode(rrow(7).ToString.Replace("&nbsp;", ""))
                    End If
                End If
            Else
                If (var = "2") Then
                    sql = "SELECT COUNT(*) categoria FROM dbo.USUARIO_IT WHERE Activo=1 AND Usuario='" + U + "' AND CategoriaUsuario IN('IT','EN')"
                    Dim tipou = cn.obtieneEntero(sql, "categoria")
                    If (tipou = 0) Then
                        pnlsolicitante.Visible = False
                    End If
                    btnBuscar.Text = "Buscar"
                    LlenarHistorial()
                    traducir()
                    If (gvhistorial.Rows.Count = 0) Then
                        lblhistorialV.Text = "No tiene solicitudes creadas"
                        pnlhaysolicitu.Visible = True
                    Else
                        pnlhaysolicitu.Visible = False
                    End If
                End If
            End If
        End If

    End Sub
    Private Sub traducir()
        For Each gvrow As GridViewRow In gvhistorial.Rows
            Dim btn As Button = gvrow.FindControl("btnfinalizado")
            btn.Text = "Seleccionar"
            Dim btn2 As Button = gvrow.FindControl("btnCalificar")
            btn2.Text = "Calificar"
        Next
    End Sub
    Private Sub LlenarHistorial()
        Dim tipoDeCuenta As String = Session("M")
        If (tipoDeCuenta <> "IT") Then
            cn.llenarComboEmp(ddlSolicitante, U)
            pnlsolicitante.Visible = True
        Else
            sql = "SELECT EmployeeId,CompleteName FROM dbo.FwEmployee WHERE  Email IS NOT NULL ORDER BY CompleteName ASC"
            cn.llenarComboFam(sql, ddlSolicitante, "EmployeeId", "CompleteName")
        End If
        pnlsolicitante.Visible = True
        sql = "SELECT S.NoSolicitud Ticket,CONVERT(VARCHAR,S.FechaCreacion,105) Creacion,C.Categoria,SUBSTRING(S.Descripcion,1,30)+'...' Descripcion,S.Solicitante, S.UsuarioIT, E.Estado, Convert(VARCHAR, s.FechaEstado, 105) 'Fecha Estado' FROM dbo.SOLICITUD S INNER JOIN dbo.CdCategoriaIT C ON S.Categoria=C.IdCategoria INNER JOIN dbo.CdEstadoSol E ON S.Estado = E.IdEstado AND E.Activo=1 WHERE S.Activo=1 AND S.Solicitante='" + U + "' ORDER BY S.Estado DESC, S.FechaCreacion DESC"
        cn.llenarGrid(sql, gvhistorial)
    End Sub

    Protected Sub gvhistorial_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvhistorial.RowCommand

        Dim index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
        Dim nosol As String = gvhistorial.Rows(index).Cells(0).Text.ToString()
        If e.CommandName = "cmdSeleccionar" Then
            sql = "SELECT S.NoSolicitud Ticket,CONVERT(VARCHAR,S.FechaCreacion,105), CONVERT(VARCHAR,S.FechaCreacion,108),C.Categoria, S.Descripcion, e.Estado, Convert(VARCHAR, s.FechaEstado, 105) 'Fecha Estado',S.UsuarioIT,S.Solicitante,S.NotasCalificacion FROM dbo.SOLICITUD S INNER JOIN dbo.CdCategoriaIT C ON S.Categoria=C.IdCategoria INNER JOIN dbo.CdEstadoSol E ON S.Estado = E.IdEstado AND E.Activo=1 WHERE S.Activo=1 AND S.NoSolicitud='" + nosol + "' ORDER BY S.Estado DESC"
            Dim dt As DataTable
            cn.llenarDataTable(sql, dt)
            If (dt.Rows.Count = 0) Then
            Else
                pnlWarning.Visible = True
                gvhistorial.Visible = False
                Dim rrow As DataRow = dt.Rows(0)
                lblVdesc.Text = HttpUtility.HtmlDecode(rrow(4).ToString.Replace("&nbsp;", ""))
                lblVservicio.Text = HttpUtility.HtmlDecode(rrow(3).ToString.Replace("&nbsp;", ""))
                lblcategoria.Text = HttpUtility.HtmlDecode(rrow(3).ToString.Replace("&nbsp;", ""))
                lblestado.Text = "Estado: " + HttpUtility.HtmlDecode(rrow(5).ToString.Replace("&nbsp;", ""))
                lblticket.Text = HttpUtility.HtmlDecode(rrow(0).ToString.Replace("&nbsp;", ""))
                lblfecha.Text = HttpUtility.HtmlDecode(rrow(1).ToString.Replace("&nbsp;", ""))
                lblhora.Text = HttpUtility.HtmlDecode(rrow(2).ToString.Replace("&nbsp;", ""))
                lblsolicitante.Text = HttpUtility.HtmlDecode(rrow(8).ToString.Replace("&nbsp;", ""))
                sql = "SELECT COUNT(*) conteo FROM dbo.SOLICITUD WHERE Activo=1 AND NoSolicitud='" + nosol + "' AND Calificacion IS NOT NULL"
                Dim ver = cn.obtieneEntero(sql, "conteo")
                If (ver = 1) Then
                    sql = "SELECT Calificacion FROM dbo.SOLICITUD WHERE Activo=1 AND NoSolicitud='" + nosol + "'"
                    Dim valor2 = cn.obtienePalabra(sql, "Calificacion")
                    sql = "SELECT CASE WHEN NotasCalificacion IS NULL THEN '' ELSE NotasCalificacion END NotasCalificacion FROM dbo.SOLICITUD WHERE Activo=1 AND NoSolicitud='" + nosol + "'"
                    Dim comentario = cn.obtienePalabra(sql, "NotasCalificacion")
                    lblcomentarios.Text = HttpUtility.HtmlDecode(rrow(8).ToString.Replace("&nbsp;", "")) + ": " + comentario + "."
                    lblnota.Text = valor2
                Else
                    tblcalificacion.Visible = False
                End If

                If (String.IsNullOrEmpty(HttpUtility.HtmlDecode(rrow(7).ToString.Replace("&nbsp;", "")))) Then
                    lblusuarioit.Text = " - "
                Else
                    lblusuarioit.Text = HttpUtility.HtmlDecode(rrow(7).ToString.Replace("&nbsp;", ""))
                End If
            End If
        ElseIf e.CommandName = "cmdCalificar" Then
            Session("Cal") = nosol
            Response.Redirect("Calificacion.aspx?v=1")
        ElseIf e.CommandName = "cmdModificar" Then
            lblVdesc.Visible = False
            pnlbotones.Visible = True
            pnldescripcion.Visible = True
            sql = "SELECT S.NoSolicitud Ticket,CONVERT(VARCHAR,S.FechaCreacion,105), CONVERT(VARCHAR,S.FechaCreacion,108),C.Categoria, S.Descripcion, e.Estado, Convert(VARCHAR, s.FechaEstado, 105) 'Fecha Estado',S.UsuarioIT,S.Solicitante,S.Solucion FROM dbo.SOLICITUD S INNER JOIN dbo.CdCategoriaIT C ON S.Categoria=C.IdCategoria INNER JOIN dbo.CdEstadoSol E ON S.Estado = E.IdEstado AND E.Activo=1 WHERE S.Activo=1 AND S.NoSolicitud='" + nosol + "' ORDER BY S.Estado DESC"
            Dim dt As DataTable
            cn.llenarDataTable(sql, dt)
            If (dt.Rows.Count = 0) Then
            Else
                pnlWarning.Visible = True
                gvhistorial.Visible = False
                Dim rrow As DataRow = dt.Rows(0)
                txtdescripcion.Text = HttpUtility.HtmlDecode(rrow(4).ToString.Replace("&nbsp;", ""))
                lblVservicio.Text = HttpUtility.HtmlDecode(rrow(3).ToString.Replace("&nbsp;", ""))
                lblcategoria.Text = HttpUtility.HtmlDecode(rrow(3).ToString.Replace("&nbsp;", ""))
                lblestado.Text = "Estado: " + HttpUtility.HtmlDecode(rrow(5).ToString.Replace("&nbsp;", ""))
                lblticket.Text = HttpUtility.HtmlDecode(rrow(0).ToString.Replace("&nbsp;", ""))
                lblfecha.Text = HttpUtility.HtmlDecode(rrow(1).ToString.Replace("&nbsp;", ""))
                lblhora.Text = HttpUtility.HtmlDecode(rrow(2).ToString.Replace("&nbsp;", ""))
                sql = "SELECT COUNT(*) conteo FROM dbo.SOLICITUD WHERE Activo=1 AND NoSolicitud='" + nosol + "' AND Calificacion IS NOT NULL"
                Dim ver = cn.obtieneEntero(sql, "conteo")
                If (ver = 1) Then
                    sql = "SELECT Calificacion FROM dbo.SOLICITUD WHERE Activo=1 AND NoSolicitud='" + nosol + "'"
                    Dim valor2 = cn.obtienePalabra(sql, "Calificacion")
                    sql = "SELECT NotasCalificacion FROM dbo.SOLICITUD WHERE Activo=1 AND NoSolicitud='" + nosol + "'"
                    Dim comentario = cn.obtienePalabra(sql, "NotasCalificacion")
                    lblcomentarios.Text = HttpUtility.HtmlDecode(rrow(8).ToString.Replace("&nbsp;", "")) + ": " + comentario + "."
                    lblnota.Text = valor2
                Else
                    tblcalificacion.Visible = False
                End If

                If (String.IsNullOrEmpty(HttpUtility.HtmlDecode(rrow(7).ToString.Replace("&nbsp;", "")))) Then
                    lblusuarioit.Text = " - "
                Else
                    lblusuarioit.Text = HttpUtility.HtmlDecode(rrow(7).ToString.Replace("&nbsp;", ""))
                End If
            End If
        End If
    End Sub

    Protected Sub gvhistorial_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvhistorial.RowDataBound
        If (e.Row.RowType = DataControlRowType.Header) Then
            e.Row.Cells(0).Text = "Ticket"
            e.Row.Cells(1).Text = "Fecha de Creación"
            e.Row.Cells(2).Text = "Categoria"
            e.Row.Cells(3).Text = "Descripción"
            e.Row.Cells(4).Text = "Solicitante"
            e.Row.Cells(5).Text = "UsuarioIT"
            e.Row.Cells(6).Text = "Estado"
            e.Row.Cells(7).Text = "Fecha Estado"
            e.Row.Cells(8).Text = "Acción"
            e.Row.Cells(9).Text = "Calificación"
        End If



        For Each dr As GridViewRow In gvhistorial.Rows

            Dim me2 As String = dr.Cells(0).Text
            Dim btn As Button = dr.FindControl("btnCalificar")
            If VerificarCalificacion(me2) = 0 Then
                btn.Visible = False
            ElseIf VerificarCalificacion(me2) = 1 Then
                btn.Visible = True
            Else
                btn.Visible = False
            End If

        Next
        For Each dr As GridViewRow In gvhistorial.Rows

            Dim me3 As String = dr.Cells(0).Text
            Dim btn2 As Button = dr.FindControl("btnmodificar")

            If VerificarModificacion(me3) = 0 Then
                btn2.Visible = False
            ElseIf VerificarModificacion(me3) = 1 Then
                btn2.Visible = True
            Else
                btn2.Visible = False
            End If

        Next

        
    End Sub
    Function VerificarModificacion(ByVal NoSolicitud As String) As Integer
        Dim resultado As Integer
        sql = "SELECT COUNT(*) conteo FROM SOLICITUD WHERE Activo = 1 And NoSolicitud = " + NoSolicitud + " AND FechaCreacion BETWEEN CONVERT(varchar,'2020-02-15',110) AND GETDATE() AND Estado IN('SOLI') "
        resultado = cn.obtieneEntero(sql, "conteo")
        Return resultado
    End Function

    Function VerificarCalificacion(ByVal NoSolicitud As String) As Integer
        Dim resultado As Integer
        sql = "SELECT COUNT(*) conteo FROM SOLICITUD WHERE Activo = 1 And NoSolicitud = " + NoSolicitud + " AND Estado='FINA' AND Calificacion IS NULL AND FechaCreacion BETWEEN CONVERT(varchar,'2020-02-15',110) AND GETDATE()"
        resultado = cn.obtieneEntero(sql, "conteo")
        Return resultado
    End Function
    Protected Sub btncancelar_Click(sender As Object, e As EventArgs) Handles btncancelar.Click
        Response.Redirect("Historial.aspx?v=2")
    End Sub

    Protected Sub btnmodificar_Click(sender As Object, e As EventArgs) Handles btnmodificar.Click
        Dim descripcion = txtdescripcion.Text
        Dim solicitud = lblticket.Text
        sql = "INSERT INTO dbo.SOLICITUD(NoSolicitud,FechaCreacion,Solicitante,FechaSolicitud,Tipo,Descripcion,UsuarioIT,Estado,Finalizacion,Hrs,Minutos,Sitio,Solucion,Registro,Activo,Prioridad,Inventario,Meta,NoPresencial,Categoria,FechaEstado,Calificacion,NotasCalificacion,ParaUsuario) SELECT NoSolicitud,GETDATE() FechaCreacion,Solicitante,FechaSolicitud,Tipo,'" + descripcion + "' Descripcion, UsuarioIT, Estado,Finalizacion,Hrs ,Minutos,Sitio,Solucion,'" + U + "' Registro,Activo,Prioridad,Inventario,Meta,NoPresencial,Categoria,GETDATE() FechaEstado,Calificacion,NotasCalificacion,ParaUsuario FROM dbo.SOLICITUD WHERE Activo = 1 And NoSolicitud = '" + solicitud + "'"
        cn.ejecutarSQL(sql)
        Session("T") = solicitud
        Response.Redirect("Historial.aspx?v=1")
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        If (ddlSolicitante.SelectedIndex = 0) Then
            sql = "SELECT S.NoSolicitud Ticket,CONVERT(VARCHAR,S.FechaCreacion,105) Creacion,C.Categoria, SUBSTRING(S.Descripcion,1,30)+'...' Descripcion,S.Solicitante, S.UsuarioIT, E.Estado, Convert(VARCHAR, s.FechaEstado, 105) 'Fecha Estado' FROM dbo.SOLICITUD S INNER JOIN dbo.CdCategoriaIT C ON S.Categoria=C.IdCategoria INNER JOIN dbo.CdEstadoSol E ON S.Estado = E.IdEstado AND E.Activo=1 WHERE S.Activo=1 AND S.Solicitante='" + U + "' ORDER BY S.Estado DESC, S.FechaCreacion ASC"
            cn.llenarGrid(sql, gvhistorial)
        Else
            sql = "SELECT S.NoSolicitud Ticket,CONVERT(VARCHAR,S.FechaCreacion,105) Creacion,C.Categoria, SUBSTRING(S.Descripcion,1,30)+'...' Descripcion,S.Solicitante, S.UsuarioIT, E.Estado, Convert(VARCHAR, s.FechaEstado, 105) 'Fecha Estado' FROM dbo.SOLICITUD S INNER JOIN dbo.CdCategoriaIT C ON S.Categoria=C.IdCategoria INNER JOIN dbo.CdEstadoSol E ON S.Estado = E.IdEstado AND E.Activo=1 WHERE S.Activo=1 AND S.Solicitante='" + ddlSolicitante.SelectedValue + "' ORDER BY S.Estado DESC, S.FechaCreacion ASC"
            cn.llenarGrid(sql, gvhistorial)

            For Each dr As GridViewRow In gvhistorial.Rows

                Dim btn2 As Button = dr.FindControl("btnmodificar")
                Dim btn As Button = dr.FindControl("btnCalificar")
                Dim btn3 As Button = dr.FindControl("btnfinalizado")
                btn2.Visible = False
                btn.Visible = False
                btn3.Text = "Seleccionar"
            Next
            If (gvhistorial.Rows.Count = 0) Then
                lblhistorialV.Text = "No tiene solicitudes creadas"
                pnlhaysolicitu.Visible = True
            Else
                pnlhaysolicitu.Visible = False
            End If
        End If
    End Sub
End Class
