Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.Diagnostics.General

Public Class PlugIn1
    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
        LoadSettings()
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
    'Done: Add functions to work with Selections

    'TODO: Add option to use TargerPicker

#Region "Mover Fields"
    Private mStatementMover As IStatementMover
    Private mMemberMover As IMemberMover
    Private mSelectionMover As ISelectionMover
#End Region
#Region "Initialise"
    Private Sub SetupMovers(Of T As {IStatementMover, IMemberMover, ISelectionMover})(ByVal Mover As T)
        mStatementMover = Mover
        mMemberMover = Mover
        mSelectionMover = Mover
    End Sub
#End Region
#Region "Context"
    Private Function MultipleLinesSelected() As Boolean
        Return CodeRush.Documents.ActiveTextView.Selection.Height > 1
    End Function
    Private Function CaretOnType() As Boolean
        Dim FirstNodeOnLine = GetFirstNodeOnCaretLine()
        Return FirstNodeOnLine.Parent.ElementType = LanguageElementType.SourceFile
    End Function
    Private Function CaretOnStatement() As Boolean
        Dim FirstNodeOnLine = GetFirstNodeOnCaretLine()
        Return CodeRush.Source.IsStatement(FirstNodeOnLine.GetParentStatementOrVariable)
    End Function
    Private Function CaretOnMember() As Boolean
        'Return CodeRush.Caret.
        'Dim FirstNodeOnLine = GetFirstNodeOnCaretLine()
        'Return FirstNodeOnLine.GetParentClassInterfaceStructOrModule Is FirstNodeOnLine.Parent
        ' This Case Fails if there is no visibility specifier
        ' This is because the firstNodeOnLine is a child of the next node rather than of the type.
        Return CaretOnMethodSignature() OrElse OnPropertySignature() OrElse OnField()
    End Function
    Private Function CaretOnMethodSignature() As Boolean
        Return CodeRush.Caret.OnMethod _
            AndAlso CodeRush.Caret.SourcePoint.Line = CodeRush.Source.ActiveMethod.StartLine
    End Function
    Private Function OnPropertySignature() As Boolean
        Return CodeRush.Caret.OnProperty _
            AndAlso CodeRush.Caret.SourcePoint.Line = CodeRush.Source.ActiveProperty.StartLine
    End Function
    Private Function OnField() As Boolean
        Dim BitTypes = New LanguageElementType() {LanguageElementType.BitFieldConst, LanguageElementType.BitFieldVariable}
        Return BitTypes.Contains(CodeRush.Source.Active.ElementType)
    End Function

#End Region

#Region "Move Code Actions"
#Region "Up / Down"
    'Private Sub cmdMoveCodeDown_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeDown.Execute
    '    Log.SendMsg("MoveCodeDown: Started")
    '    Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
    '    Dim Selection = CodeRush.Documents.ActiveTextView.Selection
    '    Dim DestRange As SourceRange
    '    Select Case True
    '        Case Selection.Height > 1 'Selection
    '            Selection.ExtendToWholeLines()
    '            DestRange = mSelectionMover.MoveSelectionDown(Selection.Range)
    '        Case CodeRush.Source.IsStatement(FirstNodeOnLine.GetParentStatementOrVariable) ' Statement 
    '            DestRange = mStatementMover.MoveStatementDown(FirstNodeOnLine)
    '        Case FirstNodeOnLine.GetParentClassInterfaceStructOrModule Is FirstNodeOnLine.Parent ' Member
    '            DestRange = mMemberMover.MoveMemberDown(FirstNodeOnLine)
    '        Case FirstNodeOnLine.Parent.ElementType = LanguageElementType.SourceFile ' Type
    '            DestRange = mMemberMover.MoveMemberDown(FirstNodeOnLine)
    '        Case Else
    '            Exit Sub
    '    End Select
    '    CodeRush.Documents.ActiveTextView.MakeVisible(DestRange)
    '    CodeRush.Documents.ActiveTextDocument.Format(DestRange)
    '    Log.SendMsg("MoveCodeDown: Finished")
    'End Sub
    Private Sub cmdMoveCodeDown_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeDown.Execute
        Log.SendMsg("MoveCodeDown: Started")
        Dim FirstNodeOnLine = GetFirstNodeOnCaretLine()
        Dim Selection = CodeRush.Documents.ActiveTextView.Selection
        Dim DestRange As SourceRange
        Select Case True
            Case Selection.Height > 1 'Selection
                Selection.ExtendToWholeLines()
                DestRange = mSelectionMover.MoveSelectionDown(Selection.Range)
            Case CaretOnStatement() ' Statement 
                DestRange = mStatementMover.MoveStatementDown(FirstNodeOnLine)
            Case CaretOnMember() ' Member
                DestRange = mMemberMover.MoveMemberDown(CodeRush.Source.ActiveMember) 'FirstNodeOnLine)
            Case CaretOnType()
                DestRange = mMemberMover.MoveMemberDown(FirstNodeOnLine)
            Case Else
                Exit Sub
        End Select
        CodeRush.Documents.ActiveTextView.MakeVisible(DestRange)
        CodeRush.Documents.ActiveTextDocument.Format(DestRange)
        Log.SendMsg("MoveCodeDown: Finished")
    End Sub
    Private Sub cmdMoveCodeUp_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeUp.Execute
        Log.SendMsg("MoveCodeUp: Started")
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Dim Selection = CodeRush.Documents.ActiveTextView.Selection
        Dim DestRange As SourceRange
        Select Case True
            Case MultipleLinesSelected()
                Selection.ExtendToWholeLines()
                DestRange = mSelectionMover.MoveSelectionUp(Selection.Range)
            Case CaretOnStatement()
                DestRange = mStatementMover.MoveStatementUp(FirstNodeOnLine)
            Case CaretOnMember()
                DestRange = mMemberMover.MoveMemberUp(CodeRush.Source.ActiveMember)
            Case CaretOnType()
                DestRange = mMemberMover.MoveMemberUp(FirstNodeOnLine)
            Case Else
                Exit Sub
        End Select
        CodeRush.Documents.ActiveTextView.MakeVisible(DestRange)
        CodeRush.Documents.ActiveTextDocument.Format(DestRange)
        Log.SendMsg("MoveCodeUp: Finished")
    End Sub
#End Region
#Region "Left / Right"
    Private Sub cmdMoveCodeRight_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeRight.Execute
        Log.SendMsg("MoveCodeRight: Started")
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Dim Selection = CodeRush.Documents.ActiveTextView.Selection
        Dim DestRange As SourceRange
        If Selection.Height > 1 Then
            Selection.ExtendToWholeLines()
            DestRange = mSelectionMover.MoveSelectionRight(Selection.Range)
        Else
            DestRange = mStatementMover.MoveStatementRight(FirstNodeOnLine.GetParentStatementOrVariable)
        End If
        CodeRush.Documents.ActiveTextView.MakeVisible(DestRange)
        CodeRush.Documents.ActiveTextDocument.Format(DestRange)

        Log.SendMsg("MoveCodeRight: Finished")
    End Sub
    Private Sub cmdMoveCodeLeft_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeLeft.Execute
        Log.SendMsg("MoveCodeLeft: Started")
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Dim Selection = CodeRush.Documents.ActiveTextView.Selection
        Dim DestRange As SourceRange
        If Selection.Height > 1 Then
            Selection.ExtendToWholeLines()
            DestRange = mSelectionMover.MoveSelectionLeft(Selection.Range)
        Else
            DestRange = mStatementMover.MoveStatementLeft(FirstNodeOnLine.GetParentStatementOrVariable)
        End If
        CodeRush.Documents.ActiveTextView.MakeVisible(DestRange)
        CodeRush.Documents.ActiveTextDocument.Format(DestRange)
        Log.SendMsg("MoveCodeLeft: Finished")
    End Sub
#End Region
#End Region
#Region "Move Caret Actions"
    Private Sub cmdMoveCaretUp_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCaretUp.Execute
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Select Case True
            Case FirstNodeOnLine.GetParentMethodOrProperty IsNot Nothing
                Call MoveCaretUp(FirstNodeOnLine.GetParentStatementOrVariable)
        End Select
    End Sub
    Private Sub cmdMoveCaretDown_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCaretDown.Execute
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Select Case True
            Case FirstNodeOnLine.GetParentMethodOrProperty IsNot Nothing
                Call MoveCaretDown(FirstNodeOnLine.GetParentStatementOrVariable)
        End Select
    End Sub
    Private Sub cmdMoveCaretLeft_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCaretLeft.Execute
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Call MoveCaretLeft(FirstNodeOnLine.GetParentStatementOrVariable)
    End Sub
    Private Sub cmdMoveCaretRight_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCaretRight.Execute
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
    Private Function GetFirstNodeOnCaretLine() As LanguageElement
        Return GetFirstNodeOnLine(CodeRush.Caret.Line)
    End Function
    Private Sub MoveCaretToElement(ByVal Destination As LanguageElement)
        If Destination IsNot Nothing Then
            CodeRush.Caret.MoveTo(Destination.Range.Start)
        End If
    End Sub
#End Region

#Region "Settings"
    Private Sub PlugIn1_OptionsChanged(ByVal ea As DevExpress.CodeRush.Core.OptionsChangedEventArgs) Handles Me.OptionsChanged
        If ea.OptionsPages.Contains(GetType(Options1)) Then
            LoadSettings()
        End If
    End Sub

    Public Sub LoadSettings()
        If Options1.Storage.ReadBoolean(Options1.SECTION_MOVE_CODE, Options1.SETTING_MOVE_SOURCE, True) Then
            SetupMovers(New MoverMoveSource)
        ElseIf Options1.Storage.ReadBoolean(Options1.SECTION_MOVE_CODE, Options1.SETTING_SWAP_ELEMENTS, False) Then
            SetupMovers(New MoverSwapElements)
        End If
    End Sub
#End Region

End Class
