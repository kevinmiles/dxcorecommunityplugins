Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports EnvDTE80
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Reflection

Public Class ProjectCRUD
    Public Function EnsureProjectExists(ByVal SolutionFolder As String, ByVal ProjectName As String, ByVal Language As String) As ProjectElement
        Dim FoundProject As ProjectElement = GetProject(ProjectName)
        If FoundProject Is Nothing Then
            Call DeleteProject(SolutionFolder & "\" & ProjectName, True)
            If Not ExistsProject(CodeRush.Solution, ProjectName) Then
                Call CreateProject(SolutionFolder, Language, ProjectName)
            End If
            FoundProject = GetProject(ProjectName)
        End If
        Return FoundProject
    End Function

    Private Function GetProject(ByVal TestProjectName As String) As ProjectElement
        Dim Solution = CodeRush.Source.ActiveSolution
        Return Solution.ProjectElements.Cast(Of ProjectElement).Where( _
                                            Function(x) x.Name = TestProjectName _
                                            ).FirstOrDefault
    End Function
    Public Function DeleteProject(ByVal ProjectNameAndLocation As String, ByVal RequireConfirmation As Boolean) As Boolean
        If Not Directory.Exists(ProjectNameAndLocation) Then
            ' Nothing to delete
            Return False
        End If
        Dim Proceed = True ' default to true unless confirmation says otherwise.
        If RequireConfirmation Then
            Dim Message = String.Format("A folder '{0}' appears to exist already. Would you like me to delete it?", ProjectNameAndLocation)
            Proceed = MsgBox(Message, MsgBoxStyle.YesNo) = MsgBoxResult.Yes
        End If
        If Proceed Then
            ' Delete existing Project Folder
            Call Directory.Delete(ProjectNameAndLocation, True)
        End If
    End Function
    Private Function CreateProject(ByVal SolutionFolder As String, ByVal ProjectLanguage As String, ByVal ProjectName As String) As EnvDTE.Project

        Dim NewProjectFolder As String = SolutionFolder & "\" & ProjectName
        Dim TemplateName As String = GetTemplatePath("ClassLibrary.zip", PreProcess(ProjectLanguage))
        Return CodeRush.ApplicationObject.Solution.AddFromTemplate(TemplateName, NewProjectFolder, ProjectName, False)
    End Function
    Private Function PreProcess(ByVal Language As String) As String
        Return If(Language = "Basic", "VisualBasic", Language)
    End Function
    Private Function GetTemplatePath(ByVal Template As String, ByVal Language As String) As String
        Return TryCast(CodeRush.ApplicationObject.Solution, Solution2).GetProjectTemplate(Template, Language)
    End Function
End Class