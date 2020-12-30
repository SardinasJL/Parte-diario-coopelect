Imports System.Data.SqlClient
Imports System.Data
Public Class frmEjecucion_editar
    Dim Conexion As New SqlConnection("server=" & IP & ";uid=" & Usuario & ";password=" & Contraseña & ";database=" & Database)
    Sub Cargar_datos()
        'Si la variable global _Programacion es distinta de 0, se cargan datos
        'Si la variable global _Programacion es = 0, no se carga nada :v
        If _Programacion <> 0 Then
            Dim Query As String = "SELECT Orden, Servicio, Tipo, Fecha, Nota FROM Empleado_Ejecucion WHERE Programacion = '" & _Programacion & "'"
            'Los resultados obtenidos de la tabla Empleado_Ejecucion son puestos dentro de los controles respectivos

            'Primeramente se añada a la grilla de órdenes seleccionadas
            'listOrden.Items.Add(Consulta_select(Query, Conexion).Rows(0)("Orden"))
            'Dim Ordenes_query = "SELECT Nro, Razon FROM Ver_Servicio_Todoss WHERE Nro = '" & Consulta_select(Query, Conexion).Rows(0)("Orden") & "'"
            'Dim Ordenes_query As String = "SELECT Nro, Razon FROM Ver_Servicio_Todoss WHERE Servicio = '" & Consulta_select(Query, Conexion).Rows(0)("Servicio") & "' AND Tipo = '" & Consulta_select(Query, Conexion).Rows(0)("Tipo") & "'  AND Nro = '" & Consulta_select(Query, Conexion).Rows(0)("Orden") & "'"
            Dim Ordenes_query As String = "SELECT Ver_Servicio_Todoss.Nro, Ver_Servicio_Todoss.Razon FROM Empleado_Ejecucion INNER JOIN Ver_Servicio_Todoss ON Empleado_Ejecucion.Orden = Ver_Servicio_Todoss.Nro AND Empleado_Ejecucion.Servicio = Ver_Servicio_Todoss.Servicio AND Empleado_Ejecucion.Tipo = Ver_Servicio_Todoss.tipo WHERE Empleado_Ejecucion.Programacion = '" & _Programacion & "'"
            Dim Ordenes_resultado As DataTable = Consulta_select(Ordenes_query, Conexion)
            For i As Integer = 0 To Ordenes_resultado.Rows.Count - 1
                gridOrden.Rows.Add(Ordenes_resultado(i)(0), Ordenes_resultado(i)(1))
            Next

            cbxServicio.SelectedValue = Consulta_select(Query, Conexion).Rows(0)("Servicio")
            cbxTipo.SelectedValue = Consulta_select(Query, Conexion).Rows(0)("Tipo")

            'Se añade las las programaciones previamente guardadas en la grilla
            'listProgramacion.Items.Add(Consulta_select(Query, Conexion).Rows(0)("Programacion"))
            'Dim Programacion_query As String = "SELECT Programacion.Programacion, Actividad.Descripcion FROM Programacion INNER JOIN Actividad ON Programacion.Actividad = Actividad.Actividad WHERE Programacion.Programacion = '" & _Programacion & "'"
            'Dim Programacion_resultado As DataTable = Consulta_select(Programacion_query, Conexion)
            'For i As Integer = 0 To Programacion_resultado.Rows.Count - 1
            'gridProgramacion.Rows.Add(Programacion_resultado(i)(0), Programacion_resultado(i)(1))
            'Next


            txtNota.Text = Convert.ToString(Consulta_select(Query, Conexion).Rows(0)("Nota"))

            pickerFecha.Text = Consulta_select(Query, Conexion).Rows(0)("Fecha")
            pickerFecha_actividad.Text = Consulta_select(Query, Conexion).Rows(0)("Fecha")


            Dim Query_programacion_recuperar As String = "SELECT Programacion.Programacion, Actividad.Descripcion, CONCAT(Programacion.Programacion, ' - ',  Actividad.Descripcion) AS Prog_Act FROM Programacion INNER JOIN Actividad ON Programacion.Actividad = Actividad.Actividad WHERE Programacion = '" & _Programacion & "'"
            cbxProgramacion.DisplayMember = "Prog_Act"
            cbxProgramacion.ValueMember = "Programacion"
            cbxProgramacion.DataSource = Consulta_select(Query_programacion_recuperar, Conexion)
            'cbxProgramacion.SelectedValue = _Programacion


        Else
            pickerFecha.Text = _Fecha
            pickerFecha_actividad.Text = _Fecha
            cbxServicio.SelectedValue = _Servicio
        End If
    End Sub
    Sub Cargar_tipo()
        'Esta función sirve para cargar el tipo de Actividad, dentro del combobox correspondiente
        Dim Query As String = "SELECT Tipo, Descripcion FROM Orden_Tipo"
        cbxTipo.DisplayMember = "Descripcion"
        cbxTipo.ValueMember = "Tipo"
        cbxTipo.DataSource = Consulta_select(Query, Conexion)
    End Sub
    Sub Cargar_servicio()
        'Esta función sirve para cargar los tipos de servicio, dentro del combobox correspondiente
        Dim Query As String = "SELECT Servicio, Descripcion FROM Ver_Servicio WHERE Servicio < 4"
        cbxServicio.DisplayMember = "Descripcion"
        cbxServicio.ValueMember = "Servicio"
        cbxServicio.DataSource = Consulta_select(Query, Conexion)
    End Sub
    Sub Cargar_orden()
        'Esta función sirve para cargar las Órdenes dentro de la grilla correspondiente
        Dim Busqueda As String = txtBuscarOrden.Text
        'Dim Servicio As String = cbxServicio.SelectedValue
        Dim Servicio As String = _Servicio
        Dim Tipo As String = cbxTipo.SelectedValue
        Dim Query As String = "SELECT Nro, Razon FROM Ver_Servicio_Todoss WHERE Servicio = '" & Servicio & "' AND Tipo = '" & Tipo & "'  AND Nro LIKE '" & Busqueda & "%' ORDER BY Nro DESC"
        DataGridView1.DataSource = Consulta_select(Query, Conexion)
    End Sub
    Sub Cargar_actividad()
        'Esta función sirve para cargar las actividades dentro de la grilla correspondiente
        'Dim Servicio As String = cbxServicio.SelectedValue
        Dim Servicio As String = _Servicio
        Dim Tipo As String = cbxTipo.SelectedValue
        Dim Fecha_actividad As String = pickerFecha_actividad.Text
        Dim Query As String = "SELECT Programacion.Programacion, Actividad.Descripcion, CONCAT(Programacion.Programacion, ' - ',  Actividad.Descripcion) AS Prog_Act FROM Programacion INNER JOIN Actividad ON Programacion.Actividad = Actividad.Actividad WHERE Servicio = '" & Servicio & "' AND Fecha = '" & Fecha_actividad & "'"
        'DataGridView2.DataSource = Consulta_select(Query, Conexion)
        Dim Dt_blanco As DataTable

        cbxProgramacion.DataSource = Dt_blanco
        cbxProgramacion.DisplayMember = "Prog_Act"
        cbxProgramacion.ValueMember = "Programacion"
        cbxProgramacion.DataSource = Consulta_select(Query, Conexion)
    End Sub
    Private Sub frmEjecucion_editar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cargar_servicio()
        Cargar_tipo()
        Cargar_orden()
        Cargar_actividad()
        Cargar_datos()
    End Sub
    Private Sub pickerFecha_actividad_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pickerFecha_actividad.ValueChanged
        Cargar_actividad()
    End Sub
    Private Sub cbxServicio_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxServicio.SelectionChangeCommitted
        Cargar_orden()
        Cargar_actividad()
        gridOrden.Rows.Clear()
        'gridProgramacion.Rows.Clear()
    End Sub
    Private Sub cbxTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxTipo.SelectedIndexChanged
        Cargar_orden()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'listOrden.Items.Clear()
        'Dim Nro As Integer = DataGridView1.Item("Nro", DataGridView1.CurrentRow.Index).Value
        'listOrden.Items.Add(Nro)
        Try
            Dim Nro As Integer = DataGridView1.Item("Nro", DataGridView1.CurrentRow.Index).Value
            Dim Razon As String = DataGridView1.Item("Razon", DataGridView1.CurrentRow.Index).Value
            Dim Nro_recorrido As Integer = 0
            Dim Esta_repetido As Integer = 0
            For i As Integer = 0 To gridOrden.Rows.Count - 1
                Nro_recorrido = gridOrden.Item(0, i).Value
                If Nro = Nro_recorrido Then
                    Esta_repetido = 1
                End If
            Next
            If Esta_repetido = 0 Then
                gridOrden.Rows.Add(Nro, Razon)
            Else
                MsgBox("No pueden añadirse registros duplicados")
            End If
        Catch ex As Exception
            MsgBox("El registro no puede trasladarse")
        End Try
    End Sub

    

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        Try
            gridOrden.Rows.RemoveAt(gridOrden.CurrentRow.Index)
        Catch ex As Exception
            MsgBox("Debe seleccionar un ítem de la lista :v")
        End Try
    End Sub
    

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If cbxProgramacion.Text = "" Then
            MsgBox("Debe seleccionar una programación")
            Exit Sub
        End If

        If (cbxTipo.SelectedValue <> "") Then
            'Empleado_Ejecucion es lo mismo que la variable global _Ejecucion
            'Dim Orden As Integer = gridOrden.Items.Item(0)
            Dim Orden As Integer
            Dim Tipo As String = cbxTipo.SelectedValue
            Dim Nota As String = txtNota.Text
            'Dim Programacion As String = listProgramacion.Items.Item(0)
            Dim Programacion As String = cbxProgramacion.SelectedValue
            'Se reliza la conexión y la edición del registro en la base de datos (Tabla Empleado_Ejecucion)
            Conexion.Open()
            'Si la variable global _Ejecucion es diferente de 0 se realiza un UPDATE
            'Si la variable global _Ejecucion es igual a 0, se realiza un INSERT
            Dim Query As String
            Dim Query2 As String = ""
            Dim Mensaje_de_error As String
            If _Programacion <> 0 Then
                'Si _Programacion es distinta de 0, se realiza un UPDATE
                'Para el UPDATE, primero se borran los registros con la clave Programacion = _Programacion
                'Query = "UPDATE Empleado_Ejecucion SET Orden = '" & Orden & "', Servicio='" & _Servicio & "', Tipo='" & Tipo & "', Programacion='" & _Programacion & "', Fecha='" & _Fecha & "' WHERE Empleado_Ejecucion = '" & _Programacion & "'"
                Query = "DELETE FROM Empleado_Ejecucion WHERE Programacion = '" & _Programacion & "';"
                If gridOrden.Rows.Count = 0 Then
                    Query2 = "INSERT INTO Empleado_Ejecucion (Orden, Servicio, Tipo, Programacion, Fecha, Nota) VALUES ('', '" & _Servicio & "', '" & Tipo & "', '" & Programacion & "', '" & _Fecha & "', '" & Nota & "');"
                End If
                For i As Integer = 0 To gridOrden.Rows.Count - 1
                    Orden = gridOrden.Item("Nro", i).Value
                    Query2 = Query2 & "INSERT INTO Empleado_Ejecucion (Orden, Servicio, Tipo, Programacion, Fecha, Nota) VALUES ('" & Orden & "', '" & _Servicio & "', '" & Tipo & "', '" & Programacion & " ', '" & _Fecha & "', '" & Nota & "');"
                Next
                Query = Query + Query2
                Mensaje_de_error = "El registro seleccionado no pudo ser editado"
            Else
                If gridOrden.Rows.Count = 0 Then
                    Query2 = "INSERT INTO Empleado_Ejecucion (Orden, Servicio, Tipo, Programacion, Fecha, Nota) VALUES ('', '" & _Servicio & "', '" & Tipo & "', '" & Programacion & "', '" & _Fecha & "', '" & Nota & "');"
                End If
                For i As Integer = 0 To gridOrden.Rows.Count - 1
                    Orden = gridOrden.Item("Nro", i).Value
                    Query2 = Query2 & "INSERT INTO Empleado_Ejecucion (Orden, Servicio, Tipo, Programacion, Fecha, Nota) VALUES ('" & Orden & "', '" & _Servicio & "', '" & Tipo & "', '" & Programacion & " ', '" & _Fecha & "', '" & Nota & "');"
                Next
                Query = Query2
                Mensaje_de_error = "El registro no pudo añadirse"
            End If
            Dim Cmd As New SqlCommand(Query, Conexion)
            Try
                Cmd.ExecuteNonQuery()
                Me.Close()
            Catch ex As Exception
                MsgBox(Mensaje_de_error)
                MessageBox.Show(ex.Message.ToString, ex.Source.ToString)
            Finally
                Conexion.Close()
            End Try
        Else
            MsgBox("Debe seleccionar un tipo de ejecución")
        End If
    End Sub
    Private Sub txtBuscarOrden_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscarOrden.TextChanged
        Cargar_orden()
    End Sub
End Class