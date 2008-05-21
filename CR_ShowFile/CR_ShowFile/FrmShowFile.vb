Imports System.Collections
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class FrmShowFile
    Inherits System.Windows.Forms.Form
    Private FileNames As CodeFileCollection
#Region " Windows Form Designer generated code "
    ''' <summary>
    ''' Creates a new instance of FrmShowFile
    ''' </summary>
    ''' <param name="fileNames"></param>
    Public Sub New(ByVal fileNames As CodeFileCollection)
        MyBase.New()
        Me.FileNames = fileNames
        InitializeComponent()
        AddFileToList("")
        txtName.Focus()
    End Sub

    Public Sub AddFileToList(ByVal Filter As String)
        Dim cFile As CodeFile
        lstFiles.Items.Clear()
        For Each cFile In FileNames
            If Filter = "" Then
                lstFiles.Items.Add(cFile)
            Else
                If cFile.ToString.ToLower.IndexOf(Filter.ToLower) > -1 Then
                    lstFiles.Items.Add(cFile)
                End If
            End If
        Next
        If lstFiles.Items.Count > 0 Then
            lstFiles.SelectedIndex = 0
        End If
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lstFiles As System.Windows.Forms.ListBox
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnShow = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.txtName = New System.Windows.Forms.TextBox
        Me.lstFiles = New System.Windows.Forms.ListBox
        Me.StatusBar1 = New System.Windows.Forms.StatusBar
        Me.SuspendLayout()
        '
        'btnShow
        '
        Me.btnShow.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnShow.Location = New System.Drawing.Point(208, 184)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.TabIndex = 0
        Me.btnShow.Text = "Button1"
        Me.btnShow.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(208, 208)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Button2"
        Me.btnCancel.Visible = False
        '
        'txtName
        '
        Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtName.Location = New System.Drawing.Point(8, 8)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(188, 20)
        Me.txtName.TabIndex = 2
        Me.txtName.Text = ""
        '
        'lstFiles
        '
        Me.lstFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstFiles.Location = New System.Drawing.Point(8, 32)
        Me.lstFiles.Name = "lstFiles"
        Me.lstFiles.Size = New System.Drawing.Size(188, 134)
        Me.lstFiles.Sorted = True
        Me.lstFiles.TabIndex = 3
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 176)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(208, 22)
        Me.StatusBar1.TabIndex = 4
        '
        'FrmShowFile
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(208, 198)
        Me.ControlBox = False
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.lstFiles)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnShow)
        Me.Name = "FrmShowFile"
        Me.Text = "Find and show file"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        AddFileToList(txtName.Text)
    End Sub

    Private Sub ShowFile()
        If Not lstFiles.SelectedItem Is Nothing Then
            Me.Hide()
            If Not DirectCast(lstFiles.SelectedItem, CodeFile).TheDoc Is Nothing Then
                CodeRush.Documents.Activate(DirectCast(lstFiles.SelectedItem, CodeFile).TheDoc)
            Else
                Dim mydte As EnvDTE.DTE
                mydte = CodeRush.ApplicationObject
                mydte.ItemOperations.OpenFile(DirectCast(lstFiles.SelectedItem, CodeFile).theFile.FilePath)
                'CodeRush.File.Activate(DirectCast(lstFiles.SelectedItem, CodeFile).theFile.FilePath)
            End If
        End If
        Close()
    End Sub

 
    Private Sub txtName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                ShowFile()
                e.Handled = True
            Case Keys.Escape
                Close()
                e.Handled = True
            Case Keys.Up
                SelectPrevious()
                e.Handled = True
            Case Keys.Down
                SelectNext()
                e.Handled = True
        End Select
    End Sub
    Public Sub SelectNext()
        If lstFiles.SelectedIndex < lstFiles.Items.Count - 1 Then
            lstFiles.SelectedIndex += 1
        End If
    End Sub
    Public Sub SelectPrevious()
        If lstFiles.SelectedIndex > 0 Then
            lstFiles.SelectedIndex -= 1
        End If
    End Sub

    Private Sub lstFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        Dim cfile As CodeFile
        cfile = DirectCast(lstFiles.SelectedItem, CodeFile)
        Me.StatusBar1.Text = "Project: " & cfile.ProjectName
        Me.txtName.Focus()
    End Sub

    Private Sub lstFiles_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstFiles.DoubleClick
        ShowFile()
    End Sub
End Class
