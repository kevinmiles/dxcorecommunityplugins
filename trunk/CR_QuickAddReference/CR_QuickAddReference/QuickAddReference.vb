Option Strict On
Option Infer On
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core
Imports System.Windows.Forms

Public Class QuickAddReference
    Private Const TAB_SOLUTION As Integer = 0
    Private Const TAB_RECENT As Integer = 1

    Private Sub QuickAddReference_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call PopulateSolutionReferences()
        Call PopulateMRUList()
    End Sub

    Private Sub PopulateMRUList(Optional ByVal ForceRefresh As Boolean = False)

    End Sub
    Private Sub PopulateSolutionReferences(Optional ByVal ForceRefresh As Boolean = False)
        Static sLoadedSolutionReferences As Boolean = False
        If Not sLoadedSolutionReferences OrElse ForceRefresh Then
            lstSolution.Items.Clear()
            Dim References As ReferenceCollection = GetSolutionReferences()
            For Each Reference As AssemblyReference In References.Values
                Dim Item = New ReferenceListItem With {.Reference = Reference}

                lstSolution.Items.Add(Item)
            Next
            sLoadedSolutionReferences = True
        End If
    End Sub
    Public Function GetSolutionReferences() As ReferenceCollection
        Dim References As New ReferenceCollection
        For Each Project As ProjectElement In CodeRush.Source.ActiveSolution.AllProjects
            For Each Reference As AssemblyReference In Project.AssemblyReferences
                References.Item(Reference.Location) = Reference
            Next
        Next
        Return References
    End Function

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim TheListview As ListView = ActiveListView()
        For Each Item As ReferenceListItem In TheListview.SelectedItems
            Try
                CodeRush.Project.Active.AddReference(Item.Reference.FilePath)
            Catch ex As Exception
                
            Finally
                
            End Try
        Next
    End Sub
    Private Function ActiveListView() As ListView
        Dim TheListview As ListView
        Select Case Tabs.SelectedIndex
            Case TAB_SOLUTION
                TheListview = lstSolution
            Case TAB_RECENT
                TheListview = lstRecent
            Case Else
                Throw New ApplicationException("Could not determine Active List")
        End Select
        Return TheListview
    End Function

    Private Sub cmdRefreshTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshTab.Click
        Select Case Tabs.SelectedIndex
            Case TAB_SOLUTION
                Call PopulateSolutionReferences(True)
            Case TAB_RECENT
                Call PopulateMRUList(True)
            Case Else
                Throw New ApplicationException("Could not determine Active List")
        End Select
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Close()
    End Sub
End Class
Public Class ReferenceListItem
    Inherits ListViewItem
    Private mReference As AssemblyReference
    Public Property Reference() As AssemblyReference
        Get
            Return mReference
        End Get
        Set(ByVal value As AssemblyReference)
            mReference = value
            SubItems.Clear()
            Me.Text = mReference.Name
            Me.Name = mReference.Name
            Me.SubItems.Add(New ListViewSubItem(Me, mReference.FilePath))
        End Set
    End Property
End Class