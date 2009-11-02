Option Strict On
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Diagnostics.Core
Imports EnvDTE
Imports EnvDTE.Constants
Public Class QuickAddReferencePlugin
    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

#Region "Dialog"
    Private Sub actQuickAddReference_Execute(ByVal ea As ExecuteEventArgs) Handles actQuickAddReference.Execute
        ShowQuickReferencesDialog()
    End Sub
#End Region
#Region "SmartTags"
    Private Sub AddReferenceMenu_GetSmartTagItemColors(ByVal sender As Object, ByVal ea As GetSmartTagItemColorsEventArgs) Handles AddReferenceMenu.GetSmartTagItemColors
        ea.PopupMenuColors = New ReferenceColors
    End Sub
    Private Sub AddReferenceMenu_GetSmartTagItems(ByVal sender As Object, ByVal ea As GetSmartTagItemsEventArgs) Handles AddReferenceMenu.GetSmartTagItems
        Call InitialiseLists()
        For Each ReferenceList In Lists.Lists
            ea.CreateSubMenu(ReferenceList.Name, ReferenceList.References)
        Next
        ea.Add(New SmartTagItemEx("QuickRef Dialog", AddressOf ShowQuickReferencesDialog))
        ea.Add(New SmartTagItemEx("Browse...", AddressOf BrowseForReference))
    End Sub
#End Region

#Region "Browse Functions "
    Private Sub BrowseForReference()
        ' This function to be remove once VS2010 allows Anonymous methods in VB10.
        Call AddReferenceViaBrowse(OptionsQuickAddReference.Storage)
    End Sub
    Public Sub AddReferenceViaBrowse(ByVal Storage As DecoupledStorage)
        Dim StartPath As String = Storage.ReadString(OptionsQuickAddReference.SECTION_QUICKADD, "DefaultBrowsePath", "%WinDir%\Microsoft.Net\Framework\")
        Call AddReferenceViaBrowse(StartPath)
        Storage.WriteString(OptionsQuickAddReference.SECTION_QUICKADD, "DefaultBrowsePath", StartPath)
    End Sub

    Public Sub AddReferenceViaBrowse(ByRef StartPath As String)
        Dim References = BrowseForReferences(StartPath)
        For Each Reference In References
            Call Reference.AddToProjectTestingForGAC(CodeRush.Project.Active)
        Next
    End Sub

    Public Function BrowseForReferences(ByRef StartDir As String) As List(Of Reference)
        Dim Results As New List(Of Reference)
        Using OpenFileDialog As New OpenFileDialog() With {.InitialDirectory = StartDir, .Multiselect = True}
            If OpenFileDialog.ShowDialog = DialogResult.OK Then
                For Each Filename In OpenFileDialog.FileNames
                    Dim Reference As Reference = New Reference(Filename)
                    Try
                        Results.Add(Reference)
                    Catch ex As Exception
                        Dim LogMessage As String = String.Format("Failed to Add Reference '{0}', '{1}'", _
                                             Reference.FullName, _
                                             Reference.FileName)
                        Log.SendMsg(LogMessage)

                    End Try
                Next
                StartDir = OpenFileDialog.InitialDirectory
            End If
        End Using
        Return Results
    End Function
#End Region
    Private Sub ShowQuickReferencesDialog()
        Call (New QuickAddReference(Me)).ShowDialog()
    End Sub
#Region "Research"

    'Public Function getSelectedsolExplorerItem() As UIHierarchyItem
    '    Return
    'End Function

    'Private Sub action1_Execute(ByVal ea As ExecuteEventArgs)
    '    Dim SelectedItem = GetFirstSelectedSolItem(SolExplorer.UIHierarchyItems.Item(1))

    'End Sub

    'Private Shared Function SolExplorer() As UIHierarchy
    '    Return CType(CodeRush.ApplicationObject.Windows.Item(Constants.vsext_wk_SProjectWindow).Object, UIHierarchy)
    'End Function
    'Public Function GetFirstSelectedSolItem(ByVal item As UIHierarchyItem) As UIHierarchyItem
    '    If (item.IsSelected) Then
    '        Return item
    '    End If
    '    If (item.UIHierarchyItems.Count > 0) Then
    '        For i = 1 To item.UIHierarchyItems.Count
    '            Return GetFirstSelectedSolItem(item.UIHierarchyItems.Item(i))
    '        Next
    '    End If
    '    Return Nothing
    'End Function
#End Region

End Class
