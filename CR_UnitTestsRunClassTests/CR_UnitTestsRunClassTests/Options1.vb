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

        'TODO: Add your initialization code here.
    End Sub
#End Region

#Region " GetCategory "
    Public Shared Function GetCategory() As String
        Return "Unit Testing"
    End Function
#End Region
#Region " GetPageName "
    Public Shared Function GetPageName() As String
        Return "UnitTestsRunClassTests"
    End Function
#End Region
    Private PluginSettings As New PluginSettings
    Private Sub HookupOptions()
        AddHandler Options1.RestoreDefaults, AddressOf Options1_RestoreDefaults
        AddHandler Options1.PreparePage, AddressOf Options1_PreparePage
        AddHandler Options1.CommitChanges, AddressOf Options1_CommitChanges
    End Sub
    Const SETTING_SECTION As String = "UnitTestsRunClassTests"
    Const SETTING_TestNamespaceSuffix As String = "TestNamespaceSuffix"
    Const SETTING_TestClassSuffix As String = "TestClassSuffix"

#Region "Events"
    Private Sub Options1_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles Me.RestoreDefaults
        ' Setup defaults here
        Call LoadPageFromSettings(New PluginSettings())
    End Sub

    Private Sub Options1_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage
        ' Load options here
        Call LoadPageFromSettings(LoadSettingsFromStorage(ea.Storage))
    End Sub

    Private Sub Options1_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CommitChangesEventArgs) Handles Me.CommitChanges
        ' Save changes here.
        SavePageToSettings(ea.Storage)
    End Sub
#End Region
    Private Sub SavePageToSettings(ByVal Storage1 As DecoupledStorage)
        Using Storage = Storage1
            Storage.WriteString(SETTING_SECTION, SETTING_TestNamespaceSuffix, txtTestNamespaceSuffix.Text)
            Storage.WriteString(SETTING_SECTION, SETTING_TestClassSuffix, txtTestClassSuffix.Text)
        End Using
    End Sub

    Private Sub LoadPageFromSettings(ByVal PluginSettings As PluginSettings)
        txtTestClassSuffix.Text = PluginSettings.TestClassSuffix
        txtTestNamespaceSuffix.Text = PluginSettings.TestNamespaceSuffix
    End Sub

    Public Shared Function LoadSettingsFromStorage(ByVal Storage1 As DecoupledStorage) As PluginSettings
        Dim Settings As New PluginSettings
        Using Storage = Storage1
            Settings.TestNamespaceSuffix = Storage.ReadString(SETTING_SECTION, SETTING_TestNamespaceSuffix, PluginSettings.DEFAULT_TestNamespaceSuffix)
            Settings.TestClassSuffix = Storage.ReadString(SETTING_SECTION, SETTING_TestClassSuffix, PluginSettings.DEFAULT_TestClassSuffix)
        End Using
        Return Settings
    End Function
End Class
