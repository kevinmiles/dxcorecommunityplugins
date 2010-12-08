Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser


Public Class MoverMoveSource
    Implements IStatementMover
    Implements IMemberMover
    Implements ISelectionMover

#Region "IMemberMover"
    Public Function MoveMemberUp(ByVal FirstNodeOnLine As LanguageElement) As SourceRange Implements IMemberMover.MoveMemberUp
        Return MoveSourceElementUp(FirstNodeOnLine, "")
    End Function
    Public Function MoveMemberDown(ByVal FirstNodeOnLine As LanguageElement) As SourceRange Implements IMemberMover.MoveMemberDown
        Return MoveSourceElementDown(FirstNodeOnLine, "")
    End Function
#End Region
#Region "IStatementMover"
    Public Function MoveStatementDown(ByVal Statement As LanguageElement) As SourceRange Implements IStatementMover.MoveStatementDown
        Return MoveSourceElementDown(Statement.GetParentStatementOrVariable, "")
    End Function
    Public Function MoveStatementUp(ByVal Statement As LanguageElement) As SourceRange Implements IStatementMover.MoveStatementUp
        Return MoveSourceElementUp(Statement.GetParentStatementOrVariable, "")
    End Function
    Public Function MoveStatementLeft(ByVal Statement As LanguageElement) As SourceRange Implements IStatementMover.MoveStatementLeft
        If Statement IsNot Nothing Then
            Dim ParentBlock As Statement = TryCast(GetParentBlock(Statement), Statement)
            If ParentBlock IsNot Nothing Then
                Dim Destination = ParentBlock.GetFullBlockCutRange.Start
                Dim MovingRange = Statement.ToList.GetSuperRange
                MoveRangeLeft(MovingRange, Destination, String.Empty)
                Return MovingRange.OffsetRange(Destination.Subtract(MovingRange.Start))
            End If
        End If
        Return Nothing
    End Function
    Public Function MoveStatementRight(ByVal Statement As LanguageElement) As SourceRange Implements IStatementMover.MoveStatementRight
        If Statement IsNot Nothing Then
            Dim NextBlock = GetNextBlockSibling(Statement)
            If NextBlock IsNot Nothing Then
                Dim Destination = GetInsertPoint(NextBlock).LineStart
                Dim MovingRange = Statement.ToList.GetSuperRange
                MoveRangeRight(MovingRange, Destination, "")
                Return MovingRange.OffsetRange(Destination.Subtract(MovingRange.Start))
            End If
        End If
        Return Nothing
    End Function
#End Region
#Region "ISelectionMover"
    Public Function MoveSelectionDown(ByVal Selection As SourceRange) As SourceRange Implements ISelectionMover.MoveSelectionDown
        ' Locate Next Sibling of first element on last line of selection
        Dim Sibling = Selection.GetNextCodeElement()
        If Sibling IsNot Nothing Then
            Dim Destination = Sibling.Range.End.Down.LineStart
            MoveRangeDownToDestination(Selection, Destination, "")
            Dim FinalRange As SourceRange = Selection.OffsetRange(Sibling.Range.Height, 0)
            CodeRush.Selection.SelectRange(FinalRange)
            Return FinalRange
        End If
        Return Nothing
    End Function

    Public Function MoveSelectionUp(ByVal Selection As SourceRange) As SourceRange Implements ISelectionMover.MoveSelectionUp
        Dim Sibling = Selection.GetPriorCodeElement()
        If Sibling IsNot Nothing Then
            Dim Destination = Sibling.Range.Start.LineStart
            MoveRangeUp(Selection, Destination, "")
            Dim FinalRange As SourceRange = Selection.OffsetRange(-Sibling.Range.Height, 0)
            CodeRush.Selection.SelectRange(FinalRange)
            Return FinalRange
        End If
        Return Nothing
    End Function
    Public Function MoveSelectionRight(ByVal Selection As SourceRange) As SourceRange Implements ISelectionMover.MoveSelectionRight
        Dim NextBlock As ParentingStatement = GetNextBlockSibling(Selection)
        If NextBlock IsNot Nothing Then
            Dim StartLine = Selection.Top.Line
            Dim Destination As SourcePoint = GetInsertPoint(NextBlock).LineStart
            MoveRangeRight(Selection, Destination, "")
            'CodeRush.Selection.SelectRange(Selection)
            Dim FinalRange As SourceRange = Selection.OffsetRange(NextBlock.BlockCodeRange.Start.Line - StartLine, 0)
            CodeRush.Selection.SelectRange(FinalRange)
            Return FinalRange
        End If
        Return Nothing
    End Function
    Public Function MoveSelectionLeft(ByVal Selection As SourceRange) As SourceRange Implements ISelectionMover.MoveSelectionLeft
        Dim ParentBlock As Statement = GetParentBlock(Selection)
        If ParentBlock IsNot Nothing Then
            Dim StartLine = Selection.Top.Line
            Dim Destination As SourcePoint = ParentBlock.GetFullBlockCutRange.Start
            MoveRangeLeft(Selection, Destination, String.Empty)
            'CodeRush.Selection.SelectRange(Selection)
            Dim FinalRange As SourceRange = Selection.OffsetRange(Destination.Line - StartLine, 0)
            CodeRush.Selection.SelectRange(FinalRange)
            Return FinalRange
        End If
        Return Nothing
    End Function
#End Region

#Region "ElementMethods"
    Private Function MoveSourceElementUp(ByVal SourceElement As LanguageElement, ByVal Comment As String) As SourceRange
        If SourceElement IsNot Nothing Then
            Dim Sibling = SourceElement.PreviousCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
            If Sibling IsNot Nothing Then
                Dim Destination = Sibling.Range.Start.LineStart()
                Dim MovingRange = SourceElement.ToList.GetSuperRange()
                MoveRangeUp(MovingRange, Destination, Comment)
                Return MovingRange.OffsetRange(Destination.Subtract(MovingRange.Start))
            End If
        End If
        Return Nothing
    End Function
    Private Function MoveSourceElementDown(ByVal SourceElement As LanguageElement, ByVal Comment As String) As SourceRange
        If SourceElement Is Nothing Then
            Return Nothing
        End If
        Dim Sibling = SourceElement.NextCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
        If Sibling Is Nothing Then
            ' There is no next sibling. This is the last Sibling
            Return Nothing
        End If
        ' Sibling Found
        Dim Destination = Sibling.Range.End.Down.LineStart
        Dim MovingRange = SourceElement.ToList.GetSuperRange()
        MoveRangeDownToDestination(MovingRange, Destination, Comment)
        Return MovingRange.OffsetRange(Destination.Subtract(MovingRange.Start))
    End Function



    'Private Function AddRanges(ByVal SiblingRange As SourceRange, ByVal StatementRange As SourceRange) As SourceRange
    '    Dim StartLine As Integer = Math.Min(SiblingRange.Start.Line, StatementRange.Start.Line)
    '    Dim EndLine As Integer = Math.Max(SiblingRange.End.Line, StatementRange.End.Line)
    '    Return New SourceRange(StartLine, 1, EndLine + 1, 1)
    'End Function
#End Region

#Region "Range Methods"
    Private Function MoveRangeUp(ByVal MovingRange As SourceRange, ByVal Destination As SourcePoint, ByVal Comment As String) As SourceRange
        'Record Current position of Caret relative to MovingRange
        Dim CaretDx = CodeRush.Caret.Offset - MovingRange.Start.Offset
        Dim CaretDy = CodeRush.Caret.Line - MovingRange.Start.Line
        ' Perform Move
        CodeRush.Documents.ActiveTextDocument.Move(MovingRange, Destination, Comment)
        ' Reposition Caret relative to Destination SourcePoint
        CodeRush.Caret.MoveTo(Destination.OffsetPoint(CaretDy, CaretDx))
        ' Return calculated destination range.
        Return MovingRange.OffsetRange(Destination.Subtract(MovingRange.Start))
    End Function
    Private Function MoveRangeDownToDestination(ByVal MovingRange As SourceRange, _
                                                ByVal Destination As SourcePoint, _
                                                ByVal Comment As String) As SourceRange
        ' Record Current position of Caret relative to MovingRange
        Dim CaretDx = CodeRush.Caret.Offset - MovingRange.Start.Offset
        Dim CaretDy = CodeRush.Caret.Line - MovingRange.Start.Line
        ' Perform Move
        CodeRush.Documents.ActiveTextDocument.Move(MovingRange, Destination, Comment)
        ' Reposition Caret relative to Destination SourcePoint
        CodeRush.Caret.MoveTo(Destination.OffsetPoint(CaretDy, CaretDx).Up(MovingRange.Height - 1))
        ' Return calculated destination range.
        Return MovingRange.OffsetRange(Destination.Subtract(MovingRange.Start))
    End Function
    Private Function MoveRangeLeft(ByVal MovingRange As SourceRange, ByVal Destination As SourcePoint, ByVal Comment As String) As SourceRange
        Dim Offset = CodeRush.Caret.Offset - MovingRange.Start.Offset
        Dim Line = CodeRush.Caret.Line - MovingRange.Start.Line
        CodeRush.Documents.ActiveTextDocument.Move(MovingRange, Destination, Comment)
        CodeRush.Caret.MoveTo(Destination.OffsetPoint(Line, Offset - CodeRush.Documents.ActiveTextDocument.IndentSize))
        Return MovingRange.OffsetRange(Destination.Subtract(MovingRange.Start))
    End Function
    Private Function MoveRangeRight(ByVal MovingRange As SourceRange, ByVal Destination As SourcePoint, ByVal Comment As String) As SourceRange
        Dim Offset = CodeRush.Caret.Offset - MovingRange.Start.Offset
        Dim Line = CodeRush.Caret.Line - MovingRange.Start.Line
        CodeRush.Documents.ActiveTextDocument.Move(MovingRange, Destination, Comment)
        CodeRush.Caret.MoveTo(Destination.OffsetPoint(Line, Offset + CodeRush.Documents.ActiveTextDocument.IndentSize).Up(MovingRange.Height - 1))
        Return MovingRange.OffsetRange(Destination.Subtract(MovingRange.Start))
    End Function
#End Region
End Class
