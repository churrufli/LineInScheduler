Option Strict On

Imports System.Runtime.InteropServices
Imports FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME

Public Class Form1

    Public Enum HRESULT As Integer
        S_OK = 0
        S_FALSE = 1
        E_NOINTERFACE = &H80004002
        E_NOTIMPL = &H80004001
        E_FAIL = &H80004005
        E_UNEXPECTED = &H8000FFFF
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

    Public Const STGM_READ As Integer = &H0
    Public Const STGM_WRITE As Integer = &H1
    Public Const STGM_READWRITE As Integer = &H2

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

    Public Const DEVICE_STATE_ACTIVE As Integer = &H1
    Public Const DEVICE_STATE_DISABLED As Integer = &H2
    Public Const DEVICE_STATE_NOTPRESENT As Integer = &H4
    Public Const DEVICE_STATE_UNPLUGGED As Integer = &H8
    Public Const DEVICE_STATEMASK_ALL As Integer = &HF

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

    Public Shared _
        PKEY_Device_FriendlyName As PROPERTYKEY = New PROPERTYKEY(New Guid("a45c254e-df1c-4efd-8020-67d146a850e0"), 14)

    Public Shared _
        PKEY_Device_DeviceDesc As PROPERTYKEY = New PROPERTYKEY(New Guid("a45c254e-df1c-4efd-8020-67d146a850e0"), 2)

    Public Shared _
        PKEY_DeviceClass_IconPath As PROPERTYKEY = New PROPERTYKEY(New Guid("259abffc-50a7-47ce-af08-68c9a7d73366"), 12)

    Friend WithEvents ListView1 As ListView
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Private ReadOnly groupRender As New ListViewGroup("Render")
    Private ReadOnly groupCapture As New ListViewGroup("Capture")
    Private ReadOnly colorDefault As Color = Color.GreenYellow

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClientSize = New Size(800, 304)
        Text = "Test Core Audio APIs"

        ListView1 = New ListView With {
            .Location = New Point(10, 10),
            .Size = New Size(780, 230)
        }
        ListView1.Columns.Add("Device Name", 320, HorizontalAlignment.Left)
        ListView1.Columns.Add("Device ID", 350, HorizontalAlignment.Left)
        ListView1.Columns.Add("State", 100, HorizontalAlignment.Left)
        ListView1.View = View.Details
        ListView1.FullRowSelect = True
        ListView1.HideSelection = False
        ListView1.MultiSelect = False
        Controls.Add(ListView1)

        'ListView1.Groups.Add(groupRender)
        ListView1.Groups.Add(groupCapture)

        Button1 = New Button With {
            .Location = New Point(10, 260),
            .Name = "Button1",
            .Size = New Size(75, 23),
            .TabIndex = 0,
            .Text = "Enable",
            .UseVisualStyleBackColor = True
        }
        Controls.Add(Button1)
        Button1.Enabled = False

        Button2 = New Button With {
            .Location = New Point(95, 260),
            .Name = "Button2",
            .Size = New Size(100, 23),
            .TabIndex = 1,
            .Text = "Set as Default",
            .UseVisualStyleBackColor = True
        }
        Controls.Add(Button2)
        Button2.Enabled = False

        CenterToScreen()
        PopulateListView()
    End Sub

    Private Sub PopulateListView()
        Dim hr = HRESULT.E_FAIL
        Dim CLSID_MMDeviceEnumerator = New Guid("{BCDE0395-E52F-467C-8E3D-C4579291692E}")
        Dim MMDeviceEnumeratorType As Type = Type.GetTypeFromCLSID(CLSID_MMDeviceEnumerator, True)
        Dim MMDeviceEnumerator As Object = Activator.CreateInstance(MMDeviceEnumeratorType)
        Dim pMMDeviceEnumerator = CType(MMDeviceEnumerator, IMMDeviceEnumerator)
        If (pMMDeviceEnumerator IsNot Nothing) Then
            Dim sIdDefaultRender As String = Nothing
            Dim sIdDefaultCapture As String = Nothing
            Dim pDefaultDevice As IMMDevice = Nothing
            hr = pMMDeviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eConsole, pDefaultDevice)
            If hr = HRESULT.S_OK Then
                Dim hGlobal As IntPtr = Marshal.AllocHGlobal(260)
                hr = pDefaultDevice.GetId(hGlobal)
                sIdDefaultRender = Marshal.PtrToStringUni(hGlobal)
                Marshal.FreeHGlobal(hGlobal)
                Marshal.ReleaseComObject(pDefaultDevice)
            End If
            hr = pMMDeviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eCapture, ERole.eConsole, pDefaultDevice)
            If hr = HRESULT.S_OK Then
                Dim hGlobal As IntPtr = Marshal.AllocHGlobal(260)
                hr = pDefaultDevice.GetId(hGlobal)
                sIdDefaultCapture = Marshal.PtrToStringUni(hGlobal)
                Marshal.FreeHGlobal(hGlobal)
                Marshal.ReleaseComObject(pDefaultDevice)
            End If

            Dim pDeviceCollection As IMMDeviceCollection = Nothing
            hr = pMMDeviceEnumerator.EnumAudioEndpoints(EDataFlow.eAll,
                                                        DEVICE_STATE_ACTIVE Or DEVICE_STATE_UNPLUGGED Or
                                                        DEVICE_STATE_DISABLED, pDeviceCollection)
            If (hr = HRESULT.S_OK) Then
                Dim nDevices As UInteger = 0
                hr = pDeviceCollection.GetCount(nDevices)
                For i As UInteger = 0 To CUInt(nDevices - 1)
                    Dim pDevice As IMMDevice = Nothing
                    hr = pDeviceCollection.Item(i, pDevice)
                    If (hr = HRESULT.S_OK) Then
                        Dim pPropertyStore As IPropertyStore = Nothing
                        hr = pDevice.OpenPropertyStore(STGM_READ, pPropertyStore)
                        If hr = HRESULT.S_OK Then

                            Dim sFriendlyName As String = Nothing
                            Dim pv = New PROPVARIANT()
                            hr = pPropertyStore.GetValue(PKEY_Device_FriendlyName, pv)
                            If hr = HRESULT.S_OK Then
                                sFriendlyName = Marshal.PtrToStringUni(pv.pwszVal)
                            End If

                            Dim sIconPath As String = Nothing
                            Dim nIconId = 0
                            Dim pvIconPath = New PROPVARIANT()
                            hr = pPropertyStore.GetValue(PKEY_DeviceClass_IconPath, pvIconPath)
                            If hr = HRESULT.S_OK Then
                                ' %windir%\system32\mmres.dll,-3011
                                sIconPath = Marshal.PtrToStringUni(pvIconPath.pwszVal)
                                nIconId = PathParseIconLocationW(sIconPath)
                            End If

                            Dim hGlobal As IntPtr = Marshal.AllocHGlobal(260)
                            hr = pDevice.GetId(hGlobal)
                            Dim sId As String = Marshal.PtrToStringUni(hGlobal)
                            Marshal.FreeHGlobal(hGlobal)

                            Dim pEndpoint As IMMEndpoint = Nothing
                            pEndpoint = CType(pDevice, IMMEndpoint)
                            Dim eDirection = EDataFlow.eAll
                            hr = pEndpoint.GetDataFlow(eDirection)

                            Dim nState = 0
                            Dim sState = ""
                            hr = pDevice.GetState(nState)
                            If (nState = DEVICE_STATE_ACTIVE) Then
                                sState = "Active"
                            ElseIf (nState = DEVICE_STATE_DISABLED) Then
                                sState = "Disabled"
                            ElseIf (nState = DEVICE_STATE_NOTPRESENT) Then
                                sState = "Not present"
                            ElseIf (nState = DEVICE_STATE_UNPLUGGED) Then
                                sState = "Unplugged"
                            End If

                            Dim lvi As ListViewItem = Nothing
                            If (eDirection = EDataFlow.eRender) Then
                                lvi = ListView1.Items.Add(New ListViewItem(New String() {sFriendlyName, sId, sState},
                                                                           groupRender))
                            ElseIf (eDirection = EDataFlow.eCapture) Then
                                lvi = ListView1.Items.Add(New ListViewItem(New String() {sFriendlyName, sId, sState},
                                                                           groupCapture))
                            End If
                            If (sId = sIdDefaultRender) Then
                                lvi.BackColor = colorDefault
                            End If
                            If (sId = sIdDefaultCapture) Then
                                lvi.BackColor = colorDefault
                            End If
                            Marshal.ReleaseComObject(pPropertyStore)
                        End If
                        Marshal.ReleaseComObject(pDevice)
                    End If
                Next
                Marshal.ReleaseComObject(pDeviceCollection)
            End If
            Marshal.ReleaseComObject(pMMDeviceEnumerator)
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim selitems As ListView.SelectedListViewItemCollection = ListView1.SelectedItems
        Dim item As ListViewItem
        Dim nDevInst = 0
        Dim bEnabled = False
        Dim sState As String = Nothing
        For Each item In selitems
            sState = item.SubItems(2).Text
            If (sState = "Disabled") Then
                Button1.Text = "Enable"
            Else
                Button1.Text = "Disable"
            End If
            If (item.BackColor <> colorDefault And sState = "Active") Then
                Button2.Enabled = True
            Else
                Button2.Enabled = False
            End If
        Next
        Button1.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        hacerclick()
    End Sub

    Public Sub hacerclick()

        Dim hr = HRESULT.E_FAIL
        Dim CLSID_PolicyConfig = New Guid("{870af99c-171d-4f9e-af0d-e63df40c2bc9}")
        Dim PolicyConfigType As Type = Type.GetTypeFromCLSID(CLSID_PolicyConfig, True)
        Dim PolicyConfig As Object = Activator.CreateInstance(PolicyConfigType)
        Dim pPolicyConfig = CType(PolicyConfig, IPolicyConfig)

        Dim sCurrentId As String = ListView1.SelectedItems.Item(0).SubItems(1).Text
        Dim sState As String = ListView1.SelectedItems.Item(0).SubItems(2).Text
        Dim bEnable = False
        If pPolicyConfig IsNot Nothing Then
            If (sState = "Disabled") Then
                bEnable = True
            Else
                bEnable = False
            End If
            hr = pPolicyConfig.SetEndpointVisibility(sCurrentId, bEnable)
            Button1.Enabled = False
            Button2.Enabled = False
            ListView1.Items.Clear()
            PopulateListView()
            Marshal.ReleaseComObject(PolicyConfig)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim hr = HRESULT.E_FAIL
        Dim CLSID_PolicyConfig = New Guid("{870af99c-171d-4f9e-af0d-e63df40c2bc9}")
        Dim PolicyConfigType As Type = Type.GetTypeFromCLSID(CLSID_PolicyConfig, True)
        Dim PolicyConfig As Object = Activator.CreateInstance(PolicyConfigType)
        Dim pPolicyConfig = CType(PolicyConfig, IPolicyConfig)

        Dim sCurrentId As String = ListView1.SelectedItems.Item(0).SubItems(1).Text
        Dim sState As String = ListView1.SelectedItems.Item(0).SubItems(2).Text
        Dim bEnable = False
        If pPolicyConfig IsNot Nothing Then
            hr = pPolicyConfig.SetDefaultEndpoint(sCurrentId, ERole.eConsole)
            Button1.Enabled = False
            Button2.Enabled = False
            ListView1.Items.Clear()
            PopulateListView()
            Marshal.ReleaseComObject(PolicyConfig)
        End If
    End Sub

End Class