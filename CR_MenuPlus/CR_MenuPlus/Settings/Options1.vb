Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports System.Xml
<UserLevel(UserLevel.NewUser)> _
Public Class Options1
    'DXCore-generated code...
#Region " Initialize "
    Protected Overrides Sub Initialize()
        MyBase.Initialize()
        HookupOptions()
    End Sub
#End Region

#Region " GetCategory "
    Public Shared Function GetCategory() As String
        Return "Community"
    End Function
#End Region
#Region " GetPageName "
    Public Shared Function GetPageName() As String
        Return "MenuPlus"
    End Function
#End Region
    Private mPluginSettings As PluginSettings
    Private Sub HookupOptions()
        AddHandler Options1.RestoreDefaults, AddressOf Options1_RestoreDefaults
        AddHandler Options1.PreparePage, AddressOf Options1_PreparePage
        AddHandler Options1.CommitChanges, AddressOf Options1_CommitChanges
    End Sub

    Private Sub Options1_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles Me.RestoreDefaults
        ' Setup defaults here
        mPluginSettings = New PluginSettings
        Call UpdateUIFromSettings(mPluginSettings)

    End Sub

    Private Sub Options1_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage
        ' Load options here
        mPluginSettings = PluginSettingsRepo.LoadSettings(ea.Storage)
        Call UpdateUIFromSettings(mPluginSettings)
    End Sub


    Private Sub Options1_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CommitChangesEventArgs) Handles Me.CommitChanges
        ' Save changes here.
        UpdateSettingsFromUI(mPluginSettings)
        PluginSettingsRepo.SaveSettings(Storage, mPluginSettings)
    End Sub

#Region "UI Updating"

    Private Sub UpdateUIFromSettings(ByVal PluginSettings As PluginSettings)
        txtMenuName.Text() = ""
        Select Case PluginSettings.ChosenMenuName
            Case PluginSettings.MENU_DEVEXPRESS
                optDevExpress.Checked = True
            Case PluginSettings.MENU_DXV2
                optDXV2.Checked = True
            Case Else
                optOther.Checked = True
                txtMenuName.Text = PluginSettings.ChosenMenuName
        End Select
    End Sub
    Private Sub UpdateSettingsFromUI(ByVal PluginSettings As PluginSettings)
        Select Case True
            Case optDevExpress.Checked
                mPluginSettings.ChosenMenuName = PluginSettings.MENU_DEVEXPRESS
            Case optDXV2.Checked
                mPluginSettings.ChosenMenuName = PluginSettings.MENU_DXV2
            Case Else
                mPluginSettings.ChosenMenuName = txtMenuName.Text()
        End Select
    End Sub
#End Region


End Class
