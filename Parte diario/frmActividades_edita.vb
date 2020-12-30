Imports System.Data.SqlClient
Public Class frmActividades_edita
    Dim Conexion = New SqlConnection("server=" & IP & ";uid=" & Usuario & ";password=" & Contraseña & ";database=" & Database)
    Private Sub frmActividades_edita_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Si la variable global _Actividad es distinta de 0, se cargan los datos
        'Si la variable global _Actividad es igual a 0, no se carga nada :v
        If _Actividad <> 0 Then
            'Se realiza la conexión a la BD y la consulta
            Dim Query As String = "SELECT Descripcion FROM Actividad WHERE Actividad = '" & _Actividad & "'"
            txtDescripcion.Text = Consulta_select(Query, Conexion).Rows(0)("Descripcion")
        End If
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        'Se obtienen los valores, a partir de los controles correspondientes
        Dim Descripcion As String = txtDescripcion.Text
        'Se declara la consulta
        'Si la variable global _Actividad es diferente de 0, se realiza una consulta UPDATE
        'Si la variable global _Actividad es = 0, se realiza una consulta INSERT
        Dim Query As String
        Dim Mensaje_de_error As String
        If _Actividad <> 0 Then
            Query = "UPDATE Actividad SET Descripcion = '" & Descripcion & "' WHERE Actividad = '" & _Actividad & "'"
            Mensaje_de_error = "No se pudo editar el registro seleccionado :v"
        Else
            Query = "INSERT INTO Actividad (Descripcion, Habilitado) VALUES ('" & Descripcion & "', '" & 1 & "')"
            Mensaje_de_error = "El registro no pudo añadirse"
        End If
        Conexion.Open()
        Dim Cmd As New SqlCommand(Query, Conexion)
        Try
            Cmd.ExecuteNonQuery()
            Me.Close()
        Catch ex As Exception
            MsgBox(Mensaje_de_error)
            MessageBox.Show(ex.Message.ToString, ex.Source.ToString)
        End Try
    End Sub
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub
End Class