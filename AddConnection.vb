Public Class AddConnection

    Public Sub AddCn()
        ' Obtener el valor de la columna1 de la fila superior si existe
        Dim horaPauta As TimeSpan = DateTimePicker1.Value.TimeOfDay
        Dim duration As TimeSpan = DateTimePicker2.Value.TimeOfDay - DateTimePicker1.Value.TimeOfDay

        ' Calcular la hora de inicio para el siguiente archivo
        Dim inicioSiguiente As TimeSpan = horaPauta + duration

        Dim finicio As Date = DateTimePicker3.Value.ToString("dd/MM/yyyy")

        Main.CREARDIA(finicio)
        ' Agregar una nueva fila al DataGridView con los detalles del archivo de audio
        Dim newRow As DataGridViewRow = Main.DataGridViewEvents.Rows(Main.DataGridViewEvents.Rows.Add())

        newRow.Cells("Column1").Value = DateTimePicker3.Value.ToString("dd/MM/yyyy")
        'el valor de la columna1 de la fila superior que encuentees que en la columna3 sea "DAY"
        newRow.Cells("Column2").Value = DateTimePicker1.Value.ToString("HH\:mm\:ss") ' Hora
        newRow.Cells("Column3").Value = "CNP" ' Tipo
        newRow.Cells("Column4").Value = "Line In Enabled" ' Descripción
        newRow.Cells("Column5").Value = duration.ToString("hh\:mm\:ss") ' Duración
        newRow.Cells("Column6").Value =
            Main.ObtenerFinal(DateTimePicker1.Value.TimeOfDay, duration).ToString("hh\:mm\:ss") ' Ruta
        newRow.Cells("Column7").Value = "" ' Ruta

        ' Actualizar la hora actual para el siguiente archivo
        Dim nuevoValorDateTimePicker As DateTime = DateTimePicker1.Value.Date.Add(inicioSiguiente)
        DateTimePicker1.Value = nuevoValorDateTimePicker
        Main.SaveGuideline()
        Close()
    End Sub

    Private Sub AddConnection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configurar DateTimePicker1 al día de hoy a las 00:00:00
        DateTimePicker1.Value = DateTime.Today

        ' Configurar DateTimePicker2 al día de hoy a las 00:00:00
        DateTimePicker2.Value = DateTime.Today
    End Sub

    Private Sub btAdd_Click(sender As Object, e As EventArgs) Handles btAdd.Click
        ' Comprobar que el valor de DateTimePicker2 sea mayor que DateTimePicker1
        If DateTimePicker2.Value > DateTimePicker1.Value Then
            ' El valor de DateTimePicker2 es mayor, puedes proceder con la acción que desees
            AddCn()
            Main.NextGuideEvent = Nothing
            Main.SaveGuideline()
            Main.LoadGuideLine()
        Else
            ' El valor de DateTimePicker2 no es mayor, mostrar un mensaje de error
            MessageBox.Show("End time must be greater than start time")
            DateTimePicker2.Focus()
        End If
    End Sub

End Class