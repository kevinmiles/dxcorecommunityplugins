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
        Call CreateReloadSolution()
		'TODO: Add your initialization code here.
	End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region
    ' Please ensure the following line is not missing from your plugin's InitializeComponent
    ' components = New System.ComponentModel.Container()
    Public Sub CreateReloadSolution()
        Dim ReloadSolution As New DevExpress.CodeRush.Core.Action(components)
        CType(ReloadSolution, System.ComponentModel.ISupportInitialize).BeginInit()
        ReloadSolution.ActionName = "ReloadSolution"
        ReloadSolution.ButtonText = "Reload Solution" ' Used if button is placed on a menu.
        ReloadSolution.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.FileMenu
        ReloadSolution.RegisterInCR = True
        AddHandler ReloadSolution.Execute, AddressOf ReloadSolution_Execute
        CType(ReloadSolution, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub ReloadSolution_Execute(ByVal ea As ExecuteEventArgs)
        ' This method is executed when your action is called.
        ' Remember you must bind your action to a shortcut in order to use it.
        ' Shortcuts are created\altered using the IDE\Shortcuts option page 
        Dim SolutionFilename = CodeRush.Source.Active.Solution.FilePath
        SolutionHelper.CloseSolution(SolutionFilename)
        CodeRush.ApplicationObject.Solution.Open(SolutionFilename)
    End Sub
End Class
