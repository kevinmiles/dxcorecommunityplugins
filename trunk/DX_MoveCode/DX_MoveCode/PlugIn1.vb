Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

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

    Private Sub cmdMoveCodeUp_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles cmdMoveCodeUp.Execute
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Select Case True
            Case FirstNodeOnLine.GetParentMethodOrProperty IsNot Nothing
                Call MoveElementUp(FirstNodeOnLine)
                'Case FirstNodeOnLine.GetParentClassInterfaceStructOrModule IsNot Nothing
                '    Call MoveElementUp(FirstNodeOnLine)
        End Select
    End Sub

    Private Sub cmdMoveCodeDown_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles cmdMoveCodeDown.Execute
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Select Case True
            Case FirstNodeOnLine.GetParentMethodOrProperty IsNot Nothing
                Call MoveElementDown(FirstNodeOnLine)
                'Case FirstNodeOnLine.GetParentClassInterfaceStructOrModule IsNot Nothing
                '    Call MoveElementDown(FirstNodeOnLine)
        End Select
    End Sub
    Private Shared Function GetFirstNodeOnLine(ByVal Line As Integer) As LanguageElement
        Dim Doc = CodeRush.Documents.ActiveTextDocument
        Dim FirstAlphaChar As Integer = CodeRush.StrUtil.GetLeadingWhiteSpaceCharCount(CodeRush.Documents.ActiveLine) + 1
        Return Doc.GetNodeAt(Line, FirstAlphaChar)
    End Function
    Private Sub cmdMoveCodeRight_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles cmdMoveCodeRight.Execute
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Call MoveStatementRight(FirstNodeOnLine.GetParentStatementOrVariable)
    End Sub
    Private Sub cmdMoveCodeLeft_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles cmdMoveCodeLeft.Execute
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Call MoveStatementLeft(FirstNodeOnLine.GetParentStatementOrVariable)
    End Sub

#Region "Statement Movement"
    Private Sub MoveElementUp(ByVal Statement As LanguageElement)
        If Statement IsNot Nothing Then
            Dim Sibling = Statement.PreviousCodeSibling
            If Sibling IsNot Nothing Then
                Call SwapStatements(Statement.ToList, Sibling)
            End If
        End If
    End Sub
    Private Sub MoveElementDown(ByVal Statement As LanguageElement)
        If Statement IsNot Nothing Then
            Dim Sibling = Statement.NextCodeSibling
            If Sibling IsNot Nothing Then
                Call SwapStatements(Statement.ToList, Sibling)
            End If
        End If
    End Sub
    Private Sub MoveStatementLeft(ByVal Statement As LanguageElement)
        If Statement IsNot Nothing Then
            Dim ParentBlock As Statement = TryCast(GetParentBlock(Statement), Statement)
            If ParentBlock IsNot Nothing Then
                Call MoveElementsToPoint(Statement.ToList, ParentBlock.GetFullBlockCutRange.Start)
            End If
        End If
    End Sub
    Private Sub MoveStatementRight(ByVal Statement As LanguageElement)
        If Statement IsNot Nothing Then
            ' Locate next block
            Dim NextBlock = GetNextBlockSibling(Statement)
            If NextBlock IsNot Nothing Then
                Call MoveElementsToPoint(Statement.ToList, NextBlock.BlockCodeRange.Start.StartOfLine)
            End If
        End If
    End Sub
#End Region

#Region "Block Location"
    Public Function GetParentBlock(ByVal Statement As LanguageElement) As LanguageElement
        Dim ParentBlock As LanguageElement = Statement
        Do
            ParentBlock = ParentBlock.Parent
        Loop Until TypeOf ParentBlock Is Statement Or TypeOf ParentBlock Is Method Or TypeOf ParentBlock Is [Class]
        Return ParentBlock
    End Function
    Private Function GetNextBlockSibling(ByVal Statement As LanguageElement) As ParentingStatement
        Dim Sibling = Statement
        Do
            Sibling = Sibling.NextSibling
        Loop Until Sibling Is Nothing OrElse TypeOf Sibling Is ParentingStatement
        Return CType(Sibling, ParentingStatement)
    End Function
#End Region

    Public Sub MoveElementsToPoint(ByVal Elements As List(Of LanguageElement), ByVal Point As SourcePoint)
        Dim ElementsStartLine = Elements.First.Range.Start.Line
        Dim DestinationLine = Point.Line
        Dim ElementsHeight As Integer = Elements.Sum(Function(le) le.Range.Height)
        Dim RelativeCaretOffset = CodeRush.Caret.Offset
        Dim Doc = CodeRush.Documents.ActiveTextDocument
        For Each Element In Elements.ToReversedList
            Doc.Move(Element.GetFullBlockCutRange, Point, "")
            Doc.Format(Element.Range.MoveABS(Point.StartOfLine))
        Next
        If DestinationLine > ElementsStartLine Then
            Point = Point.OffsetPoint(-1 * ElementsHeight, 0)
        End If
        CodeRush.Caret.MoveTo(New SourcePoint(Point.Line, RelativeCaretOffset))
    End Sub
    Public Sub SwapStatements(ByVal Elements As List(Of LanguageElement), ByVal Sibling As LanguageElement)
        Dim ElementsHeight As Integer = Elements.Sum(Function(le) le.Range.Height)
        Dim RelativeCaretOffset = CodeRush.Caret.Offset
        Dim ElementsStartLine = Elements.First.Range.Start.Line
        Dim SiblingStartLine = Sibling.Range.Start.Line
        Dim LastElementLocation As SourcePoint = Nothing
        Dim SiblingDestination As SourcePoint = Nothing

        For Each Element In Elements
            Dim Doc = CodeRush.Documents.ActiveTextDocument
            Dim SiblingRange = Sibling.GetFullBlockCutRange
            Dim ElementRange = Element.GetFullBlockCutRange
            Dim CombinedRange = AddRanges(SiblingRange, ElementRange)
            SiblingDestination = If(SiblingStartLine > ElementsStartLine, ElementRange.Start, ElementRange.End)
            Doc.Move(SiblingRange, SiblingDestination, "")
            Doc.Format(CombinedRange)
        Next
    End Sub
    Public Function AddRanges(ByVal SiblingRange As SourceRange, ByVal StatementRange As SourceRange) As SourceRange
        Dim StartLine As Integer = Math.Min(SiblingRange.Start.Line, StatementRange.Start.Line)
        Dim EndLine As Integer = Math.Max(SiblingRange.End.Line, StatementRange.End.Line)
        Return New SourceRange(StartLine, 1, EndLine + 1, 1)
    End Function
End Class