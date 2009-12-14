Option Strict On
Option Infer On
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core
Imports System.Windows.Forms
Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.PlugInCore
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.CodeRush.Diagnostics.Core
Imports System.Collections.Generic


Public Class QuickAddReference
#Region "Constants"
    Private TAB_SOLUTION As Integer = 0
    Private TAB_RECENT As Integer = 1
#End Region
#Region "Fields"
    Private mReferenceListViews As New Dictionary(Of String, ReferenceListView)
    Private mPlugin As QuickAddReferencePlugin
    Private mSolutionList As ReferenceListView
    Private mRecentList As ReferenceListView
#End Region
    Private mTargetProject As Project
#Region "Properties"
    Public Shared ReadOnly Property Storage() As DecoupledStorage
        Get
            Return OptionsQuickAddReference.Storage
        End Get
    End Property
#End Region
#Region "Constructors"
    Public Sub New(ByVal Plugin As QuickAddReferencePlugin, ByVal TargetProject As Project)
        Me.InitializeComponent()

        mPlugin = Plugin
        mTargetProject = TargetProject

        Lists.InitialiseLists()

        RecreateReferenceListviews()
        mSolutionList = GetReferenceListView("Solution")
        mRecentList = GetReferenceListView("Recent")
        Call PopulateMRUReferences(True)
        Call PopulateCustomLists()
    End Sub
#End Region
#Region "Utils"
    Private Function ActiveReferenceListView() As ReferenceListView
        Dim Tab As TabPage = Tabs.SelectedTab
        If TypeOf Tab.Controls(0) Is ReferenceListView Then
            Return CType(Tab.Controls(0), ReferenceListView)
        Else
            Return Nothing
        End If
    End Function
    Public Sub RecreateReferenceListviews()
        For Each ListName In Lists.Lists.Select(Function(l) l.Name)
            Call RecreateReferenceListView(ListName)
        Next
    End Sub
    Public Sub RecreateReferenceListView(ByVal Name As String)
        Dim ReferenceListView As New ReferenceListView With {.Dock = DockStyle.Fill}
        ReferenceListView.SaveKey = Name
        Dim NewTab As New TabPage(Name)
        NewTab.Controls.Add(ReferenceListView)
        Tabs.TabPages.Add(NewTab)
        mReferenceListViews(Name) = ReferenceListView
    End Sub
    Private Function GetReferenceListView(ByVal Name As String) As ReferenceListView
        If mReferenceListViews(Name) Is Nothing Then
            RecreateReferenceListView(Name)
        End If
        Return mReferenceListViews(Name)
    End Function



#End Region
#Region "List Population"
    Private Sub PopulateSolutionReferencesListView(Optional ByVal ForceRefresh As Boolean = False)
        Static sLoadedSolutionReferences As Boolean = False
        If Not sLoadedSolutionReferences OrElse ForceRefresh Then
            mSolutionList.ListView.Items.Clear()
            For Each Reference As Reference In GetList("Solution").References().OrderBy(Function(item) item.FileName)
                Try
                    mSolutionList.ListView.Items.Add(ReferenceListViewItem.Of(Reference))
                Catch ex As Exception
                    LogReferenceAddFailure(Reference)
                End Try

            Next
            sLoadedSolutionReferences = True
        End If
    End Sub
    Private Sub PopulateUserConfiguredList(ByVal TabName As String, Optional ByVal ForceRefresh As Boolean = False)
        Static sPopulated As New List(Of String)
        If Not sPopulated.Contains(TabName) OrElse ForceRefresh Then
            Dim ReferenceList As ReferenceListView = mReferenceListViews.Item(TabName)
            ReferenceList.ListView.Items.Clear()
            For Each Reference In GetTabReferences(TabName).OrderBy(Function(item) item.FileName)
                ReferenceList.ListView.Items.Add(ReferenceListViewItem.Of(Reference))
            Next
        End If
    End Sub
    Private Function GetTabReferences(ByVal TabName As String) As List(Of Reference)
        Dim Strings As String() = Storage.ReadStrings(OptionsQuickAddReference.SECTION_QUICKADD, TabName)
        Dim References As New List(Of Reference)
        References = (From s In Strings Select New Reference(s)).ToList()
        If References.Count = 0 Then
            References = DefaultReferences.GetTabReferenceDefaults(TabName)
        End If
        Return References
    End Function
    Private Sub PopulateMRUReferences(Optional ByVal ForceRefresh As Boolean = False)
        mRecentList.ListView.Items.Clear()
        For Each Reference As Reference In Lists.Recent.References
            mRecentList.ListView.Items.Add(ReferenceListViewItem.Of(Reference))
        Next
    End Sub
    Private Sub PopulateCustomLists()
        For Each List In mReferenceListViews.Values
            Dim ListSaveKey As String = List.SaveKey
            For Each Value In GetList(ListSaveKey).References
                List.ListView.Items.Add(ReferenceListViewItem.Of(Value))
            Next
        Next
    End Sub
#End Region

#Region "UI Events"
    Private Sub QuickAddReference_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call PopulateSolutionReferencesListView()
        Call PopulateMRUReferences()
    End Sub
    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        Call mPlugin.AddReferenceViaBrowse(Storage, mTargetProject)
        PopulateMRUReferences(True)
    End Sub

    Private Sub cmdOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOptions.Click
        OptionsQuickAddReference.Show()
        Call RefreshTab(True)
    End Sub


    Private Sub cmdRefreshTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshTab.Click
        Call RefreshTab(True)
    End Sub
    Private Sub RefreshTab(ByVal ForceRefresh As Boolean)
        Select Case Tabs.SelectedIndex
            Case TAB_SOLUTION
                Call PopulateSolutionReferencesListView(ForceRefresh)
            Case TAB_RECENT
                Call PopulateMRUReferences(ForceRefresh)
            Case Else
                ' do nothing
                Call PopulateUserConfiguredList(Tabs.SelectedTab.Text, ForceRefresh)
        End Select
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim TheListview As ListView = ActiveReferenceListView().ListView
        If TheListview IsNot Nothing Then
            Dim Added As Integer
            'For Each Item As ReferenceListItem In TheListview.CheckedItems
            For Each Item As ReferenceListViewItem In TheListview.SelectedItems
                Try
                    Dim Ref = Item.Reference.AddToProjectTestingForGAC(mTargetProject)
                    If Ref IsNot Nothing Then
                        Added += 1
                    End If
                Catch ex As Exception
                    ' swallow exception
                End Try
            Next
            MsgBox(String.Format("Added '{0}' References", Added))
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Lists.Recent.Save()
        Close()
    End Sub
    Private Sub Tabs_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tabs.SelectedIndexChanged
        Call RefreshTab(False)
    End Sub
#End Region
End Class