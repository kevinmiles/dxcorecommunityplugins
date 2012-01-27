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
        Call CreateInsertNewParentClass()
    End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region
    Public Sub CreateInsertNewParentClass()
        Dim InsertNewParentClass As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(InsertNewParentClass, System.ComponentModel.ISupportInitialize).BeginInit()
        InsertNewParentClass.ProviderName = "InsertNewParentClass" ' Should be Unique
        InsertNewParentClass.DisplayName = "Insert New Parent Class"
        AddHandler InsertNewParentClass.CheckAvailability, AddressOf InsertNewParentClass_CheckAvailability
        AddHandler InsertNewParentClass.Apply, AddressOf InsertNewParentClass_Execute
        CType(InsertNewParentClass, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub InsertNewParentClass_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        If ea.CodeActive Is Nothing Then
            Exit Sub
        End If
        If Not ea.CodeActive.ElementType = LanguageElementType.Class Then
            Exit Sub
        End If
        If TryCast(ea.CodeActive, [Class]).PrimaryAncestorType Is Nothing Then
            Exit Sub
        End If
        ea.Available = True
    End Sub
    Private Sub InsertNewParentClass_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        Dim SourceClass As [Class] = TryCast(ea.CodeActive, [Class])
        Dim SourceClassName As String = SourceClass.Name
        Dim NewAncestorClassName As String = SourceClass.Name & "Ancestor"
        Dim NewAncestorClass As New [Class](NewAncestorClassName)
        If Not SourceClass.PrimaryAncestorType Is Nothing Then
            '   Set Newclass = Abstract
            NewAncestorClass.IsAbstract = True
            '   Inherits from the original ancestor
            NewAncestorClass.PrimaryAncestorType = SourceClass.PrimaryAncestorType.Clone
        End If
        NewAncestorClass.Visibility = SourceClass.Visibility
        Using ea.TextDocument.NewCompoundAction(String.Format("Insert New Parent Class: {0}", NewAncestorClassName))
            Dim StartPoint = SourceClass.PrimaryAncestorType.Range.Start
            ea.TextDocument.Replace(SourceClass.PrimaryAncestorType.Range, "", "Nullify Ancestor Name")
            ea.TextDocument.ExpandText(StartPoint, NewAncestorClassName)
            ea.TextDocument.ExpandText(SourceClass.Range.Start, CodeRush.CodeMod.GenerateCode(NewAncestorClass, False))
            ' Hack - Feels like there should be a better way.
            CodeRush.Documents.ActiveTextDocument.ParseIfTextChanged()
            SourceClass = ea.TextDocument.GetNodeAt(CodeRush.Caret.SourcePoint)
            CodeRush.Caret.MoveTo(SourceClass.PrimaryAncestorType.Range.Start)
            CallRenameRefactoring()
        End Using
    End Sub
    Private Shared Sub CallRenameRefactoring()
        Dim RenameProvider As RefactoringProviderBase = CodeRush.Refactoring.Get("Rename")
        If RenameProvider Is Nothing Then
            Return
        End If
        CodeRush.SmartTags.HidePopupMenu()
        CodeRush.Refactoring.MenuDeactivated()
        CodeRush.SmartTags.UpdateContext()
        If RenameProvider.IsAvailable Then
            Try
                RenameProvider.IsNestedProvider = True
                RenameProvider.Execute()
            Finally
                RenameProvider.IsNestedProvider = False
            End Try
        End If
    End Sub


End Class
