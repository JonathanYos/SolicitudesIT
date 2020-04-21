Imports Microsoft.VisualBasic
Imports System.Globalization

Public Class Glob
    Dim en As New CultureInfo("en-US")
    Dim sql As String
    Dim cn As New Conn
    'Dim glob As Conn

    Function SoloNumeros(ByVal Keyascii As Short) As Short
        If InStr("1234567890", Chr(Keyascii)) = 0 Then
            SoloNumeros = 0
        Else
            SoloNumeros = Keyascii
        End If
        Select Case Keyascii
            Case 8
                SoloNumeros = Keyascii
            Case 13
                SoloNumeros = Keyascii
        End Select
    End Function
End Class
