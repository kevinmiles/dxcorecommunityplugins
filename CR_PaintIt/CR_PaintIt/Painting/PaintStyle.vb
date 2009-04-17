Imports System

Namespace Painting
    Public Module PaintStyle
        Public Function RequestToMethod(ByVal Request As PaintRequestEnum, Optional ByVal SubStyle As Integer = 0) As PaintMethodEnum
            Select Case Request
                Case PaintRequestEnum.BrushStroke
                    Return PaintMethodEnum.BrushStroke
                Case PaintRequestEnum.TextHighlight
                    If SubStyle = 0 Then
                        Return PaintMethodEnum.TextHighlightFocused
                    Else
                        Return PaintMethodEnum.TextHighlightUnfocused
                    End If
                Case PaintRequestEnum.TextOverlay
                    Return PaintMethodEnum.TextOverlay
                Case PaintRequestEnum.UnderlineStroke
                    Return PaintMethodEnum.UnderlineStroke
                Case PaintRequestEnum.UnderlineThin
                    Return PaintMethodEnum.UnderlineThin
                Case PaintRequestEnum.UnderlineWavy
                    Return PaintMethodEnum.UnderlineWavy
                Case PaintRequestEnum.StrikeThrough
                    Return PaintMethodEnum.StrikeThrough
            End Select
        End Function
        Public Function StringToPaintStyle(ByVal PaintStyle As String) As PaintMethodEnum
            Try
                Return CType(PaintMethodEnum.Parse(GetType(PaintMethodEnum), PaintStyle), PaintMethodEnum)
            Catch ex As Exception
                Return PaintMethodEnum.BrushStroke
            End Try
        End Function
    End Module
End Namespace