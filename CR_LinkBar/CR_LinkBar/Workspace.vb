Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser

Public Class Workspace
    Inherits List(Of DocumentReference)
#Region "Fields"
    Private mName As String
#End Region
#Region "Simple Properties"
    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property
#End Region
#Region "Constructors"
    Public Sub New(ByVal CollectionName As String)
        mName = CollectionName
    End Sub
#End Region
    Public Overloads Sub AddDocument(ByVal TextDocument As TextDocument)
        Call AddDocument(TextDocument.Name, _
                 TextDocument.FullName, _
                 TextDocument.ActiveView.TopLine, _
                 TextDocument.ActiveView.Caret.SourcePoint)
    End Sub
    Public Overloads Sub AddDocument(ByVal Name As String, _
                              ByVal FullName As String, _
                              ByVal TopLine As Integer, _
                              ByVal SourcePoint As SourcePoint)
        MyBase.Add(New DocumentReference(Name, FullName, TopLine, SourcePoint))
    End Sub
End Class
