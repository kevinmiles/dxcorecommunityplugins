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
        CreateRedundantAssignmentCodeIssue()
		'TODO: Add your initialization code here.
	End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region
    Public Sub CreateRedundantAssignmentCodeIssue()
        Dim RedundantAssignmentCodeIssue As New DevExpress.CodeRush.Core.IssueProvider(components)
        CType(RedundantAssignmentCodeIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        RedundantAssignmentCodeIssue.ProviderName = "RedundantAssignment" ' Should be Unique
        RedundantAssignmentCodeIssue.DisplayName = "Redundant Assignment"
        AddHandler RedundantAssignmentCodeIssue.CheckCodeIssues, AddressOf RedundantAssignmentCodeIssue_CheckCodeIssues
        CType(RedundantAssignmentCodeIssue, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub RedundantAssignmentCodeIssue_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        ' This method is executed when the system checks for your issue.
        Dim Scope = TryCast(ea.Scope, LanguageElement)
        If Scope Is Nothing Then
            Exit Sub
        End If
        Dim Finder As New ElementEnumerable(Scope, GetType(Assignment), True)
        For Each FoundItem As Assignment In Finder
            If FoundItem.LeftSide.GetDeclaration Is FoundItem.Expression.GetDeclaration Then
                ea.AddError(FoundItem.Range, "Redundant Assignment")
            End If
        Next
    End Sub
End Class
