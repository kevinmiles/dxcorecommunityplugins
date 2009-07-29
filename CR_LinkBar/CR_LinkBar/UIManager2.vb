Imports System.Drawing
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Menus
Imports System.IO
Imports DevExpress.CodeRush.StructuralParser

Public Class UIManager2
    Inherits BaseUIManager

#Region "Constructors"
    Public Sub New(ByVal LinkBar As LinkBar)
        MyBase.New(LinkBar)
    End Sub
#End Region
    Public Overrides Sub Refresh()
        Call mLinkBar.ResetToolbar()
        Call CreateAdminMenu()
        Call CreateSaveAllAndCloseButton()
        Call CreateCreateNewWorkspaceButton()
        Call CreateWorkspaceButtons()
    End Sub
    Private Sub CreateAdminMenu()
        Dim AdminMenu = MenuBar.CreateAndAddDropDownButton("Manage", "Manage (Create, Rename and Delete) your workspaces.")
        'CreateLoadButton(AdminMenu)
        Call CreateRefreshButton(AdminMenu)
        Call CreateSaveWorkspacesButton(AdminMenu)
        Call CreateUIMenu(AdminMenu)
    End Sub
    Private Sub CreateSaveWorkspacesButton(ByVal ParentMenu As IMenuPopup)
        Dim SaveMenu = ParentMenu.CreateAndAddButton("Save Workspaces")
        AddHandler SaveMenu.Click, AddressOf mLinkBar.OnClickSaveWorkspaces
    End Sub


    Private Sub CreateWorkspaceButtons()
        For Each Workspace In mLinkBar.Workspaces
            Dim WorkspaceMenu = MenuBar.CreateAndAddDropDownButton(Workspace.Name)
            CreateJumpButton(WorkspaceMenu)
            'CreateAddFilesButton(WorkspaceMenu)
            CreateRenameButton(WorkspaceMenu)
            CreateDeleteButton(WorkspaceMenu)
            mLinkBar.Workspaces.Add(Workspace)
        Next
    End Sub

    Private Sub CreateJumpButton(ByVal ParentMenu As IMenuPopup)
        Dim Button = ParentMenu.CreateAndAddButton("Jump")
        Dim Transparent As TransparentBitmap = GetBitmapByName(PNG_FOLDER)
        Button.SetFace(Transparent.Bitmap, Transparent.MaskBitmap)
        Button.Style = ButtonStyle.IconAndCaption
        Button.Tag = ParentMenu.Tag
        AddHandler Button.Click, AddressOf mLinkBar.OnClickFolderButton
    End Sub
    Private Sub CreateAddFilesButton(ByVal ParentMenu As IMenuPopup)
        Dim Button = ParentMenu.CreateAndAddButton("Add Workspace")
        Dim Transparent As TransparentBitmap = GetBitmapByName(PNG_FOLDER)
        Button.SetFace(Transparent.Bitmap, Transparent.MaskBitmap)
        Button.Style = ButtonStyle.IconAndCaption
        Button.Tag = ParentMenu.Tag
        AddHandler Button.Click, AddressOf mLinkBar.OnAddFilesWorkspaceClick
    End Sub
    Private Sub CreateRenameButton(ByVal ParentMenu As IMenuPopup)
        Dim Button = ParentMenu.CreateAndAddButton("Rename")
        Dim Transparent As TransparentBitmap = GetBitmapByName(PNG_FOLDER)
        Button.SetFace(Transparent.Bitmap, Transparent.MaskBitmap)
        Button.Style = ButtonStyle.IconAndCaption
        Button.Tag = ParentMenu.Tag
        AddHandler Button.Click, AddressOf mLinkBar.OnRenameWorkspaceClick
    End Sub
    Private Sub CreateDeleteButton(ByVal ParentMenu As IMenuPopup)
        Dim Button = ParentMenu.CreateAndAddButton("Delete")
        Dim Transparent As TransparentBitmap = GetBitmapByName(PNG_FOLDER)
        Button.SetFace(Transparent.Bitmap, Transparent.MaskBitmap)
        Button.Style = ButtonStyle.IconAndCaption
        Button.Tag = ParentMenu.Tag
        AddHandler Button.Click, AddressOf mLinkBar.OnDeleteWorkspaceClick
    End Sub


End Class