Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

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
            Process.Start(String.Format("http://msdn.microsoft.com/en-us/library/{0}.aspx", _
                                        CType(Declaration, ITypeElement).FullName))
        End If
    End Sub

End Class
