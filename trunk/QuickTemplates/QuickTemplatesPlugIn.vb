Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Namespace QuickTemplates
	''' <summary>
	''' Summary description for QuickTemplatesPlugIn.
	''' </summary>
  Public Class QuickTemplatesPlugIn
	 Inherits StandardPlugIn
#Region "Standard Plugin Stuff ;)"
        Friend WithEvents CreateQuickTemplate As DevExpress.CodeRush.Core.Action
#Region " private fields... "
        Private components As System.ComponentModel.IContainer
#End Region

        ' constructor...
#Region " QuickTemplatesPlugIn "
        ''' <summary>
        ''' Required for Windows.Forms Class Composition Designer support
        ''' </summary>
        Public Sub New()
            InitializeComponent()
        End Sub
#End Region

        ' CodeRush-generated code
#Region " InitializePlugIn "
        Public Overrides Sub InitializePlugIn()
            MyBase.InitializePlugIn()

            '
            ' TODO: Add your initialization code here.
            '
        End Sub
#End Region
#Region " FinalizePlugIn "
        Public Overrides Sub FinalizePlugIn()
            '
            ' TODO: Add your finalization code here.
            '

            MyBase.FinalizePlugIn()
        End Sub
#End Region

#Region " Component Designer generated code "
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(QuickTemplatesPlugIn))
            Me.CreateQuickTemplate = New DevExpress.CodeRush.Core.Action(Me.components)
            CType(Me.CreateQuickTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'CreateQuickTemplate
            '
            Me.CreateQuickTemplate.ActionName = "Create Quick Template"
            Me.CreateQuickTemplate.ButtonText = "Create Quick Template"
            Me.CreateQuickTemplate.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
            Me.CreateQuickTemplate.Description = "Allows the user to create a simple template based on the selected text."
            Me.CreateQuickTemplate.Image = CType(resources.GetObject("CreateQuickTemplate.Image"), System.Drawing.Bitmap)
            Me.CreateQuickTemplate.ImageBackColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(254, Byte), CType(0, Byte))
            CType(Me.CreateQuickTemplate, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
#End Region
#End Region

        Private Sub CreateQuickTemplate_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles CreateQuickTemplate.Execute
            Call TemplateBuilder.CreateQuickTemplate(CodeRush.Selection.Text)
        End Sub
    End Class
End Namespace
