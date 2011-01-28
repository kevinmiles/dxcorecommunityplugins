Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports EnvDTE80
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Reflection
Public Class NUnitTestGenerator
    Inherits BaseTestGenerator

    Public Overrides ReadOnly Property TestAssemblyName As String
        Get
            Return "NUnit.Framework.dll"
        End Get
    End Property
    Public Overrides ReadOnly Property TestNamespace As String
        Get
            Return "NUnit.Framework"
        End Get
    End Property
    Public Overrides ReadOnly Property TestAttributeName As String
        Get
            Return "Test"
        End Get
    End Property
    Public Overrides ReadOnly Property TestFixtureAttributeName As String
        Get
            Return "TestFixture"
        End Get
    End Property


End Class