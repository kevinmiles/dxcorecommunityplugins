Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports Sp = DevExpress.CodeRush.StructuralParser
Imports Microsoft.VisualBasic

Public Class Refactor_CreateStubForHandlerPlugIn
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

#Region "Fields"
    Private _fromAction As Boolean = False
    Private _CaretElement As LanguageElement
#End Region
#Region "CodeProvider Trigger"
    Private Sub CreateHandlerStubCodeProvider_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs) Handles CreateHandlerStubCodeProvider.CheckAvailability
        CodeRush.Source.ParseIfTextChanged()
        Dim CaretElement As LanguageElement = ea.Element
        If Not CaretElement Is Nothing Then
            If TypeOf CaretElement Is ElementReferenceExpression _
                AndAlso DirectCast(CaretElement, ElementReferenceExpression).GetDeclaration Is Nothing _
                AndAlso Not DirectCast(CaretElement, ElementReferenceExpression).Parent Is Nothing _
                AndAlso TypeOf DirectCast(CaretElement, ElementReferenceExpression).Parent Is AddressOfExpression Then
                Me._CaretElement = CaretElement
                ea.Availability = RefactoringAvailability.Available
            End If
        End If
    End Sub

    Private Sub CreateHandlerStubCodeProvider_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles CreateHandlerStubCodeProvider.Apply
        Try
            _fromAction = False
            Picker.Start()
        Catch ex As Exception

        End Try

    End Sub
    Public Function DeclarationIsEnumberable(ByVal VarDeclaration As ITypeElement) As Boolean
        CodeRush.Source.Implements(VarDeclaration.FullName, "Systems.Collections.IEnumerable")
    End Function
#End Region
#Region "Action Trigger"
    Private Sub CreateHandlerStubAction_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles CreateHandlerStubAction.Execute
        CodeRush.Source.ParseIfTextChanged()
        _CaretElement = CodeRush.Source.Active
        If Not _CaretElement Is Nothing _
            AndAlso Not _CaretElement.Parent Is Nothing _
            AndAlso (TypeOf _CaretElement.Parent Is [AddHandler] _
                OrElse TypeOf _CaretElement.Parent Is [RemoveHandler]) Then
            _fromAction = True
            Picker.Start()
        End If
    End Sub
#End Region

    Private Sub Picker_TargetSelected(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.PlugInCore.TargetSelectedEventArgs) Handles Picker.TargetSelected
        PluginLogic.CreateMethodStub(_CaretElement, ea.Location.InsertionPoint, _fromAction)
    End Sub
End Class
