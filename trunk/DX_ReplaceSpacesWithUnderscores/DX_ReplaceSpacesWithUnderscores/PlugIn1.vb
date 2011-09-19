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
        CreateReplaceSpacesWithUnderscores()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Public Sub CreateReplaceSpacesWithUnderscores()
        Dim ReplaceSpacesWithUnderscores As New DevExpress.CodeRush.Extensions.StringProvider(components)
        CType(ReplaceSpacesWithUnderscores, System.ComponentModel.ISupportInitialize).BeginInit()
        ReplaceSpacesWithUnderscores.ProviderName = "ReplaceSpacesWithUnderscores"
        ReplaceSpacesWithUnderscores.DisplayName = "Replace Spaces with Underscores"
        AddHandler ReplaceSpacesWithUnderscores.GetString, AddressOf ReplaceSpacesWithUnderscores_GetString
        CType(ReplaceSpacesWithUnderscores, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub ReplaceSpacesWithUnderscores_GetString(ByVal ea As DevExpress.CodeRush.Core.GetStringEventArgs)
        ea.Value = ea.Parameters.All.Replace(" "c, "_"c)
    End Sub

End Class
