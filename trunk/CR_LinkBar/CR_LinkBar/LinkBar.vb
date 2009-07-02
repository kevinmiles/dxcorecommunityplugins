Option Infer On
Imports System.Drawing
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Menus
Imports System.IO
Public Class LinkBar
#Region "Constants"
    Private Const PREFIX_FILECOLLECTION As String = "FILECOLLECTIONPREFIX"
#End Region
#Region "Fields"
    Private mFileCollections As New List(Of FileCollection)
    Private mLinkBar As MenuBar
    Private mMenuGroupDictionary As New Dictionary(Of String, FileCollection)
#End Region
#Region "Simple Properties"
    Public ReadOnly Property FileCollections() As List(Of FileCollection)
        Get
            Return mFileCollections
        End Get
    End Property
#End Region
#Region "Utils"
    Private Function GetBitmapByName(ByVal BitmapName As String) As Bitmap
        Dim Asm As System.Reflection.Assembly = System.Reflection.Assembly.GetAssembly(Me.GetType)
        Dim stream As Stream = Asm.GetManifestResourceStream(String.Format("CR_LinkBar.{0}", BitmapName))
        Return CType(Bitmap.FromStream(stream), Bitmap)
    End Function
    Private Function GetGroupName() As String
        Dim GroupName As String
        Dim NextUnusedGroup As String = GetNextUnusedGroup("Group")
        Do
            GroupName = InputBox("What would you like to call your Group?", "Group Name", NextUnusedGroup)
        Loop Until Not FileCollections.Exists(Function(c) c.Name = GroupName)
        Return GroupName
    End Function
    Private Function GetNextUnusedGroup(ByVal Base As String) As String
        Dim GroupName As String = String.Empty
        Dim Count As Integer = 0
        Do
            Count += 1
            GroupName = Base & Count
        Loop Until Not FileCollections.Exists(Function(c) c.Name = GroupName)
        Return GroupName
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
        mMenuGroupDictionary = New Dictionary(Of String, FileCollection)()
        ' Add SaveAndCloseAll button
        Call CreateAdminMenu()
        Call CreateSaveAllAndCloseButton()
        Call CreateSimpleGroupButtons()
        mLinkBar.Visible = True
    End Sub
    Private Sub CreateAdminMenu()
        Dim AdminMenu = mLinkBar.CreateAndAddDropDownButton("Admin", "Allows you to Create, Rename and Delete File Groups")
        CreateLoadSaveButtons(AdminMenu)
        CreateAddFilesMenu(AdminMenu)
        CreateRenameMenu(AdminMenu)
        CreateDeleteMenu(AdminMenu)
    End Sub
    Private Sub CreateSaveAllAndCloseButton()
        Dim SaveAndCloseAllButton = CreateAndAddButton(mLinkBar, "Save All && Close", "Saves all files and then Closes them.")
        SaveAndCloseAllButton.Style = ButtonStyle.Caption
        SaveAndCloseAllButton.SetFace(GetBitmapByName("SaveAndCloseAll.bmp"))
        SaveAndCloseAllButton.Style = ButtonStyle.IconAndCaption
        AddHandler SaveAndCloseAllButton.Click, AddressOf OnClickSaveAndCloseAll
    End Sub
    Private Sub CreateSimpleGroupButtons()
        For Each FileCollection In FileCollections
            Dim GroupButton = mLinkBar.CreateAndAddButton(FileCollection.Name)
            GroupButton.Style = ButtonStyle.Caption
            GroupButton.Tag = PREFIX_FILECOLLECTION & FileCollection.Name
            mMenuGroupDictionary.Add(PREFIX_FILECOLLECTION & FileCollection.Name, FileCollection)
            AddHandler GroupButton.Click, AddressOf OnClickFolderButton
        Next
    End Sub

    Private Sub CreateLoadSaveButtons(ByVal AdminMenu As IMenuPopup)
        Dim LoadMenu = AdminMenu.CreateAndAddButton("Load Groups")
        AddHandler LoadMenu.Click, AddressOf OnClickLoadGroups
        Dim SaveMenu = AdminMenu.CreateAndAddButton("Save Groups")
        AddHandler SaveMenu.Click, AddressOf OnClickSaveGroups
    End Sub
    Private Sub CreateAddFilesMenu(ByVal AdminMenu As IMenuPopup)
        Dim AddFilesMenu = AdminMenu.CreateAndAddDropDownButton("Add Files")
        Dim AddFilesNewGroupButton = AddFilesMenu.CreateAndAddButton("New Group")
        AddFilesNewGroupButton.Style = ButtonStyle.Caption
        AddFilesNewGroupButton.BeginGroup = True
        AddHandler AddFilesNewGroupButton.Click, AddressOf OnClickAddFilesNewGroup
        For Each FileCollection In FileCollections
            Dim SomeAddFilesButton = AddFilesMenu.CreateAndAddButton(FileCollection.Name)
            SomeAddFilesButton.Tag = PREFIX_FILECOLLECTION & FileCollection.Name
            AddHandler SomeAddFilesButton.Click, AddressOf OnAddFilesGroupClick
        Next
    End Sub
    Private Sub CreateRenameMenu(ByVal AdminMenu As IMenuPopup)
        Dim RenameMenu = AdminMenu.CreateAndAddDropDownButton("Rename")
        For Each FileCollection In FileCollections
            Dim SomeRenameButton = RenameMenu.CreateAndAddButton(FileCollection.Name)
            SomeRenameButton.Tag = PREFIX_FILECOLLECTION & FileCollection.Name
            AddHandler SomeRenameButton.Click, AddressOf OnRenameGroupClick
        Next
    End Sub
    Private Sub CreateDeleteMenu(ByVal AdminMenu As IMenuPopup)
        Dim DeleteMenu = AdminMenu.CreateAndAddDropDownButton("Delete")
        For Each FileCollection In FileCollections
            Dim SomeDeleteButton = DeleteMenu.CreateAndAddButton(FileCollection.Name)
            SomeDeleteButton.Tag = PREFIX_FILECOLLECTION & FileCollection.Name
            AddHandler SomeDeleteButton.Click, AddressOf OnDeleteGroupClick
        Next
    End Sub
#Region "AlternateCode - Keep for now"
    'Private Sub CreateDropdownGroupButtons()
    '    ' Better version of what we have above if we can get the drop downs to work properly
    '    For Each FileCollection In FileCollections
    '        Dim DropDownMenu = mLinkBar.CreateAndAddDropDownButton(FileCollection.Name)
    '        DropDownMenu.CreateAndAddButton("Load Files")
    '        DropDownMenu.CreateAndAddButton("View Files")
    '        DropDownMenu.CreateAndAddButton("Delete Group")
    '        DropDownMenu.CreateAndAddButton("rename Group")
    '        'Control.Style = ButtonStyle.Caption
    '        'mMenuCollectionDictionary.Add(Control, FileCollection)
    '        'AddHandler DropDownMenu.Click, AddressOf OnClickFolderButton
    '    Next
    'End Sub
    'Private Sub CreateCreateGroupFromFilesButton()
    '    'Dim CreateGroupFromFilesButton As IMenuButton
    '    'CreateGroupFromFilesButton = mLinkBar.CreateAndAddButton("Create Group")
    '    'CreateGroupFromFilesButton.Style = ButtonStyle.Caption
    '    'AddHandler CreateGroupFromFilesButton.Click, AddressOf OnClickCreateGroupFromFiles
    'End Sub
#End Region
#End Region
#Region " Button Click Handlers "
    Private Sub OnClickLoadGroups(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Call LoadSettings()
    End Sub
    Private Sub OnClickSaveGroups(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Call SaveSettings()
    End Sub
    Private Sub OnClickAddFilesNewGroup(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Dim AllDocs As ReadOnlyDocumentCollection = CodeRush.Documents.AllDocuments
        If AllDocs.Count = 0 Then
            Exit Sub
        End If
        Dim GroupName As String = GetGroupName()
        Dim FileCollection As New FileCollection(GroupName)
        For Each Document In AllDocs.OfType(Of Document)()
            FileCollection.Add(Document.Name, Document.FullName)
        Next
        FileCollections.Add(FileCollection)
        Call Refresh()
    End Sub
    Private Sub OnAddFilesGroupClick(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Dim FileCollection = mMenuGroupDictionary.Item(e.Button.Tag)
        Dim AllDocs As ReadOnlyDocumentCollection = CodeRush.Documents.AllDocuments
        For Each Document In AllDocs.OfType(Of Document)()
            Dim Doc As Document = Document
            If Not FileCollection.Exists(Function(f) f.FileWithPath = Doc.FullName) Then
                FileCollection.Add(Document.Name, Document.FullName)
            End If
        Next
        Call Refresh()
    End Sub
    Private Sub OnRenameGroupClick(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        ' Rename Group
        Dim FileCollection = mMenuGroupDictionary.Item(e.Button.Tag)
        FileCollection.Name = GetGroupName()
        Call Refresh()
    End Sub
    Private Sub OnDeleteGroupClick(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        ' Delete Group
        Dim FileCollection = mMenuGroupDictionary.Item(e.Button.Tag)
        Call FileCollections.Remove(FileCollection)
        Call Refresh()
    End Sub
    Private Sub OnClickSaveAndCloseAll(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        For Each Document In CodeRush.Documents.AllTextDocuments.OfType(Of TextDocument).ToList
            Document.Save()
            Document.Close()
        Next
    End Sub
    Private Sub OnClickFolderButton(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        For Each FileReference In mMenuGroupDictionary.Item(e.Button.Tag)
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
    Private Sub SaveSettings()
        Dim SolutionName1 As String = CodeRush.Solution.Active.SolutionName()
        Using Storage As DecoupledStorage = MyStorage
            Call Storage.WriteStrings(SolutionName1, "FileCollections", Split(FileCollections.ToXML.ToString, System.Environment.NewLine))
        End Using
    End Sub
    Private Sub LoadSettings()
        Dim SolutionName As String = (New FileInfo(CodeRush.Solution.Active.FullName)).Name.ToLower.Replace(".sln", "")
        Using Storage As DecoupledStorage = MyStorage
            Dim SavedXML = Join(Storage.ReadStrings(SolutionName, "FileCollections"), "")
            FileCollections.LoadFromXML(XElement.Parse(SavedXML))
        End Using
        Refresh()
    End Sub



#End Region



End Class
