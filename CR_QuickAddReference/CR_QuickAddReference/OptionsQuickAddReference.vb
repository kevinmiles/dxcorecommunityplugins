Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports CR_QuickAddReference

<UserLevel(UserLevel.Advanced)> _
Public Class OptionsQuickAddReference
    Public Const SECTION_QUICKADD As String = "QuickAdd"
#Region "Fields"
    Private mEditors As New List(Of ReferenceListEditor)
    Private mWebOptions As ReferenceListEditor
    Private mWinOptions As ReferenceListEditor
#End Region

    'DXCore-generated code...
#Region " Initialize "
    Protected Overrides Sub Initialize()
        MyBase.Initialize()

        'TODO: Add your initialization code here.

    End Sub
#End Region
#Region " GetCategory "
    Public Shared Function GetCategory() As String
        Return "IDE"
    End Function
#End Region
#Region " GetPageName "
    Public Shared Function GetPageName() As String
        Return "Quick Add References"
    End Function
#End Region

    Public Sub New()
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        InitialiseTabs()
    End Sub


    Private Function CreateOptionTab(ByVal TabName As String) As ReferenceListEditor
        Dim NewTab As New TabPage(TabName)
        Tabs.TabPages.Add(NewTab)
        Dim ReferenceListEditor As ReferenceListEditor = New ReferenceListEditor()
        NewTab.Controls.Add(ReferenceListEditor)
        ReferenceListEditor.Dock = DockStyle.Fill
        ReferenceListEditor.SaveKey = TabName
        mEditors.Add(ReferenceListEditor)
        Return ReferenceListEditor
    End Function

    Private Sub InitialiseTabs()
        Call ClearOptionTabs()
        mWinOptions = CreateOptionTab("Win")
        mWebOptions = CreateOptionTab("Web")
    End Sub
    Private Sub ClearOptionTabs()
        Tabs.TabPages.Clear()
        mEditors.Clear()
    End Sub

    Private Sub OptionsQuickAddReference_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage
        ' Load Data into Page
        Dim Storage As DecoupledStorage = ea.Storage
        For Each Editor In mEditors
            Dim Values = Storage.ReadStrings(SECTION_QUICKADD, Editor.SaveKey)
            For Each Value In Values
                Editor.ReferenceListView.ListView.Items.Add(ReferenceListItem.Of(Value))
            Next
        Next
    End Sub
    Private Sub OptionsQuickAddReference_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CommitChangesEventArgs) Handles Me.CommitChanges
        ' Save Data into Page
        Dim Storage As DecoupledStorage = ea.Storage
        For Each Editor In mEditors
            Dim Values As New List(Of String)
            For Each Item As ReferenceListItem In Editor.ReferenceListView.ListView.Items
                Values.Add(Item.Reference.FullName)
            Next
            Storage.WriteStrings(SECTION_QUICKADD, Editor.SaveKey, Values.ToArray)
        Next
        ' Indicate to options Subsystem that options have changed
    End Sub

    Private Sub OptionsQuickAddReference_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles Me.RestoreDefaults
        For Each Editor In mEditors
            Editor.ReferenceListView.ListView.Items.Clear()
            ' Potentially add default items back 
        Next
    End Sub


End Class
