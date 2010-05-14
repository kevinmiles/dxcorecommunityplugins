Imports DevExpress.CodeRush.Core

Partial Class PlugIn1
    Inherits DevExpress.CodeRush.PlugInCore.StandardPlugIn

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()
        'CreateToggleStackNav()
        'This call is required by the Component Designer.
        InitializeComponent()

    End Sub

    'StandardPlugIn overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    'Public Sub CreateToggleStackNav()
    '    Dim ToggleStackNav As New DevExpress.CodeRush.Core.Action(components)
    '    CType(ToggleStackNav, System.ComponentModel.ISupportInitialize).BeginInit()
    '    ToggleStackNav.ActionName = "ToggleStackNav"
    '    ToggleStackNav.ButtonText = "ToggleStackNav" ' Used if button is placed on a menu.
    '    ToggleStackNav.RegisterInCR = True
    '    AddHandler ToggleStackNav.Execute, AddressOf ToggleStackNav_Execute
    '    CType(ToggleStackNav, System.ComponentModel.ISupportInitialize).EndInit()
    'End Sub
    'Private Sub ToggleStackNav_Execute(ByVal ea As ExecuteEventArgs)
    '    CodeRush.ToolWindows.Show()
    'End Sub
End Class
