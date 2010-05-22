'-------------------------------------------------------------
' GACWrap.cs
'
' This implements managed wrappers to GAC API Interfaces
'-------------------------------------------------------------

Imports System
Imports System.Runtime.InteropServices
Imports System.Text
Imports Microsoft.VisualBasic

Namespace System.GACManagedAccess
    '-------------------------------------------------------------
    ' Interfaces defined by fusion
    '-------------------------------------------------------------
    <ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("e707dcde-d1cd-11d2-bab9-00c04f8eceae")> _
    Friend Interface IAssemblyCache
        <PreserveSig()> _
        Function UninstallAssembly(ByVal flags As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal assemblyName As [String], ByVal refData As InstallReference, ByRef disposition As AssemblyCacheUninstallDisposition) As Integer

        <PreserveSig()> _
        Function QueryAssemblyInfo(ByVal flags As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal assemblyName As [String], ByRef assemblyInfo As AssemblyInfo) As Integer
        <PreserveSig()> _
        Function Reserved(ByVal flags As Integer, ByVal pvReserved As IntPtr, ByRef ppAsmItem As [Object], <MarshalAs(UnmanagedType.LPWStr)> ByVal assemblyName As [String]) As Integer
        <PreserveSig()> _
        Function Reserved(ByRef ppAsmScavenger As [Object]) As Integer

        <PreserveSig()> _
        Function InstallAssembly(ByVal flags As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal assemblyFilePath As [String], ByVal refData As InstallReference) As Integer
    End Interface
    ' IAssemblyCache
    <ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("CD193BC0-B4BC-11d2-9833-00C04FC31D2E")> _
    Friend Interface IAssemblyName
        <PreserveSig()> _
        Function SetProperty(ByVal PropertyId As Integer, ByVal pvProperty As IntPtr, ByVal cbProperty As Integer) As Integer

        <PreserveSig()> _
        Function GetProperty(ByVal PropertyId As Integer, ByVal pvProperty As IntPtr, ByRef pcbProperty As Integer) As Integer

        <PreserveSig()> _
        Function Finalize() As Integer

        <PreserveSig()> _
        Function GetDisplayName(ByVal pDisplayName As StringBuilder, ByRef pccDisplayName As Integer, ByVal displayFlags As Integer) As Integer

        <PreserveSig()> _
        Function Reserved(ByRef guid As Guid, ByVal obj1 As [Object], ByVal obj2 As [Object], ByVal string1 As [String], ByVal llFlags As Int64, ByVal pvReserved As IntPtr, _
         ByVal cbReserved As Integer, ByRef ppv As IntPtr) As Integer

        <PreserveSig()> _
        Function GetName(ByRef pccBuffer As Integer, ByVal pwzName As StringBuilder) As Integer

        <PreserveSig()> _
        Function GetVersion(ByRef versionHi As Integer, ByRef versionLow As Integer) As Integer
        <PreserveSig()> _
        Function IsEqual(ByVal pAsmName As IAssemblyName, ByVal cmpFlags As Integer) As Integer

        <PreserveSig()> _
        Function Clone(ByRef pAsmName As IAssemblyName) As Integer
    End Interface
    ' IAssemblyName
    <ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("21b8916c-f28e-11d2-a473-00c04f8ef448")> _
    Friend Interface IAssemblyEnum
        <PreserveSig()> _
        Function GetNextAssembly(ByVal pvReserved As IntPtr, ByRef ppName As IAssemblyName, ByVal flags As Integer) As Integer
        <PreserveSig()> _
        Function Reset() As Integer
        <PreserveSig()> _
        Function Clone(ByRef ppEnum As IAssemblyEnum) As Integer
    End Interface
    ' IAssemblyEnum
    <ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("582dac66-e678-449f-aba6-6faaec8a9394")> _
    Friend Interface IInstallReferenceItem
        ' A pointer to a FUSION_INSTALL_REFERENCE structure.
        ' The memory is allocated by the GetReference method and is freed when
        ' IInstallReferenceItem is released. Callers must not hold a reference to this
        ' buffer after the IInstallReferenceItem object is released.
        ' This uses the InstallReferenceOutput object to avoid allocation
        ' issues with the interop layer.
        ' This cannot be marshaled directly - must use IntPtr
        <PreserveSig()> _
        Function GetReference(ByRef pRefData As IntPtr, ByVal flags As Integer, ByVal pvReserced As IntPtr) As Integer
    End Interface
    ' IInstallReferenceItem
    <ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("56b1a988-7c0c-4aa2-8639-c3eb5a90226f")> _
    Friend Interface IInstallReferenceEnum
        <PreserveSig()> _
        Function GetNextInstallReferenceItem(ByRef ppRefItem As IInstallReferenceItem, ByVal flags As Integer, ByVal pvReserced As IntPtr) As Integer
    End Interface
    ' IInstallReferenceEnum
    Public Enum AssemblyCommitFlags
        [Default] = 1
        Force = 2
    End Enum
    ' enum AssemblyCommitFlags
    Public Enum AssemblyCacheUninstallDisposition
        Unknown = 0
        Uninstalled = 1
        StillInUse = 2
        AlreadyUninstalled = 3
        DeletePending = 4
        HasInstallReference = 5
        ReferenceNotFound = 6
    End Enum

    <Flags()> _
    Friend Enum AssemblyCacheFlags
        GAC = 2
    End Enum

    Friend Enum CreateAssemblyNameObjectFlags
        CANOF_DEFAULT = 0
        CANOF_PARSE_DISPLAY_NAME = 1
    End Enum

    <Flags()> _
    Friend Enum AssemblyNameDisplayFlags
        VERSION = &H1
        CULTURE = &H2
        PUBLIC_KEY_TOKEN = &H4
        PROCESSORARCHITECTURE = &H20
        RETARGETABLE = &H80
        ' This enum will change in the future to include
        ' more attributes.
        ALL = VERSION Or CULTURE Or PUBLIC_KEY_TOKEN Or PROCESSORARCHITECTURE Or RETARGETABLE
    End Enum

    <StructLayout(LayoutKind.Sequential)> _
    Public Class InstallReference
        Public Sub New(ByVal guid As Guid, ByVal id As [String], ByVal data As [String])
            cbSize = CInt((2 * IntPtr.Size + 16 + (id.Length + data.Length) * 2))
            flags = 0
            ' quiet compiler warning
            If flags = 0 Then
            End If
            m_guidScheme = guid
            m_identifier = id
            m_description = data
        End Sub

        Public ReadOnly Property GuidScheme() As Guid
            Get
                Return m_guidScheme
            End Get
        End Property

        Public ReadOnly Property Identifier() As [String]
            Get
                Return m_identifier
            End Get
        End Property

        Public ReadOnly Property Description() As [String]
            Get
                Return m_description
            End Get
        End Property

        Private cbSize As Integer
        Private flags As Integer
        Private m_guidScheme As Guid
        <MarshalAs(UnmanagedType.LPWStr)> _
        Private m_identifier As [String]
        <MarshalAs(UnmanagedType.LPWStr)> _
        Private m_description As [String]
    End Class

    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure AssemblyInfo
        Public cbAssemblyInfo As Integer
        ' size of this structure for future expansion
        Public assemblyFlags As Integer
        Public assemblySizeInKB As Long
        <MarshalAs(UnmanagedType.LPWStr)> _
        Public currentAssemblyPath As [String]
        Public cchBuf As Integer
        ' size of path buf.
    End Structure

    <ComVisible(False)> _
    Public Class InstallReferenceGuid
        Public Shared Function IsValidGuidScheme(ByVal guid__1 As Guid) As Boolean
            Return (guid__1.Equals(UninstallSubkeyGuid) OrElse guid__1.Equals(FilePathGuid) OrElse guid__1.Equals(OpaqueGuid) OrElse guid__1.Equals(Guid.Empty))
        End Function

        Public Shared ReadOnly UninstallSubkeyGuid As New Guid("8cedc215-ac4b-488b-93c0-a50a49cb2fb8")
        Public Shared ReadOnly FilePathGuid As New Guid("b02f9d65-fb77-4f7a-afa5-b391309f11c9")
        Public Shared ReadOnly OpaqueGuid As New Guid("2ec93463-b0c3-45e1-8364-327e96aea856")
        ' these GUID cannot be used for installing into GAC.
        Public Shared ReadOnly MsiGuid As New Guid("25df0fc1-7f97-4070-add7-4b13bbfd7cb8")
        Public Shared ReadOnly OsInstallGuid As New Guid("d16d444c-56d8-11d5-882d-0080c847b195")
    End Class

    <ComVisible(False)> _
    Public Module AssemblyCache
        Public Sub InstallAssembly(ByVal assemblyPath As [String], ByVal reference As InstallReference, ByVal flags As AssemblyCommitFlags)
            If reference IsNot Nothing Then
                If Not InstallReferenceGuid.IsValidGuidScheme(reference.GuidScheme) Then
                    Throw New ArgumentException("Invalid reference guid.", "guid")
                End If
            End If

            Dim ac As IAssemblyCache = Nothing

            Dim hr As Integer = 0

            hr = Utils.CreateAssemblyCache(ac, 0)
            If hr >= 0 Then
                hr = ac.InstallAssembly(CInt(flags), assemblyPath, reference)
            End If

            If hr < 0 Then
                Marshal.ThrowExceptionForHR(hr)
            End If
        End Sub

        ' assemblyName has to be fully specified name.
        ' A.k.a, for v1.0/v1.1 assemblies, it should be "name, Version=xx, Culture=xx, PublicKeyToken=xx".
        ' For v2.0 assemblies, it should be "name, Version=xx, Culture=xx, PublicKeyToken=xx, ProcessorArchitecture=xx".
        ' If assemblyName is not fully specified, a random matching assembly will be uninstalled.
        Public Sub UninstallAssembly(ByVal assemblyName As [String], ByVal reference As InstallReference, ByRef disp As AssemblyCacheUninstallDisposition)
            Dim dispResult As AssemblyCacheUninstallDisposition = AssemblyCacheUninstallDisposition.Uninstalled
            If reference IsNot Nothing Then
                If Not InstallReferenceGuid.IsValidGuidScheme(reference.GuidScheme) Then
                    Throw New ArgumentException("Invalid reference guid.", "guid")
                End If
            End If

            Dim ac As IAssemblyCache = Nothing

            Dim hr As Integer = Utils.CreateAssemblyCache(ac, 0)
            If hr >= 0 Then
                hr = ac.UninstallAssembly(0, assemblyName, reference, dispResult)
            End If

            If hr < 0 Then
                Marshal.ThrowExceptionForHR(hr)
            End If

            disp = dispResult
        End Sub

        ' See comments in UninstallAssembly
        Public Function QueryAssemblyInfo(ByVal assemblyName As [String]) As [String]
            If assemblyName Is Nothing Then
                Throw New ArgumentException("Invalid name", "assemblyName")
            End If

            Dim aInfo As New AssemblyInfo()

            aInfo.cchBuf = 1024
            ' Get a string with the desired length
            aInfo.currentAssemblyPath = New [String](ControlChars.NullChar, aInfo.cchBuf)

            Dim ac As IAssemblyCache = Nothing
            Dim hr As Integer = Utils.CreateAssemblyCache(ac, 0)
            If hr >= 0 Then
                hr = ac.QueryAssemblyInfo(0, assemblyName, aInfo)
            End If
            If hr < 0 Then
                Marshal.ThrowExceptionForHR(hr)
            End If

            Return aInfo.currentAssemblyPath
        End Function
    End Module

    <ComVisible(False)> _
    Public Class AssemblyCacheEnum
        ' null means enumerate all the assemblies
        Public Sub New(ByVal assemblyName As [String])
            Dim fusionName As IAssemblyName = Nothing
            Dim hr As Integer = 0

            If assemblyName IsNot Nothing Then
                hr = Utils.CreateAssemblyNameObject(fusionName, assemblyName, CreateAssemblyNameObjectFlags.CANOF_PARSE_DISPLAY_NAME, IntPtr.Zero)
            End If

            If hr >= 0 Then
                hr = Utils.CreateAssemblyEnum(m_AssemblyEnum, IntPtr.Zero, fusionName, AssemblyCacheFlags.GAC, IntPtr.Zero)
            End If

            If hr < 0 Then
                Marshal.ThrowExceptionForHR(hr)
            End If
        End Sub

        Public Function GetNextAssembly() As [String]
            Dim hr As Integer = 0
            Dim fusionName As IAssemblyName = Nothing

            If done Then
                Return Nothing
            End If

            ' Now get next IAssemblyName from m_AssemblyEnum
            hr = m_AssemblyEnum.GetNextAssembly(CType(0, IntPtr), fusionName, 0)

            If hr < 0 Then
                Marshal.ThrowExceptionForHR(hr)
            End If

            If fusionName IsNot Nothing Then
                Return GetFullName(fusionName)
            Else
                done = True
                Return Nothing
            End If
        End Function

        Private Function GetFullName(ByVal fusionAsmName As IAssemblyName) As [String]
            Dim sDisplayName As New StringBuilder(1024)
            Dim iLen As Integer = 1024

            Dim hr As Integer = fusionAsmName.GetDisplayName(sDisplayName, iLen, CInt(AssemblyNameDisplayFlags.ALL))
            If hr < 0 Then
                Marshal.ThrowExceptionForHR(hr)
            End If

            Return sDisplayName.ToString()
        End Function

        Private m_AssemblyEnum As IAssemblyEnum = Nothing
        Private done As Boolean
    End Class
    ' class AssemblyCacheEnum
    Public Class AssemblyCacheInstallReferenceEnum
        Public Sub New(ByVal assemblyName As [String])
            Dim fusionName As IAssemblyName = Nothing

            Dim hr As Integer = Utils.CreateAssemblyNameObject(fusionName, assemblyName, CreateAssemblyNameObjectFlags.CANOF_PARSE_DISPLAY_NAME, IntPtr.Zero)

            If hr >= 0 Then
                hr = Utils.CreateInstallReferenceEnum(refEnum, fusionName, 0, IntPtr.Zero)
            End If

            If hr < 0 Then
                Marshal.ThrowExceptionForHR(hr)
            End If
        End Sub

        Public Function GetNextReference() As InstallReference
            Dim item As IInstallReferenceItem = Nothing
            Dim hr As Integer = refEnum.GetNextInstallReferenceItem(item, 0, IntPtr.Zero)
            If CUInt(hr) = &H80070103 Then
                ' ERROR_NO_MORE_ITEMS
                Return Nothing
            End If

            If hr < 0 Then
                Marshal.ThrowExceptionForHR(hr)
            End If

            Dim refData As IntPtr
            Dim instRef As New InstallReference(Guid.Empty, [String].Empty, [String].Empty)

            hr = item.GetReference(refData, 0, IntPtr.Zero)
            If hr < 0 Then
                Marshal.ThrowExceptionForHR(hr)
            End If

            Marshal.PtrToStructure(refData, instRef)
            Return instRef
        End Function

        Private refEnum As IInstallReferenceEnum
    End Class

    Friend Class Utils
        <DllImport("fusion.dll")> _
        Friend Shared Function CreateAssemblyEnum(ByRef ppEnum As IAssemblyEnum, ByVal pUnkReserved As IntPtr, ByVal pName As IAssemblyName, ByVal flags As AssemblyCacheFlags, ByVal pvReserved As IntPtr) As Integer
        End Function

        <DllImport("fusion.dll")> _
        Friend Shared Function CreateAssemblyNameObject(ByRef ppAssemblyNameObj As IAssemblyName, <MarshalAs(UnmanagedType.LPWStr)> ByVal szAssemblyName As [String], ByVal flags As CreateAssemblyNameObjectFlags, ByVal pvReserved As IntPtr) As Integer
        End Function

        <DllImport("fusion.dll")> _
        Friend Shared Function CreateAssemblyCache(ByRef ppAsmCache As IAssemblyCache, ByVal reserved As Integer) As Integer
        End Function

        <DllImport("fusion.dll")> _
        Friend Shared Function CreateInstallReferenceEnum(ByRef ppRefEnum As IInstallReferenceEnum, ByVal pName As IAssemblyName, ByVal dwFlags As Integer, ByVal pvReserved As IntPtr) As Integer
        End Function
    End Class
End Namespace
