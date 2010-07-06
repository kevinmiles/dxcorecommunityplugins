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
    Public Function MoveMemberUp(ByVal MemberOrType As LanguageElement) As SourceRange Implements IMemberMover.MoveMemberUp
        Return MoveElementUp(MemberOrType)
    End Function
    Public Function MoveMemberDown(ByVal MemberOrType As LanguageElement) As SourceRange Implements IMemberMover.MoveMemberDown
        Return MoveElementDown(MemberOrType)
    End Function
#End Region
#Region "StatementMethods"
    Public Function MoveStatementUp(ByVal FirstNodeOnLine As LanguageElement) As SourceRange Implements IStatementMover.MoveStatementUp
        Return MoveElementUp(FirstNodeOnLine.GetParentStatementOrVariable)
    End Function
    Public Function MoveStatementDown(ByVal FirstNodeOnLine As LanguageElement) As SourceRange Implements IStatementMover.MoveStatementDown
        Return MoveElementDown(FirstNodeOnLine.GetParentStatementOrVariable)
    End Function
    Public Function MoveStatementLeft(ByVal Statement As LanguageElement) As SourceRange Implements IStatementMover.MoveStatementLeft
        If Statement IsNot Nothing Then
            Dim ParentBlock As Statement = TryCast(GetParentBlock(Statement), Statement)
            If ParentBlock IsNot Nothing Then
                Return MoveElementsToPoint(Statement.ToList, ParentBlock.GetFullBlockCutRange.Start)
            End If
        End If
        Return Nothing
    End Function
    Public Function MoveStatementRight(ByVal Statement As LanguageElement) As SourceRange Implements IStatementMover.MoveStatementRight
        If Statement IsNot Nothing Then
            Dim NextBlock = GetNextBlockSibling(Statement)
            If NextBlock IsNot Nothing Then
                Return MoveElementsToPoint(Statement.ToList, GetInsertPoint(NextBlock).LineStart)
            End If
        End If
        Return Nothing
    End Function
#End Region
#Region "ElementMethods"
    Private Function MoveElementUp(ByVal Element As LanguageElement) As SourceRange
        If Element IsNot Nothing Then
            Dim Sibling = Element.PreviousCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
            If Sibling IsNot Nothing Then
                Dim DestRange = SwapStatements(Element.ToList, Sibling)
                CodeRush.Documents.ActiveTextView.MakeVisible(DestRange)
            End If
        End If
        Return Nothing
    End Function
    Private Function MoveElementDown(ByVal Element As LanguageElement) As SourceRange
        If Element IsNot Nothing Then
            Dim Sibling = Element.NextCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
            If Sibling IsNot Nothing Then
                Dim DestRange = SwapStatements(Element.ToList, Sibling)
                CodeRush.Documents.ActiveTextView.MakeVisible(DestRange)
            End If
        End If
        Return Nothing
    End Function

#End Region
    Private Function SwapStatements(ByVal Elements As List(Of LanguageElement), ByVal Sibling As LanguageElement) As SourceRange
        Dim ElementsRangeStartPoint As SourcePoint = Elements.First.Range.Start
        Dim ElementsStartLine = ElementsRangeStartPoint.Line
        Dim SiblingStartRange As SourceRange = Sibling.Range
        Dim SiblingStartPoint = SiblingStartRange.Start
        Dim SiblingStartLine = SiblingStartPoint.Line
        Dim ElementMoveVector = SiblingStartPoint.Subtract(ElementsRangeStartPoint)
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
        Return SiblingStartRange.OffsetRange(ElementMoveVector)
    End Function
    Private Function MoveElementsToPoint(ByVal Elements As List(Of LanguageElement), ByVal Point As SourcePoint) As SourceRange
        Dim ElementsStartRange = Elements.GetSuperRange
        Dim ElementMoveVector = Point.Subtract(ElementsStartRange.Start)
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
        Return ElementsStartRange.OffsetRange(ElementMoveVector)

    End Function
    Private Function AddRanges(ByVal SiblingRange As SourceRange, ByVal StatementRange As SourceRange) As SourceRange
        Dim StartLine As Integer = Math.Min(SiblingRange.Start.Line, StatementRange.Start.Line)
        Dim EndLine As Integer = Math.Max(SiblingRange.End.Line, StatementRange.End.Line)
        Return New SourceRange(StartLine, 1, EndLine + 1, 1)
    End Function
End Class
