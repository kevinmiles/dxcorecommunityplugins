Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices


Public Class MoverMoveSource
    Implements IStatementMover
    Implements IMemberMover
    Implements ISelectionMover

#Region "MemberMethods"
    Public Sub MoveMemberUp(ByVal FirstNodeOnLine As LanguageElement) Implements IMemberMover.MoveMemberUp
        Call MoveElementUp(FirstNodeOnLine, "")
    End Sub
    Public Sub MoveMemberDown(ByVal FirstNodeOnLine As LanguageElement) Implements IMemberMover.MoveMemberDown
        Call MoveElementDown(FirstNodeOnLine, "")
    End Sub
#End Region
#Region "StatementMethods"

    Public Sub MoveStatementDown(ByVal Statement As LanguageElement) Implements IStatementMover.MoveStatementDown
        Try
            Call MoveElementDown(Statement.GetParentStatementOrVariable, "")
        Catch ex As Exception
            Console.WriteLine(ex)

        End Try
    End Sub
    Public Sub MoveStatementUp(ByVal Statement As LanguageElement) Implements IStatementMover.MoveStatementUp
        Try
            Call MoveElementUp(Statement.GetParentStatementOrVariable, "")
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub
    Public Sub MoveStatementLeft(ByVal Statement As LanguageElement) Implements IStatementMover.MoveStatementLeft
        If Statement IsNot Nothing Then
            Dim ParentBlock As Statement = TryCast(GetParentBlock(Statement), Statement)
            If ParentBlock IsNot Nothing Then
                Dim Destination As SourcePoint = ParentBlock.GetFullBlockCutRange.Start
                Dim MovingRange As SourceRange = Statement.ToList.GetSuperRange
                MoveRangeLeft(MovingRange, Destination, String.Empty)
            End If
        End If
    End Sub
    Public Sub MoveStatementRight(ByVal Statement As LanguageElement) Implements IStatementMover.MoveStatementRight
        If Statement IsNot Nothing Then
            Dim NextBlock = GetNextBlockSibling(Statement)
            If NextBlock IsNot Nothing Then
                Dim Destination As SourcePoint = GetInsertPoint(NextBlock).LineStart
                Dim MovingRange As SourceRange = Statement.ToList.GetSuperRange
                MoveRangeRight(MovingRange, Destination, "")
            End If
        End If
    End Sub
#End Region
#Region "Selection Methods"

    Public Sub MoveSelectionDown(ByVal Selection As DevExpress.CodeRush.StructuralParser.SourceRange) Implements ISelectionMover.MoveSelectionDown
        ' Locate Next Sibling of first element on last line of selection
        Dim SourceElement = GetFirstNodeOnLine(Selection.End.Line - 1)
        Dim Sibling = SourceElement.NextCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
        If Sibling IsNot Nothing Then
            Dim Destination = Sibling.Range.End.Down.LineStart
            MoveRangeDown(Selection, Destination, "")
            CodeRush.Selection.SelectRange(Selection.OffsetRange(Sibling.Range.Height, 0))
        End If
    End Sub

    Public Sub MoveSelectionUp(ByVal Selection As DevExpress.CodeRush.StructuralParser.SourceRange) Implements ISelectionMover.MoveSelectionUp
        Dim SourceElement = GetFirstNodeOnLine(Selection.Start.Line)
        Dim Sibling = SourceElement.PreviousCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
        If Sibling IsNot Nothing Then
            Dim Destination = Sibling.Range.Start.LineStart
            MoveRangeUp(Selection, Destination, "")
            CodeRush.Selection.SelectRange(Selection.OffsetRange(-Sibling.Range.Height, 0))
        End If
    End Sub
    Public Sub MoveSelectionRight(ByVal Selection As DevExpress.CodeRush.StructuralParser.SourceRange) Implements ISelectionMover.MoveSelectionRight
        Dim LastStatementInSelection = GetFirstNodeOnLine(Selection.End.Line - 1)
        Dim NextBlock = GetNextBlockSibling(LastStatementInSelection)
        If NextBlock IsNot Nothing Then
            Dim StartLine = Selection.Top.Line
            Dim Destination As SourcePoint = GetInsertPoint(NextBlock).LineStart
            MoveRangeRight(Selection, Destination, "")
            'CodeRush.Selection.SelectRange(Selection)
            CodeRush.Selection.SelectRange(Selection.OffsetRange(NextBlock.BlockCodeRange.Start.Line - StartLine, 0))
        End If
    End Sub
    Public Sub MoveSelectionLeft(ByVal Selection As DevExpress.CodeRush.StructuralParser.SourceRange) Implements ISelectionMover.MoveSelectionLeft
        Dim FirstStatementInSelection = GetFirstNodeOnLine(Selection.Start.Line)
        Dim ParentBlock As Statement = TryCast(GetParentBlock(FirstStatementInSelection), Statement)
        If ParentBlock IsNot Nothing Then
            Dim StartLine = Selection.Top.Line
            Dim Destination As SourcePoint = ParentBlock.GetFullBlockCutRange.Start
            MoveRangeLeft(Selection, Destination, String.Empty)
            'CodeRush.Selection.SelectRange(Selection)
            CodeRush.Selection.SelectRange(Selection.OffsetRange(Destination.Line - StartLine, 0))
        End If

    End Sub
#End Region

#Region "ElementMethods"
    Private Sub MoveElementUp(ByVal Element As LanguageElement, ByVal Comment As String)
        If Element IsNot Nothing Then
            Dim Sibling = Element.PreviousCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
            If Sibling IsNot Nothing Then
                Dim Destination = Sibling.Range.Start.LineStart()
                Dim MovingRange = Element.ToList.GetSuperRange()
                MoveRangeUp(MovingRange, Destination, Comment)
            End If
        End If
    End Sub
    Private Sub MoveElementDown(ByVal Element As LanguageElement, ByVal Comment As String)
        If Element IsNot Nothing Then
            Dim Sibling = Element.NextCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
            If Sibling IsNot Nothing Then
                Dim Destination = Sibling.Range.End.Down.LineStart
                Dim MovingRange = Element.ToList.GetSuperRange()
                MoveRangeDown(MovingRange, Destination, Comment)
            End If
        End If
    End Sub



    'Private Function AddRanges(ByVal SiblingRange As SourceRange, ByVal StatementRange As SourceRange) As SourceRange
    '    Dim StartLine As Integer = Math.Min(SiblingRange.Start.Line, StatementRange.Start.Line)
    '    Dim EndLine As Integer = Math.Max(SiblingRange.End.Line, StatementRange.End.Line)
    '    Return New SourceRange(StartLine, 1, EndLine + 1, 1)
    'End Function
#End Region

#Region "Range Methods"
    Private Sub MoveRangeUp(ByVal MovingRange As SourceRange, ByVal Destination As SourcePoint, ByVal Comment As String)
        Dim Offset = CodeRush.Caret.Offset - MovingRange.Start.Offset
        Dim Line = CodeRush.Caret.Line - MovingRange.Start.Line
        CodeRush.Documents.ActiveTextDocument.Move(MovingRange, Destination, Comment)
        CodeRush.Documents.ActiveTextView.MakeVisible(Destination)
        CodeRush.Caret.MoveTo(Destination.OffsetPoint(Line, Offset))
    End Sub
    Private Sub MoveRangeDown(ByVal MovingRange As SourceRange, ByVal Destination As SourcePoint, ByVal Comment As String)
        Dim Offset = CodeRush.Caret.Offset - MovingRange.Start.Offset
        Dim Line = CodeRush.Caret.Line - MovingRange.Start.Line
        CodeRush.Documents.ActiveTextDocument.Move(MovingRange, Destination, Comment)
        CodeRush.Documents.ActiveTextView.MakeVisible(Destination)
        CodeRush.Caret.MoveTo(Destination.OffsetPoint(Line, Offset).Up(MovingRange.Height - 1))
    End Sub
    Private Sub MoveRangeLeft(ByVal MovingRange As SourceRange, ByVal Destination As SourcePoint, ByVal Comment As String)
        Dim Offset = CodeRush.Caret.Offset - MovingRange.Start.Offset
        Dim Line = CodeRush.Caret.Line - MovingRange.Start.Line
        CodeRush.Documents.ActiveTextDocument.Move(MovingRange, Destination, Comment)
        CodeRush.Documents.ActiveTextView.MakeVisible(Destination)
        CodeRush.Caret.MoveTo(Destination.OffsetPoint(Line, Offset - CodeRush.Documents.ActiveTextDocument.IndentSize))
    End Sub
    Private Sub MoveRangeRight(ByVal MovingRange As SourceRange, ByVal Destination As SourcePoint, ByVal Comment As String)
        Dim Offset = CodeRush.Caret.Offset - MovingRange.Start.Offset
        Dim Line = CodeRush.Caret.Line - MovingRange.Start.Line
        CodeRush.Documents.ActiveTextDocument.Move(MovingRange, Destination, Comment)
        CodeRush.Documents.ActiveTextView.MakeVisible(Destination)
        CodeRush.Caret.MoveTo(Destination.OffsetPoint(Line, Offset + CodeRush.Documents.ActiveTextDocument.IndentSize).Up(MovingRange.Height - 1))
    End Sub
#End Region
End Class
