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
        Dim TemplateCategory = FullTemplateNameAndPath.Substring(0, FullTemplateNameAndPath.Length - TemplateName.Length - 1)

        Dim FileName = ea.GetParameterValue(1)
        Dim Language = CodeRush.Documents.ActiveLanguage
        Dim Template = CodeRush.Templates.FindTemplate(TemplateName, TemplateCategory, Language)
        Dim FinalText = CodeRush.Strings.Expand(Template.FirstItemInContext.Expansion)
        Dim FilePath As String = GetActiveFolder() & "\" & FileName
        Call My.Computer.FileSystem.WriteAllText(FilePath, FinalText, False)
        CodeRush.Project.Active.AddFile(FilePath)
        If CBool(ea.GetParameterValue(2)) Then
            CodeRush.File.Activate(FilePath)
        End If

    End Sub
    Private Function GetActiveFolder() As String
        Dim ActiveFileName = CodeRush.Source.ActiveFileNode.Name
        Dim ActiveFolder = New System.IO.FileInfo(ActiveFileName).DirectoryName
        Return ActiveFolder
    End Function

    Private Sub UnusedFilename_GetString(ByVal ea As DevExpress.CodeRush.Core.GetStringEventArgs) Handles UnusedFilename.GetString
        Dim FileRoot As String = ea.Parameters(0).ValueAsStr
        Dim FileExt As String = ea.Parameters(1).ValueAsStr
        Dim CurrentCount As Integer
        Do
            CurrentCount += 1
            ea.Value = String.Format(FileRoot, CStr(CurrentCount)) 
        Loop While FileExists(ea.Value & "." & FileExt)
    End Sub
    Private Function FileExists(ByVal BaseFilename As String) As Boolean
        Return System.IO.File.Exists(GetActiveFolder() & "\" & BaseFilename)
    End Function

    Private Sub Replace_GetString(ByVal ea As DevExpress.CodeRush.Core.GetStringEventArgs)
        ea.Value = ea.Parameters(0).ValueAsStr.Replace(ea.Parameters(1).ValueAsStr, ea.Parameters(2).ValueAsStr)
    End Sub
End Class
