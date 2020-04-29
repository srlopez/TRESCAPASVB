Public Class FormUsuarios
    Private Sub FormUsuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gridUsuarios.DataSource = UseCase.Usuarios.getAll()
    End Sub
End Class
