Option Strict On
Imports System.Drawing
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.Core
Imports System.Diagnostics
Imports System
Imports System.Collections

Namespace Painting
    Public Class Painter
        Implements IDisposable
#Region "IDispose"
        Public Sub Dispose() Implements System.IDisposable.Dispose
            If Not UnderLinePainter Is Nothing Then
                UnderLinePainter.Dispose()
            End If
            If Not StrikeThroughPainter Is Nothing Then
                StrikeThroughPainter.Dispose()
            End If
        End Sub
#End Region
#Region "Fields"
        Private mUnderlinePainter As RangeHighlighter
        Private mStrikeThroughPainter As RangeHighlighter
#End Region
#Region "Painters"
        Private ReadOnly Property UnderLinePainter() As RangeHighlighter
            Get
                If mUnderlinePainter Is Nothing Then
                    mUnderlinePainter = New Underline
                End If
                Return mUnderlinePainter
            End Get
        End Property
        Private ReadOnly Property StrikeThroughPainter() As RangeHighlighter
            Get
                If mStrikeThroughPainter Is Nothing Then
                    mStrikeThroughPainter = New StrikeThrough
                End If
                Return mStrikeThroughPainter
            End Get
        End Property
#End Region
        Public Sub PaintCachedElements(ByVal Cache As PaintOptionsCache, ByVal PaintArgs As EditorPaintEventArgs)
            Dim LEPO As LanguageElementPaintOptions
            Try
                For Each CacheItem As DictionaryEntry In Cache
                    LEPO = CType(CacheItem.Value, LanguageElementPaintOptions)
                    PaintLanguageElementPaintOptions(LEPO, PaintArgs)
                Next
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Throw
            End Try
        End Sub
        Public Sub PaintLanguageElementPaintOptions(ByVal LEPO As LanguageElementPaintOptions, ByVal PaintArgs As EditorPaintEventArgs)
            Me.PaintElementName(LEPO.Element, PaintArgs, LEPO.PaintOptions, False)
        End Sub
        Public Sub PaintElementName(ByVal Element As LanguageElement, ByVal PaintArgs As EditorPaintEventArgs, ByVal PaintStyle As PaintOptions, Optional ByVal Attemptfocused As Boolean = False)
            If Not PaintStyle.Enabled Then
                Return
            End If
            If Not PaintArgs.TextView.Lines.IsVisible(Element.NameRange.Start.Line) Then
                Return
            End If
            Select Case PaintStyle.Decoration
                Case PaintRequestEnum.TextOverlay
                    PaintArgs.OverlayText(Element.Name, Element.NameRange.Start.Line, Element.NameRange.Start.Offset, PaintStyle.Color1.Base)
                Case PaintRequestEnum.BrushStroke
                    Dim Highlighter As BrushStrokeHighlighter = New BrushStrokeHighlighter
                    Dim BrushFill As SolidBrush = New SolidBrush(PaintStyle.Color1.TrueColor)
                    Highlighter.Paint(PaintArgs.Graphics, PaintArgs.TextView, Element.NameRange, BrushFill)
                    BrushFill.Dispose()
                    Highlighter.Dispose()
                Case PaintRequestEnum.TextHighlight
                    If Attemptfocused Then
                        TextHighlighter.PaintFocused(PaintArgs.TextView, Element.NameRange, PaintStyle.Color2.TrueColor)
                    Else
                        TextHighlighter.PaintUnfocused(PaintArgs.TextView, Element.NameRange, PaintStyle.Color2.TrueColor, PaintStyle.Color1.TrueColor)
                    End If
                Case PaintRequestEnum.UnderlineStroke
                    UnderLinePainter.TextView = PaintArgs.TextView
                    UnderLinePainter.FillColor = PaintStyle.Color1.TrueColor
                    UnderLinePainter.Range = Element.NameRange
                    UnderLinePainter.Paint(PaintArgs.Graphics)
                Case PaintRequestEnum.StrikeThrough
                    StrikeThroughPainter.TextView = PaintArgs.TextView
                    StrikeThroughPainter.FillColor = PaintStyle.Color1.TrueColor
                    StrikeThroughPainter.Range = Element.NameRange
                    StrikeThroughPainter.Paint(PaintArgs.Graphics)
                Case PaintRequestEnum.UnderlineThin
                    PaintArgs.DrawLine(Element.NameRange.Start.Line, Element.NameRange.Start.Offset, Element.NameRange.End.Offset - Element.NameRange.Start.Offset, PaintStyle.Color1.Base)
                Case PaintRequestEnum.UnderlineWavy
                    PaintArgs.DrawLine(Element.NameRange.Start.Line, Element.NameRange.Start.Offset, Element.NameRange.End.Offset - Element.NameRange.Start.Offset, PaintStyle.Color1.Base, LineStyle.WavyUnderline)
            End Select
        End Sub
    End Class
End Namespace
