Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports EnvDTE80
Imports System.IO

Public Module Fluent
    <Extension()> _
    Public Function ActiveLanguageVS(ByVal Source As DocumentServices) As String
        Return If(Source.ActiveLanguage = "Basic", "VisualBasic", Source.ActiveLanguage)
    End Function
    Public Function GetVSTemplatePath(ByVal Template As String, ByVal Language As String) As String
        Dim Sol2 As Solution2 = TryCast(CodeRush.ApplicationObject.Solution, Solution2)
        Return Sol2.GetProjectItemTemplate(Template, Language)
    End Function
    Private Function GetUniqueFileName(ByVal FileBase As String) As String
        Dim Mantissa As String = Path.GetFileNameWithoutExtension(FileBase)
        Dim Extension As String = Path.GetExtension(FileBase)

        Dim Count As Integer = 0
        Dim FileName As String

        Dim Folder = New FileInfo(TryCast(CodeRush.Source.ActiveFileNode, SourceFile).FilePath).Directory
        Do
            Count += 1
            FileName = String.Format("{0}{1}{2}", Mantissa, Count, Extension)
        Loop Until Folder.GetFiles(FileName).Count = 0
        Return FileName
    End Function

End Module
