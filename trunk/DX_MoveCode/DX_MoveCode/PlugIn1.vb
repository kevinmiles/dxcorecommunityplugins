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

    'Done: Move Statements Up and down
    'Done: Move Statements Left and Right
    'Done: Move Methods Up and Down

    'TODO: Add functions to work with Selections
    'TODO: Add option to use TargerPicker


#Region "Move Code Actions"
    Private Sub cmdMoveCodeUp_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeUp.Execute
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Dim Selection = CodeRush.Documents.ActiveTextView.Selection
        Select Case True
            Case Selection.Height > 1
                Call Selection.ExtendToWholeLines()

            Case CodeRush.Source.IsStatement(FirstNodeOnLine.GetParentStatementOrVariable)
                Call MoveElementUp(FirstNodeOnLine.GetParentStatementOrVariable)
            Case FirstNodeOnLine.GetParentClassInterfaceStructOrModule Is FirstNodeOnLine.Parent
                Call MoveElementUp(FirstNodeOnLine)
        End Select
    End Sub
    Private Sub cmdMoveCodeDown_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeDown.Execute
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Select Case True
            Case CodeRush.Source.IsStatement(FirstNodeOnLine.GetParentStatementOrVariable)
                Call MoveElementDown(FirstNodeOnLine.GetParentStatementOrVariable)
            Case FirstNodeOnLine.GetParentClassInterfaceStructOrModule Is FirstNodeOnLine.Parent
                Call MoveElementDown(FirstNodeOnLine)
        End Select
    End Sub

    Private Sub cmdMoveCodeRight_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeRight.Execute
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Call MoveStatementRight(FirstNodeOnLine.GetParentStatementOrVariable)
    End Sub
    Private Sub cmdMoveCodeLeft_Execute(ByVal ea As ExecuteEventArgs) Handles cmdMoveCodeLeft.Execute
        Dim FirstNodeOnLine = GetFirstNodeOnLine(CodeRush.Caret.Line)
        Call MoveStatementLeft(FirstNodeOnLine.GetParentStatementOrVariable)
    End Sub
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
#Region "Element Movement"
    Private Sub MoveElementTo(ByVal Element As LanguageElement, ByVal Line As Integer)

    End Sub
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
            Dim NextBlock = GetNextBlockSibling(Statement)
            If NextBlock IsNot Nothing Then
                Call MoveElementsToPoint(Statement.ToList, GetInsertPoint(NextBlock).StartOfLine)
            End If
        End If
    End Sub
    Private Function GetInsertPoint(ByVal NextBlock As ParentingStatement) As SourcePoint
        Dim InsertPoint As SourcePoint
        If NextBlock.BlockType = DelimiterBlockType.Brace Then
            If NextBlock.BlockRange.Start.Line = NextBlock.BlockRange.End.Line Then
                'Braces are on same line - Seperate with newline
                CodeRush.Documents.ActiveTextDocument.InsertText(NextBlock.BlockRange.Start, Environment.NewLine)
            End If
            InsertPoint = NextBlock.BlockRange.Start.OffsetPoint(1, 0).StartOfLine
        Else
            InsertPoint = NextBlock.BlockRange.Start.StartOfLine
        End If
        Return InsertPoint
    End Function

#End Region

#Region "Utility"
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

    Private Function GetFirstNodeOnLine(ByVal Line As Integer) As LanguageElement
        Return CodeRush.Documents.ActiveTextDocument.GetNodeAt(StartOfCode(Line))
    End Function
    Private Shared Function StartOfCode(ByVal LineNo As Integer) As SourcePoint
        Dim Line As String = CodeRush.Documents.GetLineAt(CodeRush.Documents.ActiveTextDocument, LineNo)
        Dim FirstAlphaChar As Integer = CodeRush.StrUtil.GetLeadingWhiteSpaceCharCount(Line) + 1
        Return New SourcePoint(LineNo, FirstAlphaChar)
    End Function
    Private Sub MoveCaretToElement(ByVal Destination As LanguageElement)
        If Destination IsNot Nothing Then
            CodeRush.Caret.MoveTo(Destination.Range.Start)
        End If
    End Sub
    Public Sub MoveElementsToPoint(ByVal Elements As List(Of LanguageElement), ByVal Point As SourcePoint)
        Dim ElementsStartLine = Elements.First.Range.Start.Line
        Dim LocalPoint = Point.Clone()
        LocalPoint.RemoveAllBindings()
        Dim DestinationLine = LocalPoint.Line
        Dim ElementsHeight As Integer = Elements.Sum(Function(le) le.Range.Height)
        Dim Doc = CodeRush.Documents.ActiveTextDocument
        For Each Element In Elements.ToReversedList
            Doc.Move(Element.GetFullBlockCutRange, LocalPoint, "")
            Doc.Format(New SourceRange(LocalPoint.StartOfLine.OffsetPoint(-1, 0), LocalPoint.StartOfLine.OffsetPoint(ElementsHeight, 0)))
        Next
        If DestinationLine > ElementsStartLine Then
            LocalPoint = LocalPoint.OffsetPoint(-1 * ElementsHeight, 0)
        End If
        Dim Offset As Integer = StartOfCode(LocalPoint.Line).Offset
        CodeRush.Caret.MoveTo(New SourcePoint(LocalPoint.Line, Offset))
    End Sub
    Public Sub SwapStatements(ByVal Elements As List(Of LanguageElement), ByVal Sibling As LanguageElement)
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
    Public Function AddRanges(ByVal SiblingRange As SourceRange, ByVal StatementRange As SourceRange) As SourceRange
        Dim StartLine As Integer = Math.Min(SiblingRange.Start.Line, StatementRange.Start.Line)
        Dim EndLine As Integer = Math.Max(SiblingRange.End.Line, StatementRange.End.Line)
        Return New SourceRange(StartLine, 1, EndLine + 1, 1)
    End Function
#End Region

End Class