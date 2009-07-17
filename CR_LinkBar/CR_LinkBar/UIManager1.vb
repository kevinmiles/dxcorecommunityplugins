Imports System.Drawing
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Menus
Imports System.IO
Imports DevExpress.CodeRush.StructuralParser

Public Class UIManager1
    Inherits BaseUIManager
    Private mMenuBar As MenuBar
    Private mLinkBar As LinkBar
    Private mEnabled As Boolean = False
    Public Property Enabled() As Boolean
        Get
            Return mEnabled
        End Get
        Set(ByVal Value As Boolean)
            For i As Integer = 0 To mMenuBar.Count
                mMenuBar.Item(i).Enabled = Value
            Next
            mEnabled = Value
        End Set
    End Property

    Public Sub New(ByVal LinkBar As LinkBar)
        mLinkBar = LinkBar
    End Sub
    Public Overrides Sub Refresh()

        If mMenuBar IsNot Nothing Then
            mMenuBar.Delete()
            mMenuBar = Nothing
        End If
        mMenuBar = CodeRush.Menus.Bars.Add("LinkBar")
        mMenuBar.Position = BarPosition.Top
        mLinkBar.Workspaces.Clear()
        ' Add SaveAndCloseAll button
        Call CreateAdminMenu()
        Call CreateSaveAllAndCloseButton()
        Call CreateCreateNewWorkspaceButton()
        Call CreateWorkspaceButtons()
        mMenuBar.Visible = True
    End Sub
    Private Sub CreateAdminMenu()
        Dim AdminMenu = mMenuBar.CreateAndAddDropDownButton("Manage", "Manage (Create, Rename and Delete) your workspaces.")
        'CreateLoadButton(AdminMenu)
        CreateSaveWorkspacesButton(AdminMenu)
        CreateAddFilesMenu(AdminMenu)
        CreateRenameMenu(AdminMenu)
        CreateDeleteMenu(AdminMenu)
    End Sub
    Private Sub CreateSaveAllAndCloseButton()
        Dim Button = CreateAndAddButton(mMenuBar, "Clear Workspace", "Saves all files and then Closes them.")
        Button.Style = ButtonStyle.Icon
        Button.SetFace(GetBitmapByName(PNG_SAVEANDCLOSEALL))
        AddHandler Button.Click, AddressOf mLinkBar.OnClickSaveAndCloseAll
    End Sub
    Private Sub CreateCreateNewWorkspaceButton()
        Dim Button As IMenuButton
        Button = mMenuBar.CreateAndAddButton("Create Workspace")
        Button.Style = ButtonStyle.Icon
        Button.SetFace(GetBitmapByName(PNG_CREATEWORKSPACE))

        AddHandler Button.Click, AddressOf mLinkBar.OnClickCreateNewWorkspace
    End Sub

    Private Sub CreateWorkspaceButtons()
        For Each Workspace In mLinkBar.Workspaces
            Dim Button = mMenuBar.CreateAndAddButton(Workspace.Name)
            Button.SetFace(GetBitmapByName(PNG_FOLDER))
            Button.Style = ButtonStyle.IconAndCaption
            Button.Tag = Workspace.Name
            mLinkBar.Workspaces.Add(Workspace)
            AddHandler Button.Click, AddressOf mLinkBar.OnClickFolderButton
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