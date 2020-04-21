'@Copyright ©: 2019
'@Company: Familias de Esperanza
'@Author: Jonathan Yos
'@Update: Jonathan Yos
'@Description: Aplicacion para creacion, modificacion y eliminacion de solicitudes


Partial Class Login
    Inherits System.Web.UI.Page
    Public Shared comprobantenombre As Boolean
    Dim cn As New Conn
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (Page.IsPostBack) Then
        Else
            Advertenciatb.Visible = False
            Cambio_Contraseña.Visible = False
            Formulario.Visible = True
            txtuser.Focus()
        End If

    End Sub
    Protected Sub Ingresar_Click(sender As Object, e As EventArgs) Handles Ingresar.Click
        ' VerificarContrasena(txtpass.Text)
        Dim cuenta As String
        Dim usu As String = txtuser.Text
        Dim pass As String = txtpass.Text
        Dim result As Integer = cn.obtener_credenciales(usu, pass)
        Dim sql As String = "SELECT COUNT(*) conteo FROM dbo.USUARIO_IT WHERE Usuario = '" + usu + "' AND Activo=1 AND CategoriaUsuario IN('SU','IT')"
        Dim res As Integer = cn.obtieneEntero(sql, "conteo")
        If result = 1 Then
            VerificarTiempoContraseña(res, usu)
        ElseIf result = 2 Then

            Advertenciatb.Visible = True
            Formulario.Visible = False
            lblmens.Text = "Contraseña Incorrecta"

        ElseIf result = 3 Then

            Advertenciatb.Visible = True
            Formulario.Visible = False
            lblmens.Text = "Usuario Incorrecto"

        ElseIf result = 0 Then

            Advertenciatb.Visible = True
            Formulario.Visible = False
            lblmens.Text = "Error de Conexion: cierre e inicie otra vez la aplicacion."

        Else

            Advertenciatb.Visible = True
            Formulario.Visible = False
            lblmens.Text = "Ha ocurrido un error inesperado por favor envie esta informacion a Sistemas"

        End If
    End Sub

    Protected Sub Aceptarbtn_Click(sender As Object, e As EventArgs) Handles Aceptarbtn.Click
        If (Aceptarbtn.Text = "Cambiar") Then
            Advertenciatb.Visible = False
            Formulario.Visible = False
            Cambio_Contraseña.Visible = True
            Instrucciones.Visible = True

        ElseIf (Aceptarbtn.Text = "OK") Then
            Advertenciatb.Visible = False
            Formulario.Visible = False
            Cambio_Contraseña.Visible = True
            Instrucciones.Visible = True

        ElseIf (Aceptarbtn.Text = "Aceptar") Then
            Instrucciones.Visible = False
            Advertenciatb.Visible = False
            Formulario.Visible = True
            Cambio_Contraseña.Visible = False

        ElseIf (Aceptarbtn.Text = "Entendido") Then
            Dim usu = Session("U")
            Dim sql As String = "SELECT COUNT(*) conteo FROM dbo.USUARIO_IT WHERE Usuario = '" + usu + "'"
            Dim res As Integer = cn.obtieneEntero(sql, "conteo")
            Dim cuenta As String
            If (res = 1) Then
                Session("U") = usu
                cuenta = "IT"
                Session("M") = cuenta
                Response.Redirect("SeguimientoS.aspx")
            Else
                cuenta = "NORM"
                Session("U") = usu
                Session("M") = cuenta
                Response.Redirect("Historial.aspx?v=2")
            End If

        Else
            Formulario.Visible = True
            Cambio_Contraseña.Visible = False
            Advertenciatb.Visible = False
            Instrucciones.Visible = False
        End If
    End Sub


    Protected Sub btncambio_Click(sender As Object, e As EventArgs) Handles btncambio.Click
        Try
            Dim passnueva, confirpass, passactual As String
            passnueva = txtnuevac.Text
            confirpass = txtconfirmarc.Text
            passactual = txtactualc.Text
            Dim nombre As String = Convert.ToString(Session("nombre"))
            VerificarContrasena(passnueva, confirpass, passactual, nombre)
        Catch ex As Exception
            Aceptarbtn.Text = "Aceptar"
            Advertenciatb.Visible = True
            Formulario.Visible = False
            Cambio_Contraseña.Visible = False
            Instrucciones.Visible = False
            lblmens.Text = "Ha ocurrido un error por favor comuniquese con el area de Sistemas :" + ex.Message
        End Try
    End Sub
    Private Sub VerificarTiempoContraseña(res As Integer, usu As String)
        'Dim nombreUsuario As String = usu
        'Dim sql As String = "DECLARE @Z int DECLARE @D int DECLARE @Q int DECLARE @W datetime SET @W=(SELECT DATEADD(MONTH,6,PasswordDate) Fecha FROM dbo.FwEmployeePassword WHERE EmployeeId='" + nombreUsuario + "')  SET @D=(SELECT DATEDIFF(MM,GETDATE(),@W) Diferencia FROM dbo.FwEmployeePassword WHERE EmployeeId='" + nombreUsuario + "')  IF @D = 1 OR @D=0 OR @D<0 SET @Z = (SELECT DATEDIFF(dd,GETDATE(),@W) dias )  IF @Z<16 AND @Z>0 SELECT @Z Resultado ELSE IF @Z<=0 SELECT 0 Resultado ELSE SELECT 35 Resultado"
        'Dim numero = cn.obtieneEnteroFam(sql, "Resultado")
        'If (numero = 0) Then
        '    Advertenciatb.Visible = True
        '    Formulario.Visible = False
        '    Cambio_Contraseña.Visible = False
        '    Instrucciones.Visible = False
        '    lblmens.Text = "Expiro el tiempo de su contaseña, por favor cambie su contraseña"
        '    Aceptarbtn.Text = "Cambiar"
        '    lbltitul.Text = " Instrucciones"
        '    lblmen.Text = " La nueva contraseña debe contener lo siguiente:"
        '    lblreg1.Text = " 1. Debe tener mayúsculas y minuculas."
        '    lblreg2.Text = " 2. Debe contener números"
        '    lblreg3.Text = " 3. Debe contener más de 8 caracteres"
        '    lblreg4.Text = " 4. No incluya datos de su nombre o fechas fáciles de piratear"
        '    lblreg5.Text = " 5. La nueva contraseña no se deber repetir con las ultimas 3 contraseñas."
        '    Session("nombre") = usu
        'ElseIf (numero < 16 And numero > 0) Then
        '    Advertenciatb.Visible = True
        '    Formulario.Visible = False
        '    Cambio_Contraseña.Visible = False
        '    Instrucciones.Visible = False
        '    lblmens.Text = "Faltan " + numero.ToString() + " dias para el cambio de su contraseña"
        '    Aceptarbtn.Text = "Entendido"
        '    Session("U") = usu
        '    numero = 0
        'Else
        Dim cuenta As String
        If (res = 1) Then
            Session("U") = usu
            cuenta = "IT"
            Session("M") = cuenta
            Response.Redirect("SeguimientoS.aspx")
        Else
            cuenta = "NORM"
            Session("U") = usu
            Session("M") = cuenta
            Response.Redirect("Historial.aspx?v=2")
        End If

        'End If
    End Sub
   
    Function VerificarNombreUsuario(ByVal nuevacontrasena As String, ByVal usuario As String) As Integer
        Try
            Dim sql As String = "SELECT CompleteName FROM dbo.FwEmployee WHERE EmployeeId= '" + Session("nombre") + "'"
            Dim nombre2 As String = cn.obtienePalabraFam(sql, "CompleteName")
            Dim nombre3 As String() = nombre2.Split(New Char() {" "c})
            Dim conteo3 As Integer = 0


            For Each numero3 As String In nombre3

                Dim sql3 As String = "SELECT COUNT(*) conteo WHERE '" + nuevacontrasena + "' like('%" + numero3 + "%')"
                Dim numero5 As String = cn.obtienePalabraFam(sql3, "conteo")
                Dim conteo6 As Integer = Convert.ToInt32(numero5)
                conteo3 = conteo3 + conteo6

                Dim remplazo As String = numero3.Replace("á", "a")
                remplazo = remplazo.Replace("é", "e")
                remplazo = remplazo.Replace("í", "i")
                remplazo = remplazo.Replace("ó", "o")
                remplazo = remplazo.Replace("ú", "u")
                remplazo = remplazo.Replace("Á", "A")
                remplazo = remplazo.Replace("É", "E")
                remplazo = remplazo.Replace("Í", "I")
                remplazo = remplazo.Replace("Ó", "O")
                remplazo = remplazo.Replace("Ú", "U")
                Dim sql2 As String = "SELECT COUNT(*) conteo WHERE '" + nuevacontrasena + "' like('%" + remplazo + "%')"
                Dim numero4 As String = cn.obtienePalabraFam(sql2, "conteo")
                Dim conteo2 As Integer = Convert.ToInt32(numero4)
                conteo3 = conteo3 + conteo2
            Next
            Return conteo3
        Catch ez As Exception
            Response.Write(ez.Message)
        End Try
        Return 1
    End Function
    Private Sub VerificarContrasena(ByVal Contrasena As String, ByVal Confirmar As String, ByVal Actual As String, ByVal usuario As String)
        Dim mayus As String() = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "Ñ"}
        Dim minus As String() = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
        Dim numeros As String() = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}
        Dim comprobantemayus = verificarCaracter(Contrasena, mayus)
        Dim comprobanteminus = verificarCaracter(Contrasena, minus)
        Dim comprobantenumeros = verificarCaracter(Contrasena, numeros)
        Dim comprobantecantidad As Boolean
        Dim comprobantesoniguales As Boolean
        Dim comprobanteanteriorescontra As Boolean
        Dim comprobantecontrasenaactual As Boolean
        Dim sql, nombreusuario As String
        nombreusuario = Session("nombre")
        sql = "SELECT CASE WHEN (('" + Contrasena + "' = FEP.Pass1) OR ('" + Contrasena + "' = FEP.Pass2) OR ('" + Contrasena + "'= FEP.Pass3) OR ('" + Contrasena + "'= FEP.Password)) THEN 1 ELSE 0 END conteo FROM FwEmployeePassword FEP WHERE FEP.EmployeeId = '" + Session("nombre").ToString() + "'"

        Dim anteriorescontra = Convert.ToString(verificarContrasenaIgual(Contrasena, usuario))
        If (anteriorescontra = "0") Then
            comprobanteanteriorescontra = True
        Else
            comprobanteanteriorescontra = False
        End If
        sql = "SELECT COUNT(*) conteo FROM dbo.FwEmployeePassword WHERE EmployeeId='" + nombreusuario + "' AND Password='" + Actual + "'"
        Dim contraactual = cn.obtieneEnteroFam(sql, "conteo")
        If (contraactual = 0) Then
            comprobantecontrasenaactual = True
        Else
            comprobantecontrasenaactual = True
        End If
        If (Contrasena = Confirmar) Then
            comprobantesoniguales = True
        Else
            comprobantesoniguales = False
        End If
        If (Contrasena.Length > 7) Then
            comprobantecantidad = True
        Else
            comprobantecantidad = False
        End If
        If (comprobantemayus And comprobanteminus And comprobantenumeros And comprobantecantidad And comprobantesoniguales And comprobantecontrasenaactual And comprobanteanteriorescontra) Then
           
                Dim consulta As String = "UPDATE FwEmployeePassword SET Pass3=Pass2, Pass2=Pass1, Pass1=FwEmployeePassword.Password, FwEmployeePassword.Password='" + Contrasena + "', PasswordDate=GETDATE() WHERE EmployeeId='" + Session("nombre") + "' "
            cn.ejecutarSQLFam(consulta)
                Aceptarbtn.Text = "Aceptar"
                Advertenciatb.Visible = True
                Formulario.Visible = False
                Cambio_Contraseña.Visible = False
                Instrucciones.Visible = False
            lblmens.Text = "Cambio de Contraseña Exitoso por favor ingrese su nueva contraseña"
                Session("nombre") = Nothing
                Session("nombre") = ""

        Else
            Cambio_Contraseña.Visible = False
            Advertenciatb.Visible = True
            Formulario.Visible = False
            lblmens.Text = "La contraseña no cumple con las instrucciones"
            Aceptarbtn.Text = "OK"
            Instrucciones.Visible = False
        End If
    End Sub
    Function verificarContrasenaIgual(ByVal Contrasena As String, ByVal usuario As String) As Integer
        Try
            Dim consulta = "SELECT CASE WHEN (('" + Contrasena + "' = FEP.Pass1) OR ('" + Contrasena + "' = FEP.Pass2) OR ('" + Contrasena + "'= FEP.Pass3) OR ('" + Contrasena + "'= FEP.Password)) THEN 1 ELSE 0 END conteo FROM FwEmployeePassword FEP WHERE FEP.EmployeeId = '" + usuario + "'"
            Return cn.obtieneEnteroFam(consulta, "conteo")
        Catch ex As Exception
            Aceptarbtn.Text = "Aceptar"
            Advertenciatb.Visible = True
            Formulario.Visible = False
            Cambio_Contraseña.Visible = False
            Instrucciones.Visible = False
            lblmens.Text = "Ha ocurrido un error por favor comuniquese con el area de Sistemas :" + ex.Message
            Return -60
        End Try
    End Function
    Function verificarCaracter(ByVal cadena As String, ByVal abc As String()) As Boolean
        For i As Integer = 0 To cadena.Length - 1
            For j As Integer = 0 To abc.Length - 1
                If cadena(i) = abc(j) Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function

End Class
