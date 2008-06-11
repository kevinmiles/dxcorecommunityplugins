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
    Private WithEvents Somefield As TextField
    Private Sub PickFromListStringProvider_GetString(ByVal ea As GetStringEventArgs) Handles PickFromListStringProvider.GetString
        ea.Value = PickFromList(ea.Parameters.All.Split(","c))
    End Sub
    Private Function PickFromList(ByVal parameters As String()) As String
        Using SelectForm As New SelectForm
            SelectForm.LoadItems(parameters)
            Call SelectForm.ShowDialog()
            Return If(SelectForm.DialogResult = DialogResult.OK, SelectForm.SelectedText, String.Empty)
        End Using
    End Function
End Class
