Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.StructuralParser
Imports System.IO

Public Class Reference
#Region "Fields"
    Private mFileinfo As FileInfo
#End Region
#Region "Properties"
    Public ReadOnly Property FileName() As String
        Get
            Return mFileinfo.Name
        End Get
    End Property
    Public ReadOnly Property Location() As String
        Get
            Return mFileinfo.DirectoryName
        End Get
    End Property
    Public ReadOnly Property FullName() As String
        Get
            Return mFileinfo.FullName
        End Get
    End Property

#End Region
    Public Sub New(ByVal DllLocation As String)
        Me.New(New FileInfo(DllLocation))
    End Sub
    Public Sub New(ByVal Reference As AssemblyReference)
        Me.New(Reference.FilePath)
    End Sub
    Public Sub New(ByVal FileInfo As FileInfo)
        mFileinfo = FileInfo
    End Sub
    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Return Me.FullName = CType(obj, Reference).FullName
    End Function

End Class