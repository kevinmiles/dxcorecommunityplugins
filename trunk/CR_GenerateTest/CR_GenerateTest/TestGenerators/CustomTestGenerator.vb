Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser

Public Class CustomTestGenerator
    Inherits BaseTestGenerator
    Public Sub New(ByVal FrameworkAssemblyName As String, _
                   ByVal FrameworkNamespace As String, _
                   ByVal FrameworkFixtureAttributeName As String, _
                   ByVal FrameworkTestAttributeName As String)
        MyBase.New(FrameworkAssemblyName, FrameworkNamespace, FrameworkFixtureAttributeName, FrameworkTestAttributeName)
    End Sub
End Class
