Imports DevExpress.CodeRush.Core

Public Class PluginSettings
    Public Const MENU_DEVEXPRESS As String = "DevE&xpress"
    Public Const MENU_DXV2 As String = "DX&V2"
    Public Const DEFAULT_Menu As String = PluginSettings.MENU_DXV2
    Public Sub New()
        ChosenMenuName = DEFAULT_MENU
    End Sub
    Public Property ChosenMenuName() As String
End Class