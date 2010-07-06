Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module SourcePointRangeExt
    <Extension()> _
    Public Function GetSuperRange(ByVal Elements As List(Of LanguageElement)) As SourceRange
        Dim Result As SourceRange = Nothing
        For Each Element In Elements
            Result = AddRanges(Result, Element.GetFullBlockCutRange())
        Next
        Return Result
    End Function
    Public Function AddRanges(ByVal Range1 As SourceRange, ByVal Range2 As SourceRange) As SourceRange
        If Range1.IsEmpty And Range2.IsEmpty Then
            Return Range1 ' empty
        End If
        If Range1.IsEmpty Then
            Return Range2
        End If
        If Range2.IsEmpty Then
            Return Range1
        End If
        Dim StartPoint As SourcePoint = If(Range1.Start < Range2.Start, Range1.Start, Range2.Start)
        Dim EndPoint As SourcePoint = If(Range1.End > Range2.End, Range1.End, Range2.End)
        Return New SourceRange(StartPoint, EndPoint)
    End Function

    <Extension()> _
    Public Function LineStart(ByVal Source As SourcePoint) As SourcePoint
        Return New SourcePoint(Source.Line, 1)
    End Function
    <Extension()> _
    Public Function Down(ByVal Source As SourcePoint) As SourcePoint
        Return New SourcePoint(Source.Line + 1, Source.Offset)
    End Function
    <Extension()> _
    Public Function Down(ByVal Source As SourcePoint, ByVal Height As Integer) As SourcePoint
        Return New SourcePoint(Source.Line + Height, Source.Offset)
    End Function
    <Extension()> _
    Public Function Up(ByVal Source As SourcePoint) As SourcePoint
        Return New SourcePoint(Source.Line - 1, Source.Offset)
    End Function
    <Extension()> _
    Public Function Up(ByVal Source As SourcePoint, ByVal Height As Integer) As SourcePoint
        Return New SourcePoint(Source.Line - Height, Source.Offset)
    End Function
    <Extension()> _
    Public Function StartOfLineAfterMethod(ByVal Source As SourceRange) As SourcePoint
        Return Source.End.OffsetPoint(1, 0).LineStart
    End Function
    <Extension()> _
    Public Function ToList(Of T)(ByVal Source As T) As List(Of T)
        Dim List As New List(Of T)
        If Source IsNot Nothing Then
            List.Add(Source)
        End If
        Return List
    End Function
    <Extension()> _
    Public Function ToReversedList(Of T)(ByVal Source As List(Of T)) As List(Of T)
        Dim OutList As New List(Of T)(Source.Count)
        For Each item In Source
            OutList.Insert(0, item)
        Next
        Return OutList
    End Function
    <Extension()> _
    Public Function Subtract(ByVal Source As SourcePoint, ByVal Point As SourcePoint) As SourcePoint
        Return Source.OffsetPoint(Source.Line - Point.Line, Source.Offset - Point.Offset)
    End Function
    <Extension()> _
    Public Function OffsetRange(ByVal Source As SourceRange, ByVal Point As SourcePoint) As SourceRange
        Return New SourceRange(Source.Start.OffsetPoint(Point.Line, Point.Offset), Source.End.OffsetPoint(Point.Line, Point.Offset))
    End Function
    <Extension()> _
    Public Function NextCodeSiblingWhichIsNot(ByVal Source As LanguageElement, ByVal ParamArray Types() As LanguageElementType) As LanguageElement
        NextCodeSiblingWhichIsNot = Source
        Do
            NextCodeSiblingWhichIsNot = NextCodeSiblingWhichIsNot.NextCodeSibling
        Loop Until NextCodeSiblingWhichIsNot Is Nothing OrElse Not Types.Contains(NextCodeSiblingWhichIsNot.ElementType)
    End Function
    <Extension()> _
    Public Function NextRealCodeSibling(ByVal Statement As LanguageElement) As LanguageElement
        Return Statement.NextCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
    End Function
    <Extension()> _
    Public Function PreviousCodeSiblingWhichIsNot(ByVal Source As LanguageElement, ByVal ParamArray Types() As LanguageElementType) As LanguageElement
        PreviousCodeSiblingWhichIsNot = Source
        Do
            PreviousCodeSiblingWhichIsNot = PreviousCodeSiblingWhichIsNot.PreviousCodeSibling
        Loop Until PreviousCodeSiblingWhichIsNot Is Nothing OrElse Not Types.Contains(PreviousCodeSiblingWhichIsNot.ElementType)
    End Function
    <Extension()> _
    Public Function PreviousRealCodeSibling(ByVal Element As LanguageElement) As LanguageElement
        Return Element.PreviousCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
    End Function
    <Extension()> _
    Public Function GetFirstNodeOnLine(ByVal Line As Integer) As LanguageElement
        Return CodeRush.Documents.ActiveTextDocument.GetNodeAt(StartOfCode(Line))
    End Function
    <Extension()> _
    Public Function StartOfCode(ByVal LineNo As Integer) As SourcePoint
        Dim Line As String = CodeRush.Documents.GetLineAt(CodeRush.Documents.ActiveTextDocument, LineNo)
        Dim FirstAlphaChar As Integer = CodeRush.StrUtil.GetLeadingWhiteSpaceCharCount(Line) + 1
        Return New SourcePoint(LineNo, FirstAlphaChar)
    End Function
    <Extension()> _
    Public Function Normalise(ByVal Source As SourceRange) As SourceRange
        If Source.StartPrecedesEnd Then
            Return Source
        End If
        Return New SourceRange(Source.End, Source.Start)
    End Function
    <Extension()> _
    Public Function GetNextCodeElement(ByVal Selection As DevExpress.CodeRush.StructuralParser.SourceRange) As LanguageElement
        Dim SourceElement = GetFirstNodeOnLine(Selection.Normalise.End.Line - 1)
        Dim Sibling = SourceElement.NextCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
        Return Sibling
    End Function
    <Extension()> _
    Public Function GetPriorCodeElement(ByVal Selection As DevExpress.CodeRush.StructuralParser.SourceRange) As LanguageElement
        Dim SourceElement = GetFirstNodeOnLine(Selection.Normalise.Start.Line)
        Dim Sibling = SourceElement.PreviousCodeSiblingWhichIsNot(LanguageElementType.XmlDocComment, LanguageElementType.AttributeSection)
        Return Sibling
    End Function
    <Extension()> _
    Public Function RelativeTo(ByVal Source As SourcePoint, ByVal DestPoint As SourcePoint) As Point
        Return New Point(Source.Offset - DestPoint.Offset, DestPoint.Line - DestPoint.Line)
    End Function

End Module
