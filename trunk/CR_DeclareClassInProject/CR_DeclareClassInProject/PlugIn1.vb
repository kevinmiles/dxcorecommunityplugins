Option Strict On
Option Explicit On
Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.Refactor.Core
Imports System.IO
Imports DevExpress.CodeRush.Interop.OLE.Helpers

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
    Private mProjectElements As List(Of ProjectElement)
    Private Sub DeclareClassInProject_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs) Handles DeclareClassInProject.CheckAvailability
        If ea.Element Is Nothing Then
            ea.Available = False
            Exit Sub
        End If
        'Dim X As New Fred()
        Dim TRE As TypeReferenceExpression = TryCast(ea.Element, TypeReferenceExpression)
        If TRE IsNot Nothing AndAlso TRE.Parent.ElementType = LanguageElementType.ObjectCreationExpression Then
            ea.Available = True
        End If
        ea.MenuCaption = "Create Class"
        mProjectElements = CodeRush.Source.ActiveSolution.AllProjects.Cast(Of ProjectElement).ToList
        For Each Project In mProjectElements
            Call ea.AddSubMenuItem(Project.Name, Project.Name, String.Format("Create this class in the '{0}' project", Project.Name))
        Next
    End Sub
    Private Sub DeclareClassInProject_Apply(ByVal sender As Object, ByVal ea As ApplyContentEventArgs) Handles DeclareClassInProject.Apply
        Dim ChosenMenu = ea.SelectedSubMenuItem
        If ChosenMenu Is Nothing Then
            Exit Sub
        End If
        Dim TheProject = mProjectElements.Where(Function(p) p.Name = ChosenMenu.Name).First
        Dim NewClassName As String = CType(ea.Element, TypeReferenceExpression).Name
        Dim FileAndPath = CreateFileInProject(TheProject, NewClassName)
        Call FileOperations.JumpToFileWithUndo(FileAndPath)
        Call FileOperations.InsertElementWithUndo(New [Class](NewClassName), TheProject.Language)
    End Sub
End Class
