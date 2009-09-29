Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

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

    Private Sub PlugIn1_EditorPaintLanguageElement(ByVal ea As DevExpress.CodeRush.Core.EditorPaintLanguageElementEventArgs) Handles Me.EditorPaintLanguageElement
        Dim View = CodeRush.Documents.ActiveTextView
        If ea.LanguageElement.IsCollapsible _
        AndAlso View.IsOnScreen(ea.LanguageElement.CollapsibleRange.End.Line) Then
            ' Add Tile
            Dim EndPoint = ea.LanguageElement.CollapsibleRange.End
            Dim Rect = View.GetRectangleFromRange(New SourceRange(EndPoint.Line, 1, EndPoint.Line, 2))
            Rect.X = Rect.X - Rect.Width
            View.Graphics.FillRectangle(Brushes.Black, Rect)
            View.AddTile(NewTile(Rect, ea.LanguageElement))
        End If
    End Sub

    Private Sub PlugIn1_TileMouseDown(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.TileMouseEventArgs) Handles Me.TileMouseDown
        If Me.TileIsOurs(ea.Tile) Then
            Call (CType(ea.Tile.Object, LanguageElement)).Collapse()
        End If
    End Sub
End Class
