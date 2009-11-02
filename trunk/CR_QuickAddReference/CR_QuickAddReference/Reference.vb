Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.StructuralParser
Imports System.IO
Imports DevExpress.CodeRush.Core

Public Class Reference
#Region "Fields"
    Private mFileinfo As FileInfo
    Private mDescription As String
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
    Public ReadOnly Property FilenameWithDescription() As String
        Get
            Return If(mDescription = "", FullName, String.Format("{0}({1})", FileName, Description))
        End Get
    End Property

    Public ReadOnly Property Description() As String
        Get
            Return mDescription
        End Get
    End Property
#End Region
    Public Sub New(ByVal DllLocation As String, Optional ByVal Description As String = "")
        Me.New(New FileInfo(System.Environment.ExpandEnvironmentVariables(DllLocation)), Description)
    End Sub
    Public Sub New(ByVal Reference As AssemblyReference, Optional ByVal Description As String = "")
        Me.New(Reference.FilePath, Description)
    End Sub
    Public Sub New(ByVal FileInfo As FileInfo, Optional ByVal Description As String = "")
        mFileinfo = FileInfo
        mDescription = Description
    End Sub
    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Return FullName = CType(obj, Reference).FullName
    End Function
    Public ReadOnly Property IsGACReference() As Boolean
        Get
            Return DefaultReferences.GACFolders.Any(Function(f) FullName.StartsWith(f.FullName))
        End Get
    End Property
    Public Function IsReferencedByProject(ByVal Project As Project) As Boolean
        Return Project.GetVSLangProj().References.Find(System.IO.Path.GetFileNameWithoutExtension(Me.FileName)) IsNot Nothing
    End Function
    Public Sub AddToProjectTestingForGAC(ByVal Project As Project)
        Try
            Dim Ref = Project.AddReference(FullName)
            Ref.CopyLocal = Not IsGACReference
            Lists.Recent.Add(Me)
            Lists.Recent.Save()
        Catch ex As Exception

        End Try
    End Sub

End Class
