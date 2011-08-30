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
        LoadSettings()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

#Region "Options"
    Private Options As NamingOptions
    Private Sub PlugIn1_OptionsChanged(ByVal ea As DevExpress.CodeRush.Core.OptionsChangedEventArgs) Handles Me.OptionsChanged
        If (ea.OptionsPages.Contains(GetType(Options1))) Then
            LoadSettings()
        End If
    End Sub
    Private Sub LoadSettings()
        Options = Options1.LoadNamingOptions
    End Sub
#End Region
    Private TestGenerator As ITestGenerator
    Private Sub GenerateTest_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs) Handles GenerateTest.CheckAvailability
        TestGenerator = New NUnitTestGenerator
        Call TestGenerator.SetOptions(Options.ProjectSuffix, _
                                      Options.FixtureSuffix, _
                                      Options.TestPrefix, _
                                      Options.TestSuffix)
        If TestGenerator.IsNonTestClass(ea.CodeActive) Then
            ea.Available = True
            Return
        End If

        If TestGenerator.IsNonTestMethod(ea.CodeActive) Then
            ea.Available = True
            Return
        End If
    End Sub

    Private Sub GenerateTest_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles GenerateTest.Apply
        Dim SourceProject = CodeRush.Source.ActiveProject
        Dim SourceType = CodeRush.Source.ActiveClass
        Dim ProjectLanguage = SourceProject.Language

        TestGenerator.GenerateTest(SourceProject, SourceType, ProjectLanguage)
    End Sub
End Class