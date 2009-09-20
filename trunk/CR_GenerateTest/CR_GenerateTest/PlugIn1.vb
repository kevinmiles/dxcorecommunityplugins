Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports EnvDTE80
Imports System.IO

Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Private Sub GenerateTest_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs) Handles GenerateTest.CheckAvailability
        ea.Available = True
    End Sub

    Private Sub GenerateTest_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles GenerateTest.Apply
        Dim InitiallyActiveClass As [Class] = CodeRush.Source.ActiveClass
        Dim InitiallyActiveProject As ProjectElement = CodeRush.Source.ActiveProject
        Dim InitiallyActiveMethod As Method = CodeRush.Source.ActiveMethod

        Dim TestProject = EnsureTestProjectExists(InitiallyActiveProject)
        Dim TestType = EnsureTestTypeExists(InitiallyActiveClass, TestProject)
        Dim TestMethod = EnsureTestMethodExists(InitiallyActiveMethod, TestType)
    End Sub
#Region "EnsureTestProjectExists"
    Private Function EnsureTestProjectExists(ByVal Project As ProjectElement) As ProjectElement
        Dim TestProjectName As String = String.Format("{0}_Tests", Project.Name)
        Dim Solution As SolutionServices = CodeRush.Solution
        If Not Solution.AllProjects.Any(Function(x) x.Name = TestProjectName) Then
            Call CreateConsoleProject(Project, TestProjectName)
        End If
        Return GetProject(TestProjectName)
    End Function
    Private Function CreateConsoleProject(ByVal Project As ProjectElement, ByVal ProjectName As String) As EnvDTE.Project
        'Creating Project because it was not found in the solution
        ' Project might already exist on disk
        Dim NewProjectFolder As String = Project.GetSolutionFolderName() & "\" & ProjectName
        If Directory.Exists(NewProjectFolder) Then
            If MsgBox("A folder 'X' appears to exist already. Would you like me to delete it?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                ' Delete existing Project Folder
                Call Directory.Delete(NewProjectFolder, True)
            Else
                Return CodeRush.ApplicationObject.Solution.AddFromFile(Project.GetSolutionFolderName() & "\" & ProjectName)
            End If
        End If
        Dim TemplateName As String = GetTemplatePath("ConsoleApplication.Zip", Project.Language)
        Return CodeRush.ApplicationObject.Solution.AddFromTemplate(TemplateName, NewProjectFolder, ProjectName, False)
    End Function
    Private Function GetTemplatePath(ByVal Template As String, ByVal Language As String) As String
        Return TryCast(CodeRush.ApplicationObject.Solution, Solution2).GetProjectTemplate(Template, Language)
    End Function
    Private Function GetProject(ByVal TestProjectName As String) As ProjectElement
        Dim Solution = CodeRush.Source.ActiveSolution
        Return Solution.ProjectElements.Cast(Of ProjectElement).Where( _
                                            Function(x) x.Name = TestProjectName _
                                            ).FirstOrDefault
    End Function
#End Region

#Region "EnsureTestTypeExists"
    Private Function EnsureTestTypeExists(ByVal TypeToTest As TypeDeclaration, ByVal TestProject As ProjectElement) As TypeDeclaration
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
    Private Function EnsureTestMethodExists(ByVal Method As Method, ByVal TestType As TypeDeclaration) As Method
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
            Dim InsertionPoint = StartOfLine(TestType.BlockCodeRange.Start, 0)
            Dim ActiveDoc As TextDocument = GetTypeTextDocument(TestType)
            ActiveDoc.Format(ActiveDoc.ExpandText(InsertionPoint, TestMethod.GenerateCode(TestType.Project.Language) & System.Environment.NewLine))
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

End Class
