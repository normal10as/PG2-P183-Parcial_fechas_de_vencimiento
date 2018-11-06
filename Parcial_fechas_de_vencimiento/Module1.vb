Module Module1
    Enum Dias As Byte
        Lunes = 1
        Martes
        Miércoles
        Jueves
        Viernes
    End Enum

    Sub Main()
        'Console.WriteLine(ObtenerPrimerFecha)
        'Console.WriteLine(IngresarCantidadMeses())
        Dim primerFecha As Date = IngresarPrimerFecha()
        Dim cantidadMeses As Byte = IngresarCantidadMeses()
        Dim fechas As New SortedList(Of Byte, Date)
        fechas = GetsFechas(primerFecha, cantidadMeses)
        editarFechas(fechas)
    End Sub

    Sub editarFechas(fechas As SortedList(Of Byte, Date))
        Dim clave As Byte
        Do
            mostrarFechas(fechas)
            Console.Write("Ingrese el orden de una fecha para modificar (0 para terminar)")
            clave = Console.ReadLine()
            If clave > 0 Then
                If fechas.ContainsKey(clave) Then
                    MoverFecha(fechas, clave)
                Else
                    Console.WriteLine("Orden no existe")
                End If
            End If
        Loop Until clave = 0
    End Sub

    Private Sub MoverFecha(ByRef fechas As SortedList(Of Byte, Date), clave As Byte)
        Dim fecha As Date
        fecha = fechas.Item(clave)
        fecha = fecha.AddDays(1)
        fecha = SaltearFinDeSemana(fecha)
        fechas(clave) = fecha
    End Sub

    Function GetsFechas(primerFecha As Date, cantidadMeses As Byte) As SortedList(Of Byte, Date)
        Dim fechas As New SortedList(Of Byte, Date)
        Dim fecha As Date
        For x = 1 To cantidadMeses
            fecha = primerFecha.AddMonths(x - 1)
            fecha = SaltearFinDeSemana(fecha)
            fechas.Add(x, fecha)
        Next
        Return fechas
    End Function

    Sub mostrarFechas(fechas As SortedList(Of Byte, Date))
        Dim diaSemana As Dias
        Dim fecha As Date
        For Each parValorFecha In fechas
            fecha = parValorFecha.Value
            diaSemana = fecha.DayOfWeek
            Console.WriteLine("{0} - {1} {2}", parValorFecha.Key, diaSemana, fecha)
        Next
    End Sub

    Private Function SaltearFinDeSemana(fecha As Date) As Date
        While fecha.DayOfWeek = 0 Or fecha.DayOfWeek = 6
            fecha = fecha.AddDays(1)
        End While
        Return fecha
    End Function

    Function IngresarCantidadMeses() As Byte
        Dim meses As Byte
        Do
            Console.Write("Ingrese la cantidad de meses: ")
            meses = Console.ReadLine()
        Loop Until ValidarCantidadMeses(meses)
        Return meses
    End Function

    Function ValidarCantidadMeses(meses As Byte) As Boolean
        If meses < 3 Or meses > 24 Then
            Console.WriteLine("El número de meses debe ser entre 3 y 24")
            Return False
        Else
            Return True
        End If
    End Function

    Function IngresarPrimerFecha() As Date
        Dim primerFecha As Date
        primerFecha = New Date(GetProximoMes.Year, GetProximoMes.Month, IngresarDiaFecha())
        Return primerFecha
    End Function

    Function IngresarDiaFecha() As UShort
        Dim dia As UShort
        Do
            Console.Write("Ingrese un día de vencimiento: ")
            dia = Console.ReadLine()
        Loop Until ValidarDiaFecha(dia)
        Return dia
    End Function

    Function ValidarDiaFecha(dia As UShort) As Boolean
        Dim diasProximoMes As UShort = DateTime.DaysInMonth(GetProximoMes.Year, GetProximoMes.Month)
        If dia < 1 Or dia > diasProximoMes Then
            Console.WriteLine("El día de la fecha tiene que ser entre 1 y " & diasProximoMes)
            Return False
        End If
        Return True
    End Function

    Function GetProximoMes() As Date
        Return Now.AddMonths(1)
    End Function
End Module
