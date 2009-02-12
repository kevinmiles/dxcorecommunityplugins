Imports HighlightCurrentLineInEditor.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HighlightCurrentLineOptions
    Inherits DevExpress.CodeRush.Core.OptionsPage

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

    End Sub

    'OptionsPage overrides dispose to clean up the component list.
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
        Me.InnerColor = New HighlightCurrentLineInEditor.Controls.ColorPicker
        Me.OuterColor = New HighlightCurrentLineInEditor.Controls.ColorPicker
        Me.chkEnabled = New System.Windows.Forms.CheckBox
        Me.TextColor = New HighlightCurrentLineInEditor.Controls.ColorPicker
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'InnerColor
        '
        Me.InnerColor.AllowOpacity = True
        Me.InnerColor.ColorBase = System.Drawing.Color.Blue
        Me.InnerColor.LabelWidth = 104
        Me.InnerColor.Location = New System.Drawing.Point(41, 66)
        Me.InnerColor.Name = "InnerColor"
        Me.InnerColor.Opacity = 70
        Me.InnerColor.Size = New System.Drawing.Size(264, 24)
        Me.InnerColor.TabIndex = 0
        Me.InnerColor.Text = "Inner Color"
        '
        'OuterColor
        '
        Me.OuterColor.AllowOpacity = False
        Me.OuterColor.ColorBase = System.Drawing.Color.Blue
        Me.OuterColor.LabelWidth = 104
        Me.OuterColor.Location = New System.Drawing.Point(41, 96)
        Me.OuterColor.Name = "OuterColor"
        Me.OuterColor.Opacity = 255
        Me.OuterColor.Size = New System.Drawing.Size(264, 24)
        Me.OuterColor.TabIndex = 0
        Me.OuterColor.Text = "Outer Color"
        '
        'chkEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Checked = True
        Me.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEnabled.Location = New System.Drawing.Point(41, 28)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(163, 17)
        Me.chkEnabled.TabIndex = 1
        Me.chkEnabled.Text = "Enable Highlight Current Line"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'TextColor
        '
        Me.TextColor.AllowOpacity = False
        Me.TextColor.ColorBase = System.Drawing.Color.Blue
        Me.TextColor.LabelWidth = 104
        Me.TextColor.Location = New System.Drawing.Point(41, 126)
        Me.TextColor.Name = "TextColor"
        Me.TextColor.Opacity = 255
        Me.TextColor.Size = New System.Drawing.Size(264, 24)
        Me.TextColor.TabIndex = 2
        Me.TextColor.Text = "Text Color"
        '
        'HighlightCurrentLineOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TextColor)
        Me.Controls.Add(Me.chkEnabled)
        Me.Controls.Add(Me.OuterColor)
        Me.Controls.Add(Me.InnerColor)
        Me.Name = "HighlightCurrentLineOptions"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public Shared ReadOnly Property Storage() As DevExpress.CodeRush.Core.DecoupledStorage
        Get
            Return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName())
        End Get
    End Property

    Public Overrides ReadOnly Property Category() As String
        Get
            Return HighlightCurrentLineOptions.GetCategory()
        End Get
    End Property

    Public Overrides ReadOnly Property PageName() As String
        Get
            Return HighlightCurrentLineOptions.GetPageName()
        End Get
    End Property

    Public Shared Shadows Sub Show()
        DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath)
    End Sub
    Friend WithEvents InnerColor As Global.HighlightCurrentLineInEditor.Controls.ColorPicker
    Friend WithEvents OuterColor As Global.HighlightCurrentLineInEditor.Controls.ColorPicker
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents TextColor As Global.HighlightCurrentLineInEditor.Controls.ColorPicker

    Public Shared ReadOnly Property FullPath() As String
        Get
            Return GetCategory() + "\" + GetPageName()
        End Get
    End Property

End Class
