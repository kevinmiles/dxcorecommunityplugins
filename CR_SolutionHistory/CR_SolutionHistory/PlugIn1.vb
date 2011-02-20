Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.IO
Imports System.Runtime.CompilerServices

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



    Private Sub actBackupSolution_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles actBackupSolution.Execute
        ' Backup Solution

        ' Determine some Folders
        Dim Solution As SolutionElement = CodeRush.Source.ActiveSolution
        Dim NewSolutionFolder As DirectoryInfo = CreateAndReturnDestFolder("NewSolutionName")

        ' Copy solution file
        File.Copy(Solution.FilePath, RebaseFile(Solution.FilePath, Solution.Directory, NewSolutionFolder))

        ' Copy Project Files
        Dim Projects As IEnumerable(Of ProjectElement) = Solution.AllProjects.Cast(Of ProjectElement)()
        For Each Project In Projects
            CopyProject(Project, NewSolutionFolder)
        Next

        '' Copy Solution files - TODO
        'For Each File In Solution.AllFiles.Cast(Of SourceFile)()
        '    CopyFile(File, DestinationFolder
        'Next
    End Sub
    Private Shared Function CreateAndReturnDestFolder(ByVal SolutionName As String) As DirectoryInfo
        ' Assumes Folder will be parallel toexisting solution for now.
        Dim Solution = CodeRush.Source.ActiveSolution
        Dim SolutionFileParentFolder As String = Solution.Directory.Parent.FullName
        Return System.IO.Directory.CreateDirectory(SolutionFileParentFolder & "\" & SolutionName)
    End Function
    Private Sub CopyProject(ByVal Project As ProjectElement, ByVal NewSolutionFolder As DirectoryInfo)
        Dim ProjectFiles As IEnumerable(Of SourceFile) = Project.AllFiles.Cast(Of SourceFile)()
        For Each SourceFile In ProjectFiles
            File.Copy(SourceFile.Name, RebaseFile(SourceFile, NewSolutionFolder))
        Next
    End Sub
#Region "RebaseFileForNewSolution"
    Public Function RebaseFile(ByVal SourceFile As SourceFile, ByVal NewSolutionFolder As DirectoryInfo) As String
        Dim SolutionFolder As DirectoryInfo = New DirectoryInfo(SourceFile.Solution.FullName)
        Return RebaseFile(SourceFile.Name, SolutionFolder, NewSolutionFolder)
    End Function
    Private Function RebaseFile(ByVal FileName As String, ByVal OldSolFolder As DirectoryInfo, ByVal NewSolFolder As DirectoryInfo) As String
        Return FileName.Replace(OldSolFolder.Name, NewSolFolder.Name)
    End Function
#End Region
    'Public Sub CopyFileToRelativeLocationUnder(ByVal SourceFile As SourceFile, ByVal NewSolutionFolder As DirectoryInfo)
    '    Dim NewLocation = RebaseFile(SourceFile, NewSolutionFolder)
    '    File.Copy(SourceFile.Name, NewLocation)
    '    'Dim ActiveSolutionFolder As DirectoryInfo = New FileInfo(Project.Solution.FilePath).Directory
    '    'Dim ProjectFolder As String = Project.FilePath
    '    'Dim SourceFilePath = SourceFile.FilePath
    '    'Dim DestinationPath = SourceFilePath.Replace(ActiveSolutionFolder.Name, NewSolutionFolder.Name)
    '    'Dim SourceFileInfo As FileInfo = New FileInfo(SourceFile.FilePath)
    '    'Dim DestinationFile As String = SourceFileInfo.DirectoryName & "\" & SourceFileInfo.Name
    '    'File.Copy(SourceFilePath, DestinationPath)

    'End Sub
End Class
Public Module SolutionExt
    <Extension()> _
    Public Function Directory(ByVal Source As SolutionElement) As DirectoryInfo
        Return New FileInfo(Source.FilePath).Directory
    End Function
End Module