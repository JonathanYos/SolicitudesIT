Imports Microsoft.VisualBasic
Imports Microsoft.SqlServer
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.DataTable

Public Class Conn
    Dim conIT As String = conexionIT()
    Dim log As String = con()
    Dim err As String

    Private Function conexionIT() As String
        conexionIT = "data source = SVRDBS; initial catalog = IT;" & svrUP()
    End Function
    Private Function conexionVACAS() As String
        conexionVACAS = "data source = SVRDBS; initial catalog = VACACIONES;" & svrUP()
    End Function
    Private Function con() As String
        con = "data source = SVRFAMILIAS; initial catalog = FamiliasPruebas;" & svr()
    End Function
    Public Sub ejecutarInsert(ByVal stringSQL As String)
        Dim cn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        cn = New SqlConnection(conIT)
        cmd = New SqlCommand(stringSQL, cn)

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
            ' InputBox("SQL", "", stringSQL)
        Catch ex As Exception
            'ex.Message.ToString & " - " & 
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.StackTrace & vbCrLf & vbCrLf & "IT: " & stringSQL
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Public Sub ejecutarSQL(ByVal stringSQL As String)
        Dim cn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        cn = New SqlConnection(conIT)
        cmd = New SqlCommand(stringSQL, cn)

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: " & stringSQL
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try

    End Sub
    Public Sub ejecutarSQLFam(ByVal stringSQL As String)
        Dim cn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        cn = New SqlConnection(log)
        cmd = New SqlCommand(stringSQL, cn)

        Try
            cn.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: " & stringSQL
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try

    End Sub

    Public Sub llenarGrid(ByVal stringSQL As String, ByRef grid As GridView)
        Dim conn As New SqlConnection()
        conn = New SqlConnection(conIT)
        Dim conexto2 = New SqlDataAdapter(stringSQL, conn)
        Dim tabla2 = New DataSet()
        Try
            conexto2.Fill(tabla2)
            grid.DataSource = tabla2.Tables(0).DefaultView
            grid.DataBind()
            conn.Close()
        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: " & stringSQL
        End Try

    End Sub
    Public Sub llenarGrid2(ByVal stringSQL As String, ByRef grid As GridView)
        Dim conn As New SqlConnection()
        conn = New SqlConnection(conIT)
        Dim conexto2 = New SqlDataAdapter(stringSQL, conn)
        Dim tabla2 = New DataSet()
        Try
            conexto2.Fill(tabla2)
            'grid.DataSource = tabla2.Tables(0).DefaultView
            grid.DataBind()
            conn.Close()
        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: " & stringSQL
        End Try

    End Sub

    Public Sub llenarDataSet(ByVal stringSQL As String, ByRef dataSetO As DataSet)
        Dim cn As New SqlConnection(conIT) ' nueva conexión indicando al SqlConnection la cadena de conexión   
        Try
            cn.Open()            ' Abrir la conexión a Sql   
            Dim cmd As New SqlCommand(stringSQL, cn) ' Pasar la consulta sql y la conexión al Sql Command    
            Dim da As New SqlDataAdapter(cmd) ' Inicializar un nuevo SqlDataAdapter 
            da.Fill(dataSetO)
            cn.Close()
        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: " & stringSQL
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Public Sub llenarDataTable(ByVal stringSQL As String, ByRef tableData As DataTable)
        Dim conn As New SqlConnection()
        conn = New SqlConnection(conIT)
        Dim adaptador = New SqlDataAdapter(stringSQL, conn)
        Dim setDatos = New DataSet()
        adaptador.Fill(setDatos, "listado")
        tableData = setDatos.Tables("listado")
    End Sub

    Public Sub llenarCombo(ByVal sql As String, ByVal ComboBox As DropDownList, ByVal codigo As String, ByVal Descripcion As String)
        Dim adapter As New SqlDataAdapter(sql, conIT)
        Dim datos As New DataTable
        Try
            adapter.Fill(datos)
            ComboBox.DataSource = datos
            ComboBox.DataValueField = codigo
            ComboBox.DataTextField = Descripcion
            ComboBox.DataBind()
            ComboBox.Items.Insert(0, New ListItem(String.Empty, String.Empty))
            ComboBox.SelectedIndex = 0
        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: "
        End Try
    End Sub

    Public Sub llenarComboEmp(ByVal ddl As DropDownList, ByVal U As String)
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("", ""))
        Dim tblTipos As DataTable
        tblTipos = obtenerEmpleadosACargo(U)
        Dim Code As String = ""
        Dim Tipo As String = ""
        Dim item As ListItem

        For Each row As DataRow In tblTipos.Rows()
            Code = row("USUARIO").ToString()
            Tipo = row("NOMBRE").ToString()
            item = New ListItem(Tipo, Code)
            ddl.Items.Add(item)
        Next
    End Sub


    Public Function obtenerEmpleadosACargo(ByVal nombreUsuario As String) As DataTable
        Try
            Dim idJefe As String
            idJefe = retornarIdEmpleado(nombreUsuario)
            Dim conexion As SqlConnection = New SqlConnection(conexionVACAS)
            conexion.Open()
            Dim empleadosACargo As String = recopilarEmpleadosACargo(nombreUsuario)
            Dim comandoString As String = "SELECT NOMBRE, USUARIO FROM EMPLEADO INNER JOIN USUARIO ON EMPLEADO.EMPLEADO_ID = USUARIO.Cod_Emp WHERE (EMPLEADO_ID IN(" + empleadosACargo + "'') AND ACTIVO = 'S') OR (EMPLEADO_ID=@idJefe AND ACTIVO = 'S') ORDER BY NOMBRE"
            Dim comando As SqlCommand = New SqlCommand(comandoString, conexion)
            comando.Parameters.AddWithValue("@idJefe", idJefe)
            Dim adaptador As SqlDataAdapter = New SqlDataAdapter()
            adaptador.SelectCommand = comando
            Dim tablaEmpleadosACargoJefe As DataTable = New DataTable()
            adaptador.Fill(tablaEmpleadosACargoJefe)
            conexion.Close()
            obtenerEmpleadosACargo = tablaEmpleadosACargoJefe
        Catch ex As Exception
            obtenerEmpleadosACargo = New DataTable()
        End Try
    End Function

    Private Function recopilarEmpleadosACargo(ByVal nombreUsuario As String) As String
        Try
            Dim idJefe As String = retornarIdEmpleado(nombreUsuario)
            Dim conexion As SqlConnection = New SqlConnection(conexionVACAS)
            conexion.Open()
            Dim comandoString As String = "SELECT USUARIO, COD_EMP FROM USUARIO WHERE DEP_AUX = @idJefe AND DEP_AUX != COD_EMP"
            Dim comando As SqlCommand = New SqlCommand(comandoString, conexion)
            comando.Parameters.AddWithValue("@idJefe", idJefe)
            Dim adaptador As SqlDataAdapter = New SqlDataAdapter()
            adaptador.SelectCommand = comando
            Dim tablaEmpleadosACargoJefe As DataTable = New DataTable()
            adaptador.Fill(tablaEmpleadosACargoJefe)
            conexion.Close()
            Dim usuarioEmpleadoACargo As String = ""
            Dim idEmpleadoACargo As String = ""
            Dim empleadosACargo As String = ""
            For Each empleadoPrimerNivel As DataRow In tablaEmpleadosACargoJefe.Rows
                usuarioEmpleadoACargo = empleadoPrimerNivel("USUARIO").ToString()
                idEmpleadoACargo = empleadoPrimerNivel("COD_EMP").ToString()
                empleadosACargo = empleadosACargo + "'" + idEmpleadoACargo + "'," + recopilarEmpleadosACargo(usuarioEmpleadoACargo)
            Next
            recopilarEmpleadosACargo = empleadosACargo
        Catch ex As Exception
            recopilarEmpleadosACargo = ""
        End Try
    End Function

    Public Function retornarIdEmpleado(ByVal nombreUsuario As String) As String
        Try
            Dim idUsuario As Int32 = retornarIdUsuario(nombreUsuario)
            Dim conexion As SqlConnection = New SqlConnection(conexionVACAS)
            conexion.Open()
            Dim comandoString As String = "SELECT EMP.Empleado_Id FROM dbo.Empleado EMP INNER JOIN dbo.Usuario USR ON EMP.Empleado_Id = USR.Cod_Emp WHERE USR.Id = @idUsuario"
            Dim comando As SqlCommand = New SqlCommand(comandoString, conexion)
            comando.Parameters.AddWithValue("@idUsuario", idUsuario)
            Dim adaptador As SqlDataAdapter = New SqlDataAdapter()
            adaptador.SelectCommand = comando
            Dim tablaDatos As DataTable = New DataTable()
            adaptador.Fill(tablaDatos)
            conexion.Close()
            retornarIdEmpleado = tablaDatos.Rows(0)("Empleado_Id").ToString()
        Catch ex As Exception
            retornarIdEmpleado = ""
        End Try

    End Function

    Public Function retornarIdUsuario(ByVal nombreUsuario As String) As Int32
        Try
            Dim conexion As SqlConnection = New SqlConnection(conexionVACAS)
            conexion.Open()
            Dim comandoString As String = "SELECT USR.Id FROM dbo.Usuario USR WHERE USR.Usuario = @nombreUsuario"
            Dim comando As SqlCommand = New SqlCommand(comandoString, conexion)
            comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario)
            Dim adaptador As SqlDataAdapter = New SqlDataAdapter()
            adaptador.SelectCommand = comando
            Dim tablaDatos As DataTable = New DataTable()
            adaptador.Fill(tablaDatos)
            conexion.Close()
            retornarIdUsuario = Convert.ToInt32(tablaDatos.Rows(0)("Id").ToString())
        Catch ex As Exception
            retornarIdUsuario = ""
        End Try
    End Function

    Public Sub llenarComboFam(ByVal sql As String, ByVal ComboBox As DropDownList, ByVal codigo As String, ByVal Descripcion As String)
        Dim adapter As New SqlDataAdapter(sql, log)
        Dim datos As New DataTable
        Try
            adapter.Fill(datos)
            ComboBox.DataSource = datos
            ComboBox.DataValueField = codigo
            ComboBox.DataTextField = Descripcion
            ComboBox.DataBind()
            ComboBox.Items.Insert(0, New ListItem(String.Empty, String.Empty))
            ComboBox.SelectedIndex = 0
        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: "
        End Try
    End Sub
    Public Sub llenarCombo2(ByVal sql As String, ByVal ComboBox As DropDownList, ByVal codigo As String, ByVal Descripcion As String)
        Dim adapter As New SqlDataAdapter(sql, conIT)
        Dim datos As New DataTable
        Try
            adapter.Fill(datos)
            ComboBox.DataSource = datos
            ComboBox.DataValueField = codigo
            ComboBox.DataTextField = Descripcion
            ComboBox.DataBind()
            ComboBox.Items.Insert(0, New ListItem(String.Empty, String.Empty))
        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: "
        End Try


    End Sub
    Public Sub llenarComboSinCodigo(ByVal ComboBox As DropDownList, _
                    ByVal sql As String, _
                    ByVal blankRow As Boolean)
        Dim cn As New SqlConnection(conIT)
        Try
            cn.Open()            ' Abrir la conexión a Sql   
            Dim cmd As New SqlCommand(sql, cn) ' Pasar la consulta sql y la conexión al Sql Command    
            Dim da As New SqlDataAdapter(cmd) ' Inicializar un nuevo SqlDataAdapter 
            Dim ds As New DataSet 'Crear y Llenar un Dataset 

            da.Fill(ds)
            ComboBox.DataSource = ds.Tables(0)          ' asignar el DataSource al combobox 
            ComboBox.DataTextField = ds.Tables(0).Columns(0).ColumnName ' Asignar el campo a la propiedad DisplayMember del combo 
            If blankRow = True Then
                ComboBox.SelectedIndex = -1
            End If

        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: " & sql
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Function obtieneDoble(ByVal stringSQL As String, _
          ByVal title As String) As Double
        Dim cn As New SqlConnection(conIT)
        Dim daUser As SqlDataAdapter
        Dim adap As DataTableReader
        Dim tableData As New DataTable
        Dim temp As Double

        Try
            cn.Open()
            daUser = New SqlDataAdapter(stringSQL, conIT)
            daUser.Fill(tableData)
            adap = New DataTableReader(tableData)
            cn.Close()
            temp = tableData.Rows(0)(title)

        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: " & stringSQL
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try

        obtieneDoble = temp
    End Function

    Function obtieneEntero(ByVal stringSQL As String, _
              ByVal title As String) As Integer
        Dim cn As New SqlConnection(conIT)
        Dim daUser As SqlDataAdapter
        Dim adap As DataTableReader
        Dim tableData As New DataTable
        Dim temp As Integer

        Try
            cn.Open()
            daUser = New SqlDataAdapter(stringSQL, conIT)
            daUser.Fill(tableData)
            adap = New DataTableReader(tableData)
            cn.Close()
            temp = tableData.Rows(0)(title)

        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: " & stringSQL
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try

        obtieneEntero = temp
    End Function
    Function obtieneEnteroFam(ByVal stringSQL As String, _
              ByVal title As String) As Integer
        Dim cn As New SqlConnection(log)
        Dim daUser As SqlDataAdapter
        Dim adap As DataTableReader
        Dim tableData As New DataTable
        Dim temp As Integer

        Try
            cn.Open()
            daUser = New SqlDataAdapter(stringSQL, log)
            daUser.Fill(tableData)
            adap = New DataTableReader(tableData)
            cn.Close()
            temp = tableData.Rows(0)(title)

        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: " & stringSQL
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try

        obtieneEnteroFam = temp
    End Function
    Function obtienePalabra(ByVal stringSQL As String, _
           ByVal title As String) As String
        Dim cn As New SqlConnection(conIT)
        Dim daUser As SqlDataAdapter
        Dim adap As DataTableReader
        Dim tableData As New DataTable
        Dim temp As String = ""

        Try
            cn.Open()
            daUser = New SqlDataAdapter(stringSQL, conIT)
            daUser.Fill(tableData)
            adap = New DataTableReader(tableData)
            cn.Close()
            temp = tableData.Rows(0)(title)

        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: " & stringSQL
            temp = err
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try

        obtienePalabra = temp
    End Function
    Function obtienePalabraFam(ByVal stringSQL As String, _
          ByVal title As String) As String
        Dim cn As New SqlConnection(log)
        Dim daUser As SqlDataAdapter
        Dim adap As DataTableReader
        Dim tableData As New DataTable
        Dim temp As String = ""

        Try
            cn.Open()
            daUser = New SqlDataAdapter(stringSQL, log)
            daUser.Fill(tableData)
            adap = New DataTableReader(tableData)
            cn.Close()
            temp = tableData.Rows(0)(title)

        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: " & stringSQL
            temp = err
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try

        Return temp
    End Function


    Public Function palabra(ByVal inst As String, _
                         ByVal tit As String) As String
        Dim conn As New SqlConnection()
        conn = New SqlConnection(conIT)
        Dim adaptador = New SqlDataAdapter(inst, conn)
        Dim setDatos = New DataSet()
        adaptador.Fill(setDatos, "listado")
        Dim tabla As New DataTable
        tabla = setDatos.Tables("listado")
        Dim fila1 As DataRow = tabla.Rows(0)
        palabra = fila1(tit)
    End Function
    Public Function obtener_credenciales(ByVal usuario As String, ByVal contraseña As String) As Integer
        Try
            Dim cs = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim conn As New SqlConnection
            conn = New SqlConnection(cs)
            conn.Open()
            Dim commando As String = "SELECT dbo.fn_GEN_verificarCredenciales(@usuario,@contraseña) R"
            Dim com As SqlCommand = New SqlCommand(commando, conn)
            com.Parameters.AddWithValue("@usuario", usuario)
            com.Parameters.AddWithValue("@contraseña", contraseña)
            Dim adap As SqlDataAdapter = New SqlDataAdapter
            adap.SelectCommand = com
            Dim td As DataTable = New DataTable
            adap.Fill(td)
            conn.Close()
            Dim resultado As Integer = td.Rows(0)("R")
            Return resultado
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Sub llenarCombof(ByVal sql As String, ByVal ComboBox As DropDownList, ByVal codigo As String, ByVal Descripcion As String)

        Dim cs = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim adapter As New SqlDataAdapter(sql, cs)
        Dim datos As New DataTable
        Try
            adapter.Fill(datos)
            ComboBox.DataSource = datos
            ComboBox.DataValueField = codigo
            ComboBox.DataTextField = Descripcion
            ComboBox.DataBind()
            ComboBox.Items.Insert(0, New ListItem(String.Empty, String.Empty))
            ComboBox.SelectedIndex = 0
        Catch ex As Exception
            err = "Por favor envíe esta información a Sistemas: " & vbCrLf & vbCrLf & ex.Message.ToString & " - " & vbCrLf & ex.StackTrace & vbCrLf & "ref: "
        End Try
    End Sub

    Private Function svrUP() As String
        'svrUP = "user id = sa; password = 3xactu2!"
        svrUP = "user id = saApps; password = AppsC0mm0nH0p3"
        'svrUP = "user id = sa; password = alagran"
    End Function
    Private Function svr() As String
        svr = "user id = saApps; password = AppsC0mm0nH0p3"
    End Function


End Class
