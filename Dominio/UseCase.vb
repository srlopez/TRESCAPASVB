
Imports DataAccess
Public Module UseCase
    Public Function CrearUsuario(u As Modelos.Usuario) As Integer
        Return DAM.Usuarios.insert(u)
    End Function
End Module

