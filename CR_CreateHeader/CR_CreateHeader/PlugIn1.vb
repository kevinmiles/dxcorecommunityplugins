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
	End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region

    Private Sub actExpandHeader_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles actExpandHeader.Execute
        ' Get Params
        Dim HeaderType As String = ea.Action.Parameters.Item("HeaderType").ValueAsStr
        Dim FullTemplateNameAndPath As String = ea.Action.Parameters.Item("Template").ValueAsStr
        ' Drop Marker
        CodeRush.Markers.Drop()
        ' Determine Insert Point
        Dim InsertPoint = GetJumpLocation(HeaderType)
        ' Expand Template
        Call ExpandTemplateAtSourcepoint(FullTemplateNameAndPath, InsertPoint)
        ' Collect Marker
        CodeRush.Markers.Collect()
    End Sub
    Private Function GetJumpLocation(ByVal HeaderType As String) As SourcePoint
        Dim ElementRange As SourceRange
        Select Case HeaderType.ToLower
            Case "file"
                ElementRange = CodeRush.Documents.ActiveTextDocument.Range
            Case "type"
                ElementRange = CodeRush.Source.ActiveClassInterfaceStructOrModule.Range
            Case "member", "method"
                ElementRange = CodeRush.Source.ActiveMember.Range
            Case Else
                ElementRange = CodeRush.Documents.ActiveTextDocument.Range
        End Select
        Return ElementRange.Start
    End Function
    Private Sub ExpandTemplateAtSourcepoint(ByVal FullTemplateNameAndPath As String, ByVal InsertPoint As SourcePoint)
        Dim TemplateName = FullTemplateNameAndPath.Split("\"c).Last
        Dim TemplateCategory = FullTemplateNameAndPath.Substring(0, FullTemplateNameAndPath.Length - TemplateName.Length - 1)

        Dim Template = CodeRush.Templates.FindTemplate(TemplateName, TemplateCategory, _
                                                       CodeRush.Documents.ActiveLanguage)

        Dim FinalText = CodeRush.Strings.Expand(Template.FirstItemInContext.Expansion)

        ' Again this isn't the best as we loose links etc... But it was a prety quick turnaround right ? :P
        CodeRush.Documents.ActiveTextDocument.InsertText(InsertPoint, FinalText)
    End Sub
End Class
