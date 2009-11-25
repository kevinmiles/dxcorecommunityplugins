Option Strict On
Option Explicit On
Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports SP = DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.Refactor.Core
Imports System.IO
Imports DevExpress.CodeRush.Interop.OLE.Helpers
Imports DevExpress.DXCore.TextBuffers

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
#Region "Common"
    Private Shared Function isReferenceToUndeclared(ByVal Element As LanguageElement) As Boolean
        If Element Is Nothing Then
            Return True
        End If
        Dim TRE As TypeReferenceExpression = TryCast(Element, TypeReferenceExpression)
        If TRE IsNot Nothing _
        AndAlso TRE.Parent.ElementType = LanguageElementType.ObjectCreationExpression Then
            Return True
        End If
        Return False
    End Function
    Private Sub SetupDeclareXSmartTagMenu(ByVal ea As CheckContentAvailabilityEventArgs, ByVal MissingType As String)
        ea.Available = isReferenceToUndeclared(ea.Element)
        Call SetupDeclareXSubMenus(ea, MissingType)
    End Sub
    Private Sub SetupDeclareXSubMenus(ByVal ea As CheckContentAvailabilityEventArgs, ByVal MissingType As String)
        ea.MenuCaption = String.Format("Declare {0} in Project...", MissingType)
        mProjectElements = CodeRush.Source.ActiveSolution.AllProjects.Cast(Of ProjectElement).ToList
        For Each Project In mProjectElements.Where(Function(p) Not p.IsMiscProject)
            Call ea.AddSubMenuItem(Project.Name, Project.Name, String.Format("Create this {1} in the '{0}' project", Project.Name, MissingType))
        Next
    End Sub
    Private Sub ApplyDeclareXInProject(ByVal ea As ApplyContentEventArgs, ByVal MissingType As String, ByVal FactoryMethod As Func(Of String, LanguageElement))
        Dim ChosenMenu = ea.SelectedSubMenuItem
        If ChosenMenu IsNot Nothing Then
            Dim TheProject = mProjectElements.Where(Function(p) p.Name = ChosenMenu.Name).First
            Dim NewTypeName As String = CType(ea.Element, TypeReferenceExpression).Name
            Dim FileAndPath As String
            Dim Action As ICompoundAction = CodeRush.TextBuffers.NewMultiFileCompoundAction(String.Format("Declare {0} in Project", MissingType))
            Try
                Dim Code As String = FactoryMethod.Invoke(NewTypeName).GenerateCode(TheProject.Language)
                FileAndPath = CreateFileInProject(TheProject, NewTypeName, Code)
            Finally
                Action.Close()
            End Try
            Call FileOperations.JumpToFileWithUndo(FileAndPath)
        End If
    End Sub
#End Region
#Region "Declare Class"
    Private Sub DeclareClassInProject_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs) Handles DeclareClassInProject.CheckAvailability
        SetupDeclareXSmartTagMenu(ea, "Class")
    End Sub
    Private Sub DeclareClassInProject_Apply(ByVal sender As Object, ByVal ea As ApplyContentEventArgs) Handles DeclareClassInProject.Apply
        Call ApplyDeclareXInProject(ea, "Class", Function(name) New [Class](name))
    End Sub
#End Region
#Region "Declare Struct"
    Private Sub DeclareStructInProject_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs) Handles DeclareStructInProject.CheckAvailability
        Call SetupDeclareXSmartTagMenu(ea, "Struct")
    End Sub

    Private Sub DeclareStructInProject_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles DeclareStructInProject.Apply
        Call ApplyDeclareXInProject(ea, "Struct", Function(name) New Struct(name))
    End Sub
#End Region
#Region "Declare Interface"
    Private Sub DeclareInterfaceInProject_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs) Handles DeclareInterfaceInProject.CheckAvailability
        Call SetupDeclareXSmartTagMenu(ea, "Interface")
    End Sub

    Private Sub DeclareInterfaceInProject_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles DeclareInterfaceInProject.Apply
        Call ApplyDeclareXInProject(ea, "Interface", Function(name) New [Interface](name))
    End Sub
#End Region
End Class
