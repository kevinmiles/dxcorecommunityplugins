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
Imports System.Reflection
Imports DevExpress.CodeRush.Menus
Imports DevExpress.CodeRush.Extensions

Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
        Call LoadSettings()
        Call CreateVisualizeButton()

    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    'Improvements
    ' Done - "Enabled" option
    ' Done - DXCore visualize Toolbar
    ' Configure %ages
    ' Configure Colors
    ' Configure Opacity
    ' Configure Widths

#Region "Fields"
    Private mEnabled As Boolean = True ' Default to On
    Private mMetric As ICodeMetricProvider
    Private mVisualizeButton As IMenuButton
#End Region
#Region "Resources"
    Public Function GetBitmapByName(ByVal BitmapName As String) As Bitmap
        Dim Asm As Assembly = Assembly.GetAssembly(Me.GetType)
        Dim stream As IO.Stream = Asm.GetManifestResourceStream(String.Format("CR_MetricShader.{0}", BitmapName))
        Return CType(Bitmap.FromStream(stream), Bitmap)
    End Function
#End Region
#Region "Settings"
    Private Sub LoadSettings()
        mMetric = Options1.Providers(Options1.Storage.ReadInt32(Options1.STR_MetricShader, Options1.STR_MetricName, 0))
        mEnabled = Options1.Storage.ReadBoolean(Options1.STR_MetricShader, Options1.STR_ShaderEnabled, True)
        Call CodeRush.TextViews.Refresh()
        If (mVisualizeButton IsNot Nothing) Then
            mVisualizeButton.IsDown = mEnabled
        End If
    End Sub
#End Region
#Region "Editor Events"
    Private Sub PlugIn1_EditorPaintBackground(ByVal ea As EditorPaintEventArgs) Handles Me.EditorPaintBackground
        If CodeRush.Source.ActiveClass Is Nothing Then
            Exit Sub
        End If
        If Not mEnabled Then
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

    Private Sub PlugIn1_OptionsChanged(ByVal ea As OptionsChangedEventArgs) Handles Me.OptionsChanged
        If ea.OptionsPages.Contains(GetType(Options1)) Then
            LoadSettings()
        End If
    End Sub
#End Region
#Region "Visualize Button"
    Private Sub CreateVisualizeButton()
        If (CodeRush.Menus Is Nothing OrElse CodeRush.Menus.VisualizeToolBar Is Nothing) Then
            Return
        End If
        Dim Image = GetBitmapByName("CR_MetricShader.ico")
        mVisualizeButton = CodeRush.Menus.VisualizeToolBar.AddButton(Options1.GetPageName(), Image)
        If (mVisualizeButton Is Nothing) Then
            Return
        End If
        mVisualizeButton.Style = ButtonStyle.Icon
        mVisualizeButton.SetFace(Image, Image)
        mVisualizeButton.IsDown = mEnabled
        AddHandler mVisualizeButton.Click, AddressOf VisualizeButton_Click
    End Sub

    Private Sub VisualizeButton_Click(ByVal sender As Object, ByVal e As MenuButtonClickEventArgs)
        ' Toggle Feature
        mEnabled = Not mEnabled
        Using Storage = Options1.Storage
            Storage.WriteBoolean(Options1.STR_MetricShader, Options1.STR_ShaderEnabled, mEnabled)
        End Using
        Call LoadSettings()
    End Sub
#End Region

#Region "Demo Metric"
    Private Sub LineCountDemoMetric_GetMetricValue(ByVal sender As System.Object, ByVal e As GetMetricValueEventArgs) Handles LineCountDemoMetric.GetMetricValue
        e.Value = e.LanguageElement.Range.Height
    End Sub
#End Region

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

End Class
