Imports System.Data.SqlClient
Public Class frmActividades
    Dim Conexion = New SqlConnection("server=" & IP & ";uid=" & Usuario & ";password=" & Contraseña & ";database=" & Database)
    Private Sub frmActividades_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cargar_datos()
    End Sub

    Sub Cargar_datos()
        'Esta función se utiliza para cargar datos a la grilla
        Dim Query As String = "SELECT Actividad, Descripcion FROM Actividad"
        DataGridView1.DataSource = Consulta_select(Query, Conexion)
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        'Se abre el formulario para añadir datos, la variable global _Actividad = 0
        _Actividad = 0
        Dim frmAct_nuevo As New frmActividades_edita
        frmAct_nuevo.ShowDialog()
        'Una vez cerrado el formulario, se vuelven a cargar los datos a la grilla
        Cargar_datos()
    End Sub

    Private Sub btnEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditar.Click
        'Se abre el formulario para editar datos, la variable global _Actividad es diferente de 0
        _Actividad = DataGridView1.Item("Actividad", DataGridView1.CurrentRow.Index).Value
        Dim frmAct_editar As New frmActividades_edita
        frmAct_editar.ShowDialog()
        'Una vez cerrado el formulario, se vuelven a cargar los datos nuevamente a la grilla
        Cargar_datos()
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        'Se llama a un mensaje box, si es afirmativo recién se procede al llenado
        Dim Borrar As DialogResult
        Borrar = MessageBox.Show("¿Desea eliminar el registro seleccionado?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If (Borrar = DialogResult.Yes) Then
            Conexion.Open()
            _Actividad = DataGridView1.Item("Actividad", DataGridView1.CurrentRow.Index).Value
            Dim Query As String = "DELETE Actividad WHERE Actividad = '" & _Actividad & "'"
            Dim Cmd As New SqlCommand(Query, Conexion)
            Try
                Cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("El registro no pudo eliminarse")
                MessageBox.Show(ex.Message.ToString, ex.Source.ToString)
            Finally
                Conexion.Close()
                Cargar_datos()
            End Try
        End If
    End Sub
End Class