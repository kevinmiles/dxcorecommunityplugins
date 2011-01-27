Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports System.IO

Public Module ProjectElementExt
    <Extension()> _
    Public Sub AddReference(ByVal Project As ProjectElement, ByVal Reference As String)
        Project.ToProject.AddReference(Reference)
    End Sub
    <Extension()> _
    Public Sub AddReference(ByVal Project As ProjectElement, ByVal Project2 As ProjectElement)
        Dim projectdte As EnvDTE.Project = SolutionHelper.FindEnvDTEProject(Project.Name)
        If ((Not projectdte Is Nothing) AndAlso TypeOf projectdte.Object Is VSLangProj.VSProject) Then
            Dim VSProject As VSLangProj.VSProject = DirectCast(projectdte.Object, VSLangProj.VSProject)
            If (Not VSProject Is Nothing) Then
                VSProject.References.AddProject(SolutionHelper.FindEnvDTEProject(Project2.Name))
            End If
        End If

    End Sub
    <Extension()> _
    Public Function ToProject(ByVal Project As ProjectElement) As Project
        Dim Result As Project = Nothing
        Dim ProjectPointer As Integer = 0
        Do Until Result IsNot Nothing OrElse ProjectPointer > CodeRush.Project.Count - 1
            If CodeRush.Project.Item(ProjectPointer).FileName = Project.FilePath Then
                Result = CodeRush.Project.Item(ProjectPointer)
            End If
            ProjectPointer += 1
        Loop
        Return Result
    End Function
    <Extension()> _
    Public Function GetFolderName(ByVal Project As ProjectElement) As String
        Return New FileInfo(Project.FullName).Directory.FullName
    End Function
    <Extension()> _
    Public Function GetSolutionFolderName(ByVal Project As ProjectElement) As String
        Return New FileInfo(Project.FullName).Directory.Parent.FullName
    End Function

    <Extension()> _
    Public Function GetFilePathForClass(ByVal TestProject As ProjectElement, ByVal TestClass As TypeDeclaration) As String
        Return TestProject.GetFolderName & "\" & TestClass.Name & TestProject.PrimaryFileExt()
    End Function
    <Extension()> _
    Public Function FirstTypeWhere(ByVal TestProject As ProjectElement, ByVal Func As Func(Of TypeDeclaration, Boolean)) As TypeDeclaration
        Return TestProject.AllTypes.OfType(Of TypeDeclaration).Where(Func).FirstOrDefault
        'Return TryCast(TestProject.GetClassIterator().FirstOrDefault(Func), SP.Class)
    End Function

    <Extension()> _
    Private Function ProjectFolder(ByVal TheProject As ProjectElement) As String
        Return New FileInfo(TheProject.FilePath).DirectoryName
    End Function
    <Extension()> _
    Public Function CreateNewBasefile(ByVal TheProject As ProjectElement, ByVal BaseFileName As String, ByVal code As String, Optional ByVal RelativePath As String = "") As String
        Dim FileAndPath As String = TheProject.GetPathAndFilename(BaseFileName, RelativePath)

        Return TheProject.CreateFileWithCode(FileAndPath, code)
    End Function
    <Extension()> _
    Public Function GetPathAndFilename(ByVal TheProject As ProjectElement, ByVal BaseFileName As String, Optional ByVal RelativePath As String = "") As String
        Dim ThePath = Path.Combine(TheProject.ProjectFolder(), RelativePath)

        Dim ProjExt As String = TheProject.PrimaryFileExt()
        Dim RealExt As String = If(BaseFileName.EndsWith(ProjExt), "", ProjExt)

        Return String.Format("{0}\{1}{2}", ThePath, BaseFileName, RealExt)
    End Function
    <Extension()> _
    Public Function PrimaryFileExt(ByVal TheProject As ProjectElement) As String
        Return CodeRush.Language.GetSupportedFileExtensions(TheProject.Language).Split(";"c)(0)
    End Function
    <Extension()> _
    Public Function CreateEmptyFile(ByVal TheProject As ProjectElement, ByVal FileAndPath As String) As String
        Return CreateFileWithCode(TheProject, FileAndPath, "")
    End Function
    <Extension()> _
    Private Function CreateFileWithCode(ByVal TheProject As ProjectElement, ByVal FileAndPath As String, ByVal code As String) As String
        Call FileOperations.CreateFileWithUndo(FileAndPath, code)
        Call TheProject.AddFileToProjectWithUndo(FileAndPath)
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
