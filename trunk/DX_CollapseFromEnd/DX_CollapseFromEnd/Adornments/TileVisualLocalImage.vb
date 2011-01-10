Imports DevExpress.DXCore.Adornments
Imports System.Drawing
Imports System.Reflection


Public Class TileVisualLocalImage
    Inherits TileVisual
    Public Sub New(ByVal binding As IElementFrame)
        MyBase.New(binding)
    End Sub
    Public Shared Function GetBitmapByName(ByVal BitmapName As String) As DevExpress.DXCore.Platform.Drawing.Image
        Dim Asm As Assembly = Assembly.GetAssembly(GetType(TileVisualLocalImage))
        Dim stream As IO.Stream = Asm.GetManifestResourceStream(String.Format("DX_CollapseFromEnd.{0}", BitmapName))
        Return New DevExpress.DXCore.Platform.Drawing.Image(CType(Bitmap.FromStream(stream), Bitmap))
    End Function
End Class
