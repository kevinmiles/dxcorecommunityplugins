Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports EnvDTE80
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Reflection

Public Class NamingOptions
    Public Const DEFAULT_ProjectSuffix As String = "_Tests"
    Public Const DEFAULT_FixtureSuffix As String = "_Tests"
    Public Const DEFAULT_TestPrefix As String = ""
    Public Const DEFAULT_TestSuffix As String = "_Test"
    Public ProjectSuffix As String = DEFAULT_ProjectSuffix
    Public FixtureSuffix As String = DEFAULT_FixtureSuffix
    Public TestPrefix As String = DEFAULT_TestPrefix
    Public TestSuffix As String = DEFAULT_TestSuffix
    Public Sub New()
    End Sub
    Public Sub New(ByVal ProjectSuffix As String, _
                   ByVal FixtureSuffix As String, _
                   ByVal TestPrefix As String, _
                   ByVal TestSuffix As String)
        Me.ProjectSuffix = ProjectSuffix
        Me.FixtureSuffix = FixtureSuffix
        Me.TestPrefix = TestPrefix
        Me.TestSuffix = TestSuffix
    End Sub
End Class
