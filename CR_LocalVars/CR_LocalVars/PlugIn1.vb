Option Infer On
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

		'TODO: Add your initialization code here.
	End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region

    Private Sub LocalVarsProvider_GetSmartTagItems(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.GetSmartTagItemsEventArgs) Handles LocalVarsProvider.GetSmartTagItems
        For Each Var As Param In CodeRush.Source.ActiveMethod.Parameters
            ea.Add(New SmartTagLocalVar(Var.Name))
        Next
    End Sub
End Class
Public Class SmartTagLocalVar
    Inherits SmartTagItem
    Private mVarName As String
    Public Sub New(ByVal VarName As String)
        mVarName = VarName
        Caption = VarName
    End Sub

    Private Sub SmartTagLocalVar_Execute(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Execute
        CodeRush.Documents.ActiveTextDocument.InsertText(CodeRush.Caret.SourcePoint, Me.mVarName)
    End Sub
End Class