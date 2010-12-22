Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports EnvDTE80
Imports System.IO
Imports System.Runtime.CompilerServices

Public Module StringExt
    <Extension()> _
    Public Function Add_Test(ByVal Name As String) As String
        Return String.Format("{0}_Test", Name)
    End Function
    <Extension()> _
    Public Function Add_Tests(ByVal Name As String) As String
        Return String.Format("{0}_Tests", Name)
    End Function
End Module
