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
    End Sub
#End Region

#Region " GetCategory "
    Public Shared Function GetCategory() As String
        Return "Editor\Auto Complete"
    End Function
#End Region
#Region " GetPageName "
    Public Shared Function GetPageName() As String
        Return "ExceptionHelper"
    End Function
#End Region
    Private Settings As PluginSettings
    Public Sub Options1_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage
        Settings = PluginSettings.LoadFromStorage(ea.Storage)
        chkLogMissingFiles.Checked = Settings.ShouldLog
    End Sub

    Private Sub Options1_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CommitChangesEventArgs) Handles Me.CommitChanges
        Settings.ShouldLog = chkLogMissingFiles.Checked
        Settings.SaveToStorage(ea.Storage)
    End Sub

    Private Sub Options1_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles Me.RestoreDefaults
        Settings = New PluginSettings
        chkLogMissingFiles.Checked = Settings.ShouldLog
    End Sub
End Class
