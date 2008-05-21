Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Namespace CR_ShowFile
	''' <summary>
	''' Summary description for ShowFilePlugIn.
	''' </summary>
  Public Class ShowFilePlugIn
	 Inherits StandardPlugIn
        Friend WithEvents ShowOpenFile As DevExpress.CodeRush.Core.Action
        Friend WithEvents ShowSolutionFile As DevExpress.CodeRush.Core.Action
#Region " private fields... "
        Private components As System.ComponentModel.IContainer
#End Region

        ' constructor...
#Region " ShowFilePlugIn "
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
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ShowFilePlugIn))
            Me.ShowOpenFile = New DevExpress.CodeRush.Core.Action(Me.components)
            Me.ShowSolutionFile = New DevExpress.CodeRush.Core.Action(Me.components)
            CType(Me.ShowOpenFile, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ShowSolutionFile, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'ShowOpenFile
            '
            Me.ShowOpenFile.ActionName = "ShowFile"
            Me.ShowOpenFile.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
            Me.ShowOpenFile.Description = "Search an already opened file by it's name and activate it"
            Me.ShowOpenFile.Image = CType(resources.GetObject("ShowOpenFile.Image"), System.Drawing.Bitmap)
            Me.ShowOpenFile.ImageBackColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(254, Byte), CType(0, Byte))
            '
            'ShowSolutionFile
            '
            Me.ShowSolutionFile.ActionName = "ShowSolutionFile"
            Me.ShowSolutionFile.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
            Me.ShowSolutionFile.Description = "Find any solution file by it's name and show it in the editor"
            Me.ShowSolutionFile.Image = CType(resources.GetObject("ShowSolutionFile.Image"), System.Drawing.Bitmap)
            Me.ShowSolutionFile.ImageBackColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(254, Byte), CType(0, Byte))
            CType(Me.ShowOpenFile, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ShowSolutionFile, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
#End Region

        Private Sub ShowOpenFile_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles ShowOpenFile.Execute
            Dim colFiles As New CodeFileCollection
            Dim doc As TextDocument = CodeRush.Documents.Active
            Dim frmFiles As FrmShowFile
            For Each doc In CodeRush.Documents.AllDocuments
                colFiles.Add(New CodeFile(doc))
            Next

            frmFiles = New FrmShowFile(colFiles)
            frmFiles.ShowDialog()
        End Sub

        Private Sub ShowSolutionFile_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles ShowSolutionFile.Execute
            Dim colFiles As New CodeFileCollection
            Dim doc As TextDocument
            Dim frmFiles As FrmShowFile
            Dim sol As SolutionElement
            Dim proj As ProjectElement
            If CodeRush.Source.ActiveProject Is Nothing OrElse CodeRush.Source.ActiveProject.Solution Is Nothing Then Return
            sol = CodeRush.Source.ActiveProject.Solution
            For Each proj In sol.ProjectElements
                For Each projFile As SourceFile In proj.AllFiles
                    colFiles.Add(New CodeFile(projFile))
                Next
            Next
            frmFiles = New FrmShowFile(colFiles)
            frmFiles.ShowDialog()

        End Sub
    End Class
End Namespace
