Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports DX_MoveCode.MoveCodeV2
Public Class PlugIn1

    Private mStatementToMove As Statement = Nothing
    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    'Done: Move Statements Up and down
    'Done: Move Statements Left and Right
    'Done: Move Methods Up and Down

    'TODO: Add functions to work with Selections
    'TODO: Add option to use TargerPicker

    Private mStatementMover As IStatementMover
    Private mMemberMover As IMemberMover
#Region "Move Code Actions"
    Public Sub SetupMovers()
        Dim Mover = New MoverMoveSource
        mStatementMover = Mover
        mMemberMover = Mover
    End Sub
    Private Sub cmdMoveCodeUp_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeUp.Execute
        Call SetupMovers()
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Dim Selection = CodeRush.Documents.ActiveTextView.Selection
        Select Case True
            Case Selection.Height > 1
                Call Selection.ExtendToWholeLines()

            Case CodeRush.Source.IsStatement(FirstNodeOnLine.GetParentStatementOrVariable)
                mStatementMover.MoveStatementUp(FirstNodeOnLine)
            Case FirstNodeOnLine.GetParentClassInterfaceStructOrModule Is FirstNodeOnLine.Parent
                mMemberMover.MoveMemberUp(FirstNodeOnLine)
        End Select
    End Sub

    Private Sub cmdMoveCodeDown_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeDown.Execute
        Call SetupMovers()
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Select Case True
            Case CodeRush.Source.IsStatement(FirstNodeOnLine.GetParentStatementOrVariable)
                mStatementMover.MoveStatementDown(FirstNodeOnLine)
            Case FirstNodeOnLine.GetParentClassInterfaceStructOrModule Is FirstNodeOnLine.Parent
                mMemberMover.MoveMemberDown(FirstNodeOnLine)
        End Select
    End Sub


    Private Sub cmdMoveCodeRight_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeRight.Execute
        Call SetupMovers()
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Call mStatementMover.MoveStatementRight(FirstNodeOnLine.GetParentStatementOrVariable)
    End Sub
    Private Sub cmdMoveCodeLeft_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeLeft.Execute
        Call SetupMovers()
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Call mStatementMover.MoveStatementLeft(FirstNodeOnLine.GetParentStatementOrVariable)
    End Sub
#End Region
#Region "Move Caret Actions"
    Private Sub cmdMoveCaretUp_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCaretUp.Execute
        Call SetupMovers()
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Select Case True
            Case FirstNodeOnLine.GetParentMethodOrProperty IsNot Nothing
                Call MoveCaretUp(FirstNodeOnLine.GetParentStatementOrVariable)
        End Select
    End Sub
    Private Sub cmdMoveCaretDown_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCaretDown.Execute
        Call SetupMovers()
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Select Case True
            Case FirstNodeOnLine.GetParentMethodOrProperty IsNot Nothing
                Call MoveCaretDown(FirstNodeOnLine.GetParentStatementOrVariable)
        End Select
    End Sub
    Private Sub cmdMoveCaretLeft_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCaretLeft.Execute
        Call SetupMovers()
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Call MoveCaretLeft(FirstNodeOnLine.GetParentStatementOrVariable)
    End Sub
    Private Sub cmdMoveCaretRight_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCaretRight.Execute
        Call SetupMovers()
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Call MoveCaretRight(FirstNodeOnLine.GetParentStatementOrVariable)
    End Sub
#End Region

#Region "Caret Movement"
    Public Sub MoveCaretUp(ByVal Source As LanguageElement)
        MoveCaretToElement(Source.PreviousCodeSibling)
    End Sub
    Private Sub MoveCaretDown(ByVal Source As LanguageElement)
        MoveCaretToElement(Source.NextCodeSibling)
    End Sub
    Public Sub MoveCaretLeft(ByVal Source As LanguageElement)
        Call MoveCaretToElement(Source.Parent)
    End Sub
    Public Sub MoveCaretRight(ByVal Source As LanguageElement)
        If Source IsNot Nothing Then
            Dim NextBlock = GetNextBlockSibling(Source)
            If NextBlock IsNot Nothing Then
                Call CodeRush.Caret.MoveTo(NextBlock.BlockCodeRange.Start)
            End If
        End If
    End Sub
#End Region
#Region "Utility"
    Private Sub MoveCaretToElement(ByVal Destination As LanguageElement)
        If Destination IsNot Nothing Then
            CodeRush.Caret.MoveTo(Destination.Range.Start)
        End If
    End Sub
#End Region

End Class
