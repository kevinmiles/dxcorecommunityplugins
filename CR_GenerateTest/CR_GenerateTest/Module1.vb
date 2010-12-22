Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core

Public Module Module1
#Region "EnsureTestTypeExists"
    Public Function EnsureTestTypeExists(ByVal TypeToTest As TypeDeclaration, ByVal TestProject As ProjectElement) As TypeDeclaration
        Dim Func As Func(Of TypeDeclaration, Boolean) = Function(x) x.Name = TypeToTest.Name
        Dim TestType = TestProject.FirstTypeWhere(Func)
        If TestType Is Nothing Then
            ' Create new Class
            Dim Builder = CodeRush.Language.GetElementBuilder(TypeToTest.Project.Language)
            TestType = Builder.BuildClass(TypeToTest.Name)
            ' Create file with code within
            Dim TestClassFilename As String = GetFilePathForClass(TestProject, TestType)
            Call My.Computer.FileSystem.WriteAllText(TestClassFilename, TestType.GenerateCode(TestProject.Language), False)
            CodeRush.Solution.AddFileToProject(TestProject.Name, TestClassFilename)
            '-----ISSUES--------------------------------------------------
            'CodeRush.File.Activate(TestClassFilename)
            CodeRush.Language.Parse(TestClassFilename)
            '-------------------------------------------------------------
        End If
        Return TestProject.FirstTypeWhere(Func)
    End Function
#End Region
#Region "EnsureTestMethodExists"
    Public Function EnsureTestMethodExists(ByVal Method As Method, ByVal TestType As TypeDeclaration) As Method
        ' Create Method_Test is it doesn't exist
        Dim TestMethodName As String = String.Format("{0}_Test", Method.Name)
        Dim TestMethod As Method = TestType.FirstMethodWhere(Function(M) M.Name = TestMethodName)

        If TestMethod Is Nothing Then
            ' Create TestMethod
            Dim eb As ElementBuilder = New ElementBuilder
            TestMethod = eb.BuildMethod(String.Empty, TestMethodName)
            TestMethod.Attributes.Add(eb.BuildAttribute("Test"))
            TestType.AddNode(TestMethod)
            ' Render method
            Dim InsertionPoint = TestType.BlockCodeRange.Start
            Dim ActiveDoc As TextDocument = GetTypeTextDocument(TestType)
            ActiveDoc.Format(ActiveDoc.ExpandText(InsertionPoint, TestMethod.GenerateCode(TestType.Project.Language)))
        End If
        Return TestMethod
    End Function
    Private Function GetTypeTextDocument(ByVal TestType As TypeDeclaration) As TextDocument
        '-----ISSUES--------------------------------------------------
        'Dim ActiveDoc As TextDocument = TryCast(CodeRush.File.Activate(TestClass.FileNode.FilePath), TextDocument)
        Return TryCast(CodeRush.File.Activate(TestType.GetSourceFile().FilePath), TextDocument)
        '-------------------------------------------------------------
    End Function
#End Region

End Module
