'@Copyright ©: 2019
'@Company: Familias de Esperanza
'@Author: Jonathan Yos
'@Update: Jonathan Yos
'@Description: Aplicacion para creacion, modificacion y eliminacion de solicitudes

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Session("U") = ""
        Response.Redirect("Login.aspx")
        'FormsAuthentication.SignOut()
        'FormsAuthentication.RedirectToLoginPage()
    End Sub
End Class
