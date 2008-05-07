Imports DevExpress.CodeRush.Core
Imports DevExpress.DXCore.Testing
Imports DevExpress.Refactor.Testing
Imports DevExpress.CodeRush.StructuralParser

Public Class CreateStubForHandlerTestFixture
    Inherits RefactoringTestFixture

#Region "RefactoringName"
    Protected Overrides ReadOnly Property RefactoringName() As String
        Get
            Return "Create Handler Stub"
        End Get
    End Property
#End Region
#Region "FixtureName"
    Protected Overrides ReadOnly Property FixtureName() As String
        Get
            Return "Create Handler Stub (VB.Net)"

        End Get
    End Property
#End Region
#Region "Language"
    Protected Overrides ReadOnly Property Language() As Language
        Get
            Return Language.Basic
        End Get
    End Property
#End Region
    Private Const RESOURCE_PATH As String = "Refactor_CreateStubForHandler_Tests"
    <Test("Availability on incomplete AddHandler Statement", RESOURCE_PATH, "Availability_1.vb")> _
    Public Sub TestAvailability_1(ByVal sender As Object, ByVal ea As TestEventArgs)
        Call AssertAvailable()
    End Sub
    <Test("Test Execute 1", RESOURCE_PATH, "Execute_1.vb")> _
    Public Sub TestExecute_1(ByVal sender As Object, ByVal ea As TestEventArgs)
        Call AssertExecute()

    End Sub
End Class
