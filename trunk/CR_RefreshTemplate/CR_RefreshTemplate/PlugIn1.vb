Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Text.RegularExpressions

Public Class PlugIn1
    Private Const RegExStartsWithHeader As String = "(//|') RefreshTemplate:(.+)"
    Private Const RegExStartsWithFooter As String = "(//|') EndRefreshTemplate:(.+)"
    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        CreateRefreshTemplate()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region
    Public Sub CreateRefreshTemplate()
        Dim RefreshTemplate As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(RefreshTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        RefreshTemplate.ProviderName = "RefreshTemplate" ' Should be Unique
        RefreshTemplate.DisplayName = "Refresh Template"
        AddHandler RefreshTemplate.CheckAvailability, AddressOf RefreshTemplate_CheckAvailability
        AddHandler RefreshTemplate.Apply, AddressOf RefreshTemplate_Execute
        CType(RefreshTemplate, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub RefreshTemplate_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        Dim HeaderLine As Integer = GetClosestHeaderLine()
        Dim FooterLine As Integer = GetClosestFooterLine()
        If HeaderLine = -1 OrElse FooterLine = -1 Then
            ' Either or footer not found in this file.
            Exit Sub
        End If
        If Not HeaderAndFootersMatch(HeaderLine, FooterLine) Then
            ' Header and footer do not match.
            Exit Sub
        End If
        ea.Available = True
    End Sub
    Private Function GetClosestHeaderLine() As Integer
        ' Iterate lines starting on the current one and moving up.
        ' Look for a comment which starts "RefreshUsing:"
        For Line As Integer = CodeRush.Caret.SourcePoint.Line To 1 Step -1
            If LineMatchesPattern(Line, RegExStartsWithHeader) Then
                Return Line
            End If
        Next
        Return -1
    End Function
    Private Function GetClosestFooterLine() As Integer
        ' Iterate lines starting on the current one and moving down.
        ' Look for a comment which starts "EndRefreshUsing:"
        For Line As Integer = CodeRush.Caret.SourcePoint.Line To CodeRush.Documents.ActiveTextDocument.LineCount - 1
            If LineMatchesPattern(Line, RegExStartsWithFooter) Then
                Return Line
            End If
        Next
        Return -1
    End Function
    Private Function HeaderAndFootersMatch(ByVal HeaderLine As Integer, ByVal FooterLine As Integer) As Boolean
        Dim HeaderTemplate = GetMatchFromLine(HeaderLine, RegExStartsWithHeader, 2)
        Dim FooterTemplate = GetMatchFromLine(FooterLine, RegExStartsWithFooter, 2)
        Return HeaderTemplate = FooterTemplate
    End Function
    Private Sub RefreshTemplate_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        Dim HeaderLine As Integer = GetClosestHeaderLine()
        Dim FooterLine As Integer = GetClosestFooterLine()
        Dim Range As SourceRange = New SourceRange(HeaderLine, 1, FooterLine, CodeRush.Documents.ActiveTextDocument.GetLineLength(FooterLine) + 1)
        Dim TemplateName As String = GetMatchFromLine(HeaderLine, RegExStartsWithHeader, 2)
        ' Wipe Text
        CodeRush.Documents.ActiveTextDocument.DeleteText(Range)
        ' Expand Template
        CodeRush.Caret.MoveTo(HeaderLine, 1)
        CodeRush.Templates.ExpandTemplate(TemplateName)
    End Sub
    Private Function GetMatchFromLine(ByVal HeaderLine As Integer, ByVal MatchPattern As String, ByVal GroupIndex As Integer) As String
        Dim LineText = CodeRush.Documents.ActiveTextDocument.GetText(HeaderLine)
        Dim Match As Match = Regex.Match(LineText, MatchPattern)
        Return Match.Groups(GroupIndex).Value
    End Function
    Private Function LineMatchesPattern(ByVal Line As Integer, ByVal Pattern As String) As Boolean
        Return Regex.IsMatch(CodeRush.Documents.ActiveTextDocument.GetText(Line), Pattern)
    End Function

End Class
