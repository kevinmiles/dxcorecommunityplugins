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
        CreateGenerateGuid()
    End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region
    Public Sub CreateGenerateGuid()
        Dim GenerateGuid As New DevExpress.CodeRush.Extensions.StringProvider(components)
        CType(GenerateGuid, System.ComponentModel.ISupportInitialize).BeginInit()
        GenerateGuid.ProviderName = "GenerateGuid"
        AddHandler GenerateGuid.GetString, AddressOf GenerateGuid_GetString
        CType(GenerateGuid, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub GenerateGuid_GetString(ByVal ea As DevExpress.CodeRush.Core.GetStringEventArgs)
        ea.Value = Guid.NewGuid.ToString
    End Sub

End Class
