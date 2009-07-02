Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Menus
Imports System.Runtime.CompilerServices
Imports System.IO

Public Class PlugIn1
    Private Const PREFIX_FILECOLLECTION As String = "FILECOLLECTIONPREFIX"

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        Call RefreshToolbar()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.
        MyBase.FinalizePlugIn()
    End Sub
#End Region

#Region "Fields"
    Private FileCollections As New List(Of FileCollection)
    Private mLinkBar As MenuBar
    Private mMenuGroupDictionary As New Dictionary(Of String, FileCollection)
#End Region
#Region "Utils"
    Private Function BitmapFromString(ByVal Caption As String) As Bitmap
        Dim Font As Font = New Font("Tahoma", 16)
        Using g As Graphics = Graphics.FromImage(New Bitmap(1, 1))
            'CodeRush.Graphics.MeasureString(g, )
            Return CodeRush.Graphics.MakeBitmapFromString(Caption, g, Color.Black, Color.Gray)
        End Using
    End Function
#End Region

#Region "Toolbar Setup"
    Private Sub RefreshToolbar()
        Call Recreate()
        Call PopulateLinkBar()
    End Sub
    Private Sub Recreate()
        If mLinkBar IsNot Nothing Then
            mLinkBar.Delete()
            mLinkBar = Nothing
        End If
        mLinkBar = CodeRush.Menus.Bars.Add("LinkBar")
        mLinkBar.Position = BarPosition.Top
        mMenuGroupDictionary = New Dictionary(Of String, FileCollection)()
    End Sub

    Private Sub PopulateLinkBar()
        ' Add SaveAndCloseAll button
        Call CreateAdminMenu()
        Call CreateSaveAllAndCloseButton()
        Call CreateSimpleGroupButtons()
        mLinkBar.Visible = True
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
    Private Sub CreateDropdownGroupButtons()
        ' Better version of what we have above if we can get the drop downs to work properly
        For Each FileCollection In FileCollections
            Dim DropDownMenu = mLinkBar.CreateAndAddDropDownButton(FileCollection.Name)
            DropDownMenu.CreateAndAddButton("Load Files")
            DropDownMenu.CreateAndAddButton("View Files")
            DropDownMenu.CreateAndAddButton("Delete Group")
            DropDownMenu.CreateAndAddButton("rename Group")
            'Control.Style = ButtonStyle.Caption
            'mMenuCollectionDictionary.Add(Control, FileCollection)
            'AddHandler DropDownMenu.Click, AddressOf OnClickFolderButton
        Next
    End Sub

#End Region
#Region "Toolbar Button Setup"
#Region "Fixed Buttons"
    Private Sub CreateSaveAllAndCloseButton()
        Dim SaveAndCloseAllButton = CreateAndAddButton(mLinkBar, "Save All && Close", "Saves all files and then Closes them.")
        SaveAndCloseAllButton.Style = ButtonStyle.Caption
        SaveAndCloseAllButton.SetFace(GetBitmapByName("SaveAndCloseAll.bmp"))
        SaveAndCloseAllButton.Style = ButtonStyle.IconAndCaption
        AddHandler SaveAndCloseAllButton.Click, AddressOf OnClickSaveAndCloseAll
    End Sub
    Private Function GetBitmapByName(ByVal BitmapName As String) As Bitmap
        Dim Asm As System.Reflection.Assembly = System.Reflection.Assembly.GetAssembly(Me.GetType)
        Dim stream As Stream = Asm.GetManifestResourceStream("CR_LinkBar." & BitmapName)
        Return CType(Bitmap.FromStream(stream), Bitmap)
    End Function
    Private Sub CreateCreateGroupFromFilesButton()
        'Dim CreateGroupFromFilesButton As IMenuButton
        'CreateGroupFromFilesButton = mLinkBar.CreateAndAddButton("Create Group")
        'CreateGroupFromFilesButton.Style = ButtonStyle.Caption
        'AddHandler CreateGroupFromFilesButton.Click, AddressOf OnClickCreateGroupFromFiles
    End Sub
    Private Sub CreateAdminMenu()
        Dim AdminMenu = mLinkBar.CreateAndAddDropDownButton("Admin", "Allows you to Create, Rename and Delete File Groups")
        CreateLoadSAveButtons(AdminMenu)
        CreateAddFilesMenu(AdminMenu)
        CreateRenameMenu(AdminMenu)
        CreateDeleteMenu(AdminMenu)
    End Sub
    Private Sub CreateLoadsAveButtons(ByVal AdminMenu As IMenuPopup)
        Dim LoadMenu = AdminMenu.CreateAndAddButton("Load Groups")
        AddHandler LoadMenu.Click, AddressOf OnClickLoadGroups
        Dim SaveMenu = AdminMenu.CreateAndAddButton("Save Groups")
        AddHandler SaveMenu.Click, AddressOf OnClickSaveGroups
    End Sub
    Private Sub OnClickSaveGroups(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Call SaveSettings()
    End Sub
    Private Sub OnClickLoadGroups(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Call LoadSettings()
    End Sub
    Private Sub CreateAddFilesMenu(ByVal AdminMenu As IMenuPopup)
        Dim AddFilesMenu = AdminMenu.CreateAndAddDropDownButton("Add Files")
        Dim AddFilesNewGroupButton = AddFilesMenu.CreateAndAddButton("New Group")
        AddFilesNewGroupButton.Style = ButtonStyle.Caption
        AddFilesNewGroupButton.BeginGroup = True
        AddHandler AddFilesNewGroupButton.Click, AddressOf OnClickCreateGroupFromFiles
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
#End Region
#End Region
#Region "Actions"
    Private Sub OpenFileCollection(ByVal Files As FileCollection)
        For Each File In Files
            If System.IO.File.Exists(File.FileWithPath) Then
                Call CodeRush.File.Activate(File.FileWithPath)
            End If
        Next
    End Sub
#Region "Groups"
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
#End Region
#Region "Button Click Handlers"
    Private Sub OnDeleteGroupClick(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        ' Delete Group
        Dim FileCollection = mMenuGroupDictionary.Item(e.Button.Tag)
        Call FileCollections.Remove(FileCollection)
        Call RefreshToolbar()
    End Sub
    Private Sub OnRenameGroupClick(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        ' Rename Group
        Dim FileCollection = mMenuGroupDictionary.Item(e.Button.Tag)
        FileCollection.Name = GetGroupName()
        Call RefreshToolbar()
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
        Call RefreshToolbar()
    End Sub
    Private Sub OnClickCreateGroupFromFiles(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
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
        Call RefreshToolbar()
    End Sub
    Private Sub OnClickSaveAndCloseAll(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        For Each Document In CodeRush.Documents.AllTextDocuments.OfType(Of TextDocument).ToList
            Document.Save()
            Document.Close()
        Next
    End Sub
    Private Sub OnClickFolderButton(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Call OpenFileCollection(mMenuGroupDictionary.Item(e.Button.Tag))
    End Sub
#End Region
    Private Sub SaveSettings()
        Dim SolutionName As String = (New FileInfo(CodeRush.Solution.Active.FullName)).Name.ToLower.Replace(".sln", "")
        Using Storage As DecoupledStorage = MyStorage
            Call Storage.WriteStrings(SolutionName, "FileCollections", Split(FileCollections.ToXML.ToString, System.Environment.NewLine))
        End Using
    End Sub
    Private Sub LoadSettings()
        Dim SolutionName As String = (New FileInfo(CodeRush.Solution.Active.FullName)).Name.ToLower.Replace(".sln", "")
        Using Storage As DecoupledStorage = MyStorage
            Dim SavedXML = Join(Storage.ReadStrings(SolutionName, "FileCollections"), "")
            FileCollections = FromXML(XElement.Parse(SavedXML))
        End Using
        RefreshToolbar()
    End Sub
    Private ReadOnly Property MyStorage() As DecoupledStorage
        Get
            Return CodeRush.Options.GetStorage("CR_LinkBar")
        End Get
    End Property
    Function FromXML(ByVal Source As XElement) As List(Of FileCollection)
        Dim LocalFileCollections As New List(Of FileCollection)
        For Each FileCollectionXML In Source.<FileCollection>
            Dim FC As New FileCollection(FileCollectionXML.@Name)
            For Each File In FileCollectionXML.<File>
                FC.Add(New FileReference(File.@Display, File.@FileWithPath))
            Next
            Call LocalFileCollections.Add(FC)
        Next
        Return LocalFileCollections
    End Function
End Class
Public Module IMenuContainerExt
    <Extension()> _
    Public Function CreateAndAddButton(ByVal LinkBar As IMenuContainer, ByVal Caption As String, Optional ByVal Description As String = "") As IMenuButton
        If Description = String.Empty Then
            Description = Caption
        End If
        Dim Control As IMenuButton = LinkBar.AddButton()
        Control.Caption = Caption
        Control.Visible = True
        Control.Enabled = True
        Control.DescriptionText = Description
        Return Control
    End Function
    <Extension()> _
    Public Function CreateAndAddDropDownButton(ByVal LinkBar As IMenuContainer, ByVal Caption As String, Optional ByVal Description As String = "") As IMenuPopup
        If Description = String.Empty Then
            Description = Caption
        End If
        Dim Control As IMenuPopup = LinkBar.AddPopup
        Control.Caption = Caption
        Control.Visible = True
        Control.Enabled = True
        Control.DescriptionText = Description
        Return Control
    End Function
    <Extension()> _
    Function ToXML(ByVal Source As List(Of FileCollection)) As XElement
        Return <FileCollections>
                   <%= From FileCollection In Source _
                       Select <FileCollection Name=<%= FileCollection.Name %>>
                                  <%= From File In FileCollection _
                                      Select <File Display=<%= File.Display %> FileWithPath=<%= File.FileWithPath %>/> _
                                  %>
                              </FileCollection> _
                   %>
               </FileCollections>
    End Function
End Module

