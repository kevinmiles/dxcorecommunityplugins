Option Infer On
Imports DevExpress.CodeRush.StructuralParser

Public Interface ITestGenerator
    Sub GenerateTest(ByVal SourceProject As ProjectElement, _
                     ByVal SourceType As [Class], _
                     ByVal TestLanguage As String)
    ReadOnly Property TestnamePrefix As String
    ReadOnly Property TestnamePostfix As String
    ReadOnly Property TestProjectSuffix() As String
    ReadOnly Property TestFixtureSuffix() As String
    Function IsNonTestClass(ByVal CodeActive As LanguageElement) As Boolean
    Function IsNonTestMethod(ByVal CodeActive As LanguageElement) As Boolean
End Interface
