Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports System.Xml

<UserLevel(UserLevel.NewUser)> _
Public Class Options1
    Private Const Section As String = "TestGeneration"
    Private Const KEY_ProjectSuffix As String = "ProjectSuffix"
    Private Const KEY_FixtureSuffix As String = "FixtureSuffix"
    Private Const KEY_TestPrefix As String = "TestPrefix"
    Private Const KEY_TestSuffix As String = "TestSuffix"
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
        Return "Generation"
    End Function
#End Region
    Public Shared Function LoadNamingOptions() As NamingOptions
        Dim Options As New NamingOptions
        Options.ProjectSuffix = Storage.ReadString(Section, KEY_ProjectSuffix, Options.ProjectSuffix)
        Options.FixtureSuffix = Storage.ReadString(Section, KEY_FixtureSuffix, Options.FixtureSuffix)
        Options.TestPrefix = Storage.ReadString(Section, KEY_TestPrefix, Options.TestPrefix)
        Options.TestSuffix = Storage.ReadString(Section, KEY_TestSuffix, Options.TestSuffix)
        Return Options
    End Function

    Private Sub Options1_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage
        ' Load
        Dim Options = LoadNamingOptions()
        txtProjectSuffix.Text = Options.ProjectSuffix
        txtFixtureSuffix.Text = Options.FixtureSuffix
        txtTestPrefix.Text = Options.TestPrefix
        txtTestSuffix.Text = Options.TestSuffix
    End Sub

    Private Sub Options1_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CommitChangesEventArgs) Handles Me.CommitChanges
        ' Save 
        Using Storage As DecoupledStorage = ea.Storage
            Storage.WriteString(Section, KEY_ProjectSuffix, txtProjectSuffix.Text)
            Storage.WriteString(Section, KEY_FixtureSuffix, txtFixtureSuffix.Text)
            Storage.WriteString(Section, KEY_TestPrefix, txtTestPrefix.Text)
            Storage.WriteString(Section, KEY_TestSuffix, txtTestSuffix.Text)
        End Using
    End Sub

    Private Sub Options1_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles Me.RestoreDefaults
        ' Set Default
        txtProjectSuffix.Text = NamingOptions.DEFAULT_ProjectSuffix
        txtFixtureSuffix.Text = NamingOptions.DEFAULT_FixtureSuffix
        txtTestPrefix.Text = NamingOptions.DEFAULT_TestPrefix
        txtTestSuffix.Text = NamingOptions.DEFAULT_TestSuffix
    End Sub
End Class
