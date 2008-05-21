Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core

Namespace Refactor_Resolve
	''' <summary>
	''' Summary description for OptRefactor_Resolve.
	''' </summary>
	Public Class OptRefactor_Resolve
		Inherits OptionsPage
        Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
#Region " private fields... "
        Private components As System.ComponentModel.Container = Nothing
#End Region

#Region "Private types"
        Structure options
            Public enabled As Boolean
            Public enhanced As Boolean
            Public ignoreCase As Boolean
        End Structure
#End Region

        ' constructor...
#Region " OptRefactor_Resolve "
        Public Sub New()
            MyBase.New()
            ''' <summary>
            ''' Required for Windows.Forms Class Composition Designer support
            ''' </summary>
            InitializeComponent()

            ReadSettings()
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
            Return "Resolve"
        End Function

        Public Shared Function GetPageName() As String
            Return "Resolve"
        End Function

        Public Shared ReadOnly Property Storage() As DecoupledStorage
            Get
                Return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName())
            End Get
        End Property

        Public Overrides ReadOnly Property Category() As String
            Get
                Return OptRefactor_Resolve.GetCategory()
            End Get
        End Property

        Public Overrides ReadOnly Property PageName() As String
            Get
                Return OptRefactor_Resolve.GetPageName()
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
        Friend WithEvents chkEnhanced As System.Windows.Forms.CheckBox
        Friend WithEvents chkIgnoreCase As System.Windows.Forms.CheckBox
        Private Sub InitializeComponent()
            Me.chkEnabled = New System.Windows.Forms.CheckBox
            Me.chkEnhanced = New System.Windows.Forms.CheckBox
            Me.chkIgnoreCase = New System.Windows.Forms.CheckBox
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'chkEnabled
            '
            Me.chkEnabled.Location = New System.Drawing.Point(72, 40)
            Me.chkEnabled.Name = "chkEnabled"
            Me.chkEnabled.TabIndex = 0
            Me.chkEnabled.Text = "Enable painting"
            '
            'chkEnhanced
            '
            Me.chkEnhanced.Location = New System.Drawing.Point(72, 72)
            Me.chkEnhanced.Name = "chkEnhanced"
            Me.chkEnhanced.Size = New System.Drawing.Size(168, 24)
            Me.chkEnhanced.TabIndex = 1
            Me.chkEnhanced.Text = "Enhanced search"
            '
            'chkIgnoreCase
            '
            Me.chkIgnoreCase.Location = New System.Drawing.Point(72, 104)
            Me.chkIgnoreCase.Name = "chkIgnoreCase"
            Me.chkIgnoreCase.TabIndex = 2
            Me.chkIgnoreCase.Text = "Ignore case"
            '
            'OptRefactor_Resolve
            '
            Me.Controls.Add(Me.chkIgnoreCase)
            Me.Controls.Add(Me.chkEnhanced)
            Me.Controls.Add(Me.chkEnabled)
            Me.Name = "OptRefactor_Resolve"
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

#Region "Private methods"
        Private Sub ReadSettings()
            Dim opt As String
            'TODO: Add your initialization code here.
            If Storage.ValueExists("Resolve", "Options") Then
                opt = Storage.ReadString("Resolve", "Options")
                If opt.Substring(0, 1) = "1" Then
                    chkEnabled.Checked = True
                Else
                    chkEnabled.Checked = False
                End If
                If opt.Substring(1, 1) = "1" Then
                    chkEnhanced.Checked = True
                Else
                    chkEnhanced.Checked = False
                End If
                If opt.Substring(2, 1) = "1" Then
                    chkIgnoreCase.Checked = True
                Else
                    chkIgnoreCase.Checked = False
                End If
            Else
                chkEnabled.Checked = True
                chkEnhanced.Checked = True
                chkIgnoreCase.Checked = True
            End If
        End Sub
#End Region

#Region "Event handling"
        Private Sub OptRefactor_Resolve_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles MyBase.CommitChanges
            Dim opt As String = ""
            If chkEnabled.Checked Then
                opt += "1"
            Else
                opt += "0"
            End If
            If chkEnhanced.Checked Then
                opt += "1"
            Else
                opt += "0"
            End If
            If chkIgnoreCase.Checked Then
                opt += "1"
            Else
                opt += "0"
            End If

            Storage.WriteString("Resolve", "Options", opt)
            Refactor_ResolvePlugIn.IsEnabled = chkEnabled.Checked
            Refactor_ResolvePlugIn.Enhanced = chkEnhanced.Checked
            Refactor_ResolvePlugIn.IgnoreCase = chkIgnoreCase.Checked
        End Sub
#End Region
    End Class
End Namespace
