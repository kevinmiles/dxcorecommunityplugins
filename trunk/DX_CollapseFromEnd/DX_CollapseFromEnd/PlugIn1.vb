Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Reflection
Imports DevExpress.DXCore.Platform.Drawing

Public Class PlugIn1

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

    Private Sub PlugIn1_DecorateLanguageElement(ByVal sender As Object, ByVal args As DevExpress.CodeRush.Core.DecorateLanguageElementEventArgs) Handles Me.DecorateLanguageElement
        Dim View = CodeRush.Documents.ActiveTextView
        If View Is Nothing Then
            Exit Sub
        End If
        ' Note this test will not surface a VB.Net collapsed Item <- Need to investigate this.
        If args.LanguageElement.IsCollapsible _
        AndAlso View.IsOnScreen(args.LanguageElement.CollapsibleRange.End.Line) Then
            ' Add Tile
            Dim EndPoint = args.LanguageElement.CollapsibleRange.End
            If args.LanguageElement.IsCollapsedInView(View) Then
                args.AddAdornment(New CollapsedImageFactory(New DocPoint(EndPoint), Me, args.LanguageElement))
            Else
                args.AddAdornment(New ExpandedImageFactory(New DocPoint(EndPoint), Me, args.LanguageElement))
            End If
        End If
        'Dim Rect As Rect = View.GetRectangleFromSourceRange(New SourceRange(EndPoint, EndPoint.OffsetPoint(0, 1)))
        'Call Rect.Offset(3, 4)
        'View.Graphics.DrawImage(GetBitmap(ea.LanguageElement), New PointF(Rect.Left, Rect.Top))

    End Sub



End Class
