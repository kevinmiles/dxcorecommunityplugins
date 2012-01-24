Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class PlugIn1

	'DXCore-generated code...
#Region " InitializePlugIn "
	Public Overrides Sub InitializePlugIn()
		MyBase.InitializePlugIn()
        Call CreateToDoSearchProvider()
    End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region

    Private Sub CreateToDoSearchProvider()
        Dim ToDoSearchProvider = New DevExpress.CodeRush.Core.SearchProvider()
        CType(ToDoSearchProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        ToDoSearchProvider.Description = "ToDoSearchProvider"
        ToDoSearchProvider.ProviderName = "ToDoSearchProvider" ' Needs to be Unique
        ToDoSearchProvider.Register = True
        ToDoSearchProvider.UseForNavigation = True
        AddHandler ToDoSearchProvider.CheckAvailability, AddressOf ToDoSearchProvider_CheckAvailability
        AddHandler ToDoSearchProvider.SearchReferences, AddressOf ToDoSearchProvider_SearchReferences
        CType(ToDoSearchProvider, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Function IsOnToDoLikeComment(ByRef Comment As Comment) As Boolean

        Dim ActiveElement As LanguageElement = CodeRush.Source.Active
        If ActiveElement Is Nothing Then
            Return False
        End If
        Dim FoundAttribute As Boolean = ActiveElement.ElementType = LanguageElementType.Comment _
                                        AndAlso Not TryCast(ActiveElement, Comment).Name.Trim = "" _
                                        AndAlso TryCast(ActiveElement, Comment).Name.Contains(":")

        If FoundAttribute Then
            Comment = ActiveElement
        End If
        Return FoundAttribute
    End Function
    Private Sub ToDoSearchProvider_CheckAvailability(ByVal sender As Object, ByVal ea As CheckSearchAvailabilityEventArgs)
        Dim FoundComment As Comment = Nothing
        ea.Available = IsOnToDoLikeComment(FoundComment)
    End Sub
    Private Sub ToDoSearchProvider_SearchReferences(Sender As Object, ByVal ea As DevExpress.CodeRush.Core.SearchEventArgs)
        Dim FoundComment As Comment = Nothing
        If Not IsOnToDoLikeComment(FoundComment) Then
            Exit Sub
        End If
        Dim StartPhrase = FoundComment.Name.Substring(0, FoundComment.Name.IndexOf(":"))
        For Each Document As TextDocument In CodeRush.Documents.AllSolutionTextDocuments
            Dim Comments = CodeRush.Source.GetComments(TryCast(Document.FileNode, SourceFile))
            For Each Comment As Comment In Comments
                If Comment.Name.StartsWith(StartPhrase) Then
                    Dim CommentStartRange = New SourceRange(Comment.InternalRange.Start.OffsetPoint(0, 1), Comment.InternalRange.Start.OffsetPoint(0, StartPhrase.Length + 1))
                    ea.AddRange(Comment.FileNode, CommentStartRange)
                End If
            Next

        Next
    End Sub
End Class
