Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports HighlightCurrentLineInEditor.Painting
Imports System.IO

Public Class HighlightCurrentLineInEditor

    'DXCore-generated code...
#Region " InitializePlugIn "
    Private mBackGroundBrush As Brush = Nothing
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        Call LoadSettings()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region
    Private mEnabled As Boolean
    Private mOuterColor As PaintColor
    Private mInnerColor As PaintColor
    Public Sub LoadSettings()
        Using lStorage As DecoupledStorage = HighlightCurrentLineOptions.Storage
            mEnabled = lStorage.ReadBoolean(HighlightCurrentLineOptions.SECTION, "Enabled", True)
            Dim BackColor As Color = lStorage.ReadColor(HighlightCurrentLineOptions.SECTION, HighlightCurrentLineOptions.KEY_InnerBaseColor, HighlightCurrentLineOptions.DEFAULT_COLOR_INNER.Base)
            Dim BackOpacity As Integer = lStorage.ReadInt32(HighlightCurrentLineOptions.SECTION, HighlightCurrentLineOptions.KEY_InnerOpacity, HighlightCurrentLineOptions.DEFAULT_COLOR_INNER.Opacity)
            mInnerColor = New PaintColor(BackColor, BackOpacity)
            Dim ForeColor As Color = lStorage.ReadColor(HighlightCurrentLineOptions.SECTION, HighlightCurrentLineOptions.KEY_OuterBaseColor, HighlightCurrentLineOptions.DEFAULT_COLOR_OUTER.Base)
            mOuterColor = New PaintColor(ForeColor, 255)
        End Using
    End Sub

    Private TextHighlighter As New TextHighlighter
    Private mEaElementRange As SourceRange
    Private mLastLine As Integer = -1
    Private Sub HighlightCurrentLineInEditor_LanguageElementActivated(ByVal ea As DevExpress.CodeRush.Core.LanguageElementActivatedEventArgs) Handles Me.LanguageElementActivated
        Dim Line As Integer = CodeRush.Caret.Line
        If mLastLine <> Line Then
            mEaElementRange = New SourceRange(Line, 0, Line, 150)
            Call CodeRush.TextViews.Active.Invalidate()
            mLastLine = Line
        End If
    End Sub

    Private Sub HighlightCurrentLineInEditor_EditorPaint(ByVal ea As DevExpress.CodeRush.Core.EditorPaintEventArgs) Handles Me.EditorPaint
        If mEnabled AndAlso mEaElementRange.ToString <> String.Empty Then
            TextHighlighter.PaintUnfocused(CodeRush.TextViews.Active, mEaElementRange, mOuterColor.TrueColor, mInnerColor.TrueColor)
        End If
    End Sub

    Private Sub HighlightCurrentLineInEditor_OptionsChanged(ByVal ea As DevExpress.CodeRush.Core.OptionsChangedEventArgs) Handles Me.OptionsChanged
        If (ea.OptionsPages.Contains(GetType(HighlightCurrentLineOptions))) Then
            LoadSettings()
        End If
    End Sub
End Class
