Imports System
Imports System.Environment
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.DXCore.PlugInCore
Imports System.ComponentModel
Imports EnvDTE
Imports System.Drawing

Namespace CR_WorkBench
	''' <summary>
	''' Summary description for CR_WorkBenchWindow.
	''' </summary>
	<Guid("5fdbfa6b-15b1-43b9-b396-9e519c5fcf28"), _
	 Title("Workbench")> _
	Public Class CR_WorkBenchWindow
		Inherits ToolWindowPlugIn

#Region "Usual CR Precreated Goodness :)"
#Region " private fields... "
        Friend WithEvents events_ As DXCoreEvents
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As IContainer
#End Region

        ' constructor...
#Region " CR_WorkBenchWindow "
        Public Sub New()
            ' This call is required by the Windows.Forms Form Designer.
            InitializeComponent()
        End Sub
#End Region

        ' DXCore-generated code
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

#Region " ShowWindow "
        ''' <summary>
        ''' Opens and displays this tool window. If the tool window is already open,
        ''' it will be focused.
        ''' </summary>
        Public Shared Function ShowWindow() As Window
            Return CodeRush.ToolWindows.Show(GetType(CR_WorkBenchWindow).GUID)
        End Function
#End Region
#Region " HideWindow "
        ''' <summary>
        ''' Hides this tool window if it is open.
        ''' </summary>
        Public Shared Function HideWindow() As Window
            Return CodeRush.ToolWindows.Hide(GetType(CR_WorkBenchWindow).GUID)
        End Function
#End Region

#Region " Instance "
        ''' <summary>
        ''' Returns the created instance of this tool window if it is open. If the tool window
        ''' is not open in Visual Studio, this property returns null.
        ''' </summary>
        Public Shared ReadOnly Property Instance() As CR_WorkBenchWindow
            Get
                Return CType(CodeRush.ToolWindows.GetPlugInControl(GetType(CR_WorkBenchWindow)), CR_WorkBenchWindow)
            End Get
        End Property
#End Region

#Region " Component Designer generated code "
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Friend WithEvents lstFiles As ListView
        Friend WithEvents colFile As ColumnHeader
        Friend WithEvents colFolder As ColumnHeader
        Friend WithEvents ContextMenu1 As ContextMenu
        Friend WithEvents mnuSaveWorkBench As MenuItem
        Friend WithEvents mnuLoadWorkBench As MenuItem
        Friend WithEvents mnuRemoveItems As MenuItem
        Friend WithEvents mnuClearWorkBench As MenuItem
        Private Sub InitializeComponent()
            Me.components = New Container
            Me.events_ = New DXCoreEvents(Me.components)
            Me.lstFiles = New ListView
            Me.colFile = New ColumnHeader
            Me.colFolder = New ColumnHeader
            Me.ContextMenu1 = New ContextMenu
            Me.mnuRemoveItems = New MenuItem
            Me.mnuClearWorkBench = New MenuItem
            Me.mnuSaveWorkBench = New MenuItem
            Me.mnuLoadWorkBench = New MenuItem
            CType(Me.events_, ISupportInitialize).BeginInit()
            CType(Me, ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lstFiles
            '
            Me.lstFiles.AllowDrop = True
            Me.lstFiles.Columns.AddRange(New ColumnHeader() {Me.colFile, Me.colFolder})
            Me.lstFiles.ContextMenu = Me.ContextMenu1
            Me.lstFiles.Dock = DockStyle.Fill
            Me.lstFiles.Location = New Point(0, 0)
            Me.lstFiles.Name = "lstFiles"
            Me.lstFiles.Size = New Size(296, 168)
            Me.lstFiles.TabIndex = 2
            Me.lstFiles.View = View.Details
            '
            'colFile
            '
            Me.colFile.Text = "File"
            Me.colFile.Width = 138
            '
            'colFolder
            '
            Me.colFolder.Text = "Folder"
            Me.colFolder.Width = 154
            '
            'ContextMenu1
            '
            Me.ContextMenu1.MenuItems.AddRange(New MenuItem() {Me.mnuRemoveItems, Me.mnuClearWorkBench, Me.mnuSaveWorkBench, Me.mnuLoadWorkBench})
            '
            'mnuRemoveItems
            '
            Me.mnuRemoveItems.Index = 0
            Me.mnuRemoveItems.Text = "&Remove Item(s)"
            '
            'mnuClearWorkBench
            '
            Me.mnuClearWorkBench.Index = 1
            Me.mnuClearWorkBench.Text = "&Clear WorkBench"
            '
            'mnuSaveWorkBench
            '
            Me.mnuSaveWorkBench.Index = 2
            Me.mnuSaveWorkBench.Text = "&Save WorkBench"
            '
            'mnuLoadWorkBench
            '
            Me.mnuLoadWorkBench.Index = 3
            Me.mnuLoadWorkBench.Text = "&Load WorkBench"
            '
            'CR_WorkBenchWindow
            '
            Me.Controls.Add(Me.lstFiles)
            Me.Name = "CR_WorkBenchWindow"
            Me.Size = New Size(296, 168)
            CType(Me.events_, ISupportInitialize).EndInit()
            CType(Me, ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        ' protected methods...
#Region " Dispose "
        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If

            MyBase.Dispose(disposing)
        End Sub
#End Region
#End Region

#Region "Fields"
        Private SourceType As String
#End Region
#Region "Properties"
        Private Shared ReadOnly Property GetWorkBenchFileName() As String
            Get
                Return GetWorkBenchDir() & (New FileInfo(CodeRush.Source.ActiveSolution.Name)).Name & ".wrk"
            End Get
        End Property
        Private Shared Function GetWorkBenchDir() As String
            Return GetFolderPath(SpecialFolder.LocalApplicationData) & "\WorkBenches\"
        End Function
#End Region
#Region "UI Events"
        Private Sub Files_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles lstFiles.DragOver
            Select Case True
                Case e.Data.GetDataPresent(DataFormats.FileDrop)
                    e.Effect = DragDropEffects.Copy
                    SourceType = DataFormats.FileDrop
                Case e.Data.GetDataPresent(DataFormats.StringFormat)
                    e.Effect = DragDropEffects.Copy
                    SourceType = DataFormats.StringFormat
                Case Else
                    e.Effect = DragDropEffects.None
            End Select

        End Sub
        Private Sub lstFiles_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles lstFiles.DragDrop
            Dim Filename As String
            Select Case SourceType
                Case DataFormats.StringFormat
                    Filename = e.Data.GetData(SourceType)
                Case DataFormats.FileDrop
                    Filename = e.Data.GetData(SourceType)(0)
                Case Else
                    ' Do nothing as we really don't know how to handle anything else
                    Exit Sub
            End Select
            Call AddFileToWorkBench(Filename)
        End Sub
        Private Sub lstFiles_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstFiles.DoubleClick
            CodeRush.File.Activate(CType(lstFiles.SelectedItems(0), FileItem).Fullname)
        End Sub
        Private Sub mnuSaveWorkBench_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuSaveWorkBench.Click
            Call SaveWorkBench()
        End Sub
        Private Sub mnuLoadWorkBench_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuLoadWorkBench.Click
            Call LoadWorkBench()
        End Sub
#End Region
        Private Sub AddFileToWorkBench(ByVal Filename As String)
            lstFiles.Items.Add(New FileItem(New FileInfo(Filename)))
        End Sub
        Friend Sub SaveWorkBench()
            Dim SaveDir As String = GetWorkBenchDir()
            If Not Directory.Exists(SaveDir) Then
                Directory.CreateDirectory(SaveDir)
            End If
            Using sw As New System.IO.StringWriter()
                For Each FileItem As FileItem In lstFiles.Items
                    sw.WriteLine(FileItem.Fullname)
                Next
                SetTextinFile(New FileInfo(GetWorkBenchFileName), sw.ToString, Encoding.Unicode)
            End Using
        End Sub
        Friend Sub LoadWorkBench()
            lstFiles.Items.Clear()
            Dim SaveFile As String = GetWorkBenchFileName
            If File.Exists(SaveFile) Then
                Dim X As String = GetTextinFile(New FileInfo(SaveFile))
                For Each File As String In X.Split(Environment.NewLine)
                    If File.Trim <> String.Empty Then
                        Call AddFileToWorkBench(File.Trim)
                    End If
                Next
            End If
        End Sub

        Private Sub mnuRemoveItems_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuRemoveItems.Click
            For CurrentItemIndex As Integer = lstFiles.SelectedItems.Count - 1 To 0 Step -1
                lstFiles.Items.RemoveAt(CurrentItemIndex)
            Next
        End Sub

        Private Sub mnuClearWorkBench_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuClearWorkBench.Click
            lstFiles.Items.Clear()
        End Sub
    End Class
End Namespace
