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
        CreateLocateInSolExplorer()
    End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region
    Public Sub CreateLocateInSolExplorer()
        Dim LocateInSolExplorer As New DevExpress.CodeRush.Core.Action(components)
        CType(LocateInSolExplorer, System.ComponentModel.ISupportInitialize).BeginInit()
        LocateInSolExplorer.ActionName = "LocateInSolExplorer"
        LocateInSolExplorer.RegisterInCR = True
        AddHandler LocateInSolExplorer.Execute, AddressOf LocateInSolExplorer_Execute
        CType(LocateInSolExplorer, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub LocateInSolExplorer_Execute(ByVal ea As ExecuteEventArgs)
        Dim DTE = CodeRush.ApplicationObject
        DTE.ExecuteCommand("View.TrackActivityinSolutionExplorer")
        DTE.ExecuteCommand("View.TrackActivityinSolutionExplorer")
        DTE.ExecuteCommand("View.SolutionExplorer")
    End Sub
End Class
