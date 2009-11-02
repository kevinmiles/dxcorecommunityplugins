Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.Core

Public Module ProjectExt
    <Extension()> _
     Public Function GetVSLangProj(ByVal Source As Project) As VSLangProj.VSProject
        Dim project As EnvDTE.Project = CodeRush.Solution.FindEnvDTEProject(Source.Name)
        Return TryCast(project.Object, VSLangProj.VSProject)
    End Function
    '<Extension()> _
    'Public Sub AddToProject(ByVal Source As Reference, ByVal Project As Project)
    '    Project..Add(Source.FullName)
    'End Sub

End Module
