Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports System.Runtime.CompilerServices

<UserLevel(UserLevel.NewUser)> _
Public Class Options1
    Friend Const STR_MetricName As String = "MetricName"
    Friend Const STR_MetricShader As String = "MetricShader"
    Friend Const STR_ShaderEnabled As String = "ShaderEnabled"

    'DXCore-generated code...
#Region " Initialize "
    Protected Overrides Sub Initialize()
        MyBase.Initialize()

        'TODO: Add your initialization code here.
    End Sub
#End Region

#Region " GetCategory "
    Public Shared Function GetCategory() As String
        Return "Editor\Painting"
    End Function
#End Region
#Region " GetPageName "
    Public Shared Function GetPageName() As String
        Return "Metric Shader"
    End Function
#End Region

    Friend Shared Function Providers() As ICodeMetricProvider()
        Return CodeRush.Source.GetAllCodeMetricProviders.Where(Function(f) f.MetricGoal <> MetricGoal.Types).ToArray
    End Function
    Private Sub RefreshProviderList()
        cbxMetric.Items.Clear()
        For Each Provider In Providers()
            cbxMetric.Items.Add(Provider.DisplayName)
        Next
    End Sub
    Private Sub Options1_RestoreDefaults(ByVal sender As Object, ByVal ea As OptionsPageEventArgs) Handles Me.RestoreDefaults
        ' Load Default Values
        Call RefreshProviderList()
        cbxMetric.SelectedIndex = 0
        chkEnabled.Checked = True
    End Sub

    Private Sub Options1_PreparePage(ByVal sender As Object, ByVal ea As OptionsPageStorageEventArgs) Handles Me.PreparePage
        Call RefreshProviderList()
        cbxMetric.SelectedIndex = ea.Storage.ReadInt32(STR_MetricShader, STR_MetricName, 0)
        chkEnabled.Checked = ea.Storage.ReadBoolean(STR_MetricShader, STR_ShaderEnabled, True)
    End Sub

    Private Sub Options1_CommitChanges(ByVal sender As Object, ByVal ea As CommitChangesEventArgs) Handles Me.CommitChanges
        ea.Storage.WriteInt32(STR_MetricShader, STR_MetricName, cbxMetric.SelectedIndex)
        ea.Storage.WriteBoolean(STR_MetricShader, STR_ShaderEnabled, chkEnabled.Checked)
    End Sub

    Private Sub cbxMetric_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxMetric.SelectedIndexChanged
        lblMax.text = Providers(cbxMetric.SelectedIndex).WarningValue
    End Sub
End Class
