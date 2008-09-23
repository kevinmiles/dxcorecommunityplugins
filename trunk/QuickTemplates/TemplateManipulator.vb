Imports System.Text
Imports System.text.RegularExpressions
Public Module TemplateManipulator
    Public Function CreateTemplate(ByVal SourceString As String, ByVal FieldList As String(), Optional ByVal CaretItem As String = "") As String
        For Each Field As String In FieldList
            Field = Field.Trim
            ' Replace All instances with TypeLink
            SourceString = ReplaceAllNotQuoted(SourceString, Field, LinkOfText(Field), "TypeLink«(", ")»")
            ' Wrap First occurence with Field
            SourceString = ReplaceFirstOnly(SourceString, LinkOfText(Field), FieldOfText(LinkOfText(Field)), "«FieldStart»", "«FieldEnd»")
        Next
        ' Wrap Nomination with Caret-BlockAnchor
        SourceString = ReplaceFirstOnly(SourceString, FieldOfText(LinkOfText(CaretItem)), CaretWrappedText(FieldOfText(LinkOfText(CaretItem))), "«Caret»", "«BlockAnchor»")
        Return SourceString
    End Function

    Private Function ReplaceAllNotQuoted(ByVal Text As String, ByVal SearchFor As String, ByVal ReplaceWith As String, ByVal StartQuote As String, ByVal EndQuote As String) As String
        Dim Pattern As String = GetReplacePatternFromSearchData(SearchFor, StartQuote, EndQuote)
        'Return Regex.Replace(Text, Pattern, ReplaceWith)
        Dim RegexObj As Regex = New Regex(Pattern)
        Return RegexObj.Replace(Text, ReplaceWith)
    End Function
    Private Function GetReplacePatternFromSearchData(ByVal SearchFor As String, ByVal StartQuote As String, ByVal EndQuote As String) As String
        StartQuote = Regex.Escape(StartQuote)
        EndQuote = Regex.Escape(EndQuote)
        SearchFor = Regex.Escape(SearchFor)
        Return String.Format("(?!{1}\w*){0}(?!\w*{2})", SearchFor, StartQuote, EndQuote)
    End Function
    Private Function ReplaceFirstOnly(ByVal Search As String, ByVal SearchFor As String, ByVal ReplaceWith As String, ByVal StartQuote As String, ByVal EndQuote As String) As String
        Dim Pattern As String = GetReplacePatternFromSearchData(SearchFor, StartQuote, EndQuote)
        Dim RegexMatch As Match = Regex.Match(Search, Pattern)
        If RegexMatch.Captures.Count = 0 Then
            Return Search
        End If
        Dim Pos As Integer = RegexMatch.Captures(0).Index
        Return Search.Substring(0, Pos) & ReplaceWith & Search.Substring(Pos + SearchFor.Length)
    End Function

#Region "Wrappers"
    Private Function CaretWrappedText(ByVal Text As String) As String
        Return String.Format("«Caret»{0}«BlockAnchor»", Text)
    End Function
    Private Function FieldOfText(ByVal Text As String) As String
        Return String.Format("«FieldStart»{0}«FieldEnd»", Text)
    End Function
    Private Function LinkOfText(ByVal Text As String) As String
        Return String.Format("«TypeLink({0})»", Text)
    End Function
#End Region
End Module
