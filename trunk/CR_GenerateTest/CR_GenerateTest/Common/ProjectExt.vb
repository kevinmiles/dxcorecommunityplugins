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
    Public Function FirstMethodWhere(ByVal TestType As TypeDeclaration, ByVal Func As Func(Of Method, Boolean)) As Method
        Return TestType.AllMethods.OfType(Of Method).Where(Func).FirstOrDefault
    End Function
End Module
