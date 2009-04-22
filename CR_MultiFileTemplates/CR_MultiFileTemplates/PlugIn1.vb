Option Strict On
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


    Private Sub ExpandTemplateToFile_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteTextCommandEventArgs) Handles ExpandTemplateToFile.Execute
            ' Template Name and Category
            Dim FullTemplateNameAndPath = ea.GetParameterValue(0)
            Dim TemplateName = FullTemplateNameAndPath.Split("\"c).Last 
            Dim TemplateCategory = FullTemplateNameAndPath.Substring(0, FullTemplateNameAndPath.Length - TemplateName.Length -1)

            Dim FileName = ea.GetParameterValue(1)
            Dim Language = CodeRush.Documents.ActiveLanguage
            Dim Template = CodeRush.Templates.FindTemplate(TemplateName, TemplateCategory, Language)
            Dim FinalText = Template.FirstItemInContext.Expansion
            Dim ActiveFileName = CodeRush.Source.ActiveFileNode.Name
            Dim ActiveFolder = New System.IO.FileInfo(ActiveFileName).DirectoryName
            Call My.Computer.FileSystem.WriteAllText(ActiveFolder & "\" & FileName, FinalText, False) ' Although append might also work
            CodeRush.Project.Active.AddFile(ActiveFolder & "\" & FileName)
    End Sub
End Class
