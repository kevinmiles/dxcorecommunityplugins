Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.IO

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

        ' Copy solution file

        Dim Solution As SolutionElement = CodeRush.Source.ActiveSolution
        Dim SolutionName As String = Solution.Name
        Dim SolutionFile As New FileInfo(Solution.FilePath)
        Dim SolutionFolder = SolutionFile.Directory
        Dim DestFolder As DirectoryInfo = Directory.CreateDirectory(SolutionFile.Directory.Parent.FullName & "\" & SolutionName)
        For Each Project In Solution.AllProjects.Cast(Of ProjectElement)()
            Dim ProjectFolder As String = Project.FilePath
            For Each File In Project.AllOpenFiles.Cast(Of SourceFile)()
                Dim SourceFilePath = File.FilePath
                Dim DestinationPath = SourceFilePath.Replace(SolutionFolder.Name, DestFolder.Name)
                Dim SourceFile As FileInfo = New FileInfo(File.FilePath)
                Dim DestinationFile As String = SourceFile.DirectoryName & "\" & SourceFile.Name
                File.Copy(SourceFilePath, DestinationPath)

            Next
        Next
        For Each File In Solution.AllFiles.Cast(Of SourceFile)()
            CopyFile(File, DestinationFolder
        Next
    End Sub
End Class
