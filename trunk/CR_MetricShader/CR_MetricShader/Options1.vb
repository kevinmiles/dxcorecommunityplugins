Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports System.Runtime.CompilerServices

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
    Private Sub Options1_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles Me.RestoreDefaults
        ' Load default values
        cbxMetric.Items.Clear()
        For Each Provider In Providers()
            cbxMetric.Items.Add(Provider.DisplayName)
        Next
        cbxMetric.SelectedIndex = 0
    End Sub

    Private Sub Options1_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage
        cbxMetric.Items.Clear()
        For Each Provider In Providers()
            cbxMetric.Items.Add(Provider.DisplayName)
        Next
        cbxMetric.SelectedIndex = ea.Storage.ReadInt32("MetricShader", "MetricName", 0)
    End Sub

    Private Sub Options1_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CommitChangesEventArgs) Handles Me.CommitChanges
        ea.Storage.WriteInt32("MetricShader", "MetricName", cbxMetric.SelectedIndex)
    End Sub

    Private Sub cbxMetric_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxMetric.SelectedIndexChanged
        lblMax.text = Providers(cbxMetric.SelectedIndex).WarningValue
    End Sub
End Class
