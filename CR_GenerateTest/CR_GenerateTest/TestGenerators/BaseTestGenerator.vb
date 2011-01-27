Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports EnvDTE80
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Reflection

Public MustInherit Class BaseTestGenerator
    Implements ITestGenerator
    Private ProjectCRUD As New ProjectCRUD

#Region "Properties"

    Public MustOverride ReadOnly Property TestAssemblyName As String
    Public MustOverride ReadOnly Property TestNamespace As String
    Public MustOverride ReadOnly Property TestAttributeName As String
    Public MustOverride ReadOnly Property TestFixtureAttributeName As String

    Protected Overridable ReadOnly Property TestnamePrefix As String
        Get
            Return String.Empty
        End Get
    End Property

    Protected Overridable ReadOnly Property TestnamePostfix As String
        Get
            Return String.Empty
        End Get
    End Property


    Public Overridable ReadOnly Property TestProjectSuffix() As String
        Get
            Return "_Tests"
        End Get
    End Property
    Public Overridable ReadOnly Property TestFixtureSuffix() As String
        Get
            Return "_Tests"
        End Get
    End Property
#End Region

    Public Sub GenerateTest(ByVal SourceProject As ProjectElement, ByVal SourceType As String, ByVal TestLanguage As String) Implements ITestGenerator.GenerateTest
        ' Create File
        CodeRush.Markers.Drop()

        ' Ensure Test Project
        Dim TestProject = EnsureTestProject(SourceProject, TestLanguage)
        Dim ClassName As String = SourceType & TestFixtureSuffix
        Dim Document = EnsureDocument(TestProject, ClassName)
        Dim Type = EnsureType(TestProject, Document, ClassName, TestLanguage)
        Call CreateTestMethod(Document, Type, TestLanguage)
    End Sub
    Private Function EnsureTestProject(ByVal SourceProject As SP.ProjectElement, ByVal TestLanguage As String) As ProjectElement
        Dim TestProject = ProjectCRUD.EnsureProjectExists(SourceProject.GetSolutionFolderName(), SourceProject.Name & TestProjectSuffix, TestLanguage)
        Call TestProject.AddReference(TestAssemblyName)
        Call TestProject.AddReference(SourceProject)
        Return TestProject
    End Function
    Private Function EnsureDocument(ByVal Project As SP.ProjectElement, ByVal ClassName As String) As TextDocument
        Dim BaseFilename As String = ClassName
        Dim FullFilename = Project.GetPathAndFilename(BaseFilename, "")
        If Not System.IO.File.Exists(FullFilename) Then
            Project.CreateNewBasefile(BaseFilename, "")
        End If
        CodeRush.File.Activate(FullFilename)
        Dim Document As TextDocument = CodeRush.Documents.GetTextDocument(FullFilename)
        If Document Is Nothing Then
            Throw New ApplicationException(String.Format("Document '{0}' not found in solution", FullFilename))
        End If
        Return Document
    End Function
    Private Function EnsureType(ByVal Project As ProjectElement, ByVal Document As TextDocument, ByVal ClassName As String, ByVal TestLanguage As String) As TypeDeclaration
        Dim Type As TypeDeclaration = Project.GetTypeWithName(ClassName)
        If Type Is Nothing Then
            Type = New [Class](ClassName) With {.Visibility = MemberVisibility.Public}
            Type.AddAttribute(TestFixtureAttributeName)
            Document.ExpandText(Document.Range.Start, String.Format("«AddNamespace({0})»", TestNamespace))
            Document.InsertText(Document.Range.End, Type.GenerateCode(TestLanguage))
            CodeRush.Source.ParseIfNeeded()
            Type = Project.GetTypeWithName(ClassName)
        End If
        Return Type
    End Function
    Private Sub CreateTestMethod(ByVal Document As TextDocument, ByVal Type As TypeDeclaration, ByVal TestLanguage As String)
        Dim MethodName As String = Type.FirstMethodNameNotInUse("Test")
        Dim Method = New Method(TestnamePrefix & MethodName.WrapInSelection.WrapInField & TestnamePostfix) With {.MethodType = MethodTypeEnum.Void, .Visibility = MemberVisibility.Public}
        Method.AddAttribute(TestAttributeName)
        Method.AddNode(New PrimitiveExpression("«FinalTarget»" & Environment.NewLine))
        Dim InsertionPoint As SourcePoint = Type.BlockEnd.Start
        Document.ExpandText(InsertionPoint, Method.GenerateCode(TestLanguage))
    End Sub
End Class
