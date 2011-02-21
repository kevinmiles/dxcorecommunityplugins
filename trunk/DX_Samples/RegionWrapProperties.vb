Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Namespace DX_Samples
	''' <summary>
	''' Summary description for RegionWrapProperties.
	''' </summary>
  Public Class RegionWrapProperties
	 Inherits StandardPlugIn
        Friend WithEvents actRegionWrapProperties As DevExpress.CodeRush.Core.Action
#Region "Prebuilt Plugin goodness :)"
#Region " private fields... "
        Private components As System.ComponentModel.IContainer
#End Region

        ' constructor...
#Region " RegionWrapProperties "
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
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(RegionWrapProperties))
            Me.actRegionWrapProperties = New DevExpress.CodeRush.Core.Action(Me.components)
            CType(Me.actRegionWrapProperties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'actRegionWrapProperties
            '
            Me.actRegionWrapProperties.ActionName = "RegionWrapProperties"
            Me.actRegionWrapProperties.ButtonText = "Region-Wrap Properties"
            Me.actRegionWrapProperties.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
            Me.actRegionWrapProperties.Description = "Wraps Properties in Regions"
            Me.actRegionWrapProperties.Image = CType(resources.GetObject("actRegionWrapProperties.Image"), System.Drawing.Bitmap)
            Me.actRegionWrapProperties.ImageBackColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(254, Byte), CType(0, Byte))
            CType(Me.actRegionWrapProperties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
#End Region
#End Region

        Private Sub actRegionWrapProperties_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles actRegionWrapProperties.Execute
            CodeRush.Source.IterateNodesInRange(CodeRush.Source.ActiveClass.Range, New NodeIterationEventHandler(AddressOf HandleNode))
            CodeRush.Documents.ActiveTextDocument.ApplyQueuedEdits("Wrap Properties in Regions")
        End Sub
        Private Sub HandleNode(ByVal ea As NodeIterationEventArgs)
            If ea.LanguageElement.ElementType = LanguageElementType.Property Then
                Call WrapElementInRegion(ea.LanguageElement, ea.LanguageElement.Name & " Property")
            End If
        End Sub
        Private Sub WrapElementInRegion(ByVal LanguageElement As LanguageElement, ByVal RegionName As String)
            Dim ActiveDocument As TextDocument = LanguageElement.Document
            Dim RegionHeader As String = CodeRush.Language.GetRegionHeader(RegionName) & Environment.NewLine
            Dim ElementText As String = ActiveDocument.GetText(LanguageElement.GetFullBlockCutRange())
            Dim RegionFooter As String = CodeRush.Language.GetRegionFooter() & Environment.NewLine
            Dim ReplacementCode As String = RegionHeader & ElementText & RegionFooter
            ActiveDocument.QueueReplace(LanguageElement, ReplacementCode)

        End Sub
    End Class
End Namespace
