Imports System.Data.SqlClient
Public Class frmEjecucion
    Dim Conexion As New SqlConnection("server=" & IP & ";uid=" & Usuario & ";password=" & Contraseña & ";database=" & Database)
    Private Sub frmEjec_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cargar_servicio()
        Cargar_datos()
    End Sub
    Sub Cargar_servicio()
        'Esta función sirve para cargar los tipos de servicio, dentro del combobox correspondiente
        Dim Query As String = "SELECT Servicio, Descripcion FROM Ver_Servicio WHERE Servicio < 4"
        cbxServicio.DisplayMember = "Descripcion"
        cbxServicio.ValueMember = "Servicio"
        cbxServicio.DataSource = Consulta_select(Query, Conexion)
    End Sub
    Sub Cargar_datos()
        'Esta función sirve para cargar datos en la grilla
        _Fecha = pickerFecha.Text
        _Servicio = cbxServicio.SelectedValue
        'Dim Query As String = "SELECT Empleado_Ejecucion.Empleado_Ejecucion AS Ejecucion, Empleado_Ejecucion.Orden, Empleado_Ejecucion.Programacion, Ver_Servicio.Descripcion Servicio, Orden_Tipo.Descripcion Tipo FROM Empleado_Ejecucion INNER JOIN Ver_Servicio ON Empleado_Ejecucion.Servicio = Ver_Servicio.Servicio INNER JOIN Orden_Tipo ON Empleado_Ejecucion.Tipo = Orden_Tipo.Tipo WHERE Empleado_Ejecucion.Fecha = '" & _Fecha & "' AND Empleado_Ejecucion.Servicio = '" & _Servicio & "'"
        Dim Query As String = "SELECT DISTINCT Empleado_Ejecucion.Programacion, Ver_Servicio.Descripcion Servicio, Orden_Tipo.Descripcion Tipo FROM Empleado_Ejecucion INNER JOIN Ver_Servicio ON Empleado_Ejecucion.Servicio = Ver_Servicio.Servicio INNER JOIN Orden_Tipo ON Empleado_Ejecucion.Tipo = Orden_Tipo.Tipo WHERE Empleado_Ejecucion.Fecha = '" & _Fecha & "' AND Empleado_Ejecucion.Servicio = '" & _Servicio & "'"
        DataGridView1.DataSource = Consulta_select(Query, Conexion)
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        _Programacion = 0
        _Fecha = pickerFecha.Text
        _Servicio = cbxServicio.SelectedValue
        'Se abre el formulario correspondiente
        Dim frmEje_nuevo As New frmEjecucion_editar
        frmEje_nuevo.ShowDialog()
        'Una vez cerrado el formulario, se cargan los datos nuevamente a la grilla
        Cargar_datos()
    End Sub

    Private Sub btnEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditar.Click
        Try
            _Programacion = DataGridView1.Item("Programacion", DataGridView1.CurrentRow.Index).Value
        Catch
            MsgBox("Debe seleccionar un registro")
            'Exist sub sirve para salir del procedimiento y que ya no ejecute ninguna sentencia más
            Exit Sub
        End Try
        _Fecha = pickerFecha.Text
        _Servicio = cbxServicio.SelectedValue
        'Se abre el formulario correspondiente
        Dim frmEje_editar As New frmEjecucion_editar
        frmEje_editar.ShowDialog()
        'Una vez cerrados los datos, se cargan los datos nuevamente a la grilla
        Cargar_datos()
    End Sub
    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        'Se llama a un mensaje box, si es afirmativo recién se procede al llenado
        Dim Borrar As DialogResult
        Borrar = MessageBox.Show("¿Desea eliminar el registro seleccionado?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If (Borrar = DialogResult.Yes) Then
            Conexion.Open()
            _Programacion = DataGridView1.Item("Programacion", DataGridView1.CurrentRow.Index).Value
            Dim Query As String = "DELETE FROM Empleado_Ejecucion WHERE Programacion = '" & _Programacion & "'"
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

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pickerFecha.ValueChanged
        Cargar_datos()
    End Sub

    Private Sub cbxServicio_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxServicio.SelectionChangeCommitted
        Cargar_datos()
    End Sub

    Private Sub btnReporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReporte.Click
        'Conexión a la base de datos
        _Fecha = pickerFecha.Text
        _Servicio = cbxServicio.SelectedValue

        'Dim Query As String = "SELECT Empleado_Ejecucion.Empleado_Ejecucion AS Ejecucion, Empleado_Ejecucion.Orden, Empleado_Programacion.Empleado, Programacion_Empleados.Empleados, Ver_Empleado.Nombre, Ver_Empleado.Paterno, Ver_Empleado.Materno, Ver_Servicio.Descripcion Servicio, Orden_Tipo.Descripcion Tipo, Empleado_Ejecucion.Programacion, Empleado_Ejecucion.Fecha, Empleado_Ejecucion.Nota, Ver_Servicio_Todoss.Razon INTO ##REP_PARTE_DIARIO FROM Empleado_Ejecucion INNER JOIN Ver_Servicio ON Empleado_Ejecucion.Servicio = Ver_Servicio.Servicio INNER JOIN Orden_Tipo ON Empleado_Ejecucion.Tipo = Orden_Tipo.Tipo INNER JOIN Ver_Servicio_Todoss ON Ver_Servicio_Todoss.Nro = Empleado_Ejecucion.Orden AND Ver_Servicio_Todoss.Servicio = Empleado_Ejecucion.Servicio AND Ver_Servicio_Todoss.Tipo = Empleado_Ejecucion.Tipo INNER JOIN Empleado_Programacion ON Empleado_Programacion.Programacion = Empleado_Ejecucion.Programacion INNER JOIN Ver_Empleado ON Empleado_Programacion.Empleado = Ver_Empleado.Empleado INNER JOIN Programacion_Empleados ON Empleado_Ejecucion.Programacion = Programacion_Empleados.Programacion WHERE Empleado_Ejecucion.Fecha = '" & _Fecha & "' AND Empleado_Ejecucion.Servicio = '" & _Servicio & "'"
        Dim Query As String = "SELECT Empleado_Ejecucion.Empleado_Ejecucion AS Ejecucion, Empleado_Ejecucion.Orden, Empleado_Programacion.Empleado, Programacion_Empleados.Empleados, Ver_Empleado.Nombre, Ver_Empleado.Paterno, Ver_Empleado.Materno, Ver_Servicio.Descripcion Servicio, Orden_Tipo.Descripcion Tipo, Empleado_Ejecucion.Programacion, Empleado_Ejecucion.Fecha, Empleado_Ejecucion.Nota, Ver_Servicio_Todoss.Razon INTO ##REP_PARTE_DIARIO FROM Empleado_Ejecucion INNER JOIN Ver_Servicio ON Empleado_Ejecucion.Servicio = Ver_Servicio.Servicio INNER JOIN Orden_Tipo ON Empleado_Ejecucion.Tipo = Orden_Tipo.Tipo LEFT JOIN Ver_Servicio_Todoss ON Ver_Servicio_Todoss.Nro = Empleado_Ejecucion.Orden AND Ver_Servicio_Todoss.Servicio = Empleado_Ejecucion.Servicio AND Ver_Servicio_Todoss.Tipo = Empleado_Ejecucion.Tipo INNER JOIN Empleado_Programacion ON Empleado_Programacion.Programacion = Empleado_Ejecucion.Programacion INNER JOIN Ver_Empleado ON Empleado_Programacion.Empleado = Ver_Empleado.Empleado INNER JOIN Programacion_Empleados ON Empleado_Ejecucion.Programacion = Programacion_Empleados.Programacion WHERE Empleado_Ejecucion.Fecha = '" & _Fecha & "' AND Empleado_Ejecucion.Servicio = '" & _Servicio & "'"
        'Dim Query As String = "SELECT Empleado_Ejecucion.Empleado_Ejecucion AS Ejecucion, Empleado_Ejecucion.Orden, Empleado_Programacion.Empleado, CONCAT(Ver_Empleado.Paterno,' ', Ver_Empleado.Materno, ' ', Ver_Empleado.Nombre) AS Empleados, Ver_Empleado.Nombre, Ver_Empleado.Paterno, Ver_Empleado.Materno, Ver_Servicio.Descripcion Servicio, Orden_Tipo.Descripcion Tipo, Empleado_Ejecucion.Programacion, Empleado_Ejecucion.Fecha, Ver_Servicio_Todoss.Razon INTO ##REP_PARTE_DIARIO FROM Empleado_Ejecucion INNER JOIN Ver_Servicio ON Empleado_Ejecucion.Servicio = Ver_Servicio.Servicio INNER JOIN Orden_Tipo ON Empleado_Ejecucion.Tipo = Orden_Tipo.Tipo INNER JOIN Ver_Servicio_Todoss ON Ver_Servicio_Todoss.Nro = Empleado_Ejecucion.Orden AND Ver_Servicio_Todoss.Servicio = Empleado_Ejecucion.Servicio AND Ver_Servicio_Todoss.Tipo = Empleado_Ejecucion.Tipo INNER JOIN Empleado_Programacion ON Empleado_Programacion.Programacion = Empleado_Ejecucion.Programacion INNER JOIN Ver_Empleado ON Empleado_Programacion.Empleado = Ver_Empleado.Empleado WHERE Empleado_Ejecucion.Fecha = '" & _Fecha & "' AND Empleado_Ejecucion.Servicio = '" & _Servicio & "'"
        Try
            Conexion.Open()
            Dim Cmd As New SqlCommand(Query, Conexion)
            Cmd.ExecuteNonQuery()
            Dim rpt As New CrystalReport2
            rpt.SetDatabaseLogon(Usuario, Contraseña)
            Dim formulario As New frmEjecucion_generar_reporte
            formulario.CrystalReportViewer1.ReportSource = rpt
            formulario.CrystalReportViewer1.Dock = DockStyle.Fill
            Dim frmReporte As New Form()
            With frmReporte
                .Controls.Add(formulario.CrystalReportViewer1)
                .Text = "PARTE DIARIO DE ACTIVIDADES"
                .WindowState = FormWindowState.Maximized
                .ShowDialog()
            End With
        Catch ex As Exception
            MsgBox("El reporte no pudo ser generado")
            MessageBox.Show(ex.Message.ToString, ex.Source.ToString)
        Finally
            Conexion.Close()
        End Try
    End Sub
End Class