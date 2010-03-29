Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core

<UserLevel(UserLevel.NewUser)> _
Public Class XPO_EasyFields_Options
    Public Const STR_Setting As String = "Setting"
    Public Const STR_RegionName As String = "RegionName"
    Public Const STR_AvailableClassNameOnly As String = "ClassNameOnly"
    Public Const STR_UpdateOnSave As String = "UpdateOnSave"
    Public Const STR_ReplaceClassOnly As String = "ReplaceClassOnly"

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
        ea.Storage.WriteBoolean(STR_Setting, STR_AvailableClassNameOnly, chkClassNameOnly.Checked)
        ea.Storage.WriteBoolean(STR_Setting, STR_UpdateOnSave, chkUpdateOnSave.Checked)
        ea.Storage.WriteBoolean(STR_Setting, STR_ReplaceClassOnly, chkReplaceClassOnly.Checked)
        '          ea.Storage.WriteBoolean("Preferences", "Enabled",
        'chkMySuperFeatureEnabled.Checked);
    End Sub

    Private Sub XPO_EasyFields_Options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub XPO_EasyFields_Options_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage
        chkClassNameOnly.Checked = ea.Storage.ReadBoolean(STR_Setting, STR_AvailableClassNameOnly)
        chkReplaceClassOnly.Checked = ea.Storage.ReadBoolean(STR_Setting, STR_ReplaceClassOnly)
        chkUpdateOnSave.Checked = ea.Storage.ReadBoolean(STR_Setting, STR_UpdateOnSave)
    End Sub

    Private Sub XPO_EasyFields_Options_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles Me.RestoreDefaults
        chkClassNameOnly.Checked = False
        chkReplaceClassOnly.Checked = True
        chkUpdateOnSave.Checked = False
    End Sub
End Class
