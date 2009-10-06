Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Reflection

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
        If View Is Nothing Then
            Exit Sub
        End If
        ' Note this test will not surface a VB.Net collapsed Item <- Need to investigate this.
        If ea.LanguageElement.IsCollapsible _
        AndAlso View.IsOnScreen(ea.LanguageElement.CollapsibleRange.End.Line) Then
            ' Add Tile
            Dim EndPoint = ea.LanguageElement.CollapsibleRange.End
            Dim Rect = View.GetRectangleFromRange(New SourceRange(EndPoint, EndPoint.OffsetPoint(0, 1)))
            Call Rect.Offset(3, 4)
            View.Graphics.DrawImage(GetBitmap(ea.LanguageElement), New PointF(Rect.Left, Rect.Top))
            View.AddTile(NewTile(Rect, ea.LanguageElement))
        End If
    End Sub
    Private Function GetBitmap(ByVal LE As LanguageElement) As Bitmap
        If LE.Collapsed Then
            Return GetBitmapByName("Expand.bmp")
        End If
        Return GetBitmapByName("Collapse.bmp")
    End Function

    Private Sub PlugIn1_TileMouseDown(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.TileMouseEventArgs) Handles Me.TileMouseDown
        If Me.TileIsOurs(ea.Tile) Then
            Dim Element = CType(ea.Tile.Object, LanguageElement)
            If Element.Collapsed Then
                Call Element.Expand()
            Else
                Call Element.Collapse()
            End If
        End If
    End Sub

    Private Sub PlugIn1_TileSetCursor(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.TileSetCursorEventArgs) Handles Me.TileSetCursor
        If Me.TileIsOurs(ea.Tile) Then
            ' Provide visual clue
            Cursor.Current = Cursors.Hand
            ea.SetCursorArgs.Cancel = True
        End If
    End Sub

    Public Function GetBitmapByName(ByVal BitmapName As String) As Bitmap
        Dim Asm As Assembly = Assembly.GetAssembly(Me.GetType)
        Dim stream As IO.Stream = Asm.GetManifestResourceStream(String.Format("DX_CollapseFromEnd.{0}", BitmapName))
        Return CType(Bitmap.FromStream(stream), Bitmap)
    End Function

End Class
