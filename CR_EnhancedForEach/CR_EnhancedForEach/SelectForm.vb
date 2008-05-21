Imports System.Collections
Imports System
Public Class SelectForm
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents lstItems As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblSize As System.Windows.Forms.Label
    Friend WithEvents tmrClear As System.Windows.Forms.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.lstItems = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblSize = New System.Windows.Forms.Label
        Me.tmrClear = New System.Windows.Forms.Timer(Me.components)
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
        Me.lstItems.Size = New System.Drawing.Size(160, 212)
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
        Me.Label1.Size = New System.Drawing.Size(160, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select enumerable type"
        '
        'lblSize
        '
        Me.lblSize.AutoSize = True
        Me.lblSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSize.Location = New System.Drawing.Point(72, 104)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(45, 18)
        Me.lblSize.TabIndex = 2
        Me.lblSize.Text = "Label2"
        '
        'tmrClear
        '
        Me.tmrClear.Interval = 1000
        '
        'SelectForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(160, 236)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstItems)
        Me.Controls.Add(Me.lblSize)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "SelectForm"
        Me.ShowInTaskbar = False
        Me.Text = "SelectForm"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private _selectedElement As EnumerableElement
    Private _ClipBoardElement As EnumerableElement

    Public Sub LoadItems(ByVal items As ArrayList)
        _CloseFromSelect = False
        lstItems.Items.Clear()
        For Each item As EnumerableElement In items
            lstItems.Items.Add(item)
        Next
        lstItems.SelectedIndex = 0
    End Sub

    Public Property ClipBoardElement() As EnumerableElement
        Get
            Return _ClipBoardElement
        End Get
        Set(ByVal value As EnumerableElement)
            _ClipBoardElement = value
            For iVar As Integer = 0 To lstItems.Items.Count - 1 Step 1
                If lstItems.Items(iVar).ToString = ClipBoardElement.ToString Then
                    lstItems.SelectedIndex = iVar
                End If
            Next
        End Set
    End Property

    Public Property SelectedElement() As EnumerableElement
        Get
            Return _selectedElement
        End Get
        Set(ByVal Value As EnumerableElement)
            _selectedElement = Value
        End Set
    End Property
    Private _CloseFromSelect As Boolean = False
    Private Sub SelectAndClose()
        _CloseFromSelect = True
        SelectedElement = lstItems.SelectedItem
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Left = -1000
        Me.Visible = False
        Me.Hide()
    End Sub
    Private Sub lstItems_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstItems.DoubleClick
        SelectAndClose()
    End Sub

    Private Sub lstItems_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstItems.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            SelectAndClose()
        ElseIf e.KeyCode = System.Windows.Forms.Keys.Escape Then
            _CloseFromSelect = False
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Hide()
        End If
    End Sub

    Private Sub SelectForm_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        If Not _CloseFromSelect Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Hide()
        End If

    End Sub

    Private Sub SelectForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lstItems.Focus()
    End Sub

    Private _searchString As String
    Private Sub SelectForm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        _searchString+=e.KeyChar
        For i As Integer = 0 To lstItems.Items.Count - 1 Step 1
            If CType(lstItems.Items(i), EnumerableElement).Name.StartsWith(_searchString) Then
                lstItems.SelectedIndex=i
                Return
            End If
        Next
    End Sub

    Private Sub tmrClear_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrClear.Tick
        _searchString=""
    End Sub

    Private Sub SelectForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        static blnOnEnd as Boolean=False 
        Static blnOnTop As Boolean = False

        system.Diagnostics.Debug.WriteLine(e.KeyCode.ToString)
        If e.KeyCode=Windows.Forms.Keys.Down andalso lstItems.SelectedIndex=lstItems.Items.Count-1 then 'and blnOnEnd Then
            lstItems.SelectedIndex=0
            e.Handled=True 
        End If
        'If e.KeyCode=Windows.Forms.Keys.Down andalso lstItems.SelectedIndex=lstItems.Items.Count-1 Then
        '    blnOnEnd=True 
        '    Return
        'End If

        If e.KeyCode=Windows.Forms.Keys.up andalso lstItems.SelectedIndex=0 then 'and blnOnTop Then
            lstItems.SelectedIndex=lstItems.Items.Count-1
            e.Handled =True 
        End If
        'If e.KeyCode=Windows.Forms.Keys.up andalso lstItems.SelectedIndex=0 Then
        '    blnOntop=True 
        '    Return
        'End If
        'blnOnEnd=False 
        'blnontop=False 
    End Sub
End Class