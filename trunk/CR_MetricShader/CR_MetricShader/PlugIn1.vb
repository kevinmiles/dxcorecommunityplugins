
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class PlugIn1

    Private mWhiteBrush As SolidBrush = New SolidBrush(Color.White)
    Private mBlueBrush As SolidBrush = New SolidBrush(Color.LightBlue)
    Private mPinkBrush As SolidBrush = New SolidBrush(Color.Pink)

    'DXCore-generated code...
#Region " InitializePlugIn "
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

    Private Sub PlugIn1_EditorPaintBackground(ByVal ea As EditorPaintEventArgs) Handles Me.EditorPaintBackground
        If CodeRush.Source.ActiveClass Is Nothing Then
            Exit Sub
        End If
        For Each Member In CodeRush.Source.ActiveClass.AllMembers.OfType(Of Member)()
            Dim View = CodeRush.Documents.ActiveTextView
            Dim Rect As Rectangle = View.GetRectangleFromRange(Member.GetFullBlockCutRange)
            Dim TheColor As Color = Color.LightBlue
            View.HighlightCode(Member.GetFullBlockCutRange, TheColor, TheColor, Color.White)
            'ea.Graphics.FillRectangle(GetCorrectBrush(Member), Rect)
        Next
    End Sub
    Private Function GetCorrectBrush(ByVal Member As Member) As Brush
        Select Case Member.Range.Height
            Case 0 To 10
                Return mWhiteBrush
            Case 11 To 20
                Return mBlueBrush
            Case Else
                Return mPinkBrush
        End Select
    End Function
End Class