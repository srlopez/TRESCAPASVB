
Imports System.Data
Imports Microsoft.Data.Sqlite
Imports System.Data.OleDb

#Const SQLite = 0


Namespace DAM
    Public Module DB
#If SQLite = 1 Then
        Public Function getConnection() As SqliteConnection
            'Console.WriteLine(Directory.GetCurrentDirectory())

            Dim connString = "Data Source=..\..\..\..\database.sq3"
            getConnection = New SqliteConnection(connString)
        End Function
#Else
        Public Function getConnection() As OleDbConnection
            Dim connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\..\..\..\database.accdb;Persist Security Info=False"
            getConnection = New OleDbConnection(connString)
        End Function
#End If

    End Module

    Public Module Usuarios
        Public Function getAll() As DataTable
            Dim query = "SELECT * FROM USUARIOS"
            Dim conn = DB.getConnection()
            conn.Open()
#If SQLite = 1 Then
            Dim sqlCommand = New SqliteCommand(query, conn)
#Else
            Dim sqlCommand = New OleDbCommand(query, conn)
#End If
            Dim table = New DataTable()
            Dim executeReader = sqlCommand.ExecuteReader()
            table.Load(executeReader)
            sqlCommand.Dispose()
            conn.Close()
            Return table
        End Function

#If SQLlite = 1 Then
        Public Function insert(u As Modelos.Usuario) As Integer
            Dim id = -1
            Dim conn = DB.getConnection()
            conn.Open()
            Try
                Dim cmd = conn.CreateCommand
                cmd.CommandText = "INSERT INTO USUARIOS 
                 (NOMBRE, EMAIL, NOTAS, FNAC, MAP) VALUES 
                 (@nombre, @email, @notas, @fnac, @map);
                 SELECT last_insert_rowid();"
                cmd.Parameters.AddWithValue("@nombre", u.nombre)
                cmd.Parameters.AddWithValue("@email", u.email)
                cmd.Parameters.AddWithValue("@notas", u.notas)
                cmd.Parameters.AddWithValue("@fnac", Format(u.fnac, "ddMMyyyy"))
                cmd.Parameters.AddWithValue("@map", u.map)
                'cmd.ExecuteNonQuery()
                id = cmd.ExecuteScalar()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
            Return id
        End Function
#Else
        Public Function insert(u As Modelos.Usuario) As Integer
            Dim id = -1
            Dim conn = DB.getConnection()
            conn.Open()
            Try
                Dim cmd = conn.CreateCommand
                cmd.CommandText = "INSERT INTO USUARIOS 
                 (NOMBRE, EMAIL, NOTAS, FNAC, MAP) VALUES 
                 (@nombre, @email, @notas, @fnac, @map)"
                cmd.Parameters.AddWithValue("@nombre", u.nombre)
                cmd.Parameters.AddWithValue("@email", u.email)
                cmd.Parameters.AddWithValue("@notas", u.notas)
                cmd.Parameters.AddWithValue("@fnac", Format(u.fnac, "ddMMyyyy"))
                cmd.Parameters.AddWithValue("@map", u.map)
                cmd.ExecuteNonQuery()
                cmd.CommandText = "SELECT @@Identity"
                id = cmd.ExecuteScalar()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
            Return id
        End Function

#End If

        Public Function update(u As Modelos.Usuario) As Integer
            Dim ret As Integer = -1
            Dim conn = DB.getConnection()
            conn.Open()
            Try
                Dim cmd = conn.CreateCommand
                cmd.CommandText = "UPDATE USUARIOS SET
                    NOMBRE = @nombre,
                    EMAIL = @email,
                    NOTAS = @notas, 
                    FNAC = @fnac,
                    MAP = @map 
                WHERE ID = @id;"
                cmd.Parameters.AddWithValue("@nombre", u.nombre)
                cmd.Parameters.AddWithValue("@email", u.email)
                cmd.Parameters.AddWithValue("@notas", u.notas)
                cmd.Parameters.AddWithValue("@fnac", Format(u.fnac, "ddMMyyyy"))
                cmd.Parameters.AddWithValue("@map", u.map)
                cmd.Parameters.AddWithValue("@id", u.id)
                ret = cmd.ExecuteNonQuery()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
            Return ret
        End Function

        Public Function delete(u As Modelos.Usuario) As Integer
            Dim ret As Integer = -1
            Dim conn = DB.getConnection()
            conn.Open()
            Try
                Dim cmd = conn.CreateCommand
                cmd.CommandText = "DELETE FROM USUARIOS
                WHERE ID = @id;"
                cmd.Parameters.AddWithValue("@id", u.id)
                ret = cmd.ExecuteNonQuery()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
            Return ret
        End Function
    End Module



End Namespace

