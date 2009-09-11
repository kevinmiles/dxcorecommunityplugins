
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class PlugIn1


    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
        Call LoadSettings()

    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Private mMetric As ICodeMetricProvider
    Private Sub LoadSettings()
        mMetric = Options1.Providers(Options1.Storage.ReadInt32("MetricShader", "MetricName", 0))
    End Sub
    Private Sub PlugIn1_EditorPaintBackground(ByVal ea As EditorPaintEventArgs) Handles Me.EditorPaintBackground
        If CodeRush.Source.ActiveClass Is Nothing Then
            Exit Sub
        End If
        For Each Member In CodeRush.Source.ActiveClass.AllMembers.OfType(Of Member)()
            Dim View = CodeRush.Documents.ActiveTextView
            Dim Rect As Rectangle = View.GetRectangleFromRange(Member.GetFullBlockCutRange)
            View.HighlightCode(Member.GetFullBlockCutRange, _
                               GetColor(Member), _
                               GetColor(Member), _
                                Color.White)
        Next
    End Sub
    Private Function GetColor(ByVal Member As Member) As Color
        Select Case mMetric.GetValue(Member)
            Case Is < 0.25 * mMetric.WarningValue
                Return Color.FromArgb(0, Color.White)
            Case Is < 0.5 * mMetric.WarningValue
                Return Color.FromArgb(30, Color.Green)
            Case Is < 0.75 * mMetric.WarningValue
                Return Color.FromArgb(50, Color.Orange)
            Case Else
                Return Color.FromArgb(75, Color.Red)
        End Select
    End Function

    Private Sub LineCountDemoMetric_GetMetricValue(ByVal sender As System.Object, ByVal e As DevExpress.CodeRush.Extensions.GetMetricValueEventArgs) Handles LineCountDemoMetric.GetMetricValue
        e.Value = e.LanguageElement.Range.Height
    End Sub

    Private Sub PlugIn1_OptionsChanged(ByVal ea As DevExpress.CodeRush.Core.OptionsChangedEventArgs) Handles Me.OptionsChanged
        If ea.OptionsPages.Contains(GetType(Options1)) Then
            LoadSettings()
        End If
    End Sub
End Class