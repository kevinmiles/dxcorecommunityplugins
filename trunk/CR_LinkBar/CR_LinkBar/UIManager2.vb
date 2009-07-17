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
        Call ResetToolBar()
        'Call CreateAdminMenu()
        Call CreateSaveAllAndCloseButton()
        Call CreateCreateNewWorkspaceButton()
        Call CreateWorkspaceButtons()
    End Sub
    Private Sub CreateAdminMenu()
        Dim AdminMenu = mMenuBar.CreateAndAddDropDownButton("Manage", "Manage (Create, Rename and Delete) your workspaces.")
        'CreateLoadButton(AdminMenu)
        CreateSaveWorkspacesButton(AdminMenu)
        CreateAddFilesMenu(AdminMenu)
        CreateRenameMenu(AdminMenu)
        CreateDeleteMenu(AdminMenu)
    End Sub

    Private Sub CreateWorkspaceButtons()
        For Each Workspace In mLinkBar.Workspaces
            Dim WorkspaceMenu = mMenuBar.CreateAndAddDropDownButton(Workspace.Name)
            Dim JumpButton = WorkspaceMenu.CreateAndAddButton(String.Format("Jump to '{0}'", Workspace.Name))
            JumpButton.SetFace(GetBitmapByName(PNG_FOLDER))
            JumpButton.Style = ButtonStyle.IconAndCaption
            JumpButton.Tag = Workspace.Name
            mLinkBar.Workspaces.Add(Workspace)
            AddHandler JumpButton.Click, AddressOf mLinkBar.OnClickFolderButton
        Next
    End Sub

    Private Sub CreateLoadWorkspacesButton(ByVal AdminMenu As IMenuPopup)
        Dim LoadMenu = AdminMenu.CreateAndAddButton("Load Workspaces")
        AddHandler LoadMenu.Click, AddressOf mLinkBar.OnClickLoadWorkspaces
    End Sub
    Private Sub CreateSaveWorkspacesButton(ByVal AdminMenu As IMenuPopup)
        Dim SaveMenu = AdminMenu.CreateAndAddButton("Save Workspaces")
        AddHandler SaveMenu.Click, AddressOf mLinkBar.OnClickSaveWorkspaces
    End Sub
    Private Sub CreateAddFilesMenu(ByVal AdminMenu As IMenuPopup)
        Dim AddFilesMenu = AdminMenu.CreateAndAddDropDownButton("Add Files")
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
    Private Sub CreateRenameMenu(ByVal AdminMenu As IMenuPopup)
        Dim RenameMenu = AdminMenu.CreateAndAddDropDownButton("Rename")
        For Each Workspace In mLinkBar.Workspaces
            Dim SomeRenameButton = RenameMenu.CreateAndAddButton(Workspace.Name)
            SomeRenameButton.Tag = Workspace.Name
            AddHandler SomeRenameButton.Click, AddressOf mLinkBar.OnRenameWorkspaceClick
        Next
    End Sub
    Private Sub CreateDeleteMenu(ByVal AdminMenu As IMenuPopup)
        Dim DeleteMenu = AdminMenu.CreateAndAddDropDownButton("Delete")
        For Each Workspace In mLinkBar.Workspaces
            Dim SomeDeleteButton = DeleteMenu.CreateAndAddButton(Workspace.Name)
            SomeDeleteButton.Tag = Workspace.Name
            AddHandler SomeDeleteButton.Click, AddressOf mLinkBar.OnDeleteWorkspaceClick
        Next
    End Sub

End Class