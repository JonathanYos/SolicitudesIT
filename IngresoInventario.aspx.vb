
Partial Class IngresoInventario
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
           
        End If
    End Sub
End Class
