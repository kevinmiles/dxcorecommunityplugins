Imports System.Drawing
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Menus
Imports System.IO
Imports DevExpress.CodeRush.StructuralParser


Public MustInherit Class BaseUIManager
    Implements IUIManager
#Region "Constants"
    Protected Const PNG_FOLDER As String = "Workspace.png"
    Protected Const PNG_SAVEANDCLOSEALL As String = "SaveAndCloseAll.png"
    Protected Const PNG_CREATEWORKSPACE As String = "CreateWorkspace.png"
#End Region

    Public MustOverride Sub Refresh() Implements IUIManager.Refresh
    Public Function GetBitmapByName(ByVal BitmapName As String) As Bitmap
        Dim Asm As System.Reflection.Assembly = System.Reflection.Assembly.GetAssembly(Me.GetType)
        Dim stream As IO.Stream = Asm.GetManifestResourceStream(String.Format("CR_LinkBar.{0}", BitmapName))
        Return CType(Bitmap.FromStream(stream), Bitmap)
    End Function
End Class
