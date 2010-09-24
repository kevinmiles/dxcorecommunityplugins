Imports System.Linq
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module StringExt
    ''' <summary>Full range of the Zen Expression</summary>
    ''' <param name="LineNo"></param>
    Public Function GetZenRange(ByVal TheLine As String, ByVal LineNo As Integer) As SourceRange
        Return New SourceRange(LineNo, _
                               GetFirstNonSpaceCharPos(TheLine) + 1, _
                               LineNo, _
                               TheLine.Length + 1)
    End Function
    <Extension()> _
    Public Function Contents(ByVal Source As String, ByVal StartChar As String, ByVal EndChar As String) As String
        Dim StartPoint As Integer = Source.IndexOf(StartChar)
        Dim EndPoint As Integer = Source.IndexOf(EndChar)
        Return Source.Substring(StartPoint + 1, EndPoint - StartPoint - 1)
    End Function

    'Public Function ExtractBase(ByVal Source As String) As ParsedSection
    '    Dim Piece As String
    '    Dim Count As Integer
    '    If Source.Contains("*"c) Then
    '        Piece = Source.Split("*"c)(0)
    '    Else
    '        Piece = Source
    '    End If
    '    If Source.Contains("*"c) Then
    '        Count = Source.Split("*"c)(1)
    '    Else
    '        Count = 1
    '    End If
    '    Return New ParsedSection(Source, Count)
    'End Function
    'Public Function ParseSection(ByVal Source As String) As ParsedSection
    '    Return
    '    Dim IsMultiplicity = Source.Contains("*"c)
    '    Dim Piece As String = If(IsMultiplicity, Source.Split("*"c)(0), Source)
    '    Dim Count As Integer = If(IsMultiplicity, Source.Split("*"c)(1), 1)
    '    Return New ParsedSection(Piece, Count)
    'End Function
#Region "ExpressionBreaking"
    Public Function GetOuterContainer(ByVal Expression As String, ByVal Delimiter As Char) As String
        Dim Pos As Integer = Expression.IndexOf(Delimiter)
        Return If(Pos = -1, Expression, Expression.Substring(0, Pos))
    End Function
    Public Function GetInnerContent(ByVal Expression As String, ByVal Delimiter As Char) As String
        Dim Pos As Integer = Expression.IndexOf(Delimiter)
        Return If(Pos = -1, "", Expression.Substring(Pos + 1))
    End Function
#End Region
    Public Function GetFirstNonSpaceCharPos(ByVal TheLine As String) As Integer
        For P = 0 To TheLine.Count - 1
            If TheLine(P) <> " " Then
                Return P
            End If
        Next
        Return -1
    End Function
End Module