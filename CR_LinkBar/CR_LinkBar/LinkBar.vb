﻿Option Infer On
Imports System.Drawing
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Menus
Imports System.IO
Imports DevExpress.CodeRush.StructuralParser
Public Class LinkBar
#Region "Fields"
    Private mWorkspaces As New WorkspaceCollection
    Private mEnabled As Boolean = False
    Private mUIManager As IUIManager
#End Region
    Private mMenuBar As MenuBar
#Region "Simple Properties"
    Public ReadOnly Property Workspaces() As WorkspaceCollection
        Get
            Return mWorkspaces
        End Get
    End Property
    Public ReadOnly Property MenuBar() As MenuBar
        Get
            Return mMenuBar
        End Get
    End Property
    Friend Property UIManager() As IUIManager
        Get
            Return mUIManager
        End Get
        Set(ByVal value As IUIManager)
            mUIManager = value
        End Set
    End Property

#End Region
#Region "Compile Properties"
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
#End Region

#Region "Utils"
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

    Public Sub New()
        mUIManager = New UIManager2(Me)
    End Sub
    Public Sub RefreshToolbar()
        mUIManager.Refresh()
    End Sub
    Public Sub ResetToolbar()
        If mMenuBar IsNot Nothing Then
            mMenuBar.Delete()
            mMenuBar = Nothing
        End If
        mMenuBar = CodeRush.Menus.Bars.Add("LinkBar")
        mMenuBar.Position = BarPosition.Top
        mMenuBar.Visible = True
    End Sub

#Region " Button Click Handlers "
    Friend Sub OnClickRefreshWorkspaces(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Call RefreshToolbar()
    End Sub
    Friend Sub OnClickLoadWorkspaces(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Call LoadWorkspaces()
    End Sub
    Friend Sub OnClickSaveWorkspaces(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Call SaveWorkspaces()
    End Sub
    Friend Sub OnClickCreateNewWorkspace(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Dim AllDocs As ReadOnlyDocumentCollection = CodeRush.Documents.AllDocuments
        If AllDocs.Count = 0 Then
            Exit Sub
        End If
        Dim GroupName As String = GetWorkspaceName()
        Dim Workspace As New Workspace(GroupName)
        For Each Document In AllDocs.OfType(Of TextDocument)()
            Document.Activate()
            Workspace.AddDocument(Document.Name, _
                          Document.FullName, _
                          Document.ActiveView.TopLine, _
                          Document.ActiveView.Caret.SourcePoint)
        Next
        Workspaces.Add(Workspace)
        Call RefreshToolbar()
    End Sub
    Friend Sub OnAddFilesWorkspaceClick(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Dim Workspace = Workspaces.Item(e.Button.Tag)
        Dim AllDocs As ReadOnlyDocumentCollection = CodeRush.Documents.AllDocuments
        For Each Document In AllDocs.OfType(Of TextDocument)()
            Dim Doc As Document = Document
            If Not Workspace.Exists(Function(f) f.FileWithPath = Doc.FullName) Then
                Workspace.AddDocument(Document)
            End If
        Next
        Call RefreshToolbar()
    End Sub
    Friend Sub OnRenameWorkspaceClick(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        ' Rename Group
        Dim Workspace = Workspaces.Item(e.Button.Tag)
        Workspace.Name = GetWorkspaceName()
        Call RefreshToolbar()
    End Sub
    Friend Sub OnDeleteWorkspaceClick(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        ' Delete Group
        Dim Workspace = Workspaces.Item(e.Button.Tag)
        Call Workspaces.Remove(Workspace)
        Call RefreshToolbar()
    End Sub
    Friend Sub OnClickSaveAndCloseAll(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Call SaveAll()
        Call CloseAll()
    End Sub
    Friend Sub SaveAll()
        For Each Document In CodeRush.Documents.AllTextDocuments.OfType(Of TextDocument).ToList
            Document.Save()
        Next
    End Sub
    Friend Sub CloseAll()
        For Each Document In CodeRush.Documents.AllTextDocuments.OfType(Of TextDocument).ToList
            Document.Close()
        Next
    End Sub
    Friend Sub OnClickFolderButton(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        Call CloseAll()
        For Each FileReference In Workspaces.Item(e.Button.Tag)
            If File.Exists(FileReference.FileWithPath) Then
                Dim Doc As TextDocument = CType(CodeRush.File.Activate(FileReference.FileWithPath), TextDocument)
                Doc.ActiveView.SetTopLine(FileReference.TopLine)
                Doc.ActiveView.Caret.SourcePoint = New SourcePoint(FileReference.CaretY, FileReference.CaretX)
            End If
        Next
    End Sub
#End Region
#Region " Settings "
    Private Shared ReadOnly Property MyStorage() As DecoupledStorage
        Get
            Return CodeRush.Options.GetStorage("Organization", "CR_LinkBar")
        End Get
    End Property
    Public Sub ClearWorkSpaces()
        Call Workspaces.Clear()
    End Sub
    Public Sub SaveWorkspaces()
        Dim SolutionName As String = CodeRush.Solution.Active.SolutionName()
        Using Storage As DecoupledStorage = MyStorage
            Call Storage.WriteBoolean("Section", SolutionName, True)
            Call Storage.WriteXmlNode("Workspaces" & SolutionName, Workspaces.ToXML.ToXMLElement())
        End Using
    End Sub
    Public Sub LoadWorkspaces()
        Dim SolutionName As String = CodeRush.Solution.Active.SolutionName()
        Using Storage As DecoupledStorage = MyStorage
            Workspaces.LoadFromXML(Storage.ReadXmlNode("Workspaces" & SolutionName).ToXElement)
        End Using
        RefreshToolbar()
    End Sub
#End Region
End Class