Imports System.IO
Imports System.Windows.Forms

Public Class ReferenceListEditor
#Region "Fields"
    Private mSaveKey As String
#End Region
#Region "Properties"
    Public Property SaveKey() As String
        Get
            Return mSaveKey
        End Get
        Set(ByVal Value As String)
            mSaveKey = Value
        End Set
    End Property
#End Region

#Region "UI Events"
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If Not File.Exists(txtFilename.Filename) Then
            MsgBox("Referenced File does Not Exist")
            Exit Sub
        End If
        ReferenceListView.ListView.Items.Add(ReferenceListViewItem.Of(New Reference(txtFilename.Filename)))
        txtFilename.Filename = ""
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        For Each item As ListViewItem In ReferenceListView.ListView.SelectedItems
            ReferenceListView.ListView.Items.Remove(item)
        Next
    End Sub

    Private Sub ReferenceListEditor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ReferenceListView.ListView.CheckBoxes = False
    End Sub
#End Region

End Class

