Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Text.RegularExpressions
Imports System.Runtime.CompilerServices

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
    Public Function GetStackFrames(ByVal StackText As String) As List(Of StackNavEntry)
        Dim Regex = New Regex("\s*at\s(?<Method>.+)\(.*\)\sin\s(?<File>.+)\:line\s(?<LineNumber>\d+)")
        Dim Frames As New List(Of StackNavEntry)
        For Each Match As Match In Regex.Matches(StackText)
            Dim FileValue As String = Match.Groups("File").DefaultTo("")
            Dim FileNumberValue As String = Match.Groups("LineNumber").DefaultTo("")
            Dim MethodValue As String = Match.Groups("Method").DefaultTo("")
            Frames.Add(New StackNavEntry(FileValue, FileNumberValue, MethodValue))
        Next
        Return Frames
    End Function
    Private Sub JumpToFile(ByVal Filename As String, ByVal Line As Integer)
        Dim Document = CodeRush.File.Activate(Filename)
        If Document IsNot Nothing Then
            CodeRush.TextViews.Active.Caret.MoveTo(Line, 1)
        End If
    End Sub
    Private Sub RefresGrid()
        Dim Frames = GetStackFrames(TextBox1.Text)
        Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Grid.DataSource = Frames
        Grid.Refresh()
        If Frames.Count > 0 Then
            Grid.Columns(Grid.Columns.Count - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If
    End Sub
#End Region
#Region "UI Events "
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Call RefresGrid()
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        Call RefresGrid()
    End Sub
    Private Sub Grid_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Grid.CellDoubleClick
        Dim Frame = TryCast(Grid.Rows(e.RowIndex).DataBoundItem, StackNavEntry)
        Call JumpToFile(Frame.PathAndFile, Frame.Line)
    End Sub
#End Region

End Class
Public Module Groupext
    <Extension()> _
    Public Function DefaultTo(ByVal Source As Group, ByVal DefaultValue As String) As String
        Return If(Source Is Nothing, DefaultValue, Source.Value)
    End Function
End Module