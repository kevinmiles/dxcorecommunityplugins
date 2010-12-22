Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports EnvDTE80
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Reflection

Public Class PlugIn1
    Private Const TEMPLATE_TestsGenerateNUnitClassStub As String = "Tests\Generate\NUnitClassStub"
    Private Const TEMPLATE_TestsGenerateNUnitMethodStub As String = "Tests\Generate\NUnitMethodStub"

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
    Public Sub SetupTemplates()
        EnsureTemplateExists(TEMPLATE_TestsGenerateNUnitClassStub, GetStringResource("DefaultVBNUnitStubClass.vb"))
        EnsureTemplateExists(TEMPLATE_TestsGenerateNUnitMethodStub, GetStringResource("DefaultVBNUnitStubMethod.vb"))
    End Sub
    Public Function GetStringResource(ByVal Filename As String) As String
        Dim Asm As Assembly = Assembly.GetAssembly(Me.GetType)
        Dim stream As IO.Stream = Asm.GetManifestResourceStream(String.Format("CR_GenerateTest.{0}", Filename))
        Using Reader As StreamReader = New IO.StreamReader(stream)
            Return Reader.ReadToEnd()
        End Using
    End Function
    Private Sub GenerateTest_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles GenerateTest.Apply
        Call SetupTemplates()
        Dim TestTypeName As String = CodeRush.Source.ActiveClass.Name.Add_Test
        Dim TestMethodName = CodeRush.Source.ActiveMethod.Name.Add_Test
        Dim TestProjectName As String = CodeRush.Source.ActiveProject.Name.Add_Tests
        Dim ProjectLanguage As String = CodeRush.Source.ActiveProject.Language
        Dim SolutionFolder As String = CodeRush.Source.ActiveProject.GetSolutionFolderName()


        Call SetTemplateVariable("TypeName", TestTypeName)
        Call SetTemplateVariable("MethodName", TestMethodName)


        Dim TestProject = EnsureProjectExists(SolutionFolder, TestProjectName, ProjectLanguage)
        Dim TestType = CreateTypeViaTemplate(TestProject, TestTypeName, TEMPLATE_TestsGenerateNUnitClassStub)
        Dim TestMethod = CreateMethodViaTemplate(TestType, TestMethodName, TEMPLATE_TestsGenerateNUnitMethodStub)
    End Sub

#Region "EnsureTestProjectExists"
    Private Function EnsureProjectExists(ByVal SolutionFolder As String, ByVal ProjectName As String, ByVal Language As String) As ProjectElement
        Dim FoundProject As ProjectElement = GetProject(ProjectName)
        If FoundProject Is Nothing Then
            Call DeleteProject(SolutionFolder & "\" & ProjectName, True)
            If Not ExistsProject(CodeRush.Solution, ProjectName) Then
                Call CreateProject(SolutionFolder, Language, ProjectName)
            End If
            FoundProject = GetProject(ProjectName)
        End If
        Return FoundProject
    End Function
    Public Function DeleteProject(ByVal ProjectNameAndLocation As String, ByVal RequireConfirmation As Boolean) As Boolean
        If Not Directory.Exists(ProjectNameAndLocation) Then
            ' Nothing to delete
            Return False
        End If
        Dim Proceed = True ' default to true unless confirmation says otherwise.
        If RequireConfirmation Then
            Dim Message = String.Format("A folder '{0}' appears to exist already. Would you like me to delete it?", ProjectNameAndLocation)
            Proceed = MsgBox(Message, MsgBoxStyle.YesNo) = MsgBoxResult.Yes
        End If
        If Proceed Then
            ' Delete existing Project Folder
            Call Directory.Delete(ProjectNameAndLocation, True)
        End If
    End Function
    Private Function CreateProject(ByVal SolutionFolder As String, ByVal ProjectLanguage As String, ByVal ProjectName As String) As EnvDTE.Project

        Dim NewProjectFolder As String = SolutionFolder & "\" & ProjectName
        Dim TemplateName As String = GetTemplatePath("ClassLibrary.zip", PreProcess(ProjectLanguage))
        Return CodeRush.ApplicationObject.Solution.AddFromTemplate(TemplateName, NewProjectFolder, ProjectName, False)
    End Function
    Private Function PreProcess(ByVal Language As String) As String
        Return If(Language = "Basic", "VisualBasic", Language)
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

    Private Function CreateTypeViaTemplate(ByVal Project As ProjectElement, ByVal TypeName As String, ByVal TemplateName As String) As TypeDeclaration
        ' Create a Type in a Project using a Template
        If Project.GetTypeWithName(TypeName) Is Nothing Then
            ' Create New File 
            Dim TestClassFilename = Project.CreateNewFile(TypeName, "")
            Dim ActiveDoc = CodeRush.File.Activate(TestClassFilename)
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
