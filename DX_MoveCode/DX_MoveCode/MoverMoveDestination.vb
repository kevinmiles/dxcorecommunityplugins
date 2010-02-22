Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices


Public Class MoverMoveDestination
    Implements IStatementMover
    Implements IMemberMover

#Region "MemberMethods"
    Public Sub MoveMemberUp(ByVal FirstNodeOnLine As LanguageElement) Implements IMemberMover.MoveMemberUp
        Call MoveElementUp(FirstNodeOnLine)
    End Sub
    Public Sub MoveMemberDown(ByVal FirstNodeOnLine As LanguageElement) Implements IMemberMover.MoveMemberDown
        Call MoveElementDown(FirstNodeOnLine)
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
                Call MoveElementsToPoint(Statement.ToList, GetInsertPoint(NextBlock).LineStart)
            End If
        End If
    End Sub
#End Region
#Region "ElementMethods"
    Private Sub MoveElementUp(ByVal Element As LanguageElement)
        If Element IsNot Nothing Then
            Dim Sibling = Element.PreviousCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
            If Sibling IsNot Nothing Then
                Call SwapStatements(Element.ToList, Sibling)
                CodeRush.Documents.ActiveTextView.MakeVisible(Element)
            End If
        End If
    End Sub
    Private Sub MoveElementDown(ByVal Statement As LanguageElement)
        If Statement IsNot Nothing Then
            Dim Sibling = Statement.NextCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
            If Sibling IsNot Nothing Then
                Call SwapStatements(Statement.ToList, Sibling)
                CodeRush.Documents.ActiveTextView.MakeVisible(Statement)
            End If
        End If
    End Sub

#End Region
    Private Sub SwapStatements(ByVal Elements As List(Of LanguageElement), ByVal Sibling As LanguageElement)
        Dim ElementsStartLine = Elements.First.Range.Start.Line
        Dim SiblingStartLine = Sibling.Range.Start.Line
        Dim SiblingDestination As SourcePoint = Nothing
        For Each Element In Elements
            Dim Doc = CodeRush.Documents.ActiveTextDocument
            Dim SiblingRange = Sibling.GetFullBlockCutRange(BlockElements.AllLeadingWhiteSpaces Or BlockElements.TrailingWhiteSpace Or BlockElements.Region Or BlockElements.AllSupportElements Or BlockElements.XmlDocComments Or BlockElements.Attributes)
            Dim ElementRange = Element.GetFullBlockCutRange(BlockElements.AllLeadingWhiteSpaces Or BlockElements.TrailingWhiteSpace Or BlockElements.Region Or BlockElements.AllSupportElements Or BlockElements.XmlDocComments Or BlockElements.Attributes)
            Dim CombinedRange = AddRanges(SiblingRange, ElementRange)
            SiblingDestination = If(SiblingStartLine > ElementsStartLine, ElementRange.Start, ElementRange.End)
            Doc.Move(SiblingRange, SiblingDestination, "")
            Doc.Format(CombinedRange)
        Next
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
    Private Function AddRanges(ByVal SiblingRange As SourceRange, ByVal StatementRange As SourceRange) As SourceRange
        Dim StartLine As Integer = Math.Min(SiblingRange.Start.Line, StatementRange.Start.Line)
        Dim EndLine As Integer = Math.Max(SiblingRange.End.Line, StatementRange.End.Line)
        Return New SourceRange(StartLine, 1, EndLine + 1, 1)
    End Function
End Class
