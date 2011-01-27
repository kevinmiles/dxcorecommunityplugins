Option Infer On
Imports DevExpress.CodeRush.StructuralParser

Public Interface ITestGenerator
    Sub GenerateTest(ByVal SourceProject As ProjectElement, ByVal SourceType As String, ByVal TestLanguage As String)
End Interface
