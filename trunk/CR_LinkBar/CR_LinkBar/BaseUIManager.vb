Imports System.Drawing
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Menus
Imports System.IO
Imports DevExpress.CodeRush.StructuralParser
Imports System.Reflection


Public MustInherit Class BaseUIManager
    Implements IUIManager
#Region "Constants"
    Protected Const PNG_FOLDER As String = "Workspace.png"
    Protected Const PNG_SAVEANDCLOSEALL As String = "SaveAndCloseAll.png"
    Protected Const PNG_CREATEWORKSPACE As String = "CreateWorkspace.png"
#End Region
#Region "Fields"
    Protected mLinkBar As LinkBar
#End Region
#Region "Constructors"
    Public Sub New(ByVal LinkBar As LinkBar)
        mLinkBar = LinkBar
    End Sub
#End Region

#Region "Abstract Methods"
    Public MustOverride Sub Refresh() Implements IUIManager.Refresh
#End Region

    Public Function GetBitmapByName(ByVal BitmapName As String) As TransparentBitmap
        Dim Asm As Assembly = Assembly.GetAssembly(Me.GetType)
        Dim stream As IO.Stream = Asm.GetManifestResourceStream(String.Format("CR_LinkBar.{0}", BitmapName))
        Dim BitmapFromStream As Bitmap = CType(Bitmap.FromStream(stream), Bitmap)
        Return New TransparentBitmap(BitmapFromStream)
    End Function
    Protected Sub CreateSaveAllAndCloseButton()
        Dim Button = CreateAndAddButton(mLinkBar.MenuBar, "Clear Workspace", "Saves all files and then Closes them.")
        Button.Style = ButtonStyle.Icon
        Dim Image As TransparentBitmap = GetBitmapByName(PNG_SAVEANDCLOSEALL)
        Button.SetFace(Image.Bitmap, Image.MaskBitmap)
        AddHandler Button.Click, AddressOf mLinkBar.OnClickSaveAndCloseAll
    End Sub
    Protected Sub CreateCreateNewWorkspaceButton()
        Dim Button As IMenuButton = mLinkBar.MenuBar.CreateAndAddButton("Create Workspace")
        Button.Style = ButtonStyle.Icon
        Dim Image As TransparentBitmap = GetBitmapByName(PNG_CREATEWORKSPACE)
        Button.SetFace(Image.Bitmap, Image.MaskBitmap)
        AddHandler Button.Click, AddressOf mLinkBar.OnClickCreateNewWorkspace
    End Sub
    Protected Sub CreateRefreshButton(ByVal ParentMenu As IMenuPopup)
        Dim RefreshMenu = ParentMenu.CreateAndAddButton("Refresh")
        AddHandler RefreshMenu.Click, AddressOf mLinkBar.OnClickRefreshWorkspaces
    End Sub

    Protected Sub CreateUIMenu(ByVal ParentMenu As IMenuPopup)
        Dim RenameMenu = ParentMenu.CreateAndAddDropDownButton("UI")
        Dim UI1Button = RenameMenu.CreateAndAddButton("UI1")
        AddHandler UI1Button.Click, AddressOf (New UIManager1(mLinkBar)).AssignUI
        Dim UI2Button = RenameMenu.CreateAndAddButton("UI2")
        AddHandler UI2Button.Click, AddressOf (New UIManager2(mLinkBar)).AssignUI
    End Sub
    Public Sub AssignUI(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        mLinkBar.UIManager = Me
        mLinkBar.RefreshToolbar()
    End Sub
    Protected ReadOnly Property MenuBar() As MenuBar
        Get
            Return mLinkBar.MenuBar
        End Get
    End Property

End Class
