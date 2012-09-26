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
        CreateCopyBashPathToClipboard()
		'TODO: Add your initialization code here.
	End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region
    Public Sub CreateCopyBashPathToClipboard()
        Dim CopyBashPathToClipboard As New DevExpress.CodeRush.Core.Action(components)
        CType(CopyBashPathToClipboard, System.ComponentModel.ISupportInitialize).BeginInit()
        CopyBashPathToClipboard.ActionName = "CopyBashPathToClipboard"
        CopyBashPathToClipboard.ButtonText = "Copy Bash path to Clipboard" ' Used if button is placed on a menu.
        CopyBashPathToClipboard.RegisterInCR = True
        CopyBashPathToClipboard.ParentMenu = "Easy MDI Document Window"
        AddHandler CopyBashPathToClipboard.Execute, AddressOf CopyBashPathToClipboard_Execute
        CType(CopyBashPathToClipboard, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub CopyBashPathToClipboard_Execute(ByVal ea As ExecuteEventArgs)
        Dim ActiveFilename As String = CodeRush.Documents.Active.FullName
        Dim InvertedName = ActiveFilename.Replace("\", "/")
        Dim BashName = If(InvertedName.Substring(1, 1) = ":", InvertedName.Substring(2), InvertedName)
        System.Windows.Forms.Clipboard.SetText(BashName)
    End Sub

End Class
