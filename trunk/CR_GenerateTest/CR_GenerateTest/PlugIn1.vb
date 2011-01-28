Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports EnvDTE80
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Reflection


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

    Private TestGenerator As ITestGenerator
    Private Sub GenerateTest_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs) Handles GenerateTest.CheckAvailability
        TestGenerator = New NUnitTestGenerator
        ea.Available = True
        ' In Class 
        Dim [Class] = TryCast(ea.CodeActive, [Class])
        If [Class] Is Nothing Then
            ea.Available = False
        End If
        ' Class Not suffixed
        If [Class].Name.EndsWith(TestGenerator.TestFixtureSuffix) Then
            ea.Available = False
        End If
    End Sub

    Private Sub GenerateTest_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles GenerateTest.Apply

        Dim SourceProject As ProjectElement = CodeRush.Source.ActiveProject
        Dim SourceTypeName As String = CodeRush.Source.ActiveClass.Name
        Dim ProjectLanguage As String = SourceProject.Language

        'TestGenerator = New NUnitTestGenerator
        TestGenerator.GenerateTest(SourceProject, SourceTypeName, ProjectLanguage)
    End Sub



End Class