Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports EnvDTE80
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Reflection
Public Class KnackeredGenerator
    Implements ITestGenerator
#Region "Constants"
    Private Const ProviderTestAttributeName As String = "Test"
    Private Const ProviderTestAssembly As String = "NUnit.Framework.dll"
    Private Const ProviderTestFixtureAttribute As String = "TestFixture"
    Private Const ProviderTestingNamespaceReference As String = "NUnit.Framework"
#End Region
#Region "Fields"
    Private ReadOnly ProjectCRUD As New ProjectCRUD()
#End Region
#Region "Utility"
#End Region
    Public Sub GenerateTest(ByVal SourceProject As ProjectElement, ByVal SourceType As String, ByVal TestLanguage As String) Implements ITestGenerator.GenerateTest
        ' Ensure Test ProjectCRUD
        Dim TestProjectName As String = SourceProject.Name.Add_Tests
        Dim TestProject = ProjectCRUD.EnsureProjectExists(SourceProject.GetSolutionFolderName(), TestProjectName, TestLanguage)
        Call TestProject.AddReference(ProviderTestAssembly)
        Call TestProject.AddReference(SourceProject)
        CodeRush.Markers.Drop()
        ' Ensure Class SourceClass_Tests
        Dim Type = EnsureTestFixture(TestProject, SourceType.Add_Tests)
        Dim FixtureFilename As String = Type.FileNode.Name
        Dim Method = EnsureTestMethod(Type)
        CodeRush.File.Activate(FixtureFilename)
        ' Add blank line inside of method 
        ' Position caret inside Method
    End Sub

    Private Function EnsureTestFixture(ByVal TestProject As ProjectElement, ByVal FixtureName As String) As TypeDeclaration
        Dim FullFilename = EnsureFileExists(TestProject, TestProject.GetPathAndFilename(FixtureName))
        Dim Type = TestProject.GetTypeWithName(FixtureName)
        If Type IsNot Nothing Then
            Return Type
        End If
        Type = CreateClass(FixtureName)
        Dim Doc = CType(CodeRush.File.Activate(FullFilename), TextDocument)
        Doc.FileNode.AddNode(Type)
        Call Type.AddAttribute(ProviderTestFixtureAttribute)
        Doc.InsertText(Doc.FileNode.Range.End.OffsetPoint(1, 1), Type.GenerateCode())
        CodeRush.Source.ParseIfNeeded()
        Return TestProject.GetTypeWithName(FixtureName)
    End Function
    Private Function EnsureFileExists(ByVal TestProject As ProjectElement, ByVal PathAndFilename As String) As String
        If IO.File.Exists(PathAndFilename) Then
            Return PathAndFilename
        End If
        Return TestProject.CreateEmptyFile(PathAndFilename)
    End Function
    Private Function CreateClass(ByVal ClassName As String) As TypeDeclaration
        Dim TrueClassname As String = ExtractClassname(ClassName)
        Return New [Class](TrueClassname) With {.Visibility = MemberVisibility.Public}
    End Function


    Private Function EnsureTestMethod(ByVal TestFixture As TypeDeclaration) As Method
        Dim NewTestName = TestFixture.FirstMethodNameNotInUse("Test")
        Dim Method = New Method("", NewTestName.WrapInSelection.WrapInField) With {.MethodType = MethodTypeEnum.Void, .Visibility = MemberVisibility.Public}
        Method.AddAttribute(ProviderTestAttributeName)
        'Method.AddNode(ExpansionBuilder.GetTextCommand("FinalTarget"))
        TestFixture.AddNode(Method)

        Dim Document = CodeRush.Documents.GetTextDocument(TestFixture.Document.FullName)
        Dim InsertionPoint = TestFixture.BlockCodeRange.Start
        Dim Code As String = Method.GenerateCode() '& ExpansionBuilder.GetTextCommand("AddNamespace", ProviderTestingNamespaceReference)
        Dim OperationName As String = "Generate Test"

        'Call Document.ExpandText(InsertionPoint, Code)
        Call ExpandUsingExpansionBuilder(Document, InsertionPoint, Code, OperationName)

        CodeRush.Source.ParseIfNeeded()
        Dim TestProject As ProjectElement = CType(TestFixture.Project, ProjectElement)
        TestFixture = TestProject.GetTypeWithName(TestFixture.Name)
        Return TestFixture.FirstMethodWhere(Function(m) m.Name = NewTestName)
    End Function
    Private Shared Sub InsertUsingQueue(ByVal Document As TextDocument, ByVal InsertionPoint As SourcePoint, ByVal Code As String, ByVal OperationName As String)
        Document.QueueInsert(InsertionPoint, Code)
        Document.ApplyQueuedEdits(OperationName)
    End Sub
    Private Shared Function ExpandUsingExpansionBuilder(ByVal Document As TextDocument, ByVal InsertionPoint As SourcePoint, ByVal Code As String, ByVal OperationName As String) As SourceRange
        Dim Exp As New ExpansionBuilder()
        Exp.AddText(Code)
        'Exp.AddText("Hi There")
        Return Exp.Expand(Document, InsertionPoint, OperationName)
    End Function

#Region "Other Funcs"
    Private Sub CreateFile(ByVal Project As ProjectElement, ByVal FileName As String)
        ' Create a Type in a Project using a Template
        If Project.GetTypeWithName(FileName) Is Nothing Then
            ' Create New File 
            Dim TestClassFilename = Project.CreateNewBasefile(FileName, "")
            Call CodeRush.File.Activate(TestClassFilename)
        End If
    End Sub
    Private Function ExpandTypeViaTemplate(ByVal Project As ProjectElement, ByVal TypeName As String, ByVal TemplateName As String) As TypeDeclaration
        ' Create a Type in a Project using a Template
        If Project.GetTypeWithName(TypeName) Is Nothing Then
            ' Create New File 
            Dim TestClassFilename = Project.CreateNewBasefile(TypeName, "")
            Call CodeRush.File.Activate(TestClassFilename)
            CodeRush.Documents.ActiveTextView.ExpandTemplate(TemplateName)
            CodeRush.Language.ParseActiveDocument()
        End If
        Return Project.GetTypeWithName(TypeName)
    End Function
    Private Function CreateTypeViaTemplate(ByVal Project As ProjectElement, ByVal TypeName As String, ByVal TemplateName As String) As TypeDeclaration
        ' Create a Type in a Project using a Template
        If Project.GetTypeWithName(TypeName) Is Nothing Then
            ' Create New File 
            Dim TestClassFilename = Project.CreateNewBasefile(TypeName, "")
            Call CodeRush.File.Activate(TestClassFilename)
            CodeRush.Documents.ActiveTextView.ExpandTemplate(TemplateName)
            CodeRush.Language.ParseActiveDocument()
        End If
        Return Project.GetTypeWithName(TypeName)
    End Function
    Private Function CreateMethodViaTemplate(ByVal TestType As TypeDeclaration, ByVal MethodName As String, ByVal TemplateName As String) As Method
        ' Create a Method in a Type using a Template
        If TestType.GetMethodWithName(MethodName) Is Nothing Then
            ' No test Method Found with given name.
            TestType.Document.Activate()
            Dim View As TextView = CodeRush.Documents.ActiveTextView
            View.Caret.MoveTo(TestType.Range.End.Line, 1)
            Dim Range = View.ExpandTemplate(TemplateName)
            View.TextDocument.Format(Range)
        End If
        Return TestType.GetMethodWithName(MethodName)
    End Function
#End Region
End Class
Public Module ViewExt
    <Extension()> _
    Public Function ExpandTemplate(ByVal View As TextView, ByVal TemplateNameAndPath As String) As SourceRange
        Dim TemplateName = TemplateNameAndPath.Split("\"c).Last
        Dim TemplateCategory = TemplateNameAndPath.Substring(0, TemplateNameAndPath.Length - TemplateName.Length - 1)
        Dim Template = CodeRush.Templates.FindTemplate(TemplateName, TemplateCategory, CodeRush.Documents.ActiveLanguage)
        Return View.TextDocument.ExpandText(View.Caret.SourcePoint, Template.FirstItemInContext.Expansion)
    End Function
End Module
Public Module StringFuncs
    Public Function ExtractClassname(ByVal FullName As String) As String
        Return FullName.Substring(FullName.LastIndexOf("."c) + 1)
    End Function
End Module
