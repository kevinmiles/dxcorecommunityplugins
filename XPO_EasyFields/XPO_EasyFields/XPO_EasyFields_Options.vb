Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core

<UserLevel(UserLevel.NewUser)> _
Public Class XPO_EasyFields_Options

	'DXCore-generated code...
#Region " Initialize "
	Protected Overrides Sub Initialize()
		MyBase.Initialize()

		'TODO: Add your initialization code here.
	End Sub
#End Region

#Region " GetCategory "
	Public Shared Function GetCategory() As String
		Return "Editor\XPO"
	End Function
#End Region
#Region " GetPageName "
	Public Shared Function GetPageName() As String
		Return "EasyFields"
	End Function
#End Region

    Private Sub XPO_EasyFields_Options_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CommitChangesEventArgs) Handles Me.CommitChanges
        'ea.Storage.WriteString("Setting","RegionName",
        '          ea.Storage.WriteBoolean("Preferences", "Enabled",
        'chkMySuperFeatureEnabled.Checked);
    End Sub

    Private Sub XPO_EasyFields_Options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub XPO_EasyFields_Options_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage
        '          chkMySuperFeatureEnabled.Checked = ea.Storage.ReadBoolean("Preferences",
        '"Enabled", true);
    End Sub

    Private Sub XPO_EasyFields_Options_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles Me.RestoreDefaults
        'chkMySuperFeatureEnabled.Checked = true;
    End Sub
End Class
