Option Strict On
Imports System
Imports System.Environment
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.DXCore.TextBuffers

Namespace DX_Samples
    ''' <summary>
    ''' Summary description for ExplicitlyOptionStrictOnProject.
    ''' </summary>
    Public Class ExplicitlyOptionStrictOnProject
        Inherits StandardPlugIn
        Protected Const OPTIONSTRICTON As String = "Option Strict On"
        Protected Const OPTIONSTRICTOFF As String = "Option Strict Off"
        Friend WithEvents actExplicityOptionStrictProject As DevExpress.CodeRush.Core.Action
#Region "Standard Plugin stuff :)"
#Region " private fields... "
        Private components As System.ComponentModel.IContainer
#End Region

        ' constructor...
#Region " ExplicitlyOptionStrictOnProject "
        Public Sub New()
            ''' <summary>
            ''' Required for Windows.Forms Class Composition Designer support
            ''' </summary>
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
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ExplicitlyOptionStrictOnProject))
            Me.actExplicityOptionStrictProject = New DevExpress.CodeRush.Core.Action(Me.components)
            CType(Me.actExplicityOptionStrictProject, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'actExplicityOptionStrictProject
            '
            Me.actExplicityOptionStrictProject.ActionName = "ExplicityOptionStrictProject"
            Me.actExplicityOptionStrictProject.ButtonText = "Explicity 'Option Strict' Project"
            Me.actExplicityOptionStrictProject.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
            Me.actExplicityOptionStrictProject.Description = "ExplicityOptionStrictProject"
            Me.actExplicityOptionStrictProject.Image = CType(resources.GetObject("actExplicityOptionStrictProject.Image"), System.Drawing.Bitmap)
            Me.actExplicityOptionStrictProject.ImageBackColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(254, Byte), CType(0, Byte))
            CType(Me.actExplicityOptionStrictProject, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
#End Region
#End Region

        Private Sub actExplicityOptionStrictOnProject_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles actExplicityOptionStrictProject.Execute
            For Each SourceFile As SourceFile In CodeRush.Source.ActiveProject.AllFiles
                If SourceFile.Name.EndsWith(".vb") Then
                    SetOptionStrictOnFile(SourceFile)
                End If
            Next
        End Sub
        Private Sub SetOptionStrictOnFile(ByVal SourceFile As SourceFile)
            Dim File As ITextBuffer = CodeRush.TextBuffers.Item(SourceFile.Name)
            Dim FileText As String
            If File Is Nothing Then ' File was Not Open in IDE
                FileText = GetTextInFile(SourceFile.Name)
            Else ' File was open in IDE
                FileText = File.GetText(File.Range)
            End If
            FileText = FileText.Replace(OPTIONSTRICTON & NewLine, "") 'remove "option strict On"
            FileText = FileText.Replace(OPTIONSTRICTOFF & NewLine, "")
            FileText = OPTIONSTRICTON & NewLine & FileText
            If File Is Nothing Then ' File was Not Open in IDE
                Call SetTextInFile(SourceFile.Name, FileText)
            Else ' File was open in IDE
                File.SetText(File.Range, FileText)
            End If
        End Sub
    End Class
End Namespace
