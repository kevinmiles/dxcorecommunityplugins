Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        Call CreateUnitTestsRunClassTests()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Public Sub CreateUnitTestsRunClassTests()
        Dim UnitTestsRunClassTests As New DevExpress.CodeRush.Core.Action(components)
        CType(UnitTestsRunClassTests, System.ComponentModel.ISupportInitialize).BeginInit()
        UnitTestsRunClassTests.ActionName = "UnitTestsRunClassTests"
        UnitTestsRunClassTests.ButtonText = "UnitTestsRunClassTests" ' Used if button is placed on a menu.
        UnitTestsRunClassTests.RegisterInCR = True
        AddHandler UnitTestsRunClassTests.Execute, AddressOf UnitTestsRunClassTests_Execute
        CType(UnitTestsRunClassTests, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub UnitTestsRunClassTests_Execute(ByVal ea As ExecuteEventArgs)
        Dim CurrentClass = CodeRush.Source.ActiveClass
        Dim ClassNameToFind As String = CurrentClass.Name
        Dim TestClass As TypeDeclaration = Nothing
        If TestClass Is Nothing Then
            ' [Namespace].[Class]_Tests
            TestClass = GetTypeDeclaration(CurrentClass.FullName & "_Tests")
        End If
        If TestClass Is Nothing Then
            ' [Namespace]_Tests.[Class]_Tests
            TestClass = GetTypeDeclaration(AddTestsToFirstNameSpace(CurrentClass.FullName) & "_Tests")
        End If
        If TestClass Is Nothing Then
            TestClass = FindTypeDeclaration(Function(c) c.Name = ClassNameToFind & "_Tests")
        End If
        CodeRush.UnitTests.Execute(TestClass)
    End Sub
    Private Function AddTestsToFirstNameSpace(ByVal FullName As String) As String
        Dim Pos = FullName.IndexOf(".")
        Return FullName.Substring(0, Pos) & "_Tests" & FullName.Substring(Pos)
    End Function
    Private Function GetTypeDeclaration(ByVal FullTypeName As String) As TypeDeclaration
        Dim FoundType = GetTypeInProject(FullTypeName, CodeRush.Source.ActiveProject)
        If FoundType IsNot Nothing Then
            Return FoundType
        End If
        For Each Project As ProjectElement In CodeRush.Source.ActiveSolution.AllProjects
            If Not Project Is CodeRush.Source.ActiveProject Then
                FoundType = GetTypeInProject(FullTypeName, Project)
                If FoundType IsNot Nothing Then
                    Return FoundType
                End If
            End If
        Next
        Return Nothing
    End Function
    Private Shared Function GetTypeInProject(ByVal FullTypeName As String, ByVal Project As ProjectElement) As TypeDeclaration
        Dim FoundType As ITypeElement = CodeRush.Source.FindType(FullTypeName, Project)
        If FoundType Is Nothing Then
            Return Nothing
        End If
        Return TryCast(FoundType.GetDeclaration(True), TypeDeclaration)
    End Function
    Private Shared Function FindTypeDeclaration(Func As Func(Of TypeDeclaration, Boolean)) As TypeDeclaration
        Dim AllProjects = CodeRush.Source.ActiveClass.Project.Solution.Projects
        For Each Project As ProjectElement In AllProjects
            For Each [Class] As TypeDeclaration In Project.AllTypes.OfType(Of TypeDeclaration)()
                If Func.Invoke([Class]) Then
                    Return [Class]
                End If
            Next
        Next
        Return Nothing
    End Function
End Class
