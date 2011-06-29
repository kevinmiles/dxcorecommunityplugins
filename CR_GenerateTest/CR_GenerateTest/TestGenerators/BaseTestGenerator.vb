Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Public MustInherit Class BaseTestGenerator
    Implements ITestGenerator
    Private ReadOnly ProjectCRUD As New ProjectCRUD
#Region "Construction"
    Protected Sub New(ByVal FrameworkAssemblyName As String, _
                          ByVal FrameworkNamespace As String, _
                          ByVal FrameworkFixtureAttributeName As String, _
                          ByVal FrameworkTestAttributeName As String)
        mTestFrameworkAssemblyName = FrameworkAssemblyName
        mTestFrameworkNamespace = FrameworkNamespace
        mTestFrameworkFixtureAttributeName = FrameworkFixtureAttributeName
        mTestFrameworkAttributeName = FrameworkTestAttributeName
        Call SetOptions("_Tests", "_Tests", "", "_Test")
    End Sub

#End Region
#Region "Options"
#Region "Fields"
    Protected mTestFrameworkAssemblyName As String
    Protected mTestFrameworkNamespace As String
    Protected mTestFrameworkAttributeName As String
    Protected mTestFrameworkFixtureAttributeName As String

    Protected mTestProjectSuffix As String
    Protected mTestNameSuffix As String
    Protected mTestFixtureSuffix As String
    Protected mTestNamePrefix As String
#End Region

    Public Sub SetOptions(ByVal ProjectSuffix As String, ByVal FixtureSuffix As String, ByVal TestPrefix As String, ByVal TestPostfix As String) Implements ITestGenerator.SetOptions
        mTestProjectSuffix = ProjectSuffix
        mTestFixtureSuffix = FixtureSuffix
        mTestNamePrefix = TestPrefix
        mTestNameSuffix = TestPostfix
    End Sub
#End Region
#Region "ITestGenerator"
    Public Function IsNonTestClass(ByVal CodeActive As DevExpress.CodeRush.StructuralParser.LanguageElement) As Boolean Implements ITestGenerator.IsNonTestClass
        Dim Dec = TryCast(CodeActive, TypeDeclaration)

        Return Dec IsNot Nothing _
            AndAlso Not Dec.HasAttribute(mTestFrameworkFixtureAttributeName)
    End Function

    Public Function IsNonTestMethod(ByVal CodeActive As DevExpress.CodeRush.StructuralParser.LanguageElement) As Boolean Implements ITestGenerator.IsNonTestMethod
        Dim Dec = TryCast(CodeActive, Method)
        Return Dec IsNot Nothing _
            AndAlso Not Dec.HasAttribute(mTestFrameworkAttributeName)

    End Function

    Public Sub GenerateTest(ByVal SourceProject As ProjectElement, ByVal SourceType As [Class], ByVal TestLanguage As String) Implements ITestGenerator.GenerateTest
        ' Create File
        CodeRush.Markers.Drop()

        ' Ensure Test Project
        Dim TestProject = EnsureTestProject(SourceProject, TestLanguage)
        Dim Document = EnsureDocument(TestProject, SourceType.Name & mTestFixtureSuffix)
        Dim ClassNamespace As String = If(SourceType.GetNamespace Is Nothing, "", SourceType.GetNamespace.FullName)
        Dim [Namespace] = EnsureNamespace(Document, ClassNamespace)
        Dim Type = EnsureTypeInDocument(TestProject, Document, [Namespace], SourceType.Name & mTestFixtureSuffix, TestLanguage)
        Dim Method = CreateTestMethod(Document, Type, TestLanguage)
        Call PlaceFieldAndFinalTargetOnMethod(Method)
    End Sub

#End Region

#Region "Project"
    Private Function EnsureTestProject(ByVal SourceProject As SP.ProjectElement, ByVal TestLanguage As String) As ProjectElement
        Dim TestProject = ProjectCRUD.EnsureProjectExists(SourceProject.GetSolutionFolderName(), SourceProject.Name & mTestProjectSuffix, TestLanguage)
        Call TestProject.AddReference(mTestFrameworkAssemblyName)
        Call TestProject.AddReference(SourceProject)
        Return TestProject
    End Function
#End Region
#Region "Document"
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
#End Region
#Region "Namespace"
    Private Function EnsureNamespace(ByVal Document As TextDocument, ByVal NamespaceName As String) As [Namespace]
        Document.ExpandText(Document.Range.Start, String.Format("«AddNamespace({0})»", mTestFrameworkNamespace))
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
#End Region
#Region "Type"
    Private Function EnsureTypeInDocument(ByVal Project As ProjectElement, ByVal Document As TextDocument, ByVal [Namespace] As [Namespace], ByVal ClassName As String, ByVal TestLanguage As String) As TypeDeclaration
        Dim Type As TypeDeclaration = Project.GetTypeWithName(ClassName)
        If Type Is Nothing Then
            Type = New [Class](ClassName) With {.Visibility = MemberVisibility.Public}
            Type.AddAttribute(mTestFrameworkFixtureAttributeName)
            Dim ClassInsertionPoint As SourcePoint = If([Namespace] Is Nothing, Document.Range.End, [Namespace].BlockCodeRange.End)
            Document.InsertText(ClassInsertionPoint, Type.GenerateCode(TestLanguage))
            CodeRush.Source.ParseIfNeeded()
            Type = Project.GetTypeWithName(ClassName)
        End If
        Return Type
    End Function
    Private Shared Function ShowCode(ByVal Element As LanguageElement) As String
        Return CodeRush.CodeMod.GenerateCode(Element)
    End Function
#End Region
    Private Function CreateTestMethod(ByVal Document As TextDocument, ByVal Type As TypeDeclaration, ByVal TestLanguage As String) As Method
        Dim MethodName As String = Type.FirstMethodNameNotInUse("Test", mTestNamePrefix, mTestNameSuffix)
        Dim Method = New Method(mTestNamePrefix & MethodName & mTestNameSuffix) With {.MethodType = MethodTypeEnum.Void, .Visibility = MemberVisibility.Public}
        Method.AddAttribute(mTestFrameworkAttributeName)
        Method.AddNode(New PrimitiveExpression(Environment.NewLine))
        Dim InsertionPoint = Type.BlockEnd.Start
        Document.ExpandText(InsertionPoint, Method.GenerateCode(TestLanguage))
        Document.Format(Document.Range)
        Return Document.ProjectElement.GetTypeWithName(Type.Name).GetMethodWithName(mTestNamePrefix & MethodName & mTestNameSuffix)
    End Function
    'Private Sub CreateTestMethodAndFormat(ByVal Document As TextDocument, ByVal Type As TypeDeclaration, ByVal TestLanguage As String)
    '    Dim MethodName As String = Type.FirstMethodNameNotInUse("Test", mTestNamePrefix, mTestNamePostfix)
    '    Dim Method = New Method(mTestNamePrefix & MethodName.WrapInSelection.WrapInField & mTestNamePostfix) With {.MethodType = MethodTypeEnum.Void, .Visibility = MemberVisibility.Public}
    '    Method.AddAttribute(mTestFrameworkAttributeName)
    '    Method.AddNode(New PrimitiveExpression(Environment.NewLine & "«FinalTarget»"))
    '    Dim InsertionPoint = Type.BlockEnd.Start
    '    Document.ExpandText(InsertionPoint, Method.GenerateCode(TestLanguage))
    '    'Document.Format(Document.Range)
    'End Sub
    'Private Sub InjectNameFieldAndFinalTarget(ByVal Document As TextDocument, ByVal Method As Method)
    '    Dim InsertionPoint = Method.NameRange.Start
    '    Dim MethodName As String = Method.Name
    '    Document.Replace(Method.NameRange, "", "")
    '    Document.ExpandText(InsertionPoint, mTestNamePrefix & MethodName.WrapInSelection.WrapInField & mTestNamePostfix)
    'End Sub
    Private Sub PlaceFieldAndFinalTargetOnMethod(ByVal Method As Method)
        Dim Doc As IDocument = Method.Document
        Doc.ExpandText(Method.BlockCodeRange.End.OffsetPoint(-1, 0), "«FinalTarget»")

        Dim FieldStartRange = Method.NameRange.Start.OffsetPoint(0, mTestNamePrefix.Length)
        Dim FieldEndRange = Method.NameRange.End.OffsetPoint(0, -1 * mTestNameSuffix.Length)
        Dim PartialMethodNameRange = New SourceRange(FieldStartRange, FieldEndRange)
        Dim PartialMethodName = Doc.GetText(PartialMethodNameRange)
        Doc.DeleteText(PartialMethodNameRange)
        Doc.ExpandText(FieldStartRange, PartialMethodName.WrapInField.WrapInSelection)
    End Sub
    Private Sub InjectNameFieldAndFinalTarget(ByVal Document As TextDocument, ByVal Method As Method)
        Dim InsertionPoint = Method.NameRange.Start
        Dim MethodName As String = Method.Name
        Document.Replace(Method.NameRange, "", "")
        Document.ExpandText(InsertionPoint, mTestNamePrefix & MethodName.WrapInSelection.WrapInField & mTestNameSuffix)
    End Sub
End Class
