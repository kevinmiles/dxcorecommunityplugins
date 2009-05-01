Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Diagnostics

Public Class MsdnBclHelpPlugin

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
    Private Sub actMsdnBclHelp_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles actMsdnBclHelp.Execute
        Dim Declaration As IElement = CodeRush.Source.Active.GetDeclaration
        If TypeOf Declaration Is ITypeElement Then
            Dim Device = ea.Action.Parameters(0).ValueAsStr
            If ea.Action.Parameters(0).ValueAsStr <> String.Empty Then
                Device = "(" & Device & ")"
            End If
            Process.Start(String.Format("http://msdn.microsoft.com/en-us/library/{0}{1}.aspx", _
                                        CType(Declaration, ITypeElement).FullName, _
                                        Device))
        End If
    End Sub

End Class
