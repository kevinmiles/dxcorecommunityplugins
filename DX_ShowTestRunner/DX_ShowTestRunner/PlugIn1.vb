Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports CR_UnitTestRunner

Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        CreateShowTestRunner()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        MyBase.FinalizePlugIn()
    End Sub
#End Region
    ' Please ensure the following line is not missing from your plugin's InitializeComponent
    ' components = New System.ComponentModel.Container()
    Public Sub CreateShowTestRunner()
        Dim ShowTestRunner As New DevExpress.CodeRush.Core.Action(components)
        CType(ShowTestRunner, System.ComponentModel.ISupportInitialize).BeginInit()
        ShowTestRunner.ActionName = "ShowTestRunner"
        ShowTestRunner.ButtonText = "Show Test Runner" ' Used if button is placed on a menu.
        ShowTestRunner.RegisterInCR = True
        AddHandler ShowTestRunner.Execute, AddressOf ShowTestRunner_Execute
        CType(ShowTestRunner, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub ShowTestRunner_Execute(ByVal ea As ExecuteEventArgs)
        ' This method is executed when your action is called.
        ' Remember you must bind your action to a shortcut in order to use it.
        ' Shortcuts are created\altered using the IDE\Shortcuts option page 
        Call CodeRush.ToolWindows.Show(GetType(TestRunnerWindow))
    End Sub

End Class
