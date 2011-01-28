Option Infer On
Imports DevExpress.CodeRush.StructuralParser

Public Interface ITestGenerator
    Sub GenerateTest(ByVal SourceProject As ProjectElement, ByVal SourceType As String, ByVal TestLanguage As String)
    ReadOnly Property TestnamePrefix As String
    ReadOnly Property TestnamePostfix As String
    ReadOnly Property TestProjectSuffix() As String
    ReadOnly Property TestFixtureSuffix() As String
End Interface
