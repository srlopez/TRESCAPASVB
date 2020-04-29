Imports System.Data
Imports DataAccess
Imports Dominio

Module Program
    Sub Main(args As String())
        Randomize()

        'INSERT
        Dim u = New Modelos.Usuario()
        u.nombre = "Us#" + CStr(CInt(1000 * Rnd()))
        u.notas = "notas"
        u.email = "email"
        u.map = CInt(10000 * Rnd()) \ 100
        u.fnac = Today
        'u.id = DAM.Usuarios.insert(u)
        u.id = UseCase.CrearUsuario(u)
        Console.WriteLine($"ID insertado {u.id}")

        'UPDATE
        u.nombre = "CAMBIO#" + CStr(CInt(1000 * Rnd()))
        Console.WriteLine($"#actualizados {DAM.Usuarios.update(u)}")

        'SELECT
        Dim tab = DAM.Usuarios.getAll()
        For Each row In tab.Rows
            Console.WriteLine($"{row("ID")}: {row("NOMBRE")} {row("MAP")}")
        Next

        'DELETE
        Console.WriteLine($"#actualizados {DAM.Usuarios.delete(u)}")

        'SELECT
        For Each row In DAM.Usuarios.getAll().Rows
            Console.WriteLine($"{row("ID")}: {row("NOMBRE")} {row("MAP")}")
        Next

    End Sub
End Module
