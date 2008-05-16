Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class DX_PickFromListStringProvider

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
    
    Private Sub PickFromListStringProvider_GetString(ByVal ea As DevExpress.CodeRush.Core.GetStringEventArgs) Handles PickFromListStringProvider.GetString
        ea.Value = PickFromList(ea.Parameters)
    End Sub
    Private Function PickFromList(ByVal parameters As ParameterCollection) As String
        Using SelectForm As New SelectForm
            SelectForm.LoadItems(parameters.All.Split(","c))
            call SelectForm.ShowDialog
            Return If(SelectForm.DialogResult = DialogResult.OK, SelectForm.SelectedText, String.Empty)
        End Using
    End Function

End Class
