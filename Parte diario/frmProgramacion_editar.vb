Imports System.Data.SqlClient
Public Class frmProgramacion_editar
    Dim Conexion As New SqlConnection("server=" & IP & ";uid=" & Usuario & ";password=" & Contraseña & ";database=" & Database)
    Sub Cargar_datos()
        'Los datos se cargan si la varible global _Programacion es diferente de 0
        'Si _Programacion es igual a 0, entonces no se carga nada UwU
        If _Programacion <> 0 Then
            Dim Query As String = "SELECT Actividad, Servicio, Descripcion, Fecha, Hora_Inicio, Hora_final, Activo FROM Programacion WHERE Programacion = '" & _Programacion & "'"
            'Se cargan los datos de la tabla Programacion
            'listActividad.Items.Add(Consulta_select(Query, Conexion).Rows(0)("Actividad"))
            Dim Actividad_query As String = "SELECT Actividad, Descripcion FROM Actividad WHERE Actividad = '" & Consulta_select(Query, Conexion).Rows(0)("Actividad") & "'"
            Dim Actividad_resultados As DataTable = Consulta_select(Actividad_query, Conexion)
            For i As Integer = 0 To Actividad_resultados.Rows.Count - 1
                gridActividad.Rows.Add(Actividad_resultados(i)(0), Actividad_resultados(i)(1))
            Next

            cbxServicio.SelectedValue = Consulta_select(Query, Conexion).Rows(0)("Servicio")
            txtDescripcion.Text = Consulta_select(Query, Conexion).Rows(0)("Descripcion")
            pickerFecha.Text = Consulta_select(Query, Conexion).Rows(0)("Fecha")
            pickerHoraInicio.Text = Consulta_select(Query, Conexion).Rows(0)("Hora_Inicio").ToString()
            pickerHoraFin.Text = Consulta_select(Query, Conexion).Rows(0)("Hora_final").ToString
            Dim Activo As String = Consulta_select(Query, Conexion).Rows(0)("Activo")

            'listVehiculo.Items.Add(Consulta_select(Query, Conexion).Rows(0)("Activo"))
            Dim Vehiculo_query As String = "SELECT Activo, Descripcion As Descrip FROM Ver_Activo WHERE cuenta = '1.2.3.1.5.2.04' AND estado = 'V' AND Activo = '" & Consulta_select(Query, Conexion).Rows(0)("Activo") & "'"
            Dim Vehiculo_resultados As DataTable = Consulta_select(Vehiculo_query, Conexion)
            For i As Integer = 0 To Vehiculo_resultados.Rows.Count - 1
                gridVehiculo.Rows.Add(Vehiculo_resultados(i)(0), Vehiculo_resultados(i)(1))
            Next


            'Se cargan los datos de la tabla Empleado_Programacion
            'Dim Empleado_programacion_query As String = "SELECT * FROM Empleado_Programacion WHERE Programacion = '" & _Programacion & "'"
            Dim Empleado_programacion_query As String = "SELECT Empleado_Programacion.Empleado, CONCAT(Ver_Empleado.Nombre, ' ',Ver_Empleado.Paterno, ' ', Ver_Empleado.Materno) AS Nombre FROM Empleado_Programacion INNER JOIN Ver_Empleado ON Empleado_Programacion.Empleado = Ver_Empleado.Empleado WHERE Empleado_Programacion.Programacion = '" & _Programacion & "'"
            Dim Empleado_programacion_resultados As DataTable = Consulta_select(Empleado_programacion_query, Conexion)
            For i As Integer = 0 To Empleado_programacion_resultados.Rows.Count - 1
                gridPersonal.Rows.Add(Empleado_programacion_resultados(i)(0), Empleado_programacion_resultados(i)(1))
            Next
            'Se cargan los datos de la tabla Activo
        Else
            pickerFecha.Text = _Fecha
        End If
    End Sub


    Private Sub frmProgramacion_nuevo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cargar_actividad()
        Cargar_vehiculo()
        Cargar_personal()
        Cargar_servicio()
        Cargar_datos()
    End Sub
    Sub Cargar_actividad()
        'Esta función sirve para cargar las actividades dentro de la grilla correspondiente
        Dim Busqueda As String = txtBuscarActividad.Text
        Dim Query As String = "SELECT Actividad, Descripcion FROM Actividad WHERE Descripcion LIKE '%" & Busqueda & "%'"
        DataGridView1.DataSource = Consulta_select(Query, Conexion)
    End Sub

    Sub Cargar_vehiculo()
        'Esta función sirve para cargar los vehículos dentro de la grilla correspondiente
        Dim Busqueda As String = txtBuscarVehiculo.Text
        Dim Query As String = "SELECT Activo, Descripcion FROM Ver_Activo WHERE cuenta = '1.2.3.1.5.2.04' AND estado = 'V' AND Descripcion LIKE '%" & Busqueda & "%'"
        DataGridView2.DataSource = Consulta_select(Query, Conexion)
    End Sub

    Sub Cargar_personal()
        'Esta función sirve para cargar los datos del personal dentro de la grilla correspondiente
        Dim Busqueda As String = txtBuscarPersonal.Text
        Dim Query As String = "SELECT Empleado, CONCAT(Nombre,' ',Paterno,' ',Materno) AS Nombre FROM Ver_empleado WHERE contrato = '1' AND CONCAT(Nombre,' ',Paterno,' ',Materno) LIKE '%" & Busqueda & "%'"
        DataGridView3.DataSource = Consulta_select(Query, Conexion)
    End Sub

    Sub Cargar_servicio()
        'Esta función sirve para cargar los tipos de servicio (energía eléctrica, tv cable) dentro
        'del combobox correspondiente
        Dim Query As String = "SELECT Servicio, Descripcion FROM Ver_Servicio WHERE Servicio < 4"
        cbxServicio.DisplayMember = "Descripcion"
        cbxServicio.ValueMember = "Servicio"
        cbxServicio.DataSource = Consulta_select(Query, Conexion)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'listActividad.Items.Clear()
        'Dim Actividad As Integer = DataGridView1.Item("Actividad", DataGridView1.CurrentRow.Index).Value
        'listActividad.Items.Add(Actividad)
        Try
            gridActividad.Rows.Clear()
            Dim Actividad As Integer = DataGridView1.Item("Actividad", DataGridView1.CurrentRow.Index).Value
            Dim Descripcion As String = DataGridView1.Item("Descripcion", DataGridView1.CurrentRow.Index).Value
            gridActividad.Rows.Add(Actividad, Descripcion)
        Catch ex As Exception
            MsgBox("El registro no puede trasladarse :P")
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'listVehiculo.Items.Clear()
        'Dim Activo As String = DataGridView2.Item("Activo", DataGridView2.CurrentRow.Index).Value
        'listVehiculo.Items.Add(Activo)
        Try
            gridVehiculo.Rows.Clear()
            Dim Activo As String = DataGridView2.Item("Activo", DataGridView2.CurrentRow.Index).Value
            Dim Descripcion As String = DataGridView2.Item("Descripcion", DataGridView2.CurrentRow.Index).Value
            gridVehiculo.Rows.Add(Activo, Descripcion)
        Catch ex As Exception
            MsgBox("El registro no puede trasladarse U_U")
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'Dim Empleado As Integer = DataGridView3.Item("Empleado", DataGridView3.CurrentRow.Index).Value
        'listPersonal.Items.Add(Empleado)
        Try
            Dim Empleado As Integer = DataGridView3.Item("Empleado", DataGridView3.CurrentRow.Index).Value
            Dim Nombre As String = DataGridView3.Item("Nombre", DataGridView3.CurrentRow.Index).Value
            Dim Empleado_recorrido As Integer = 0
            Dim Esta_repetido As Integer = 0
            For i As Integer = 0 To gridPersonal.Rows.Count - 1
                Empleado_recorrido = gridPersonal.Item("Empleado", i).Value
                If Empleado_recorrido = Empleado Then
                    Esta_repetido = 1
                End If
            Next
            If Esta_repetido = 0 Then
                gridPersonal.Rows.Add(Empleado, Nombre)
            Else
                MsgBox("No puede añadir registros repetidos")
            End If
        Catch ex As Exception
            MsgBox("El registro no puede trasladarse :P")
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'listActividad.Items.Clear()
        gridActividad.Rows.Clear()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'listVehiculo.Items.Clear()
        gridVehiculo.Rows.Clear()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'Dim index As Integer = listPersonal.SelectedIndex
        'If (index >= 0) Then
        'listPersonal.Items.RemoveAt(index)
        'Else
        'MsgBox("Debe seleccionar un ítem de la lista")
        'End If
        Try
            gridPersonal.Rows.RemoveAt(gridPersonal.CurrentRow.Index)
        Catch ex As Exception
            MsgBox("Debe seleccionar un ítem de la lista :v")
        End Try
    End Sub

    Private Sub txtBuscarActividad_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscarActividad.KeyUp
        Cargar_actividad()
    End Sub

    Private Sub txtBuscarVehiculo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscarVehiculo.KeyUp
        Cargar_vehiculo()
    End Sub

    Private Sub txtBuscarPersonal_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscarPersonal.KeyUp
        Cargar_personal()
    End Sub
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If (gridActividad.Rows.Count > 0) Then
            If (gridPersonal.Rows.Count > 0) Then
                'Dim Actividad As Integer = listActividad.Items.Item(0)
                Dim Actividad As Integer = gridActividad.Item("Actividad", 0).Value
                Dim Servicio As Integer = cbxServicio.SelectedValue
                Dim Descripcion As String = txtDescripcion.Text
                Dim Fecha As String = pickerFecha.Text
                Dim Hora_Inicio As String = pickerHoraInicio.Text
                Dim Hora_final As String = pickerHoraFin.Text
                Dim Activo As String = ""
                If (gridVehiculo.Rows.Count > 0) Then
                    'Activo = listVehiculo.Items.Item(0)
                    Activo = gridVehiculo.Item("Activo", 0).Value
                End If
                'Se reliza la conexión y la edición a la base de datos
                'Tabla Programacion
                Dim Query As String
                Dim Mensaje_de_error As String
                If _Programacion <> 0 Then
                    Query = "UPDATE Programacion SET Actividad = '" & Actividad & "', Servicio = '" & Servicio & "', Descripcion = '" & Descripcion & "', Fecha = '" & Fecha & "', Hora_Inicio = '" & Hora_Inicio & "', Hora_final = '" & Hora_final & "', Activo = '" & Activo & "' WHERE Programacion = '" & _Programacion & "'"
                    Mensaje_de_error = "El registro no pudo editarse"
                Else
                    Query = "INSERT INTO Programacion (Actividad, Servicio, Descripcion, Fecha, Hora_Inicio, Hora_final, Activo) VALUES ('" & Actividad & "', '" & Servicio & "', '" & Descripcion & "', '" & Fecha & " ', '" & Hora_Inicio & "', '" & Hora_final & "', '" & Activo & "')"
                    Mensaje_de_error = "El registro no pudo añadirse"
                End If
                Conexion.Open()
                Dim Cmd As New SqlCommand(Query, Conexion)
                Try
                    Cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(Mensaje_de_error)
                    MessageBox.Show(ex.Message.ToString, ex.Source.ToString)
                End Try
                'Se realiza la conexión y la edición a la BD
                'Tabla Empleado_Programacion
                If _Programacion <> 0 Then
                    Dim Empleado As String
                    Dim Query2 As String = "DELETE FROM Empleado_Programacion WHERE Programacion = '" & _Programacion & "'; "
                    For index As Integer = 0 To gridPersonal.Rows.Count - 1
                        'Empleado = listPersonal.Items.Item(index)
                        Empleado = gridPersonal.Item("Empleado", index).Value
                        Query2 = Query2 + "INSERT INTO Empleado_Programacion VALUES ('" & _Programacion & "', '" & Empleado & "'); "
                    Next

                    Dim Cmd2 As New SqlCommand(Query2, Conexion)
                    Try
                        Cmd2.ExecuteNonQuery()
                        Me.Close()
                    Catch ex As Exception
                        MsgBox("Los datos del personal no pudieron ser editados :(")
                        MessageBox.Show(ex.Message.ToString, ex.Source.ToString)
                    Finally
                        Conexion.Close()
                    End Try
                Else
                    'Se realiza la conexión y la inserción a la BD
                    'Tabla Empleado_Programacion
                    '     Primeramente se obtiene el valor autoincrementable de la tabla programacion
                    Dim Valor_incrementable_de_la_tabla_programacion As Integer
                    Dim Query_valor_incrementable As String = "SELECT IDENT_CURRENT ('Programacion') AS ID"
                    Dim Cmd_valor_incrementable As New SqlCommand(Query_valor_incrementable, Conexion)
                    Cmd_valor_incrementable.ExecuteNonQuery()
                    Dim Da As New SqlDataAdapter(Cmd_valor_incrementable)
                    Dim Dt As New DataTable
                    Da.Fill(Dt)
                    Valor_incrementable_de_la_tabla_programacion = Dt.Rows(0)("ID")
                    '     Una vez obtenido el valor, se procede a la inserción en la tabla Empleado_Programacion
                    Dim Empleado As String
                    Dim Query2 As String = ""
                    For index As Integer = 0 To gridPersonal.Rows.Count - 1
                        'Empleado = listPersonal.Items.Item(index)
                        Empleado = gridPersonal.Item("Empleado", index).Value
                        Query2 = Query2 + "INSERT INTO Empleado_Programacion VALUES ('" & Valor_incrementable_de_la_tabla_programacion & "', '" & Empleado & "'); "
                    Next
                    Dim Cmd2 As New SqlCommand(Query2, Conexion)
                    Try
                        Cmd2.ExecuteNonQuery()

                        Me.Close()
                    Catch ex As Exception
                        MsgBox("Los datos del personal no pudieron ser añadidos :(")
                        MessageBox.Show(ex.Message.ToString, ex.Source.ToString)
                    Finally
                        Conexion.Close()
                    End Try
                End If
            Else
                MsgBox("Debe asignar personal")
            End If
        Else
            MsgBox("Debe seleccionar una actividad")
        End If
    End Sub
End Class