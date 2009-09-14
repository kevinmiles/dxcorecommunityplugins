Option Strict On
Option Explicit On
Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

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

    'Improvements
    ' Configure %ages
    ' Configure Colors
    ' Configure Opacity
    ' Configure Widths


    Private mMetric As ICodeMetricProvider
    Private Sub LoadSettings()
        mMetric = Options1.Providers(Options1.Storage.ReadInt32(Options1.STR_MetricShader, Options1.STR_MetricName, 0))
    End Sub
    Private Sub PlugIn1_EditorPaintBackground(ByVal ea As EditorPaintEventArgs) Handles Me.EditorPaintBackground
        If CodeRush.Source.ActiveClass Is Nothing Then
            Exit Sub
        End If
        Dim View = CodeRush.Documents.ActiveTextView
        Dim Members = CodeRush.Source.ActiveClass.AllMembers.OfType(Of Member)()
        Dim ViewRange As SourceRange = View.GetRangeFromRectangle(View.Bounds)
        For Each Member In Members.Where(Function(m) ViewRange.Overlaps(m.GetFullBlockCutRange))
            Dim PaintColor = GetColor(Member)
            If PaintColor.HasValue Then
                View.HighlightCode(Member.GetFullBlockCutRange, PaintColor.Value, PaintColor.Value, Color.White)
            End If
        Next
    End Sub
    Private Function GetColor(ByVal Member As Member) As Nullable(Of Color)
        Select Case mMetric.GetValue(Member)
            Case Is < CInt(0.25 * mMetric.WarningValue)
                Return Nothing
            Case Is < CInt(0.5 * mMetric.WarningValue)
                Return Color.FromArgb(30, Color.Green)
            Case Is < CInt(0.75 * mMetric.WarningValue)
                Return Color.FromArgb(50, Color.Orange)
            Case Else
                Return Color.FromArgb(75, Color.Red)
        End Select
    End Function

    Private Sub PlugIn1_OptionsChanged(ByVal ea As OptionsChangedEventArgs) Handles Me.OptionsChanged
        If ea.OptionsPages.Contains(GetType(Options1)) Then
            LoadSettings()
        End If
    End Sub

    Private Sub LineCountDemoMetric_GetMetricValue(ByVal sender As System.Object, ByVal e As DevExpress.CodeRush.Extensions.GetMetricValueEventArgs) Handles LineCountDemoMetric.GetMetricValue
        e.Value = e.LanguageElement.Range.Height
    End Sub
End Class
