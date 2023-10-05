Imports System.Management
Imports System.Runtime.InteropServices
Imports NAudio.CoreAudioApi
Imports FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME

Public Class ConfigLineIn

    Public Enum HRESULT As Integer
        S_OK = 0
        S_FALSE = 1
        E_NOINTERFACE = &H80004002
        E_NOTIMPL = &H80004001
        E_FAIL = &H80004005
        E_UNEXPECTED = &H8000FFFF
    End Enum

    Public Enum STGM As UInteger
        STGM_READ = &H0
        STGM_WRITE = &H1
        STGM_READWRITE = &H2
        STGM_SHARE_DENY_NONE = &H40
        STGM_SHARE_DENY_READ = &H30
        STGM_SHARE_DENY_WRITE = &H20
        STGM_SHARE_EXCLUSIVE = &H10
    End Enum

    <ComImport, Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IPropertyStore

        Function GetCount(<Out> ByRef propertyCount As UInteger) As HRESULT

        Function GetAt(<[In]> propertyIndex As UInteger, <Out, MarshalAs(UnmanagedType.Struct)> ByRef key As PROPERTYKEY) _
            As HRESULT

        Function GetValue(<[In], MarshalAs(UnmanagedType.Struct)> ByRef key As PROPERTYKEY,
                          <Out, MarshalAs(UnmanagedType.Struct)> ByRef pv As PROPVARIANT) As HRESULT

        Function SetValue(<[In], MarshalAs(UnmanagedType.Struct)> ByRef key As PROPERTYKEY,
                          <[In], MarshalAs(UnmanagedType.Struct)> ByRef pv As PROPVARIANT) As HRESULT

        Function Commit() As HRESULT

    End Interface

    Public Structure PROPERTYKEY

        Public Sub New(InputId As Guid, InputPid As UInt32)
            fmtid = InputId
            pid = InputPid
        End Sub

        Private fmtid As Guid
        Private ReadOnly pid As UInteger
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure PROPARRAY
        Public cElems As UInt32
        Public pElems As IntPtr
    End Structure

    <StructLayout(LayoutKind.Explicit, Pack:=1)>
    Public Structure PROPVARIANT
        <FieldOffset(0)> Public varType As UShort

        <FieldOffset(2)> Public wReserved1 As UShort

        <FieldOffset(4)> Public wReserved2 As UShort

        <FieldOffset(6)> Public wReserved3 As UShort

        <FieldOffset(8)> Public bVal As Byte

        <FieldOffset(8)> Public cVal As SByte

        <FieldOffset(8)> Public uiVal As UShort

        <FieldOffset(8)> Public iVal As Short

        <FieldOffset(8)> Public uintVal As UInt32

        <FieldOffset(8)> Public intVal As Int32

        <FieldOffset(8)> Public ulVal As UInt64

        <FieldOffset(8)> Public lVal As Int64

        <FieldOffset(8)> Public fltVal As Single

        <FieldOffset(8)> Public dblVal As Double

        <FieldOffset(8)> Public boolVal As Short

        <FieldOffset(8)> Public pclsidVal As IntPtr

        <FieldOffset(8)> Public pszVal As IntPtr

        <FieldOffset(8)> Public pwszVal As IntPtr

        <FieldOffset(8)> Public punkVal As IntPtr

        <FieldOffset(8)> Public ca As PROPARRAY

        <FieldOffset(8)> Public filetime As FILETIME
    End Structure

    Public Enum EDataFlow
        eRender = 0
        eCapture = (eRender + 1)
        eAll = (eCapture + 1)
        EDataFlow_enum_count = (eAll + 1)
    End Enum

    Public Enum ERole
        eConsole = 0
        eMultimedia = (eConsole + 1)
        eCommunications = (eMultimedia + 1)
        ERole_enum_count = (eCommunications + 1)
    End Enum

    <ComImport>
    <Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")>
    <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IMMDeviceEnumerator

        Function EnumAudioEndpoints(dataFlow As EDataFlow, dwStateMask As Integer,
                                    <Out> ByRef ppDevices As IMMDeviceCollection) As HRESULT

        ' for 0x80070490 : Element not found
        <PreserveSig>
        Function GetDefaultAudioEndpoint(dataFlow As EDataFlow, role As ERole, <Out> ByRef ppEndpoint As IMMDevice) _
            As HRESULT

        Function GetDevice(pwstrId As String, <Out> ByRef ppDevice As IMMDevice) As HRESULT

        Function RegisterEndpointNotificationCallback(pClient As IMMNotificationClient) As HRESULT

        Function UnregisterEndpointNotificationCallback(pClient As IMMNotificationClient) As HRESULT

    End Interface


    <ComImport>
    <Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E")>
    <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IMMDeviceCollection

        Function GetCount(<Out> ByRef pcDevices As UInteger) As HRESULT

        Function Item(nDevice As UInteger, <Out> ByRef ppDevice As IMMDevice) As HRESULT

    End Interface

    <ComImport>
    <Guid("D666063F-1587-4E43-81F1-B948E807363F")>
    <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IMMDevice

        Function Activate(ByRef iid As Guid, dwClsCtx As Integer, ByRef pActivationParams As PROPVARIANT,
                          <Out> ByRef ppInterface As IntPtr) As HRESULT

        Function OpenPropertyStore(stgmAccess As Integer, <Out> ByRef ppProperties As IPropertyStore) As HRESULT

        Function GetId(<Out> ByRef ppstrId As IntPtr) As HRESULT

        Function GetState(<Out> ByRef pdwState As Integer) As HRESULT

    End Interface

    <ComImport>
    <Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0")>
    <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IMMNotificationClient

        Function OnDeviceStateChanged(pwstrDeviceId As String, dwNewState As Integer) As HRESULT

        Function OnDeviceAdded(pwstrDeviceId As String) As HRESULT

        Function OnDeviceRemoved(pwstrDeviceId As String) As HRESULT

        Function OnDefaultDeviceChanged(flow As EDataFlow, role As ERole, pwstrDefaultDeviceId As String) As HRESULT

        Function OnPropertyValueChanged(pwstrDeviceId As String, ByRef key As PROPERTYKEY) As HRESULT

    End Interface

    <ComImport>
    <Guid("1BE09788-6894-4089-8586-9A2A6C265AC5")>
    <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IMMEndpoint

        Function GetDataFlow(<Out> ByRef pDataFlow As EDataFlow) As HRESULT

    End Interface

    <ComImport>
    <Guid("f8679f50-850a-41cf-9c72-430f290290c8")>
    <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IPolicyConfig

        Function GetMixFormat(<[In]> <MarshalAs(UnmanagedType.LPWStr)> pszDeviceName As String,
                              <Out> ByRef ppFormat As WAVEFORMATEXTENSIBLE) As HRESULT

        Function GetDeviceFormat(<[In]> <MarshalAs(UnmanagedType.LPWStr)> pszDeviceName As String,
                                 <[In]> <MarshalAs(UnmanagedType.Bool)> bDefault As Boolean,
                                 <Out> ByRef ppFormat As WAVEFORMATEXTENSIBLE) As HRESULT

        Function ResetDeviceFormat(<[In]> <MarshalAs(UnmanagedType.LPWStr)> pszDeviceName As String) As HRESULT

        Function SetDeviceFormat(<[In]> <MarshalAs(UnmanagedType.LPWStr)> pszDeviceName As String,
                                 <[In]> <MarshalAs(UnmanagedType.LPStruct)> pEndpointFormat As WAVEFORMATEXTENSIBLE,
                                 <[In]> <MarshalAs(UnmanagedType.LPStruct)> pMixFormat As WAVEFORMATEXTENSIBLE) _
            As HRESULT

        Function GetProcessingPeriod(<[In]> <MarshalAs(UnmanagedType.LPWStr)> pszDeviceName As String,
                                     <[In]> <MarshalAs(UnmanagedType.Bool)> bDefault As Boolean,
                                     <Out> ByRef pmftDefaultPeriod As Int64, <Out> ByRef pmftMinimumPeriod As Int64) _
            As HRESULT

        Function SetProcessingPeriod(<[In]> <MarshalAs(UnmanagedType.LPWStr)> pszDeviceName As String,
                                     pmftPeriod As Int64) As HRESULT

        Function GetShareMode(<[In]> <MarshalAs(UnmanagedType.LPWStr)> pszDeviceName As String,
                              <Out> ByRef pMode As DeviceShareMode) As HRESULT

        Function SetShareMode(<[In]> <MarshalAs(UnmanagedType.LPWStr)> pszDeviceName As String,
                              <[In]> mode As DeviceShareMode) As HRESULT

        Function GetPropertyValue(<[In]> <MarshalAs(UnmanagedType.LPWStr)> pszDeviceName As String,
                                  <[In]> <MarshalAs(UnmanagedType.Bool)> bFxStore As Boolean, ByRef pKey As PROPERTYKEY,
                                  <Out> ByRef pv As PROPVARIANT) As HRESULT

        Function SetPropertyValue(<[In]> <MarshalAs(UnmanagedType.LPWStr)> pszDeviceName As String,
                                  <[In]> <MarshalAs(UnmanagedType.Bool)> bFxStore As Boolean,
                                  <[In]> ByRef pKey As PROPERTYKEY, ByRef pv As PROPVARIANT) As HRESULT

        Function SetDefaultEndpoint(<[In]> <MarshalAs(UnmanagedType.LPWStr)> pszDeviceName As String,
                                    <[In]> <MarshalAs(UnmanagedType.U4)> role As ERole) As HRESULT

        Function SetEndpointVisibility(<[In]> <MarshalAs(UnmanagedType.LPWStr)> pszDeviceName As String,
                                       <[In]> <MarshalAs(UnmanagedType.Bool)> bVisible As Boolean) As HRESULT

    End Interface

    <StructLayout(LayoutKind.Explicit, Pack:=1)>
    Public Class WAVEFORMATEXTENSIBLE
        Inherits WAVEFORMATEX

        <FieldOffset(0)> Public wValidBitsPerSample As Short

        <FieldOffset(0)> Public wSamplesPerBlock As Short

        <FieldOffset(0)> Public wReserved As Short

        <FieldOffset(2)> Public dwChannelMask As WaveMask

        <FieldOffset(6)> Public SubFormat As Guid
    End Class

    <Flags>
    Public Enum WaveMask
        None = &H0
        FrontLeft = &H1
        FrontRight = &H2
        FrontCenter = &H4
        LowFrequency = &H8
        BackLeft = &H10
        BackRight = &H20
        FrontLeftOfCenter = &H40
        FrontRightOfCenter = &H80
        BackCenter = &H100
        SideLeft = &H200
        SideRight = &H400
        TopCenter = &H800
        TopFrontLeft = &H1000
        TopFrontCenter = &H2000
        TopFrontRight = &H4000
        TopBackLeft = &H8000
        TopBackCenter = &H10000
        TopBackRight = &H20000
    End Enum

    <StructLayout(LayoutKind.Sequential, Pack:=2)>
    Public Class WAVEFORMATEX
        Public wFormatTag As Short
        Public nChannels As Short
        Public nSamplesPerSec As Integer
        Public nAvgBytesPerSec As Integer
        Public nBlockAlign As Short
        Public wBitsPerSample As Short
        Public cbSize As Short
    End Class

    Public Enum DeviceShareMode
        [Shared]
        Exclusive
    End Enum

    <DllImport("Shlwapi.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Shared Function PathParseIconLocationW(pszIconFile As String) As Integer
    End Function

    Private Sub ConfigLineIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim enumerator As New MMDeviceEnumerator()

        Try
            For Each device As MMDevice In enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active)
                Dim friendlyName As String = device.FriendlyName
                ComboBox1.Items.Add(friendlyName)
            Next
            For Each device As MMDevice In enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Disabled)
                Dim friendlyName As String = device.FriendlyName
                ComboBox1.Items.Add(friendlyName)
            Next
        Catch ex As Exception
            MessageBox.Show("Error listing input devices: " & ex.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try

        If My.Settings.LineIn <> Nothing Then
            ComboBox1.SelectedItem = My.Settings.LineIn
            lbIDDevice.Text = GetDeviceID(My.Settings.LineIn)
        End If
        If My.Settings.MuteProgram <> Nothing Then
            TextBox1.Text = My.Settings.MuteProgram

        End If
    End Sub

    Public Function GetDeviceID(deviceName As String) As String
        Try
            Dim query As String = $"SELECT * FROM Win32_PnPEntity WHERE Caption LIKE '%{deviceName}%'"
            Dim searcher As New ManagementObjectSearcher(query)
            Dim devices As ManagementObjectCollection = searcher.Get()

            For Each device As ManagementObject In devices
                Dim deviceId = TryCast(device("DeviceID"), String)
                If Not String.IsNullOrEmpty(deviceId) Then
                    Return deviceId
                End If
            Next
        Catch ex As Exception
        End Try

        Return String.Empty
    End Function

    Private Sub ButtonOpen_Click(sender As Object, e As EventArgs) Handles ButtonOpen.Click
        FnLineIn.EnableLineIn()
    End Sub

    Private Sub btSaveConfigLineIn_Click(sender As Object, e As EventArgs) Handles btSaveConfigLineIn.Click
        My.Settings.LineIn = ComboBox1.SelectedItem.ToString
        My.Settings.LineInID = lbIDDevice.Text
        My.Settings.MuteProgram = TextBox1.Text
        My.Settings.Save()
        MsgBox("The configuration has been saved successfully.")
        Main.SaveGuideline()
        Main.LoadGuideLine()
        Close()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        FnLineIn.DisableLineIn()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        My.Settings.LineIn = ComboBox1.SelectedItem.ToString
        My.Settings.LineInID = lbIDDevice.Text
        My.Settings.Save()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class