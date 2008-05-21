Imports System.Collections
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System

Public Class CodeFile
    Private _filename As String
    Private _fullFilename As String
    Private _theDoc As Document
    Private _theFile As SourceFile

    Private _projectName As String
    Public Property ProjectName() As String
        Get
            Return _projectName
        End Get
        Set(ByVal Value As String)
            _projectName = Value
        End Set
    End Property
    

    Public Property theFile() As SourceFile
        Get
            Return _theFile
        End Get
        Set(ByVal Value As SourceFile)
            _theFile = Value
        End Set
    End Property

    Public Property TheDoc() As Document
        Get
            Return _theDoc
        End Get
        Set(ByVal Value As Document)
            _theDoc = Value
        End Set
    End Property
    Public Overrides Function ToString() As String
        Return Filename
    End Function

    ''' <summary>
    ''' Creates a new instance of CodeFile
    ''' </summary>
    ''' <param name="theFile"></param>
    Public Sub New(ByVal theFile As SourceFile)
        MyBase.New()
        _theFile = theFile
        _filename = theFile.Name.Substring(theFile.Name.LastIndexOf("\") + 1)
        _projectName = theFile.Project.Name
    End Sub

    ''' <summary>
    ''' Creates a new instance of CodeFile
    ''' </summary>
    ''' <param name=""></param>
    ''' <param name="fullFilename"></param>
    Public Sub New(ByVal TheDoc As Document)
        MyBase.New()
        _filename = TheDoc.Name
        If Not TheDoc.FullName Is Nothing Then
            _fullFilename = TheDoc.FullName
        End If
        _theDoc = TheDoc
    End Sub

    Public Property Filename() As String
        Get
            Return _filename
        End Get
        Set(ByVal Value As String)
            _filename = Value
        End Set
    End Property
    Public Property FullFilename() As String
        Get
            Return _fullFilename
        End Get
        Set(ByVal Value As String)
            _fullFilename = Value
        End Set
    End Property
End Class
