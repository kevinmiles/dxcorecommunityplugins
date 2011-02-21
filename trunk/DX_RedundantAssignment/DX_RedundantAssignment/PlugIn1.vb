Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.Refactor

Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        CreateRedundantAssignmentCodeIssue()
        CreateRemoveRedundantAssignment()

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
        RedundantAssignmentCodeIssue.ProviderName = "Redundant Assignment" ' Should be Unique
        RedundantAssignmentCodeIssue.DisplayName = "Redundant Assignment"
        AddHandler RedundantAssignmentCodeIssue.CheckCodeIssues, AddressOf RedundantAssignmentCodeIssue_CheckCodeIssues
        CType(RedundantAssignmentCodeIssue, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

    Public Sub CreateRemoveRedundantAssignment()
        Dim RemoveRedundantAssignment As New DevExpress.Refactor.Core.RefactoringProvider(components)
        CType(RemoveRedundantAssignment, System.ComponentModel.ISupportInitialize).BeginInit()
        RemoveRedundantAssignment.ProviderName = "Remove Redundant Assignment" ' Should be Unique
        RemoveRedundantAssignment.DisplayName = "Remove Redundant Assignment"
        AddHandler RemoveRedundantAssignment.CheckAvailability, AddressOf RemoveRedundantAssignment_CheckAvailability
        AddHandler RemoveRedundantAssignment.Apply, AddressOf RemoveRedundantAssignment_Execute
        CType(RemoveRedundantAssignment, System.ComponentModel.ISupportInitialize).EndInit()
        RemoveRedundantAssignment.CodeIssueMessage = "Redundant Assignment"
        RemoveRedundantAssignment.SolvedIssues.Add("Redundant Assignment")
    End Sub


    Private Sub RedundantAssignmentCodeIssue_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        ' This method is executed when the system checks for your issue.
        Dim Scope = TryCast(ea.Scope, LanguageElement)
        If Scope Is Nothing Then
            Exit Sub
        End If
        Dim Finder As New ElementEnumerable(Scope, GetType(Assignment), True)
        For Each FoundItem As Assignment In Finder
            If IsRedundantAssignment(FoundItem) Then
                ea.AddIssue(CodeIssueType.Hint, FoundItem.Range, "Redundant Assignment")
            End If
        Next
    End Sub
    Private Shared Function IsRedundantAssignment(ByVal FoundItem As Assignment) As Boolean
        Return FoundItem.LeftSide.GetDeclaration Is FoundItem.Expression.GetDeclaration
    End Function

    Private Sub RemoveRedundantAssignment_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ea.Available = TypeOf ea.CodeActive Is Assignment AndAlso IsRedundantAssignment(TryCast(ea.CodeActive, Assignment))
    End Sub
    Private Sub RemoveRedundantAssignment_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        ea.TextDocument.SetText(ea.CodeActive.Range, "")
    End Sub

End Class
