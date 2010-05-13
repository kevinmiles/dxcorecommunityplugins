Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Text.RegularExpressions

<Title("Stack Nav")> _
Public Class ToolWindow1
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

#Region "Guts"
    Private Regex = New Regex("\s+at\s(?<Method>.+)\(\)\sin\s(?<File>.+)\:line\s(?<LineNumber>\d+)")
    Public Function GetStackFrames(ByVal StackText As String) As List(Of StackNavEntry)
        Dim Frames As New List(Of StackNavEntry)
        For Each Match As Match In Regex.Matches(StackText)
            Frames.Add(New StackNavEntry(Match.Groups("File").Value, Match.Groups("LineNumber").Value, StackText))
        Next
        Return Frames
    End Function
    Private Sub JumpToFile(ByVal Filename As String, ByVal Line As Integer)
        Dim Document = CodeRush.File.Activate(Filename)
        If Document IsNot Nothing Then
            CodeRush.TextViews.Active.Caret.MoveTo(Line, 1)
        End If
    End Sub
    Private Sub Refresh()
        Dim Frames = GetStackFrames(TextBox1.Text)
        Grid.DataSource = Frames
        Grid.Refresh()
    End Sub
#End Region
#Region "UI Events "
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Call Refresh()
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        Refresh()
    End Sub
    Private Sub Grid_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Grid.CellContentDoubleClick
        Dim Frame = TryCast(Grid.Rows(e.RowIndex).DataBoundItem, StackNavEntry)
        Call JumpToFile(Frame.PathAndFile, Frame.Line)
    End Sub
#End Region



End Class
