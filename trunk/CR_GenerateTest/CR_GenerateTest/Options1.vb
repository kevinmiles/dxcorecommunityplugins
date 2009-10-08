Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core


<UserLevel(UserLevel.Advanced)> _
Public Class Options1

    Private mSettingsXML As XElement
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


    Private Sub Options1_RestoreDefaults(ByVal sender As Object, ByVal ea As OptionsPageEventArgs) Handles Me.RestoreDefaults
        ' Restore Templates
        ' Set Default Framework (To NUnit)
        ' Set Framework Assembly Location (To "%Program Files%\NUnit 2.5.2\")

    End Sub

    Private Sub Options1_PreparePage(ByVal sender As Object, ByVal ea As OptionsPageStorageEventArgs) Handles Me.PreparePage
        ' Load Data
        mSettingsXML = PluginSettings.Load(ea.Storage)
        cbx()
    End Sub

    Private Sub Options1_CommitChanges(ByVal sender As Object, ByVal ea As CommitChangesEventArgs) Handles Me.CommitChanges

    End Sub

    Private Sub cbxFramework_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxFramework.SelectedIndexChanged
        ' Switch Settings from Old Framework to new Framework.
    End Sub
End Class