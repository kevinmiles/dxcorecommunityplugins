Option Infer On
Imports System.Drawing
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Menus
Imports System.IO
Public Class LinkBar
#Region "Constants"
    Private Const PREFIX_Workspace As String = "WorkspacePREFIX"
    Private Const PNG_FOLDER As String = "Workspace.png"
    Private Const PNG_SAVEANDCLOSEALL As String = "SaveAndCloseAll.png"
    Private Const PNG_CREATEWORKSPACE As String = "CreateWorkspace.png"
#End Region
#Region "Fields"
    Private mWorkspaces As New List(Of Workspace)
    Private mLinkBar As MenuBar
    Private mWorkspaceDictionary As New Dictionary(Of String, Workspace)
#End Region
#Region "Simple Properties"
    Public ReadOnly Property Workspaces() As List(Of Workspace)
        Get
            Return mWorkspaces
        End Get
    End Property
#End Region
#Region "Utils"
    Private Function GetBitmapByName(ByVal BitmapName As String) As Bitmap
        Dim Asm As System.Reflection.Assembly = System.Reflection.Assembly.GetAssembly(Me.GetType)
        Dim stream As Stream = Asm.GetManifestResourceStream(String.Format("CR_LinkBar.{0}", BitmapName))
        Return CType(Bitmap.FromStream(stream), Bitmap)
    End Function
    Private Function GetWorkspaceName() As String
        Dim WorkspaceName As String
        Dim NextUnusedWorkspace As String = GetNextUnusedWorkspace("Workspace")
        Do
            WorkspaceName = InputBox("What would you like to call your Workspace?", "Workspace Name", NextUnusedWorkspace)
        Loop Until Not Workspaces.Exists(Function(c) c.Name = WorkspaceName)
        Return WorkspaceName
    End Function
    Private Function GetNextUnusedWorkspace(ByVal Base As String) As String
        Dim WorkspaceName As String = String.Empty
        Dim Count As Integer = 0
        Do
            Count += 1
            WorkspaceName = Base & Count
        Loop Until Not Workspaces.Exists(Function(c) c.Name = WorkspaceName)
        Return WorkspaceName
    End Function
#End Region

#Region " Toolbar Setup "
    Public Sub Refresh()
        If mLinkBar IsNot Nothing Then
            mLinkBar.Delete()
            mLinkBar = Nothing
        End If
        mLinkBar = CodeRush.Menus.Bars.Add("LinkBar")
        mLinkBar.Position = BarPosition.Top
        mWorkspaceDictionary = New Dictionary(Of String, Workspace)()
        ' Add SaveAndCloseAll button
        Call CreateAdminMenu()
        Call CreateSaveAllAndCloseButton()
        Call CreateCreateNewWorkspaceButton()
        Call CreateWorkspaceButtons()
        mLinkBar.Visible = True
    End Sub
    Private Sub CreateAdminMenu()
        Dim AdminMenu = mLinkBar.CreateAndAddDropDownButton("Manage", "Manage (Create, Rename and Delete) your workspaces.")
        'CreateLoadButton(AdminMenu)
        CreateSaveButton(AdminMenu)
        CreateAddFilesMenu(AdminMenu)
        CreateRenameMenu(AdminMenu)
        CreateDeleteMenu(AdminMenu)
    End Sub
    Private Sub CreateSaveAllAndCloseButton()
        Dim Button = CreateAndAddButton(mLinkBar, "Clear Workspace", "Saves all files and then Closes them.")
        Button.Style = ButtonStyle.Icon
        Button.SetFace(GetBitmapByName(PNG_SAVEANDCLOSEALL))
        AddHandler Button.Click, AddressOf OnClickSaveAndCloseAll
    End Sub
    Private Sub CreateCreateNewWorkspaceButton()
        Dim Button As IMenuButton
        Button = mLinkBar.CreateAndAddButton("Create Workspace")
        Button.Style = ButtonStyle.Icon
        Button.SetFace(GetBitmapByName(PNG_CREATEWORKSPACE))

        AddHandler Button.Click, AddressOf OnClickCreateNewWorkspace
    End Sub

    Private Sub CreateWorkspaceButtons()
        For Each Workspace In Workspaces
            Dim Button = mLinkBar.CreateAndAddButton(Workspace.Name)
            Button.SetFace(GetBitmapByName(PNG_FOLDER))
            Button.Style = ButtonStyle.IconAndCaption
            Button.Tag = PREFIX_Workspace & Workspace.Name
            mWorkspaceDictionary.Add(PREFIX_Workspace & Workspace.Name, Workspace)
            AddHandler Button.Click, AddressOf OnClickFolderButton
        Next
    End Sub

    Private Sub CreateLoadButton(ByVal AdminMenu As IMenuPopup)
        Dim LoadMenu = AdminMenu.CreateAndAddButton("Load Workspaces")
        AddHandler LoadMenu.Click, AddressOf OnClickLoadWorkspaces
    End Sub
    Private Sub CreateSaveButton(ByVal AdminMenu As IMenuPopup)
        Dim SaveMenu = AdminMenu.CreateAndAddButton("Save Workspaces")
        AddHandler SaveMenu.Click, AddressOf OnClickSaveWorkspaces
    End Sub
    Private Sub CreateAddFilesMenu(ByVal AdminMenu As IMenuPopup)
        Dim AddFilesMenu = AdminMenu.CreateAndAddDropDownButton("Add Files")
        Dim AddFilesNewWorkspaceButton = AddFilesMenu.CreateAndAddButton("New Workspace")
        AddFilesNewWorkspaceButton.Style = ButtonStyle.Caption
        AddFilesNewWorkspaceButton.BeginGroup = True
        AddHandler AddFilesNewWorkspaceButton.Click, AddressOf OnClickCreateNewWorkspace
        For Each Workspace In Workspaces
            Dim SomeAddFilesButton = AddFilesMenu.CreateAndAddButton(Workspace.Name)
            SomeAddFilesButton.Tag = PREFIX_Workspace & Workspace.Name
            AddHandler SomeAddFilesButton.Click, AddressOf OnAddFilesWorkspaceClick
        Next
    End Sub
    Private Sub CreateRenameMenu(ByVal AdminMenu As IMenuPopup)
        Dim RenameMenu = AdminMenu.CreateAndAddDropDownButton("Rename")
        For Each Workspace In Workspaces
            Dim SomeRenameButton = RenameMenu.CreateAndAddButton(Workspace.Name)
            SomeRenameButton.Tag = PREFIX_Workspace & Workspace.Name
            AddHandler SomeRenameButton.Click, AddressOf OnRenameWorkspaceClick
        Next
    End Sub
    Private Sub CreateDeleteMenu(ByVal AdminMenu As IMenuPopup)
        Dim DeleteMenu = AdminMenu.CreateAndAddDropDownButton("Delete")
        For Each Workspace In Workspaces
            Dim SomeDeleteButton = DeleteMenu.CreateAndAddButton(Workspace.Name)
            SomeDeleteButton.Tag = PREFIX_Workspace & Workspace.Name
            AddHandler SomeDeleteButton.Click, AddressOf OnDeleteWorkspaceClick
        Next
    End Sub
#Region "AlternateCode - Keep for now"
#End Region
#End Region
#Region " Button Click Handlers "
    Private Sub OnClickLoadWorkspaces(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Call LoadWorkspaces()
    End Sub
    Private Sub OnClickSaveWorkspaces(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Call SaveWorkspaces()
    End Sub
    Private Sub OnClickCreateNewWorkspace(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Dim AllDocs As ReadOnlyDocumentCollection = CodeRush.Documents.AllDocuments
        If AllDocs.Count = 0 Then
            Exit Sub
        End If
        Dim GroupName As String = GetWorkspaceName()
        Dim Workspace As New Workspace(GroupName)
        For Each Document In AllDocs.OfType(Of Document)()
            Workspace.Add(Document.Name, Document.FullName)
        Next
        Workspaces.Add(Workspace)
        Call Refresh()
    End Sub
    Private Sub OnAddFilesWorkspaceClick(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Dim Workspace = mWorkspaceDictionary.Item(e.Button.Tag)
        Dim AllDocs As ReadOnlyDocumentCollection = CodeRush.Documents.AllDocuments
        For Each Document In AllDocs.OfType(Of Document)()
            Dim Doc As Document = Document
            If Not Workspace.Exists(Function(f) f.FileWithPath = Doc.FullName) Then
                Workspace.Add(Document.Name, Document.FullName)
            End If
        Next
        Call Refresh()
    End Sub
    Private Sub OnRenameWorkspaceClick(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        ' Rename Group
        Dim Workspace = mWorkspaceDictionary.Item(e.Button.Tag)
        Workspace.Name = GetWorkspaceName()
        Call Refresh()
    End Sub
    Private Sub OnDeleteWorkspaceClick(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        ' Delete Group
        Dim Workspace = mWorkspaceDictionary.Item(e.Button.Tag)
        Call Workspaces.Remove(Workspace)
        Call Refresh()
    End Sub
    Private Sub OnClickSaveAndCloseAll(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        For Each Document In CodeRush.Documents.AllTextDocuments.OfType(Of TextDocument).ToList
            Document.Save()
            Document.Close()
        Next
    End Sub
    Private Sub OnClickFolderButton(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        For Each FileReference In mWorkspaceDictionary.Item(e.Button.Tag)
            If File.Exists(FileReference.FileWithPath) Then
                Call CodeRush.File.Activate(FileReference.FileWithPath)
            End If
        Next
    End Sub
#End Region
#Region " Settings "
    Private Shared ReadOnly Property MyStorage() As DecoupledStorage
        Get
            Return CodeRush.Options.GetStorage("CR_LinkBar")
        End Get
    End Property
    Public Sub SaveWorkspaces()
        Dim SolutionName As String = CodeRush.Solution.Active.SolutionName()
        Using Storage As DecoupledStorage = MyStorage
            Call Storage.WriteStrings(SolutionName, "Workspaces", Split(Workspaces.ToXML.ToString, System.Environment.NewLine))
        End Using
    End Sub
    Public Sub LoadWorkspaces()
        Dim SolutionName As String = CodeRush.Solution.Active.SolutionName()
        Using Storage As DecoupledStorage = MyStorage
            Dim SavedXML = Join(Storage.ReadStrings(SolutionName, "Workspaces"), "")
            Workspaces.LoadFromXML(XElement.Parse(SavedXML))
        End Using
        Refresh()
    End Sub



#End Region



End Class
