Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class HighlightCurrentLineInEditor

    'DXCore-generated code...
#Region " InitializePlugIn "
    Private mBackGroundBrush As Brush = Nothing
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

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
        If mEaElementRange.ToString <> String.Empty Then
            TextHighlighter.PaintUnfocused(CodeRush.TextViews.Active, mEaElementRange, Color.LightBlue, Color.FromArgb(70, Color.LightBlue))
        End If
    End Sub
End Class
