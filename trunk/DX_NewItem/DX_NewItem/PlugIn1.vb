Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports EnvDTE80
Imports System.IO

Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        Call CreateNewItem()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region
    Public Sub CreateNewItem()
        Dim NewItem As New DevExpress.CodeRush.Core.TextCommand(components)
        CType(NewItem, System.ComponentModel.ISupportInitialize).BeginInit()
        NewItem.CommandName = "NewItem"
        AddHandler NewItem.Execute, AddressOf NewItem_Execute
        CType(NewItem, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub NewItem_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteTextCommandEventArgs)
        ' Get Template Name
        Dim TemplateName As String
        Dim ItemName As String
        Try
            TemplateName = ea.GetParameterValue(0)
        Catch ex As Exception
            Exit Sub
        End Try
        Try
            ItemName = ea.GetParameterValue(1)
        Catch ex As Exception
            ItemName = TemplateName
        End Try
        Call AddFileFromTemplate(TemplateName & ".zip", ItemName)
    End Sub
    Private Sub AddFileFromTemplate(ByVal TemplateFileName As String, ByVal ItemName As String)
        Dim VSLanguage As String = PreProcess(CodeRush.Documents.ActiveLanguage)
        Dim TemplateFullName As String = GetTemplatePath(TemplateFileName, VSLanguage)
        Dim FileName = GetUniqueFileName(ItemName, CodeRush.Language.ActiveExtension.SupportedFileExtensions)
        CodeRush.ProjectItems.Active.ProjectItems.AddFromTemplate(TemplateFullName, FileName)
    End Sub
    Private Function GetUniqueFileName(ByVal FileBase As String, ByVal FileExtension As String) As String
        Dim Count As Integer = 0
        Dim FileName As String

        Dim Folder = New FileInfo(TryCast(CodeRush.Source.ActiveFileNode, SourceFile).FilePath).Directory
        Do
            Count += 1
            FileName = String.Format("{0}{1}{2}", FileBase, Count, FileExtension)
        Loop Until Folder.GetFiles(FileName).Count = 0
        Return FileName
    End Function
    'Private Sub AddProject(ByVal TemplateFileName As String)
    '    Dim TemplateFullName As String = GetTemplatePath(TemplateFileName, PreProcess(CodeRush.Documents.ActiveLanguage))
    '    Call CodeRush.ApplicationObject.Solution.AddFromTemplate(TemplateFullName, SolutionFolder, ProjectName, False)
    'End Sub
    Private Function PreProcess(ByVal Language As String) As String
        Return If(Language = "Basic", "VisualBasic", Language)
    End Function
    Private Function GetTemplatePath(ByVal Template As String, ByVal Language As String) As String
        Dim Sol2 As Solution2 = TryCast(CodeRush.ApplicationObject.Solution, Solution2)
        Return Sol2.GetProjectItemTemplate(Template, Language)
    End Function

End Class
