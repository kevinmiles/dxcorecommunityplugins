Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core

<UserLevel(UserLevel.NewUser)> _
Public Class SortCodeOptions

	'DXCore-generated code...
#Region " Initialize "
	Protected Overrides Sub Initialize()
		MyBase.Initialize()

		'TODO: Add your initialization code here.
	End Sub
#End Region

#Region " GetCategory "
	Public Shared Function GetCategory() As String
		Return "Editor\SortCode"
	End Function
#End Region
#Region " GetPageName "
	Public Shared Function GetPageName() As String
		Return "SortCode"
	End Function
#End Region

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            If openFile.ShowDialog = DialogResult.OK Then
                txtFile.Text = openFile.FileName
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub SortCodeOptions_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.CommitChanges
        Storage.WriteString("SortCode", "LayoutFile", txtFile.Text)
    End Sub


    Private Sub SortCodeOptions_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage
        If Storage.ValueExists("SortCode", "LayoutFile") Then
            CR_SortCodePlugIn.LayoutFile = Storage.ReadString("SortCode", "LayoutFile")
        End If
    End Sub
End Class
