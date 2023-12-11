Imports System.Globalization
Imports System.IO
Imports NAudio.Wave

Public Class Main
    Public Shared NextGuideEvent As DataGridViewRow = Nothing
    Private ReadOnly currentMilliseconds As Integer = DateTime.Now.Millisecond
    Private ReadOnly _audioCapture As WaveInEvent
    Private _isCapturing As Boolean = False

    Private targetTime As DateTime

    Public Sub Adlog(t As String)
        ' Verificar si DataGridView3 tiene al menos una fila
        If DataGridViewLog.Rows.Count > 0 Then
            ' Obtener el contenido de la última celda de la última fila
            Dim lastRowText As String = DataGridViewLog.Rows(DataGridViewLog.Rows.Count - 1).Cells(0).Value.ToString()

            ' Verificar si t es igual al contenido de la última celda
            If "[" & DateTime.Now.ToString("HH:mm:ss") & "] " & t = lastRowText Then
                Return ' No añadir la entrada si es igual a la última línea
            End If
        End If

        ' Resto del código para agregar la entrada al DataGridView y al archivo de registro
        If DataGridViewLog.Columns.Count = 0 Then
            DataGridViewLog.Columns.AddRange(
                New DataGridViewColumn() {New DataGridViewTextBoxColumn With {.Name = "Log", .HeaderText = "Log"}})
        End If

        Dim newRow = New DataGridViewRow()
        newRow.CreateCells(DataGridViewLog,
                           "[" & DateTime.Now.ToString("dd/MM/yyyy") & "][" & DateTime.Now.ToString("HH:mm:ss") & "] " &
                           t)
        DataGridViewLog.Rows.Add(newRow)

        ' Seleccionar la celda de la nueva fila agregada
        Dim lastRowIndex As Integer = DataGridViewLog.Rows.Count - 1
        DataGridViewLog.CurrentCell = DataGridViewLog.Rows(lastRowIndex).Cells(0)

        ' Mover el scroll para mostrar la celda seleccionada
        If lastRowIndex >= 0 AndAlso lastRowIndex < DataGridViewLog.Rows.Count Then
            DataGridViewLog.FirstDisplayedScrollingRowIndex = lastRowIndex
        End If

        ' Obtener la fecha actual en el formato "ddMMyy"
        Dim currentDate As String = DateTime.Now.ToString("ddMMyy")

        ' Construir la ruta completa del archivo de log con la fecha actual
        Dim logFilePath As String = Directory.GetCurrentDirectory() & "/log.txt"
        ' Verificar si el archivo existe y crearlo si no existe
        If Not File.Exists(logFilePath) Then
            File.Create(logFilePath).Close()
        End If

        ' Guardar la línea en el archivo de texto
        Using writer As New StreamWriter(logFilePath, True)
            writer.WriteLine(
                "[" & DateTime.Now.ToString("dd/MM/yyyy") & "][" & DateTime.Now.ToString("HH:mm:ss") & "] " & t)
        End Using

        NotSortDataGridViews()
    End Sub

    Public Sub CleanRows()
        ' Obtiene la fecha y hora actual
        Dim currentDate As DateTime = DateTime.Now

        ' Recorre las filas del DataGridView1 de arriba a abajo
        For i As Integer = DataGridViewEvents.Rows.Count - 1 To 0 Step -1
            Dim row As DataGridViewRow = DataGridViewEvents.Rows(i)

            ' Obtiene el tipo de la fila (columna 2)
            Dim tipo As String = row.Cells(2).Value.ToString()

            If tipo = "DAY" Then
                Dim fecha As DateTime
                If DateTime.TryParse(row.Cells(0).Value.ToString(), fecha) AndAlso fecha.Date < currentDate.Date Then
                    ' Elimina la fila si el tipo es "DAY" y el día es anterior al día actual
                    DataGridViewEvents.Rows.RemoveAt(i)
                End If
            Else
                ' Obtiene la fecha y hora de la fila (columna 0 y 1)
                Dim fechaHora As DateTime
                Dim fechaHoraString As String = row.Cells(0).Value.ToString() & " " & row.Cells(1).Value.ToString()
                If DateTime.TryParse(fechaHoraString, fechaHora) AndAlso fechaHora < currentDate Then
                    ' Elimina la fila si la fecha y hora son anteriores a la actual
                    DataGridViewEvents.Rows.RemoveAt(i)
                End If
            End If
        Next
    End Sub

    Private IsEnabledLineIn As Boolean = False

    Public Sub CloseLineIn()
        If IsEnabledLineIn = True Then
            FnLineIn.DisableLineIn()
            Adlog("Line In Disabled")
            IsEnabledLineIn = False
        End If
    End Sub

    Public Sub CREARDIA(finicio As Date)
        For Each row As DataGridViewRow In DataGridViewEvents.Rows
            Dim existeFila = False
            ' Verificar si existe una fila con el valor de "finicio"
            For Each existingRow As DataGridViewRow In DataGridViewEvents.Rows
                If existingRow.Cells("Column1").Value.ToString() = finicio Then
                    existeFila = True
                    Exit For ' Se encontró una fila con el mismo valor, no es necesario seguir buscando.
                End If
            Next
            ' Si no existe la fila, entonces la creamos.
            If Not existeFila Then
                Dim newRow As DataGridViewRow = DataGridViewEvents.Rows(DataGridViewEvents.Rows.Add())
                newRow.Cells(0).Value = finicio.ToString("dd/MM/yyyy")
                newRow.Cells(1).Value = "00:00"
                newRow.Cells(2).Value = "DAY"
                newRow.Cells(3).Value = finicio.ToString("dddd, d MMMM yyyy")
                newRow.Cells(4).Value = "00:00"
                newRow.Cells(5).Value = "00:00"
                newRow.Cells(6).Value = finicio.ToString("dd/MM/yyyy")
            End If
        Next
    End Sub

    ' Declaración de la variable

    '    Return False
    'End Function

    Public Sub LoadGuideLine()
        DataGridViewEvents.Rows.Clear()

        Dim filePath As String = Directory.GetCurrentDirectory() & "\guideline.txt"

        ' Verificar si el archivo existe, de lo contrario, crearlo
        If Not File.Exists(filePath) Then
            File.Create(filePath).Dispose()
        End If

        ' Crear la estructura del DataGridView si es necesario
        If DataGridViewEvents.Columns.Count = 0 Then
            DataGridViewEvents.Columns.AddRange(
                New DataGridViewColumn() {
                                             New DataGridViewTextBoxColumn _
                                              With {.Name = "Column1", .HeaderText = "Finicio", .Visible = False},
                                             New DataGridViewTextBoxColumn _
                                              With {.Name = "Column2", .HeaderText = "Init"},
                                             New DataGridViewTextBoxColumn _
                                              With {.Name = "Column3", .HeaderText = "Type"},
                                             New DataGridViewTextBoxColumn _
                                              With {.Name = "Column4", .HeaderText = "Description"},
                                             New DataGridViewTextBoxColumn _
                                              With {.Name = "Column5", .HeaderText = "Duration"},
                                             New DataGridViewTextBoxColumn With {.Name = "Column6", .HeaderText = "End"},
                                             New DataGridViewTextBoxColumn _
                                              With {.Name = "Column7", .HeaderText = "Route", .Visible = False}
                                         }
                )

            For Each column As DataGridViewColumn In DataGridViewEvents.Columns
                column.SortMode = DataGridViewColumnSortMode.NotSortable

                Select Case column.Name
                    Case "Column2"
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        column.Width = 61 ' Establece el ancho de la columna
                    Case "Column3"
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        column.Width = 41 ' Establece el ancho de la columna
                    Case "Column4"
                        column.Width = 301 ' Establece el ancho de la columna
                    Case "Column5"
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        column.Width = 59 ' Establece el ancho de la columna
                    Case "Column6"
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        column.Width = 59 ' Establece el ancho de la columna
                End Select
            Next
        Else
            DataGridViewEvents.Rows.Clear()
        End If

        ' Leer todas las líneas del archivo
        Dim lines As String() = File.ReadAllLines(filePath)

        ' Verificar si el archivo está vacío
        'If lines.Length = 0 Then
        Dim nuevaLinea As String =
                $"{Date.Now:dd/MM/yyyy}|00:00|DAY|{DateTime.Now:dddd, d MMMM yyyy}|00:00|00:00|{Date.Now:dd/MM/yyyy}"
        If Not LineaExisteEnArchivo(filePath, nuevaLinea) Then
            File.AppendAllText(filePath, nuevaLinea & Environment.NewLine)
            lines = File.ReadAllLines(filePath)
        End If
        'End If

        ' Recorrer cada línea y agregarla al DataGridView
        For Each line As String In lines
            ' Dividir la línea en las partes separadas por el carácter '|'
            Dim parts As String() = line.Split("|"c)

            ' Verificar si hay suficientes elementos en la línea
            If parts.Length >= 7 Then
                If _
                    parts(2) = "DAY" AndAlso
                    DateTime.TryParseExact(parts(6), {"dd/MM/yyyy H:mm:ss", "dd/MM/yyyy"}, CultureInfo.InvariantCulture,
                                           DateTimeStyles.None, Nothing) Then
                    Dim fecha As DateTime = DateTime.ParseExact(parts(6), {"dd/MM/yyyy H:mm:ss", "dd/MM/yyyy"},
                                                                CultureInfo.InvariantCulture, DateTimeStyles.None)
                    Dim finicio As String = fecha.ToString("dd/MM/yyyy") ' Finicio
                    Dim inicio = "00:00" ' Inicio
                    Dim tipo = "DAY" ' Tipo
                    Dim descripcion As String = fecha.ToString("dddd, d MMMM yyyy") ' Descripción
                    Dim duracion = "00:00" ' Duración
                    Dim fin = "00:00" ' Fin
                    Dim ruta As String = fecha.ToString("dd/MM/yyyy") ' Ruta
                    'evitamos poner guias de días pasados
                    If fecha >= DateTime.Now.Date Then
                        DataGridViewEvents.Rows.Add(finicio, inicio, tipo, descripcion, duracion, fin, ruta)
                    Else
                    End If
                Else
                    DataGridViewEvents.Rows.Add(parts(0), parts(1), parts(2), parts(3), parts(4), parts(5), parts(6))
                End If
            End If
        Next
        NotSortDataGridViews()

        DataGridViewEvents.Refresh()
    End Sub

    Public Sub MainLoad()
        DataGridViewEvents.AllowUserToOrderColumns = False
        DataGridViewEvents.ColumnHeadersDefaultCellStyle.SelectionBackColor = DataGridViewEvents.DefaultCellStyle.BackColor
        DataGridViewEvents.ColumnHeadersDefaultCellStyle.SelectionForeColor = DataGridViewEvents.DefaultCellStyle.ForeColor
        DataGridViewEvents.EnableHeadersVisualStyles = False

        DataGridViewLog.AllowUserToOrderColumns = False
        DataGridViewLog.ColumnHeadersDefaultCellStyle.SelectionBackColor = DataGridViewEvents.DefaultCellStyle.BackColor
        DataGridViewLog.ColumnHeadersDefaultCellStyle.SelectionForeColor = DataGridViewEvents.DefaultCellStyle.ForeColor
        DataGridViewLog.EnableHeadersVisualStyles = False

        For Each columna As DataGridViewColumn In DataGridViewEvents.Columns
            columna.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        For Each columna As DataGridViewColumn In DataGridViewLog.Columns
            columna.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        lbclock.Text = DateTime.Now.ToString("HH:mm:ss")
        lbdate.Text = DateTime.Now.ToString("ddd, dd/MM/yyyy").ToLower()
        MainClock.Start()
        'miro sí existe la pauta del día, sino la creo
        LoadGuideLine()
        CleanRows()
        BringToFront()
    End Sub

    Public Sub NotSortDataGridViews()
        For Each columna As DataGridViewColumn In DataGridViewEvents.Columns
            columna.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        For Each columna As DataGridViewColumn In DataGridViewLog.Columns
            columna.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
    End Sub

    Public Function ObtenerFinal(horaInicio As TimeSpan, duracion As TimeSpan) As TimeSpan
        Return horaInicio.Add(duracion)
    End Function

    Public Sub OpenLineIn()
        FnLineIn.EnableLineIn()
        Adlog("Line In Enabled")
        lbLog.Text = "Line In Enabled"
    End Sub

    Public Sub PlayGuideLine(row As DataGridViewRow)
        Dim tipo = row.Cells(2).Value.ToString()
        If tipo = "CNF" Or tipo = "CNP" Then
            OpenLineIn()
            Dim duration As String = row.Cells(4).Value.ToString()
            ' Supongamos que la duración está en el formato HH:mm:ss
            Dim durationParts As String() = duration.Split(":"c)
            Dim hours As Integer = Integer.Parse(durationParts(0))
            Dim minutes As Integer = Integer.Parse(durationParts(1))
            Dim seconds As Integer = Integer.Parse(durationParts(2))
            ' Crear un objeto DateTime con la duración especificada
            targetTime = DateTime.Now.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds)
            TimerConnection.Enabled = True
            TimerConnection.Start()
            Exit Sub
        End If
    End Sub

    Public Sub SaveGuideline()
        ' Obtener la ruta del archivo de texto donde se guardará la información
        Dim filePath As String = Directory.GetCurrentDirectory() & "\guideline.txt"

        ' Crear una lista para almacenar las líneas actualizadas
        Dim updatedLines As New List(Of String)

        ' Recorrer todas las filas del DataGridView y generar las líneas actualizadas
        Dim rowsList As List(Of DataGridViewRow) = DataGridViewEvents.Rows.Cast(Of DataGridViewRow)().ToList()

        ' Ordenar las filas por fecha y hora
        rowsList.Sort(Function(x, y)
                          Dim dateX As Date = Date.ParseExact(x.Cells("Column1").Value.ToString(), "dd/MM/yyyy",
                                                CultureInfo.InvariantCulture)
                          Dim dateY As Date = Date.ParseExact(y.Cells("Column1").Value.ToString(), "dd/MM/yyyy",
                                                CultureInfo.InvariantCulture)

                          Dim timeX As TimeSpan = TimeSpan.Parse(x.Cells("Column2").Value.ToString())
                          Dim timeY As TimeSpan = TimeSpan.Parse(y.Cells("Column2").Value.ToString())

                          Dim result As Integer = dateX.CompareTo(dateY)
                          If result = 0 Then
                              result = timeX.CompareTo(timeY)
                          End If

                          Return result
                      End Function)

        For Each row As DataGridViewRow In rowsList
            Dim finicio As String = row.Cells("Column1").Value.ToString()
            Dim inicio As String = row.Cells("Column2").Value.ToString()
            Dim tipo As String = row.Cells("Column3").Value.ToString()
            Dim descripcion As String = row.Cells("Column4").Value.ToString()
            Dim duracion As String = row.Cells("Column5").Value.ToString()
            Dim fin As String = row.Cells("Column6").Value.ToString()

            If tipo = "CNF" Or tipo = "CNP" Then
                Dim inicioTime As TimeSpan = TimeSpan.Parse(inicio) ' Convert start time to TimeSpan
                Dim duracionTime As TimeSpan = TimeSpan.Parse(duracion) ' Convert duration to TimeSpan
                Dim finTime As TimeSpan = inicioTime + duracionTime ' Calculate end time
                ' Format the calculated end time as a string in the desired format
                fin = finTime.ToString("hh\:mm") ' Adjust the format as needed
            End If

            'Dim fin As String = row.Cells("Column6").Value.ToString()
            Dim ruta As String = String.Empty
            If row.Cells("Column7").Value IsNot Nothing Then
                ruta = row.Cells("Column7").Value.ToString()
            End If

            Dim fecha As String

            ' Verificar si el tipo es "DIA"
            If tipo = "DAY" Then
                ' Obtener la fecha actual en el formato "dd/MM/yyyy"
                fecha = row.Cells("Column1").Value.ToString()
                Dim fliteral As String = CDate(fecha).ToString("dddd, d MMMM yyyy")
                ' Generar la línea en el formato especificado y agregarla a la lista
                Dim line As String = $"{ruta}|{inicio}|{tipo}|{fliteral}|{duracion}|{fin}|{ruta}"
                updatedLines.Add(line)
            Else
                ' Generar la línea normalmente y agregarla a la lista
                Dim line As String = $"{finicio}|{inicio}|{tipo}|{descripcion}|{duracion}|{fin}|{ruta}"
                updatedLines.Add(line)
            End If
        Next

        ' Guardar las líneas actualizadas en el archivo de texto
        File.WriteAllLines(filePath, updatedLines)
        NextGuideEvent = Nothing
    End Sub

    Private Sub btAddConnection_Click(sender As Object, e As EventArgs) Handles btAddConnection.Click
        Dim frm As New AddConnection With {
                .StartPosition = FormStartPosition.CenterParent
                }
        frm.ShowDialog(Me)
    End Sub

    Private Sub btSaveGuideline_Click(sender As Object, e As EventArgs) Handles btSaveGuideline.Click
        NextGuideEvent = Nothing
        SaveGuideline()
        LoadGuideLine()
    End Sub

    Private Sub Button6_Click_3(sender As Object, e As EventArgs) Handles Button6.Click
        FnLineIn.EnableLineIn()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        FnLineIn.DisableLineIn()
    End Sub

    Private Sub ConfigurarEntradaDeAudioToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim frm As New ConfigLineIn With {
                .StartPosition = FormStartPosition.CenterParent
                }
        frm.ShowDialog(Me)
    End Sub

    Private Function ConvertirTiempoAMilisegundos(tiempo As String) As Long
        Dim partes As String() = tiempo.Split(":"c)
        If partes.Length = 2 Then
            Dim minutos As Integer
            Dim segundos As Integer
            If Integer.TryParse(partes(0), minutos) AndAlso Integer.TryParse(partes(1), segundos) Then
                ' Calcular el tiempo total en milisegundos
                Dim totalMilisegundos As Long = (minutos * 60 + segundos) * 1000
                Return totalMilisegundos
            End If
        End If

        ' En caso de un formato incorrecto o valores no numéricos, devuelve 0
        Return 0
    End Function

    Private Sub DataGridView1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) _
        Handles DataGridViewEvents.CellBeginEdit
        ' Obtener el valor de la columna "Tipo" de la fila actual
        Dim tipo As String = DataGridViewEvents.Rows(e.RowIndex).Cells("Column2").Value.ToString()
        ' Verificar si el tipo es "DIA" y cancelar la edición
        If tipo = "DAY" Then
            e.Cancel = True
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) _
        Handles DataGridViewEvents.CellFormatting
        Try
            ' Verificar si la fila no está en modo de edición
            If e.RowIndex >= 0 AndAlso Not DataGridViewEvents.Rows(e.RowIndex).IsNewRow Then
                ' Obtener el valor de la columna "Tipo" de la fila actual
                Dim tipo As String = DataGridViewEvents.Rows(e.RowIndex).Cells("Column3").Value.ToString()

                ' Verificar si el tipo es "DIA" y aplicar el formato de negrita y fondo gris a toda la fila
                If tipo = "DAY" Then
                    DataGridViewEvents.Rows(e.RowIndex).DefaultCellStyle.Font = New Font(DataGridViewEvents.DefaultCellStyle.Font,
                                                                                    FontStyle.Bold)
                    DataGridViewEvents.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGray
                Else
                    DataGridViewEvents.Rows(e.RowIndex).DefaultCellStyle.Font = New Font(DataGridViewEvents.DefaultCellStyle.Font,
                                                                                    FontStyle.Regular)
                    DataGridViewEvents.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub DataGridView1_DragDrop(sender As Object, e As DragEventArgs) Handles DataGridViewEvents.DragDrop
        Exit Sub
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridViewEvents.KeyDown
        If e.KeyCode = Keys.Delete AndAlso DataGridViewEvents.SelectedRows.Count > 0 Then
            Dim rowIndex As Integer = DataGridViewEvents.SelectedRows(0).Index
            Dim column3Value As String = DataGridViewEvents.Rows(rowIndex).Cells("Column3").Value.ToString()
            Dim column7Value As String = DataGridViewEvents.Rows(rowIndex).Cells("Column7").Value.ToString()

            If column3Value = "DAY" Then
                If _
                    MessageBox.Show("Delete day: " & column7Value & "?", "Confirmation", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) = DialogResult.Yes Then
                    RemoveLinesBetweenDates(column7Value)
                    SaveGuideline()
                    LoadGuideLine()
                End If
            Else
                DataGridViewEvents.Rows.RemoveAt(rowIndex)
                DataGridViewEvents.Refresh()
            End If

            e.Handled = True ' Evitar que se propague el evento a otros controles
        End If
    End Sub

    Private Sub DesactivarEntradaDeAudioToolStripMenuItem_Click(sender As Object, e As EventArgs)
        FnLineIn.DisableLineIn()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Verificar si el usuario está intentando cerrar el formulario
        If e.CloseReason = CloseReason.UserClosing Then
            ' Mostrar un cuadro de diálogo de confirmación
            Dim result As DialogResult = MessageBox.Show("¿Do you want to close the application?", "Confirm Closing",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            ' Si el usuario selecciona "No", cancelar el cierre del formulario
            If result = DialogResult.No Then
                e.Cancel = True
            Else
                Try
                    FnLineIn.DisableLineIn()
                Catch
                End Try
                Application.Exit()
            End If
        End If
    End Sub

    Private Sub GetNextRowInGuide()
        Dim currentDateTime As Date = Date.Now
        Dim closestTimeDiff As TimeSpan = TimeSpan.MaxValue
        Dim closestRow As DataGridViewRow = Nothing

        If DataGridViewEvents.Rows.Count > 1 Then ' Ignoring the first row (header row)

            For rowIndex = 1 To DataGridViewEvents.Rows.Count - 1 ' Start from index 1 (ignoring header row)
                Dim row As DataGridViewRow = DataGridViewEvents.Rows(rowIndex)
                Dim tipo As String = row.Cells("Column3").Value.ToString()

                If tipo <> "DAY" Then
                    ' Leer la fecha y hora de la fila actual y combinarla en un solo valor de fecha y hora
                    Dim dateStr As String = row.Cells("Column1").Value.ToString()
                    Dim timeStr As String = row.Cells("Column2").Value.ToString()
                    Dim dateTimeStr As String = dateStr & " " & timeStr
                    Dim targetDateTime As Date

                    If DateTime.TryParse(dateTimeStr, targetDateTime) Then
                        ' Calcular la diferencia de tiempo entre la fecha y hora actual y la fecha y hora de la fila
                        Dim timeDiff As TimeSpan = targetDateTime - currentDateTime

                        ' Si la diferencia de tiempo es positiva (la fecha y hora de la fila está en el futuro),
                        ' y es menor que la diferencia de tiempo más cercana hasta ahora, actualizamos la fila más cercana.
                        If timeDiff > TimeSpan.Zero AndAlso timeDiff < closestTimeDiff Then
                            closestTimeDiff = timeDiff
                            closestRow = row
                        End If
                    End If
                End If
            Next
        End If

        ' Asignar la fila más cercana encontrada a la variable NextGuideEvent
        NextGuideEvent = closestRow
    End Sub

    Private Function LineaExisteEnArchivo(filePath As String, linea As String) As Boolean
        Dim lines As String() = File.ReadAllLines(filePath)
        Return lines.Contains(linea)
    End Function

    Private Sub Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        PerformCleanup()
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MainClockVisual.Start()
        MainLoad()
    End Sub

    Private Sub MainClock_Tick(sender As Object, e As EventArgs) Handles MainClock.Tick
        If lbLog.Text = "Disconnection at 00:00" Then
            lbLog.Text = "LineInScheduler"
            FnLineIn.DisableLineIn()
        End If

        lbclock.Text = DateTime.Now.ToString("HH:mm:ss")
        'si el día es diferente....
        If lbdate.Text <> DateTime.Now.ToString("dddd, d MMMM yyyy").ToLower() Then
            CleanRows()
            SaveGuideline()
            LoadGuideLine()
            lbdate.Text = DateTime.Now.ToString("dddd, d MMMM yyyy").ToLower()
        End If

        'el siguiente evento da igual Flexible o no...
        If NextGuideEvent Is Nothing Then
            GetNextRowInGuide()
        End If

        If NextGuideEvent Is Nothing Then Exit Sub

        '''''''''''''''''''''''''''
        Dim cellValue As String = NextGuideEvent.Cells(1).Value.ToString()
        If Not String.IsNullOrEmpty(cellValue) AndAlso lbclock.Text = cellValue Then
            PlayGuideLine(NextGuideEvent)

        End If
        'End If
    End Sub

    Private Sub MainClockVisual_Tick(sender As Object, e As EventArgs) Handles MainClockVisual.Tick
        lbclock.Text = DateTime.Now.ToString("HH:mm:ss")
    End Sub

    Private Sub PerformCleanup()
        ' Realizar acciones de limpieza y liberación de recursos aquí
        StopCapture()
        If _audioCapture IsNot Nothing Then
            _audioCapture.Dispose()
        End If
    End Sub

    Private Sub RemoveLinesBetweenDates(startDate As String)
        Dim selectedRowIndex As Integer = DataGridViewEvents.SelectedCells(0).RowIndex

        ' Obtener el valor de la columna "Tipo" de la fila seleccionada
        Dim tipo As String = DataGridViewEvents.Rows(selectedRowIndex).Cells("Column3").Value.ToString()

        If tipo <> "DAY" Then
            Return
        End If

        ' Obtener la fecha actual en el formato "dd/MM/yyyy"
        Dim fechaActual As String = DateTime.Now.ToString("dd/MM/yyyy")

        If startDate = fechaActual Then
            ' La fila seleccionada es del día actual, mantenerla
            For i As Integer = selectedRowIndex + 1 To DataGridViewEvents.Rows.Count - 1
                If i >= DataGridViewEvents.Rows.Count Then
                    Exit For
                End If

                Dim row As DataGridViewRow = DataGridViewEvents.Rows(i)
                Dim rowTipo As String = row.Cells("Column3").Value.ToString()

                If rowTipo = "DAY" Then
                    Exit For
                End If

                DataGridViewEvents.Rows.RemoveAt(i)
                DataGridViewEvents.Refresh()
                i -= 1 ' Ajustar el índice para mantener el ciclo correctamente
            Next
        Else
            ' Eliminar la fila seleccionada y las siguientes hasta el próximo día o hasta que no haya más filas
            DataGridViewEvents.Rows.RemoveAt(selectedRowIndex)
            DataGridViewEvents.Refresh()

            For i As Integer = selectedRowIndex To DataGridViewEvents.Rows.Count - 1
                If i >= DataGridViewEvents.Rows.Count Then
                    Exit For
                End If

                Dim row As DataGridViewRow = DataGridViewEvents.Rows(i)
                Dim rowTipo As String = row.Cells("Column3").Value.ToString()

                If rowTipo = "DAY" Then
                    Exit For
                End If

                DataGridViewEvents.Rows.RemoveAt(i)
                DataGridViewEvents.Refresh()
                i -= 1 ' Ajustar el índice para mantener el ciclo correctamente
            Next
        End If
    End Sub

    Private Sub StopCapture()
        If _audioCapture IsNot Nothing AndAlso _isCapturing Then
            _audioCapture.StopRecording()
            _isCapturing = False
        End If
    End Sub

    ' Declarar una variable para hacer un seguimiento de la instancia del formulario Clock
    Private Sub TimerConnection_Tick(sender As Object, e As EventArgs) Handles TimerConnection.Tick
        Dim currentTime As DateTime = DateTime.Now
        If currentTime >= targetTime Then
            TimerConnection.Enabled = False
            CloseLineIn()
        Else
            Dim tiempoRestante As TimeSpan = targetTime - currentTime
            Dim minutos As Integer = tiempoRestante.Minutes
            Dim segundos As Integer = tiempoRestante.Seconds
            lbLog.Text = "Disconnection at " & minutos.ToString("D2") & ":" & segundos.ToString("D2")
        End If
    End Sub

    Private Sub EntradaDeAudioToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles EntradaDeAudioToolStripMenuItem.Click
        Dim frm As New ConfigLineIn With {
                .StartPosition = FormStartPosition.CenterParent
                }
        frm.ShowDialog(Me)
    End Sub

    Private Sub ControlPanelToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles ControlPanelToolStripMenuItem.Click
        Dim frm As New Form1 With {
                .StartPosition = FormStartPosition.CenterParent
                }
        frm.ShowDialog(Me)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        System.Diagnostics.Process.Start("https://github.com/churrufli/LineInScheduler")
    End Sub

End Class