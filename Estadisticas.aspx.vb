
Partial Class Estadisticas
    Inherits System.Web.UI.Page
    Dim ituser, sql, U, catPre, opcion, ticket3 As String
    Dim cn As New Conn
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (Page.IsPostBack = False) Then
            'Response.Redirect("Historial.aspx?v=2")
            CargarTop10()
        End If
    End Sub
    Protected Sub CargarTop10()
        sql = "SELECT TOP 10 C.Categoria, TS.Tipo, COUNT(*) Cantidad FROM SOLICITUD S INNER JOIN CdTipoSol TS ON S.Tipo = TS.IdTipo INNER JOIN CdCategoriaIT C ON TS.IdCategoria=C.IdCategoria WHERE S.Activo=1 AND S.Estado='FINA' GROUP BY C.Categoria,TS.Tipo ORDER BY COUNT(*) DESC  "
        cn.llenarGrid(sql, gvtop10)
    End Sub
End Class
