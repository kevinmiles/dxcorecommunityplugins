Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Namespace CR_WorkBench
	''' <summary>
	''' Summary description for WorkBench.
	''' </summary>
  Public Class WorkBench
	 Inherits StandardPlugIn
#Region "Usual Plugin Stuff"
#Region " private fields... "
        Private components As System.ComponentModel.IContainer
#End Region

        ' constructor...
#Region " WorkBench "
        Public Sub New()
            ''' <summary>
            ''' Required for Windows.Forms Class Composition Designer support
            ''' </summary>
            InitializeComponent()
            LoadSettings()
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
            components = New System.ComponentModel.Container
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        End Sub
#End Region
#End Region
        Private mAutoLoadSave As Boolean
        Private Sub WorkBench_OptionsChanged(ByVal ea As DevExpress.CodeRush.Core.OptionsChangedEventArgs) Handles MyBase.OptionsChanged
            If ea.OptionsPages.Contains(GetType(Options)) Then
                Call LoadSettings()
            End If
        End Sub
        Private Sub LoadSettings()
            mAutoLoadSave = Options.Storage.ReadBoolean("", "AutoLoadSave")
        End Sub

        Private Sub WorkBench_BeforeClosingSolution() Handles MyBase.BeforeClosingSolution
            If Me.mAutoLoadSave Then
                Call CR_WorkBenchWindow.Instance.SaveWorkBench()
            End If
            CR_WorkBenchWindow.Instance.lstFiles.Clear()
        End Sub

        Private Sub WorkBench_SolutionOpened() Handles MyBase.SolutionOpened
            If Me.mAutoLoadSave Then
                Call CR_WorkBenchWindow.Instance.LoadWorkBench()
            End If
        End Sub

    End Class
End Namespace
