Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser

Public MustInherit Class BaseTestGenerator
    Implements ITestGenerator

#Region "Fields"
    Private ReadOnly ProjectCRUD As New ProjectCRUD
#End Region
#Region "Properties"
    Public MustOverride ReadOnly Property TestAssemblyName As String
    Public MustOverride ReadOnly Property TestNamespace As String
    Public MustOverride ReadOnly Property TestAttributeName As String
    Public MustOverride ReadOnly Property TestFixtureAttributeName As String
#Region "ITestGenerator Properties"
    Protected Overridable ReadOnly Property TestnamePrefix As String Implements ITestGenerator.TestnamePrefix
        Get
            Return String.Empty
        End Get
    End Property

    Protected Overridable ReadOnly Property TestnamePostfix As String Implements ITestGenerator.TestnamePostfix
        Get
            Return String.Empty
        End Get
    End Property


    Public Overridable ReadOnly Property TestProjectSuffix() As String Implements ITestGenerator.TestProjectSuffix
        Get
            Return "_Tests"
        End Get
    End Property
    Public Overridable ReadOnly Property TestFixtureSuffix() As String Implements ITestGenerator.TestFixtureSuffix
        Get
            Return "_Tests"
        End Get
    End Property
#End Region
#End Region

#Region "Utility"
    Public Function IsNonTestClass(ByVal CodeActive As DevExpress.CodeRush.StructuralParser.LanguageElement) As Boolean Implements ITestGenerator.IsNonTestClass
        Dim Dec = TryCast(CodeActive, TypeDeclaration)

        Return Dec IsNot Nothing _
            AndAlso Not Dec.HasAttribute(TestFixtureAttributeName)
    End Function

    Public Function IsNonTestMethod(ByVal CodeActive As DevExpress.CodeRush.StructuralParser.LanguageElement) As Boolean Implements ITestGenerator.IsNonTestMethod
        Dim Dec = TryCast(CodeActive, Method)
        Return Dec IsNot Nothing _
            AndAlso Not Dec.HasAttribute(TestAttributeName)

    End Function
#End Region



    Public Sub GenerateTest(ByVal SourceProject As ProjectElement, ByVal SourceType As [Class], ByVal TestLanguage As String) Implements ITestGenerator.GenerateTest
        ' Create File
        CodeRush.Markers.Drop()

        ' Ensure Test Project
        Dim TestProject = EnsureTestProject(SourceProject, TestLanguage)
        Dim Document = EnsureDocument(TestProject, SourceType.Name & TestFixtureSuffix)
        Dim ClassNamespace As String = If(SourceType.GetNamespace Is Nothing, "", SourceType.GetNamespace.FullName)
        Dim [Namespace] = EnsureNamespace(Document, ClassNamespace)
        Dim Type = EnsureTypeInDocument(TestProject, Document, [Namespace], SourceType.Name & TestFixtureSuffix, TestLanguage)
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
    Private Function EnsureNamespace(ByVal Document As TextDocument, ByVal NamespaceName As String) As [Namespace]
        Document.ExpandText(Document.Range.Start, String.Format("«AddNamespace({0})»", TestNamespace))
        Dim [Namespace] = Document.GetNamespace(NamespaceName)
        If [Namespace] Is Nothing AndAlso Not NamespaceName = String.Empty Then
            ' Create namespace
            [Namespace] = New [Namespace](NamespaceName)
            Document.FileNode.AddNode([Namespace])
            ' Render namespace
            Document.InsertText(Document.Range.End, [Namespace].GenerateCode)
            ' return namespace
            CodeRush.Source.ParseIfNeeded()
            [Namespace] = Document.GetNamespace(NamespaceName)
            'GetNamespaces(NamespaceName).OfType(Of [Namespace]).FirstOrDefault()
            If [Namespace] Is Nothing Then
                Throw New ApplicationException("Failure to render namespace")
            End If
        End If
        Return [Namespace]
    End Function
    Private Function EnsureTypeInDocument(ByVal Project As ProjectElement, ByVal Document As TextDocument, ByVal [Namespace] As [Namespace], ByVal ClassName As String, ByVal TestLanguage As String) As TypeDeclaration
        Dim Type As TypeDeclaration = Project.GetTypeWithName(ClassName)
        If Type Is Nothing Then
            Type = New [Class](ClassName) With {.Visibility = MemberVisibility.Public}
            Type.AddAttribute(TestFixtureAttributeName)
            Dim ClassInsertionPoint As SourcePoint = If([Namespace] Is Nothing, Document.Range.End, [Namespace].BlockCodeRange.End)
            Document.InsertText(ClassInsertionPoint, Type.GenerateCode(TestLanguage))
            CodeRush.Source.ParseIfNeeded()
            Type = Project.GetTypeWithName(ClassName)
        End If
        Return Type
    End Function
    Private Sub CreateTestMethod(ByVal Document As TextDocument, ByVal Type As TypeDeclaration, ByVal TestLanguage As String)
        Dim MethodName As String = Type.FirstMethodNameNotInUse("Test")
        Dim Method = New Method(TestnamePrefix & MethodName.WrapInSelection & TestnamePostfix) With {.MethodType = MethodTypeEnum.Void, .Visibility = MemberVisibility.Public}
        Method.AddAttribute(TestAttributeName)
        Method.AddNode(New PrimitiveExpression(Environment.NewLine))
        Dim InsertionPoint = Type.BlockEnd.Start
        Document.ExpandText(InsertionPoint, Method.GenerateCode(TestLanguage))
        Document.Format(Document.Range)
    End Sub
    'Private Sub CreateTestMethodAndFormat(ByVal Document As TextDocument, ByVal Type As TypeDeclaration, ByVal TestLanguage As String)
    '    Dim MethodName As String = Type.FirstMethodNameNotInUse("Test")
    '    Dim Method = New Method(TestnamePrefix & MethodName.WrapInSelection.WrapInField & TestnamePostfix) With {.MethodType = MethodTypeEnum.Void, .Visibility = MemberVisibility.Public}
    '    Method.AddAttribute(TestAttributeName)
    '    Method.AddNode(New PrimitiveExpression("«FinalTarget»" & Environment.NewLine))
    '    Dim InsertionPoint = Type.BlockEnd.Start
    '    Document.ExpandText(InsertionPoint, Method.GenerateCode(TestLanguage))
    '    Document.Format(Document.Range)
    'End Sub
    Private Sub InjectNameFieldAndFinalTarget(ByVal Document As TextDocument, ByVal Method As Method)
        Dim InsertionPoint = Method.NameRange.Start
        Dim MethodName As String = Method.Name
        Document.Replace(Method.NameRange, "", "")
        Document.ExpandText(InsertionPoint, TestnamePrefix & MethodName.WrapInSelection.WrapInField & TestnamePostfix)
    End Sub

End Class
