Imports System.Collections.Generic

Imports DevExpress.DXCore.Adornments
Imports DevExpress.DXCore.Platform.Drawing

Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser

Class ExpandedImageFactory
    Inherits TextDocumentTile

    Dim mLanguageElement As LanguageElement
    Public Sub New(ByVal point As DocPoint, ByVal master As CoreEventHub, ByVal LanguageElement As LanguageElement)
        MyBase.New(point, master)
        mLanguageElement = LanguageElement
    End Sub

    Protected Overrides Function NewAdornment(ByVal feature As String, ByVal binding As IElementFrame) As TextViewAdornment
        Dim adornment As ExpandedTile = New ExpandedTile(binding, mLanguageElement)
        adornment.Cursor = Cursor.Arrow
        Return adornment
    End Function

End Class

Class ExpandedTile
    Inherits TileVisualLocalImage

    ReadOnly mLanguageElement As LanguageElement
    Private Shared ReadOnly MyBitmap As Image = GetBitmapByName("Collapse.bmp")

    Public Sub New(ByVal binding As IElementFrame, ByVal LanguageElement As LanguageElement)
        MyBase.New(binding)
        mLanguageElement = LanguageElement
    End Sub


    Public Overrides Sub Render(ByVal context As IDrawingSurface, ByVal geometry As ElementFrameGeometry)
        context.DrawImage(MyBitmap, New Rect(geometry.Bounds.TopLeft.Offset(4, 5), New Size(11, 11)))
    End Sub
    Public Overrides Sub OnMouseDown(ByVal ea As DevExpress.CodeRush.Core.EditorMouseEventArgs)
        mLanguageElement.CollapseInView(ea.TextView)
        CodeRush.TextViews.Active.Invalidate()
    End Sub
End Class