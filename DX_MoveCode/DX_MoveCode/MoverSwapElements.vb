Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices


Public Class MoverSwapElements
    Implements IStatementMover
    Implements IMemberMover
#Region "MemberMethods"
    Public Sub MoveMemberUp(ByVal Member As LanguageElement) Implements IMemberMover.MoveMemberUp
        Call SwapElements(Member.ToList, Member.PreviousRealCodeSibling.ToList)
    End Sub
    Public Sub MoveMemberDown(ByVal Member As LanguageElement) Implements IMemberMover.MoveMemberDown
        Call SwapElements(Member.ToList, Member.NextRealCodeSibling.ToList)
    End Sub
#End Region
#Region "StatementMethods"
    Public Sub MoveStatementUp(ByVal FirstNodeOnLine As LanguageElement) Implements IStatementMover.MoveStatementUp
        Call MoveElementUp(FirstNodeOnLine.GetParentStatementOrVariable)
    End Sub
    Public Sub MoveStatementDown(ByVal FirstNodeOnLine As LanguageElement) Implements IStatementMover.MoveStatementDown
        Call MoveElementDown(FirstNodeOnLine.GetParentStatementOrVariable)
    End Sub
    Public Sub MoveStatementLeft(ByVal Statement As LanguageElement) Implements IStatementMover.MoveStatementLeft
        If Statement IsNot Nothing Then
            Dim ParentBlock As Statement = TryCast(GetParentBlock(Statement), Statement)
            If ParentBlock IsNot Nothing Then
                Call MoveElementsToPoint(Statement.ToList, ParentBlock.GetFullBlockCutRange.Start)
            End If
        End If
    End Sub
    Public Sub MoveStatementRight(ByVal Statement As LanguageElement) Implements IStatementMover.MoveStatementRight
        If Statement IsNot Nothing Then
            Dim NextBlock = GetNextBlockSibling(Statement)
            If NextBlock IsNot Nothing Then
                Call MoveElementsToPoint(Statement.ToList, NextBlock.GetInsertPoint().LineStart)
            End If
        End If
    End Sub
#End Region
#Region "ElementMethods"
    Private Sub MoveElementUp(ByVal Element As LanguageElement)
        If Element IsNot Nothing Then
            Dim Sibling = PreviousRealCodeSibling(Element)
            If Sibling IsNot Nothing Then
                Call SwapElements(Element.ToList, Sibling.ToList)
                CodeRush.Documents.ActiveTextView.MakeVisible(Element)
            End If
        End If
    End Sub
    Private Sub MoveElementDown(ByVal Statement As LanguageElement)
        If Statement IsNot Nothing Then
            Dim Sibling = NextRealCodeSibling(Statement)
            If Sibling IsNot Nothing Then
                Call SwapElements(Statement.ToList, Sibling.ToList)
                CodeRush.Documents.ActiveTextView.MakeVisible(Statement)
            End If
        End If
    End Sub
#End Region
    Private Sub SwapElements(ByVal Source As List(Of LanguageElement), ByVal Dest As List(Of LanguageElement))
        If Source Is Nothing OrElse Source.Count = 0 Then
            Exit Sub
        End If
        If Dest Is Nothing OrElse Dest.Count = 0 Then
            Exit Sub
        End If
        Dim SourceRange = Source.Select(Function(s) s.Range).GetSuperRange()
        Dim DestRange = Dest.Select(Function(s) s.Range).GetSuperRange()

        SwapRanges(SourceRange, DestRange)
    End Sub
    Private Sub SwapRanges(ByVal SourceRange As SourceRange, ByVal DestRange As SourceRange)
        ' Calculate Offset from SourceRange.Start to Caret.
        Dim Offset = CodeRush.Caret.Offset - SourceRange.Start.Offset
        Dim Line = CodeRush.Caret.Line - SourceRange.Start.Line

        ' Precalc Points now as they are about to move.
        Dim DestPoint As SourcePoint = DestRange.Start
        Dim SourceStart As SourcePoint = SourceRange.Start
        Dim Doc As TextDocument = CodeRush.Documents.ActiveTextDocument

        Dim SourceCode = Doc.GetText(SourceRange)
        Dim DestCode = Doc.GetText(DestRange)
        ' Move Sourcerange to DestRange.Start
        Doc.QueueReplace(DestRange, SourceCode)
        ' Move DestRange to SourceRange.Start
        Doc.QueueReplace(SourceRange, DestCode)
        Doc.ApplyQueuedEdits("Swap")
        CodeRush.Caret.MoveTo(DestPoint.OffsetPoint(Line, Offset))
    End Sub
    Private Sub MoveElementsToPoint(ByVal Elements As List(Of LanguageElement), ByVal Point As SourcePoint)
        Dim ElementsStartLine = Elements.First.Range.Start.Line
        Dim LocalPoint = Point.Clone()
        LocalPoint.RemoveAllBindings()
        Dim DestinationLine = LocalPoint.Line
        Dim ElementsHeight As Integer = Elements.Sum(Function(le) le.Range.Height)
        Dim Doc = CodeRush.Documents.ActiveTextDocument
        For Each Element In Elements.ToReversedList
            Doc.Move(Element.GetFullBlockCutRange, LocalPoint, "")
            Doc.Format(New SourceRange(LocalPoint.LineStart.OffsetPoint(-1, 0), LocalPoint.LineStart.OffsetPoint(ElementsHeight, 0)))
        Next
        If DestinationLine > ElementsStartLine Then
            LocalPoint = LocalPoint.OffsetPoint(-1 * ElementsHeight, 0)
        End If
        Dim Offset As Integer = LocalPoint.Line.StartOfCode.Offset
        CodeRush.Caret.MoveTo(New SourcePoint(LocalPoint.Line, Offset))
    End Sub
End Class
