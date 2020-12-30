Imports System.Data.SqlClient
Module Configuración
    'Variables para la conexión a la BD
    Public IP As String = "10.0.0.2"
    Public Usuario As String
    Public Contraseña As String
    Public Database As String = "DIARIO"
    'Variables para llamar a los formularios
    Public _Actividad As Integer

    Public _Programacion As Integer
    Public _Fecha As Date
    Public _Servicio As Integer
    'Función para ejecutar consultas SELECT
    'Entradas: Query(string), Objeto Conexion(SqlConnection)
    'Salida: Un dataTable con los datos de la consulta
    Public Function Consulta_select(ByVal _Query As String, ByVal _Conexion As SqlConnection) As DataTable
        Dim _Cmd As New SqlCommand(_Query, _Conexion)
        Dim _da As New SqlDataAdapter(_Cmd)
        Dim _dt As New DataTable
        _da.Fill(_dt)
        Consulta_select = _dt
        Return Consulta_select
    End Function
End Module
