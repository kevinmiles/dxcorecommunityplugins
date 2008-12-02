Option Strict On
Option Infer On
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core
Imports System.Windows.Forms
Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.PlugInCore
Imports System.Linq
Imports System.Linq.Expressions

Public Class QuickAddReference
    Private Const SECTION_QUICKADD As String = "QuickAdd"
#Region "Fields"
    Private MRUList As New ReferenceCollection
    Private mDict As New Dictionary(Of String, ReferenceListView)
    Private mPlugin As StandardPlugIn

    Private mSolutionList As ReferenceListView
    Private mRecentList As ReferenceListView
    Private mWebList As ReferenceListView
    Private mWinList As ReferenceListView
#End Region

#Region "Constants"
    Private TAB_SOLUTION As Integer = 0
    Private TAB_RECENT As Integer = 1
    'Private TAB_RECENT As Integer = 2
    'Private TAB_RECENT As Integer = 3
    'Private TAB_RECENT As Integer = 4
    'Private TAB_RECENT As Integer = 5
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
    Public Function GetSolutionReferences() As ReferenceCollection
        Dim References As New ReferenceCollection
        For Each Project As ProjectElement In CodeRush.Source.ActiveSolution.AllProjects
            For Each AssemblyReference As AssemblyReference In Project.AssemblyReferences
                References.Add(New Reference(AssemblyReference))
            Next
        Next
        Return References
    End Function
    Private Sub AddReference(ByVal Reference As Reference)
        CodeRush.Project.Active.AddReference(Reference.FullName)
        MRUList.Add(Reference)
    End Sub
    Private Function CreateReferenceTab(ByVal TabName As String) As ReferenceListView
        Dim ReferenceListView As New ReferenceListView With {.Dock = DockStyle.Fill}
        ReferenceListView.SaveKey = TabName
        Dim NewTab As New TabPage(TabName)
        NewTab.Controls.Add(ReferenceListView)
        Tabs.TabPages.Add(NewTab)
        mDict.Add(TabName, ReferenceListView)
        Return ReferenceListView
    End Function

#End Region
#Region "Constructors"
    Public Sub New(ByVal Plugin As StandardPlugIn)
        Me.InitializeComponent()
        mPlugin = Plugin
        mSolutionList = CreateReferenceTab("Solution")
        mRecentList = CreateReferenceTab("Recent")
        mWinList = CreateReferenceTab("Win")
        mWebList = CreateReferenceTab("Web")
        Call LoadMRU()
        Call PopulateCustomLists()

    End Sub
#End Region
#Region "List Population"
    Private Sub PopulateSolutionReferences(Optional ByVal ForceRefresh As Boolean = False)
        Static sLoadedSolutionReferences As Boolean = False
        If Not sLoadedSolutionReferences OrElse ForceRefresh Then
            mSolutionList.ListView.Items.Clear()
            Dim References As ReferenceCollection = GetSolutionReferences()
            For Each Reference As Reference In References
                mSolutionList.ListView.Items.Add(ReferenceListItem.Of(Reference))
            Next
            sLoadedSolutionReferences = True
        End If
    End Sub
    Private Sub PopulateUserConfiguredList(ByVal ReferenceListView As ReferenceListView, ByVal TabName As String, Optional ByVal ForceRefresh As Boolean = False)
        Static sPopulated As New List(Of String)
        If Not sPopulated.Contains(TabName) OrElse ForceRefresh Then
            Dim ReferenceList As ReferenceListView = mDict.Item(TabName)
            ReferenceList.ListView.Items.Clear()
            For Each SavedReference As String In Storage.ReadStrings(SECTION_QUICKADD, TabName)
                ReferenceList.ListView.Items.Add(ReferenceListItem.Of(New Reference(SavedReference)))
            Next
        End If
    End Sub
    Private Sub PopulateMRUReferences(Optional ByVal ForceRefresh As Boolean = False)
        mRecentList.ListView.Items.Clear()
        For Each Reference As Reference In MRUList.Reverse()
            mRecentList.ListView.Items.Add(ReferenceListItem.Of(Reference))
        Next
    End Sub
#End Region

#Region "UI Events"
    Private Sub QuickAddReference_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call PopulateSolutionReferences()
        Call PopulateMRUReferences()
    End Sub
    Private Sub PopulateCustomLists()
        For Each List In mDict.Values
            Dim Values = Storage.ReadStrings("MRU", List.SaveKey)
            For Each Value In Values
                List.ListView.Items.Add(ReferenceListItem.Of(Value))
            Next
        Next
    End Sub

    Private Sub cmdRefreshTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshTab.Click
        Select Case Tabs.SelectedIndex
            Case TAB_SOLUTION
                Call PopulateSolutionReferences(True)
            Case TAB_RECENT
                Call PopulateMRUReferences(True)
            Case Else
                ' do nothing 
                Call PopulateUserConfiguredList(ActiveReferenceListView(), Tabs.TabPages(Tabs.TabIndex).Text)
        End Select
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim TheListview As ListView = ActiveReferenceListView().ListView
        If TheListview IsNot Nothing Then
            Dim Added As Integer
            For Each Item As ReferenceListItem In TheListview.CheckedItems
                Try
                    Call AddReference(Item.Reference)
                    Added += 1
                Catch ex As Exception
                    ' swallow exception
                End Try
            Next
            TheListview.CheckedItems.Cast(Of ListViewItem)().Where(Function(item) item.Checked = False)
            MsgBox(String.Format("Added '{0}' References", Added))
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        SaveMRU()
        Close()
    End Sub
    Private Sub SaveMRU()
        Dim MaxMRU = 5
        Dim ReferenceStrings As New List(Of String)
        Dim Count As Integer = 0
        For Each Reference As Reference In MRUList
            If Count = MaxMRU Then
                Exit For
            End If
            ReferenceStrings.Add(Reference.FullName)
            Count += 1
        Next
        Storage.WriteStrings("MRU", "MRU", ReferenceStrings.ToArray())
    End Sub
    Private Sub LoadMRU()
        MRUList.Clear()
        For Each item In Storage.ReadStrings(SECTION_QUICKADD, "MRU")
            MRUList.Add(New Reference(item))
        Next
        Call PopulateMRUReferences(True)
    End Sub
    Public Shared ReadOnly Property Storage() As DecoupledStorage
        Get
            Return OptionsQuickAddReference.Storage
        End Get
    End Property
#End Region
End Class