Imports System.Drawing
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Menus
Imports System.IO
Imports DevExpress.CodeRush.StructuralParser

Public Class UIManager1
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
        Call CreateAddFilesMenu(AdminMenu)
        Call CreateRenameMenu(AdminMenu)
        Call CreateDeleteMenu(AdminMenu)
        Call CreateUIMenu(AdminMenu)

    End Sub
    Private Sub CreateWorkspaceButtons()
        For Each Workspace In mLinkBar.Workspaces
            Dim Button = MenuBar.CreateAndAddButton(Workspace.Name)
            Dim Transparent As TransparentBitmap = GetBitmapByName(PNG_FOLDER)
            Button.SetFace(Transparent.Bitmap, Transparent.MaskBitmap)
            Button.Style = ButtonStyle.IconAndCaption
            Button.Tag = Workspace.Name
            mLinkBar.Workspaces.Add(Workspace)
            AddHandler Button.Click, AddressOf mLinkBar.OnClickFolderButton
        Next
    End Sub

    Private Sub CreateLoadWorkspacesButton(ByVal ParentMenu As IMenuPopup)
        Dim LoadMenu = ParentMenu.CreateAndAddButton("Load Workspaces")
        AddHandler LoadMenu.Click, AddressOf mLinkBar.OnClickLoadWorkspaces
    End Sub
    Private Sub CreateSaveWorkspacesButton(ByVal ParentMenu As IMenuPopup)
        Dim SaveMenu = ParentMenu.CreateAndAddButton("Save Workspaces")
        AddHandler SaveMenu.Click, AddressOf mLinkBar.OnClickSaveWorkspaces
    End Sub
    Private Sub CreateAddFilesMenu(ByVal ParentMenu As IMenuPopup)
        Dim AddFilesMenu = ParentMenu.CreateAndAddDropDownButton("Add Files")
        Dim AddFilesNewWorkspaceButton = AddFilesMenu.CreateAndAddButton("New Workspace")
        AddFilesNewWorkspaceButton.Style = ButtonStyle.Caption
        AddFilesNewWorkspaceButton.BeginGroup = True
        AddHandler AddFilesNewWorkspaceButton.Click, AddressOf mLinkBar.OnClickCreateNewWorkspace
        For Each Workspace In mLinkBar.Workspaces
            Dim SomeAddFilesButton = AddFilesMenu.CreateAndAddButton(Workspace.Name)
            SomeAddFilesButton.Tag = Workspace.Name
            AddHandler SomeAddFilesButton.Click, AddressOf mLinkBar.OnAddFilesWorkspaceClick
        Next
    End Sub
    Private Sub CreateRenameMenu(ByVal ParentMenu As IMenuPopup)
        Dim RenameMenu = ParentMenu.CreateAndAddDropDownButton("Rename")
        For Each Workspace In mLinkBar.Workspaces
            Dim SomeRenameButton = RenameMenu.CreateAndAddButton(Workspace.Name)
            SomeRenameButton.Tag = Workspace.Name
            AddHandler SomeRenameButton.Click, AddressOf mLinkBar.OnRenameWorkspaceClick
        Next
    End Sub
    Private Sub CreateDeleteMenu(ByVal ParentMenu As IMenuPopup)
        Dim DeleteMenu = ParentMenu.CreateAndAddDropDownButton("Delete")
        For Each Workspace In mLinkBar.Workspaces
            Dim SomeDeleteButton = DeleteMenu.CreateAndAddButton(Workspace.Name)
            SomeDeleteButton.Tag = Workspace.Name
            AddHandler SomeDeleteButton.Click, AddressOf mLinkBar.OnDeleteWorkspaceClick
        Next
    End Sub
End Class