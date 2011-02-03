Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports System.IO
Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.Diagnostics.Core

Public Module ProjectOperations
    <Extension()> _
    Public Function CreateFileInProject(ByVal TheProject As ProjectElement, ByVal BaseFileName As String, ByVal code As String, Optional ByVal RelativePath As String = "") As String
        ' Setup
        Dim ThePath As String = Path.Combine(New FileInfo(TheProject.FilePath).DirectoryName, RelativePath)
        Dim FileAndPath As String = String.Format("{0}\{1}{2}", ThePath, BaseFileName, CodeRush.Language.SupportedFileExtensions)

        Call FileOperations.CreateFileWithUndo(FileAndPath, code)
        Call ProjectOperations.AddFileToProjectWithUndo(TheProject, FileAndPath)
        Return FileAndPath
    End Function

    <Extension()> _
        Public Sub AddFileToProjectWithUndo(ByVal TheProject As ProjectElement, ByVal FileAndPath As String)
        CodeRush.Solution.AddFileToProject(TheProject.Name, FileAndPath)
        CodeRush.UndoStack.Add(New AddedProjectFileUndoUnit(TheProject.Name, FileAndPath))
    End Sub
End Module
