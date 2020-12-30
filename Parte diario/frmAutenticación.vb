Imports System.Data.SqlClient


Public Class frmAutenticación

    Private Sub bntIngresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntIngresar.Click
        Autenticar()
    End Sub

    Private Sub FrmAutenticacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtUsuario.Clear()
        txtContraseña.Clear()
    End Sub

    Private Sub txtContraseña_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtContraseña.KeyDown
        If e.KeyCode = 13 Then
            Autenticar()
        End If
    End Sub

    Sub Autenticar()
        'Se dá valor a las variables (Fueron declaradas dentro del módulo Configuracion.vb
        Usuario = txtUsuario.Text
        Contraseña = txtContraseña.Text
        'Se abre la conexión a la BD
        Dim Conexion As New SqlConnection("server=" & IP & ";uid=" & Usuario & ";password=" & Contraseña & ";database=" & Database)
        Try
            'Si la conexión está abierta, se abre el formulario correspondiente
            Conexion.Open()
            If (Conexion.State = ConnectionState.Open) Then
                frmInicio.Show()
                Me.Hide()
            End If
        Catch ex As Exception
            MsgBox("Usuario o contraseña incorrecta :P")
        End Try
        Conexion.Close()
    End Sub

    Private Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBorrar.Click
        txtUsuario.Clear()
        txtContraseña.Clear()
    End Sub
End Class
