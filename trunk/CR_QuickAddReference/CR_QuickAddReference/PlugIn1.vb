Option Strict On
Option Infer On
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Diagnostics.Core
Imports EnvDTE
Imports EnvDTE.Constants
Imports DevExpress.CodeRush.StructuralParser
Imports Alias1 = DevExpress.CodeRush.Core
Imports System.Runtime.CompilerServices

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

    Private mTargetProject As DevExpress.CodeRush.Core.Project
#Region "Dialog"
    Private Sub actQuickAddReference_Execute(ByVal ea As ExecuteEventArgs) Handles actQuickAddReference.Execute
        ShowQuickReferencesDialogFromWithinSmartTagItem()
    End Sub
#End Region
#Region "SmartTags"
    Private Sub AddReferenceMenu_GetSmartTagItemColors(ByVal sender As Object, ByVal ea As GetSmartTagItemColorsEventArgs) Handles AddReferenceMenu.GetSmartTagItemColors
        ea.PopupMenuColors = New ReferenceColors
    End Sub
    Private Sub AddReferenceMenu_GetSmartTagItems(ByVal sender As Object, ByVal ea As GetSmartTagItemsEventArgs) Handles AddReferenceMenu.GetSmartTagItems
        Call InitialiseLists()
        mTargetProject = CodeRush.Project.Active
        For Each ReferenceList In Lists.Lists
            ea.CreateSubMenu(ReferenceList.Name, ReferenceList.References)
        Next
        ea.Add(New SmartTagItemEx("QuickRef Dialog", AddressOf ShowQuickReferencesDialogFromWithinSmartTagItem))
        ea.Add(New SmartTagItemEx("Browse...", AddressOf BrowseForReferenceFromWithinSmartTagItem))
    End Sub
    Private Sub ShowQuickReferencesDialogFromWithinSmartTagItem()
        ' This function to be remove once VS2010 allows Anonymous methods in VB10.
        Call AddReferenceToProjectViaDialog(mTargetProject)
    End Sub
    Private Sub BrowseForReferenceFromWithinSmartTagItem()
        ' This function to be remove once VS2010 allows Anonymous methods in VB10.
        Call AddReferenceViaBrowse(OptionsQuickAddReference.Storage, mTargetProject)
    End Sub
#End Region
    Public Sub AddReferenceToProjectViaDialog(ByVal TargetProject As DevExpress.CodeRush.Core.Project)
        Call (New QuickAddReference(Me, TargetProject)).ShowDialog()
    End Sub

#Region "Browse Functions "

    Public Sub AddReferenceViaBrowse(ByVal Storage As DecoupledStorage, ByVal Project As DevExpress.CodeRush.Core.Project)
        Dim StartPath As String = Storage.ReadString(OptionsQuickAddReference.SECTION_QUICKADD, "DefaultBrowsePath", "%WinDir%\Microsoft.Net\Framework\")
        Call AddReferenceViaBrowse(StartPath, Project)
        Storage.WriteString(OptionsQuickAddReference.SECTION_QUICKADD, "DefaultBrowsePath", StartPath)
    End Sub

    Public Sub AddReferenceViaBrowse(ByRef StartPath As String, ByVal Project As DevExpress.CodeRush.Core.Project)
        Dim References = BrowseForReferences(StartPath)
        For Each Reference In References
            Call Reference.AddToProjectTestingForGAC(Project)
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

#Region "Research"


    ' TODO: Trying to add an action to the Project Context menu 
    '       to act upon the right clicked project instead of the 
    '       Project relating to the active TextDocument

    Private Sub actQuickAddReferenceSolExp_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles actQuickAddReferenceSolExp.Execute
        ' since This Action only positioned on Project context menu, the first selected item must be a project
        'Dim SelectedItem = GetFirstSelectedSolItem(SolExplorer.UIHierarchyItems.Item(1))
        Dim ENVDTEProject = SolExplorerSelectedProject()
        If ENVDTEProject IsNot Nothing Then
            Dim Project = CodeRush.Solution.AllProjects.Where(Function(p) p.Name = ENVDTEProject.Name).FirstOrDefault
            AddReferenceToProjectViaDialog(Project)
        End If
    End Sub
    Private Function SolExplorerSelectedProject() As EnvDTE.Project
        Dim AllSelectedItems As Object() = CType(SolExplorer.SelectedItems, Object())
        Dim Selected = CType(AllSelectedItems(0), EnvDTE.UIHierarchyItem)
        If Selected Is Nothing Then
            Return Nothing
        End If
        'If Selected.Project IsNot Nothing Then
        '    Return Selected.Project
        'End If
        If Selected.ProjectItem IsNot Nothing Then
            Return Selected.ProjectItem.ContainingProject
        End If
        For Each Item As UIHierarchyItem In CType(SolExplorer.SelectedItems, System.Array)
            If Item.Name.Equals("References") Then
                Dim parent = Item.Parent
                If TypeOf parent.Object Is EnvDTE.Project Then
                    Return CType(parent.Object, EnvDTE.Project)
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Shared Function GetFirstSelectedItem() As UIHierarchyItem
        Dim AllSelectedItems As Object() = CType(SolExplorer.SelectedItems, Object())
        Return CType(AllSelectedItems(0), UIHierarchyItem)
    End Function
    'Public Function GetUIHierarchyItemProject(ByVal SelectedItem As UIHierarchyItem) As EnvDTE.Project
    '    Try
    '        'Return CType(SelectedItem.object, ProjectItem).ContainingProject
    '        Dim HItem = SelectedItem
    '        Do
    '            Dim ProjectItem = CType(HItem.Object, ProjectItem)
    '            If ProjectItem Is Nothing Then
    '                Return Nothing
    '            End If
    '            If ProjectItem.Kind = EnvDTE.Constants.vsProjectItemKindSubProject Then
    '                Return CType(ProjectItem, EnvDTE.Project)
    '            End If
    '            ProjectItem = CType(ProjectItem.Collection.Parent, ProjectItem)
    '        Loop
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function



    Private Shared Function SolExplorer() As UIHierarchy
        Return CType(CodeRush.ApplicationObject.Windows.Item(Constants.vsext_wk_SProjectWindow).Object, UIHierarchy)
    End Function
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
Public Module UIHierarchyItemExt
    <Extension()> _
    Public Function Parent(ByVal Source As UIHierarchyItem) As UIHierarchyItem
        Return CType(Source.Collection.Parent, UIHierarchyItem)
    End Function
    <Extension()> _
    Public Function ProjectItem(ByVal Source As UIHierarchyItem) As ProjectItem
        Try
            Return CType(Source.Object, ProjectItem)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Module