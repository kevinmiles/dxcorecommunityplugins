Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core

<UserLevel(UserLevel.Advanced)> _
Public Class Options1

	'DXCore-generated code...
#Region " Initialize "
	Protected Overrides Sub Initialize()
		MyBase.Initialize()

		'TODO: Add your initialization code here.
	End Sub
#End Region

#Region " GetCategory "
	Public Shared Function GetCategory() As String
		Return "Community\Testing"
	End Function
#End Region
#Region " GetPageName "
	Public Shared Function GetPageName() As String
		Return "GenerateTest"
	End Function
#End Region


    Private Sub Options1_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles Me.RestoreDefaults

    End Sub

    Private Sub Options1_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage

    End Sub

    Private Sub Options1_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CommitChangesEventArgs) Handles Me.CommitChanges

    End Sub

End Class
