Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core

Namespace CR_WorkBench
	''' <summary>
	''' Summary description for Options.
	''' </summary>
	Public Class Options
		Inherits OptionsPage
        Friend WithEvents chkAutoLoadSave As System.Windows.Forms.CheckBox
#Region "Usual Option Page Stuff"
#Region " private fields... "
        Private components As System.ComponentModel.Container = Nothing
#End Region

        ' constructor...
#Region " Options "
        Public Sub New()
            MyBase.New()
            ''' <summary>
            ''' Required for Windows.Forms Class Composition Designer support
            ''' </summary>
            InitializeComponent()
        End Sub
#End Region

#Region " CodeRush generated code "
        Protected Overrides Sub Initialize()
            MyBase.Initialize()

            '
            ' TODO: Add your initialization code here.
            '
        End Sub

        Public Shared Function GetCategory() As String
            Return "Tool Windows"
        End Function

        Public Shared Function GetPageName() As String
            Return "WorkBench"
        End Function

        Public Shared ReadOnly Property Storage() As DecoupledStorage
            Get
                Return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName())
            End Get
        End Property

        Public Overrides ReadOnly Property Category() As String
            Get
                Return Options.GetCategory()
            End Get
        End Property

        Public Overrides ReadOnly Property PageName() As String
            Get
                Return Options.GetPageName()
            End Get
        End Property

        Public Shared Shadows Sub Show()
            DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath)
        End Sub

        Public Shared ReadOnly Property FullPath() As String
            Get
                Return GetCategory() + "\" + GetPageName()
            End Get
        End Property
#End Region

#Region " Component Designer generated code "
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.chkAutoLoadSave = New System.Windows.Forms.CheckBox
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'chkAutoLoadSave
            '
            Me.chkAutoLoadSave.Location = New System.Drawing.Point(8, 8)
            Me.chkAutoLoadSave.Name = "chkAutoLoadSave"
            Me.chkAutoLoadSave.Size = New System.Drawing.Size(176, 24)
            Me.chkAutoLoadSave.TabIndex = 0
            Me.chkAutoLoadSave.Text = "AutoLoad and AutoSave"
            '
            'Options
            '
            Me.Controls.Add(Me.chkAutoLoadSave)
            Me.Name = "Options"
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        ' protected methods...
#Region " Dispose "
        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
                MyBase.Dispose(disposing)
            End If
        End Sub
#End Region
#End Region

        Private Sub Options_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles MyBase.RestoreDefaults
            Me.chkAutoLoadSave.Checked = False
        End Sub

        Private Sub Options_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles MyBase.PreparePage
            Me.chkAutoLoadSave.Checked = ea.Storage.ReadBoolean("", "AutoLoadSave")
        End Sub

        Private Sub Options_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles MyBase.CommitChanges
            Call ea.Storage.WriteBoolean("", "AutoLoadSave", Me.chkAutoLoadSave.Checked)
        End Sub

    End Class
End Namespace
