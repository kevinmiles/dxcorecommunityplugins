Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Text.RegularExpressions

Public Class FormatAssignmentsPlugin

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

#Region "Properties"
    'Exists purely to make code prettier to read :)
    Private Property SelectedText() As String
        Get
            Return CodeRush.Documents.ActiveTextView.Selection.Text
        End Get
        Set(ByVal value As String)
            CodeRush.Documents.ActiveTextView.Selection.Text = value
        End Set
    End Property
#End Region

    Private Sub FormatAssignments_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles FormatAssignments.Apply
        CodeRush.Documents.ActiveTextView.Selection.ExtendToWholeLines()
        Call JustifyAssignments()
    End Sub

    Private Sub FormatAssignments_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs) Handles FormatAssignments.CheckAvailability
        ea.Available = SelectedText.Contains("=") AndAlso CodeRush.Documents.ActiveTextView.Selection.Height > 1
    End Sub

    Private Sub JustifyAssignments()
        Dim NewCode As String = SelectedText
        NewCode = JustifyText(NewCode, "=", FurthestRight(NewCode, "=") + 1)
        NewCode = JustifyText(NewCode, "//", FurthestRight(NewCode, "//") + 1)
        CodeRush.Documents.ActiveTextView.Selection.Text = NewCode
    End Sub

    Private Function JustifyText(ByVal SourceText As String, ByVal Delimiter As String, ByVal Position As Integer) As String
        Dim Justifier As New Regex(String.Format("^(?<s1>.*){0}(?<s2>.*)$", Delimiter), _
                                   RegexOptions.Multiline)
        Dim Lines As String() = Split(SourceText, Environment.NewLine)
        For i As Integer = 0 To Lines.Length - 1 Step 1
            If Not Lines(i).Contains(Delimiter) Then
                Continue For
            End If
            Dim Match As Match = Justifier.Match(Lines(i))
            Dim s1 As String = Match.Groups("s1").Value
            Dim s2 As String = Match.Groups("s2").Value
            Lines(i) = s1 & Repeat(" ", Position - s1.Length) & Delimiter & s2
        Next
        Return Join(Lines, Environment.NewLine)
    End Function
    Private Function Repeat(ByVal TextToRepeat As String, ByVal Count As Integer) As String
        Repeat = String.Empty
        For Counter As Integer = 1 To Count
            Repeat &= TextToRepeat
        Next
    End Function
    Private Function FurthestRight(ByVal SourceText As String, ByVal SearchFor As String) As Integer
        Dim Lines As String() = Split(SourceText, Environment.NewLine)
        Dim Rightmost As Integer = -1
        For Each Line In Lines
            Dim FoundAt As Integer = Line.IndexOf(SearchFor)
            If FoundAt > Rightmost Then
                Rightmost = FoundAt
            End If
        Next
        Return Rightmost
    End Function

End Class
