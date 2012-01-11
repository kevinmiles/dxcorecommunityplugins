Imports DevExpress.CodeRush.Core

Public Class PluginSettingsRepo
    Private Const SETTING_SECTION As String = "CR_Discover"
    Private Const SETTING_MenuName As String = "MenuName"


    Public Shared Function LoadSettings(ByVal Storage As DecoupledStorage) As PluginSettings
        Dim Settings As New PluginSettings
        Settings.ChosenMenuName = Storage.ReadString(SETTING_SECTION, SETTING_MenuName, PluginSettings.MENU_DXV2)
        Return Settings
    End Function
    Public Shared Sub SaveSettings(ByVal Storage As DecoupledStorage, ByVal PluginSettings As PluginSettings)
        Storage.WriteString(SETTING_SECTION, SETTING_MenuName, PluginSettings.ChosenMenuName)
    End Sub
End Class
