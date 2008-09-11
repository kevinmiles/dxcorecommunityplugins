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
    Private Sub Execute_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles Execute.Execute
        If ea.Action.Parameters.GetBool("DropMarkerBeforeExecution") Then
            If CodeRush.Documents.ActiveTextDocument IsNot Nothing Then
                CodeRush.Markers.Drop(CodeRush.Documents.ActiveTextDocument, _
                                      CodeRush.Caret.SourcePoint.Line, _
                                      CodeRush.Caret.SourcePoint.Offset)
            End If
        End If
        CodeRush.Command.Execute(ea.Action.Parameters.GetString("CommandName"))
    End Sub
End Class
