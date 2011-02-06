Option Infer On
Imports DevExpress.CodeRush.StructuralParser

Public Interface ITestGenerator
    Sub SetOptions(ByVal ProjectSuffix As String, ByVal FixtureSuffix As String, ByVal TestPrefix As String, ByVal TestPostfix As String)
    Sub GenerateTest(ByVal SourceProject As ProjectElement, _
                         ByVal SourceType As [Class], _
                         ByVal TestLanguage As String)
    Function IsNonTestClass(ByVal CodeActive As LanguageElement) As Boolean
    Function IsNonTestMethod(ByVal CodeActive As LanguageElement) As Boolean
End Interface
