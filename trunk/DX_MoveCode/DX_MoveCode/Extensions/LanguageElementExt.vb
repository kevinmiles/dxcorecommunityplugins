Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module LanguageElementExt
    <Extension()> _
    Public Function GetParentBlock(ByVal Statement As LanguageElement) As LanguageElement
        Dim ParentBlock As LanguageElement = Statement
        Do
            ParentBlock = ParentBlock.Parent
        Loop Until TypeOf ParentBlock Is Statement Or TypeOf ParentBlock Is Method Or TypeOf ParentBlock Is [Class]
        Return ParentBlock
    End Function
    <Extension()> _
    Public Function GetNextBlockSibling(ByVal Statement As LanguageElement) As ParentingStatement
        Dim Sibling = Statement
        Do
            Sibling = Sibling.NextSibling
        Loop Until Sibling Is Nothing OrElse TypeOf Sibling Is ParentingStatement
        Return CType(Sibling, ParentingStatement)
    End Function
    <Extension()> _
    Public Function GetInsertPoint(ByVal NextBlock As ParentingStatement) As SourcePoint
        Dim InsertPoint As SourcePoint
        If NextBlock.BlockType = DelimiterBlockType.Brace Then
            If NextBlock.BlockRange.Start.Line = NextBlock.BlockRange.End.Line Then
                'Braces are on same line - Seperate with newline
                CodeRush.Documents.ActiveTextDocument.InsertText(NextBlock.BlockRange.Start, Environment.NewLine)
            End If
            InsertPoint = NextBlock.BlockRange.Start.OffsetPoint(1, 0).LineStart
        Else
            InsertPoint = NextBlock.BlockRange.Start.LineStart
        End If
        Return InsertPoint
    End Function

    <Extension()> _
    Public Function GetSuperRange(ByVal Ranges As IEnumerable(Of SourceRange)) As SourceRange
        If Ranges.Count = 0 Then
            Return Nothing
        End If
        Dim SPoint As SourcePoint = Ranges.First.Start
        Dim EPoint As SourcePoint = Ranges.First.Start
        For Each Range In Ranges
            If Range.Start < SPoint Then
                SPoint = Range.Start
            End If
            If Range.End > Epoint Then
                Epoint = Range.End
            End If
        Next
        Return New SourceRange(SPoint, EPoint)
    End Function
End Module