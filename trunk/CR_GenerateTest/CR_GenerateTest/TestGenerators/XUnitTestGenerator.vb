Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser


Public Class XUnitTestGenerator
    Inherits BaseTestGenerator
    Public Sub New()
        MyBase.New("xunit.dll", "xunit", "", "Fact")
    End Sub
End Class
