Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmProgramacion
    Dim Conexion As New SqlConnection("server=" & IP & ";uid=" & Usuario & ";password=" & Contraseña & ";database=" & Database)
    Private Sub frmProgramacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cargar_datos()
    End Sub
    Sub Cargar_datos()
        _Fecha = pickerFecha.Text
        Dim Query As String = "SELECT Programacion.Programacion, Actividad.Descripcion Actividad, Ver_Servicio.Descripcion Servicio, Programacion.Descripcion, Programacion.Fecha, Programacion.Hora_Inicio 'Hora Inicio', Programacion.Hora_final 'Hora de Fin.', Ver_Activo.Descripcion Vehículo FROM Programacion INNER JOIN Actividad ON Programacion.Actividad = Actividad.Actividad INNER JOIN Ver_Servicio ON Programacion.Servicio = Ver_Servicio.Servicio LEFT JOIN Ver_Activo ON Programacion.Activo = Ver_Activo.Activo WHERE Programacion.Fecha = '" & _Fecha & "'"
        'Dim Query As String = "SELECT Programacion.Programacion, Actividad.Descripcion Actividad, Ver_Servicio.Descripcion Servicio, Programacion.Descripcion, Programacion.Fecha, Programacion.Hora_Inicio 'Hora Inicio', Programacion.Hora_final 'Hora de Fin.', Programacion.Activo Vehículo FROM Programacion INNER JOIN Actividad ON Programacion.Actividad = Actividad.Actividad INNER JOIN Ver_Servicio ON Programacion.Servicio = Ver_Servicio.Servicio WHERE Programacion.Fecha = '" & _Fecha & "'"
        Try
            DataGridView1.DataSource = Consulta_select(Query, Conexion)
        Catch ex As Exception
            MsgBox("Los datos no pudieron ser cargados :(")
            MessageBox.Show(ex.Message.ToString, ex.Source.ToString)
        End Try
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        _Programacion = 0
        _Fecha = pickerFecha.Text
        Dim frmPro_nuevo As New frmProgramacion_editar
        frmPro_nuevo.ShowDialog()
        Cargar_datos()
    End Sub

    Private Sub btnEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditar.Click
        _Programacion = DataGridView1.Item("Programacion", DataGridView1.CurrentRow.Index).Value
        _Fecha = pickerFecha.Text
        Dim frmPro_editar As New frmProgramacion_editar
        frmPro_editar.ShowDialog()
        Cargar_datos()
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        'Se llama a un mensaje box, si es afirmativo recién se procede al llenado
        Dim Borrar As DialogResult
        Borrar = MessageBox.Show("¿Desea eliminar el registro seleccionado?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If (Borrar = DialogResult.Yes) Then
            Conexion.Open()
            _Programacion = DataGridView1.Item("Programacion", DataGridView1.CurrentRow.Index).Value
            Dim Query As String = "DELETE Empleado_Programacion WHERE Programacion = '" & _Programacion & "'; DELETE Programacion WHERE Programacion = '" & _Programacion & "'"
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

    Private Sub pickerFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pickerFecha.ValueChanged
        Cargar_datos()
    End Sub

    Private Sub btnReporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReporte.Click
        Reporte()
    End Sub

    Sub Reporte()
        'Conexión a la base de datos
        Dim Fecha As String = pickerFecha.Text
        Dim Query As String = "SELECT * INTO ##REP_PROGRAMACION_DIARIA FROM VER_PROGRAMACION_DIARIA WHERE FECHA = '" & Fecha & "'"
        Try
            Conexion.Open()
            Dim Cmd As New SqlCommand(Query, Conexion)
            Cmd.ExecuteNonQuery()
            Dim rpt As New CrystalReport1
            rpt.SetDatabaseLogon(Usuario, Contraseña)
            Dim formulario As New frmProgramacion_generar_reporte
            formulario.CrystalReportViewer1.ReportSource = rpt
            formulario.CrystalReportViewer1.Dock = DockStyle.Fill
            Dim frmReporte As New Form()
            With frmReporte
                .Controls.Add(formulario.CrystalReportViewer1)
                .Text = "PROGRAMACIÓN DIARIA DE ACTIVIDADES"
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