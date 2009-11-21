Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module Extentions
    <Extension()> _
    Public Function StartOfLine(ByVal Source As SourcePoint) As SourcePoint
        Return New SourcePoint(Source.Line, 1)
    End Function
    <Extension()> _
    Public Function StartOfLineAfterMethod(ByVal Source As SourceRange) As SourcePoint
        Return Source.End.OffsetPoint(1, 0).StartOfLine
    End Function
    <Extension()> _
    Public Function ToList(Of T)(ByVal Source As T) As List(Of T)
        Dim List As New List(Of T)
        List.Add(Source)
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
    Public Function MoveABS(ByVal Source As SourceRange, ByVal Point As SourcePoint) As SourceRange
        Return New SourceRange(Point.Line, 1, Point.Line + Source.Height, 1)
    End Function
End Module
