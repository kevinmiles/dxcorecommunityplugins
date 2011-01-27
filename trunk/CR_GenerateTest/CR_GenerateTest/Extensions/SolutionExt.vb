Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports System.IO
Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.Diagnostics.Core



Public Module SolutionExt
    <Extension()> _
    Public Sub ExcludeFileFromProject(ByVal Solution As SolutionServices, ByVal projectName As String, ByVal filePath As String)
        If (CodeRush.StrUtil.IsNullOrEmpty(projectName)) Then
            Log.SendErrorWithStackTrace("Cannot remove file from the project because projectName is null or empty.")
            Return
        End If
        If (CodeRush.StrUtil.IsNullOrEmpty(filePath)) Then
            Log.SendErrorWithStackTrace("Cannot remove file from the project because filePath is null or empty.")
            Return
        End If
        Dim lProject As EnvDTE.Project = CodeRush.Solution.FindEnvDTEProject(projectName)
        If (lProject Is Nothing) Then
            Log.SendWarningWithStackTrace("No project was found with the name, '{0}'", projectName)
            Return
        End If
        Try
            Dim lProjectItems As EnvDTE.ProjectItems = lProject.ProjectItems
            Call RemoveProjectItemWithName(lProjectItems, filePath)
        Catch ex As Exception
            Log.SendException(String.Format("Exception raised while removing the file, '{0}', to the '{1}' project.", filePath, projectName), ex)
        End Try
    End Sub



    Friend Sub RemoveProjectItemWithName(ByVal projectItems As EnvDTE.ProjectItems, ByVal filePath As String)
        If (projectItems Is Nothing OrElse filePath Is Nothing) Then
            Return
        End If
        For i As Integer = 1 To projectItems.Count
            Dim lProjectItem As EnvDTE.ProjectItem = projectItems.Item(i)
            Dim lFileCount As Short = lProjectItem.FileCount
            Dim lDone As Boolean = False
            For j As Short = 1 To lFileCount
                If (lProjectItem.FileNames(j) = filePath) Then
                    lProjectItem.Remove()
                    lDone = True
                    Exit For
                End If
            Next
            If (lDone) Then
                Exit For
            End If
            RemoveProjectItemWithName(lProjectItem.ProjectItems, filePath)
        Next
    End Sub
    Public Function ExistsProject(ByVal Solution As SolutionServices, ByVal ProjectName As String) As Boolean
        Return Solution.AllProjects.Any(Function(x) x.Name = ProjectName)
    End Function
End Module