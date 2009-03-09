<System.Runtime.InteropServices.Guid("88face34-7f4d-49d2-882f-e4e496e24cbb"), _
Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SortCodeDesignerWindows
    Inherits DevExpress.CodeRush.PlugInCore.ToolWindowPlugIn

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

    End Sub

    'ToolWindowPlugIn overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.MyEvents = New DevExpress.DXCore.PlugInCore.DXCoreEvents(Me.components)
        Me.OrganizerTreeview1 = New CR_SortCodeDesigner.OrganizerTreeview
        CType(Me.MyEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OrganizerTreeview1
        '
        Me.OrganizerTreeview1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OrganizerTreeview1.Location = New System.Drawing.Point(0, 0)
        Me.OrganizerTreeview1.Name = "OrganizerTreeview1"
        Me.OrganizerTreeview1.Size = New System.Drawing.Size(370, 276)
        Me.OrganizerTreeview1.TabIndex = 0
        '
        'SortCodeDesignerWindows
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.OrganizerTreeview1)
        Me.Name = "SortCodeDesignerWindows"
        Me.Size = New System.Drawing.Size(370, 276)
        CType(Me.MyEvents, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MyEvents As DevExpress.DXCore.PlugInCore.DXCoreEvents

#Region " ShowWindow "
    Public Shared Function ShowWindow() As EnvDTE.Window
        Return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Show(GetType(SortCodeDesignerWindows).GUID)
    End Function
#End Region
#Region " HideWindow "
    Public Shared Function HideWindow() As EnvDTE.Window
        Return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Hide(GetType(SortCodeDesignerWindows).GUID)
    End Function
    Friend WithEvents OrganizerTreeview1 As CR_SortCodeDesigner.OrganizerTreeview
#End Region

End Class