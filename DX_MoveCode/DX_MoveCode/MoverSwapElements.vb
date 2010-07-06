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
    Implements ISelectionMover
#Region "IMemberMover"
    Public Function MoveMemberUp(ByVal Member As LanguageElement) As SourceRange Implements IMemberMover.MoveMemberUp
        Call SwapElements(Member.ToList, Member.PreviousRealCodeSibling.ToList)
    End Function
    Public Function MoveMemberDown(ByVal Member As LanguageElement) As SourceRange Implements IMemberMover.MoveMemberDown
        Call SwapElements(Member.ToList, Member.NextRealCodeSibling.ToList)
    End Function
#End Region
#Region "IStatementMover"
    Public Function MoveStatementDown(ByVal FirstNodeOnLine As LanguageElement) As SourceRange Implements IStatementMover.MoveStatementDown
        Call MoveElementDown(FirstNodeOnLine.GetParentStatementOrVariable)
    End Function
    Public Function MoveStatementUp(ByVal FirstNodeOnLine As LanguageElement) As SourceRange Implements IStatementMover.MoveStatementUp
        Call MoveElementUp(FirstNodeOnLine.GetParentStatementOrVariable)
    End Function
    Public Function MoveStatementRight(ByVal Statement As LanguageElement) As SourceRange Implements IStatementMover.MoveStatementRight
        If Statement IsNot Nothing Then
            Dim NextBlock = GetNextBlockSibling(Statement)
            If NextBlock IsNot Nothing Then
                Call MoveElementsToPoint(Statement.ToList, NextBlock.GetInsertPoint().LineStart)
            End If
        End If
    End Function
    Public Function MoveStatementLeft(ByVal Statement As LanguageElement) As SourceRange Implements IStatementMover.MoveStatementLeft
        If Statement IsNot Nothing Then
            Dim ParentBlock As Statement = TryCast(GetParentBlock(Statement), Statement)
            If ParentBlock IsNot Nothing Then
                Call MoveElementsToPoint(Statement.ToList, ParentBlock.GetFullBlockCutRange.Start)
            End If
        End If
    End Function
#End Region
#Region "ISelectionMover"
    Public Function MoveSelectionDown(ByVal Selection As DevExpress.CodeRush.StructuralParser.SourceRange) As SourceRange Implements ISelectionMover.MoveSelectionDown
        Dim Sibling As LanguageElement = Selection.GetNextCodeElement()
        Call SwapRanges(Selection, Sibling.GetFullBlockCutRange)
        CodeRush.Selection.SelectRange(Selection.OffsetRange(Sibling.Range.Height, 0))
    End Function
    Public Function MoveSelectionUp(ByVal Selection As DevExpress.CodeRush.StructuralParser.SourceRange) As SourceRange Implements ISelectionMover.MoveSelectionUp
        Dim Sibling As LanguageElement = Selection.GetPriorCodeElement()
        Call SwapRanges(Selection, Sibling.GetFullBlockCutRange)
        CodeRush.Selection.SelectRange(Selection.OffsetRange(-Sibling.Range.Height, 0))
    End Function

    Public Function MoveSelectionRight(ByVal Selection As DevExpress.CodeRush.StructuralParser.SourceRange) As SourceRange Implements ISelectionMover.MoveSelectionRight
        Call SwapRanges(Selection, Selection.GetNextBlockSibling.Range)
    End Function

    Public Function MoveSelectionLeft(ByVal Selection As DevExpress.CodeRush.StructuralParser.SourceRange) As SourceRange Implements ISelectionMover.MoveSelectionLeft
        Call SwapRanges(Selection, Selection.GetParentBlock.Range)
    End Function

#End Region
#Region "ElementMethods"
    Private Sub MoveElementUp(ByVal Element As LanguageElement)
        If Element IsNot Nothing Then
            Dim Sibling = PreviousRealCodeSibling(Element)
            If Sibling IsNot Nothing Then
                Dim DestPoint = Sibling.Range.Start
                Call SwapElements(Element.ToList, Sibling.ToList)
                CodeRush.Documents.ActiveTextView.MakeVisible(Element)
            End If
        End If
    End Sub
    Private Sub MoveElementDown(ByVal Element As LanguageElement)
        If Element IsNot Nothing Then
            Dim Sibling = NextRealCodeSibling(Element)
            If Sibling IsNot Nothing Then
                Call SwapElements(Element.ToList, Sibling.ToList)
                CodeRush.Documents.ActiveTextView.MakeVisible(Element)
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
#Region "Offset Methods"
    Private Sub MoveToOffsetPoint(ByVal DestRange As SourceRange, ByVal OffsetPoint As SourcePoint)
        CodeRush.Caret.MoveTo(DestRange.Start.OffsetPoint(OffsetPoint.Line, OffsetPoint.Offset))
    End Sub
    Private Function GetOffsetPoint(ByVal SourceRange As SourceRange) As SourcePoint
        Dim Offset = CodeRush.Caret.Offset - SourceRange.Start.Offset
        Dim Line = CodeRush.Caret.Line - SourceRange.Start.Line
        Return New SourcePoint(Line, Offset)
    End Function
#End Region
    Private Sub SwapRanges(ByVal SourceRange As SourceRange, ByVal DestRange As SourceRange)
        ' everything eventually calls this method, so here is where the swaps all actually occur
        ' therefore is is from here that we will need to reposition the caret.
        ' this requires that we store the original relative location of the caret to something
        ' Calculate Offset from SourceRange.Start to Caret.
        ' Precalc Points now as they are about to move.
        'Dim DestPoint As SourcePoint = DestRange.Start
        'Dim SourceStart As SourcePoint = SourceRange.Start
        'Dim OffsetPoint As SourcePoint = GetOffsetPoint(SourceRange)

        Dim Doc As TextDocument = CodeRush.Documents.ActiveTextDocument
        Dim RelativePoint = CodeRush.Caret.SourcePoint.RelativeTo(SourceRange.Start)
        Dim DestPoint = New SourcePoint(DestRange.Start)
        DestPoint.BindToCode(Doc, False)

        Dim SourceCode = Doc.GetText(SourceRange)
        Dim DestCode = Doc.GetText(DestRange)

        ' Move Sourcerange to DestRange.Start
        Doc.QueueReplace(DestRange, SourceCode)
        ' Move DestRange to SourceRange.Start
        Doc.QueueReplace(SourceRange, DestCode)
        Doc.ApplyQueuedEdits("Swap")
        CodeRush.Caret.MoveTo(DestPoint.OffsetPoint(RelativePoint.Y, RelativePoint.X))
        DestPoint.RemoveAllBindings()
        'Call MoveToOffsetPoint(DestRange, OffsetPoint)
    End Sub

    'Public Function MovingUp(ByVal SourceRange As SourceRange, ByVal DestRange As SourceRange) As Boolean
    '    Return SourceRange.Start.Line > DestRange.Start.Line
    'End Function
    Private Function MoveElementsToPoint(ByVal Elements As List(Of LanguageElement), ByVal Point As SourcePoint) As SourceRange
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

    End Function

End Class
