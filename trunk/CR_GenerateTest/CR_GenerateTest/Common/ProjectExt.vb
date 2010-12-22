Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports System.IO

Public Module ProjectExt
    <Extension()> _
    Public Function GetFolderName(ByVal Project As ProjectElement) As String
        Return New FileInfo(Project.FullName).Directory.FullName
    End Function
    <Extension()> _
    Public Function GetSolutionFolderName(ByVal Project As ProjectElement) As String
        Return New FileInfo(Project.FullName).Directory.Parent.FullName
    End Function
    <Extension()> _
    Public Function GetFileExt(ByVal TestProject As ProjectElement) As String
        Return CodeRush.Language.GetSupportedFileExtensions(TestProject.Language)
    End Function
    <Extension()> _
    Public Function GetFilePathForClass(ByVal TestProject As ProjectElement, ByVal TestClass As TypeDeclaration) As String
        Return TestProject.GetFolderName & "\" & TestClass.Name & TestProject.GetFileExt()
    End Function
    <Extension()> _
    Public Function FirstTypeWhere(ByVal TestProject As ProjectElement, ByVal Func As Func(Of TypeDeclaration, Boolean)) As TypeDeclaration
        Return TestProject.AllTypes.OfType(Of TypeDeclaration).Where(Func).FirstOrDefault
        'Return TryCast(TestProject.GetClassIterator().FirstOrDefault(Func), SP.Class)
    End Function
    <Extension()> _
    Public Function CreateNewFile(ByVal TheProject As ProjectElement, ByVal BaseFileName As String, ByVal code As String, Optional ByVal RelativePath As String = "") As String
        ' Setup
        Dim ThePath As String = Path.Combine(New FileInfo(TheProject.FilePath).DirectoryName, RelativePath)
        Dim FileAndPath As String = String.Format("{0}\{1}{2}", ThePath, BaseFileName, CodeRush.Language.SupportedFileExtensions)

        Call FileOperations.CreateFileWithUndo(FileAndPath, code)
        Call ProjectExt.AddFileToProjectWithUndo(TheProject, FileAndPath)
        Return FileAndPath
    End Function

    <Extension()> _
        Public Sub AddFileToProjectWithUndo(ByVal TheProject As ProjectElement, ByVal FileAndPath As String)
        CodeRush.Solution.AddFileToProject(TheProject.Name, FileAndPath)
        CodeRush.UndoStack.Add(New AddedProjectFileUndoUnit(TheProject.Name, FileAndPath))
    End Sub
    <Extension()> _
    Public Function GetTypeWithName(ByVal ParentProject As ProjectElement, ByVal TypeName As String) As TypeDeclaration
        Return ParentProject.FirstTypeWhere(Function(x) x.Name = TypeName)
    End Function

End Module
