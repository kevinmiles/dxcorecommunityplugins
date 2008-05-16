Imports System.Collections
Imports System
Imports System.Windows.Forms
Imports System.Collections.Generic
Public Class SelectForm
    Inherits Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents lstItems As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblSize As Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lstItems = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblSize = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lstItems
        '
        Me.lstItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstItems.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstItems.ItemHeight = 16
        Me.lstItems.Location = New System.Drawing.Point(0, 24)
        Me.lstItems.Name = "lstItems"
        Me.lstItems.Size = New System.Drawing.Size(256, 180)
        Me.lstItems.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.Gray
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(256, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select a String"
        '
        'lblSize
        '
        Me.lblSize.AutoSize = True
        Me.lblSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSize.Location = New System.Drawing.Point(72, 104)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(49, 16)
        Me.lblSize.TabIndex = 2
        Me.lblSize.Text = "Label2"
        '
        'SelectForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(256, 208)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstItems)
        Me.Controls.Add(Me.lblSize)
        Me.KeyPreview = True
        Me.Name = "SelectForm"
        Me.ShowInTaskbar = False
        Me.Text = "SelectForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Fields"
    Private mSelectedText As String = String.Empty
    Private mItemSelected As Boolean = False
#End Region
#Region "Properties"
    Public Property SelectedText() As String
        Get
            Return mSelectedText
        End Get
        Set(ByVal Value As String)
            mSelectedText = Value
            mItemSelected = (mSelectedText <> String.Empty)
        End Set
    End Property
#End Region
#Region "Form Events"
    Private Sub SelectForm_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        If Not mItemSelected Then
            Me.DialogResult = DialogResult.Cancel
            Me.Hide()
        End If
    End Sub

    Private Sub SelectForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lstItems.Focus()
    End Sub

    Private Sub SelectForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        Static blnOnEnd As Boolean = False
        Static blnOnTop As Boolean = False

        System.Diagnostics.Debug.WriteLine(e.KeyCode.ToString)
        If e.KeyCode = Windows.Forms.Keys.Down AndAlso lstItems.SelectedIndex = lstItems.Items.Count - 1 Then
            lstItems.SelectedIndex = 0
            e.Handled = True
        End If
        If e.KeyCode = Windows.Forms.Keys.Up AndAlso lstItems.SelectedIndex = 0 Then
            lstItems.SelectedIndex = lstItems.Items.Count - 1
            e.Handled = True
        End If
    End Sub
#End Region
#Region "List Events"
    Private Sub lstItems_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstItems.DoubleClick
        Call SelectAndClose()
    End Sub

    Private Sub lstItems_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs) Handles lstItems.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call SelectAndClose()
        ElseIf e.KeyCode = Keys.Escape Then
            Call CancelAndClose()
        End If
    End Sub
#End Region

    Public Sub LoadItems(ByVal Items() As String)
        mItemSelected = False
        lstItems.Items.Clear()
        For Each Item As String In Items
            lstItems.Items.Add(Item)
        Next
        lstItems.SelectedIndex = 0
    End Sub

    Private Sub SelectAndClose()
        SelectedText = lstItems.SelectedItem.ToString
        Me.DialogResult = DialogResult.OK
        Me.Left = -1000
        Me.Visible = False
        Me.Hide()
    End Sub
    Private Sub CancelAndClose()
        SelectedText = String.Empty
        Me.DialogResult = DialogResult.Cancel
        Me.Hide()
    End Sub


End Class