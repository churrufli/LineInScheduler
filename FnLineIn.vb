﻿Imports System.Runtime.InteropServices
Imports NAudio.CoreAudioApi

Public Class FnLineIn

    ' Constantes necesarias para la API de Windows Multimedia
    Private Const SND_ASYNC As Integer = &H1
    Private Const WAVE_VOLUME_MIN As Integer = 0
    Private Const WAVE_VOLUME_MAX As Integer = &HFFFF
    Private Const WAVE_OUT_SET_VOLUME As Integer = &H8

    ' Importar la función waveOutSetVolume de la API de Windows Multimedia
    <DllImport("winmm.dll")>
    Private Shared Function WaveOutSetVolume(uDeviceID As Integer, dwVolume As Integer) As Integer
    End Function


    ' Función para ajustar el volumen de las aplicaciones que comienzan por "X"
    Private Shared Sub AjustarVolumen(volumen As Single)
        Dim enumerator As New MMDeviceEnumerator()
        Dim devices As MMDeviceCollection = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)
        If My.Settings.MuteProgram.ToString <> Nothing Then
            For Each device As MMDevice In devices
                If device.DeviceFriendlyName.StartsWith(My.Settings.MuteProgram, StringComparison.OrdinalIgnoreCase) Then
                    ' Establecer el volumen
                    device.AudioEndpointVolume.MasterVolumeLevelScalar = volumen
                    'Exit For ' Terminar después de encontrar la primera coincidencia
                End If
            Next
        End If

    End Sub

    Public Shared Sub EnableLineIn()
        ' EstablecerVolumenCero()
        ' Crear una instancia del Form2
        Dim form1 As New Form1()
        form1.Show()
        ' Recorrer el ListView1 en Form2
        For Each item As ListViewItem In form1.ListView1.Items
            ' Comprobar si el valor de la columna coincide con My.Settings.LineIn
            If item.SubItems(0).Text = My.Settings.LineIn Then
                ' Seleccionar el elemento
                item.Selected = True
                Exit For
            End If
        Next
        'vale, ahora está seleccionado
        'el boton dirá si está Enable que el dispositivo está Disable
        Dim estado As String
        If form1.Button1.Text = "Enable" Then
            'esta disable, lo habilito
            form1.hacerclick()
        Else
            estado = "Enable"
        End If
        ' Ocultar el Form2
        form1.Hide()

        Dim prefixToSearch = My.Settings.MuteProgram
        SetVolumeToZeroForApplication(prefixToSearch)
    End Sub

    Public Shared Sub DisableLineIn()
        'EstablecerVolumenCien()
        ' Crear una instancia del Form2
        Dim form1 As New Form1()
        form1.Show()
        ' Recorrer el ListView1 en Form2
        For Each item As ListViewItem In form1.ListView1.Items
            ' Comprobar si el valor de la columna coincide con My.Settings.LineIn
            If item.SubItems(0).Text = My.Settings.LineIn Then
                ' Seleccionar el elemento
                item.Selected = True
                Exit For
            End If
        Next
        'vale, ahora está seleccionado
        'el boton dirá si está Enable que el dispositivo está Disable
        Dim estado As String
        If form1.Button1.Text = "Enable" Then
            'esta disable, lo habilito
        Else
            form1.hacerclick()
            estado = "Enable"
        End If
        ' Ocultar el Form2
        form1.Hide()
        If My.Settings.MuteProgram.ToString <> Nothing Then
            RestoreOriginalVolumeForApplication(My.Settings.MuteProgram)
        End If
    End Sub

    ' Esta variable almacenará el volumen original de las aplicaciones
    Public Shared originalVolumes As New Dictionary(Of Integer, Single)()

    Public Shared Sub SetVolumeToZeroForApplication(applicationName As String)
        Dim deviceEnumerator As New MMDeviceEnumerator()
        Dim devices As MMDeviceCollection = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)

        For Each device As MMDevice In devices
            For i = 0 To device.AudioSessionManager.Sessions.Count - 1
                Dim session As AudioSessionControl = device.AudioSessionManager.Sessions(i)
                Dim processId As Integer = session.GetProcessID

                If processId > 0 Then
                    Try
                        Dim process As Process = Process.GetProcessById(processId)
                        If process IsNot Nothing AndAlso process.MainWindowTitle.ToLower().Contains(applicationName.ToLower()) Then
                            ' Guardar el volumen original antes de establecerlo en 0
                            If Not originalVolumes.ContainsKey(processId) Then
                                originalVolumes(processId) = session.SimpleAudioVolume.Volume
                                Console.WriteLine($"Guardando volumen de {process.ProcessName} con ID {processId}: {session.SimpleAudioVolume.Volume}")
                            End If

                            ' Establecer el volumen en 0
                            session.SimpleAudioVolume.Volume = 0.0F
                        End If
                    Catch ex As Exception
                        Console.WriteLine($"Error procesando {processId}: {ex.Message}")
                    End Try
                End If
            Next
        Next
    End Sub


    Public Shared Sub RestoreOriginalVolumeForApplication(applicationName As String)
        Dim deviceEnumerator As New MMDeviceEnumerator()
        Dim devices As MMDeviceCollection = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)

        For Each device As MMDevice In devices
            For i = 0 To device.AudioSessionManager.Sessions.Count - 1
                Dim session As AudioSessionControl = device.AudioSessionManager.Sessions(i)
                Dim processId As Integer = session.GetProcessID

                If processId > 0 Then
                    Try
                        Dim process As Process = Process.GetProcessById(processId)
                        If process IsNot Nothing AndAlso process.MainWindowTitle.ToLower().Contains(applicationName.ToLower()) Then
                            ' Restaurar el volumen solo si se guardó previamente
                            If originalVolumes.ContainsKey(processId) Then
                                session.SimpleAudioVolume.Volume = originalVolumes(processId)
                                Console.WriteLine($"Restaurando volumen de {process.ProcessName} con ID {processId} a {originalVolumes(processId)}")
                                ' Eliminar el proceso de la lista para evitar restauraciones redundantes
                                originalVolumes.Remove(processId)
                            Else
                                Console.WriteLine($"No se encontró un volumen original para {process.ProcessName} con ID {processId}")
                            End If
                        End If
                    Catch ex As Exception
                        Console.WriteLine($"Error procesando {processId}: {ex.Message}")
                    End Try
                End If
            Next
        Next
    End Sub



End Class