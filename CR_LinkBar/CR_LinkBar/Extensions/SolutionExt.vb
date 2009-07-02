Imports DevExpress.CodeRush.Menus
Imports System.Runtime.CompilerServices
Imports System.IO

Public Module SolutionExt
    <Extension()> _
    Public Function SolutionName(ByVal ActiveSolution As EnvDTE.Solution) As String
        Return (New FileInfo(ActiveSolution.FullName)).Name.ToLower.Replace(".sln", "")
    End Function
End Module
