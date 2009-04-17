Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports System.Windows.Forms
Imports CR_PaintIt.Controls
Imports CR_PaintIt.Painting
Namespace Controls
    Public Class PaintPicker
        Inherits System.Windows.Forms.UserControl
        Implements Painting.IPaintStyle

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call

        End Sub

        'UserControl overrides dispose to clean up the component list.
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
        Friend WithEvents ColorPicker1 As ColorPicker
        Friend WithEvents ColorPicker2 As ColorPicker
        Friend WithEvents PaintStyleList As StyleList
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.PaintStyleList = New Controls.StyleList
            Me.ColorPicker1 = New Controls.ColorPicker
            Me.ColorPicker2 = New Controls.ColorPicker
            Me.SuspendLayout()
            '
            'PaintStyleList
            '
            Me.PaintStyleList.CurrentStyle = Painting.PaintRequestEnum.TextHighlight
            Me.PaintStyleList.Dock = System.Windows.Forms.DockStyle.Left
            Me.PaintStyleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.PaintStyleList.Items.AddRange(New Object() {Painting.PaintRequestEnum.StrikeThrough, Painting.PaintRequestEnum.UnderlineStroke, Painting.PaintRequestEnum.UnderlineThin, Painting.PaintRequestEnum.UnderlineWavy, Painting.PaintRequestEnum.BrushStroke, Painting.PaintRequestEnum.TextHighlight, Painting.PaintRequestEnum.TextOverlay})
            Me.PaintStyleList.Location = New System.Drawing.Point(0, 0)
            Me.PaintStyleList.Name = "PaintStyleList"
            Me.PaintStyleList.Size = New System.Drawing.Size(96, 21)
            Me.PaintStyleList.TabIndex = 0
            '
            'ColorPicker1
            '
            Me.ColorPicker1.AllowOpacity = True
            Me.ColorPicker1.ColorBase = System.Drawing.Color.Empty
            Me.ColorPicker1.Dock = System.Windows.Forms.DockStyle.Left
            Me.ColorPicker1.LabelWidth = 45
            Me.ColorPicker1.Location = New System.Drawing.Point(96, 0)
            Me.ColorPicker1.Name = "ColorPicker1"
            Me.ColorPicker1.Opacity = 255
            Me.ColorPicker1.Size = New System.Drawing.Size(160, 24)
            Me.ColorPicker1.TabIndex = 1
            Me.ColorPicker1.Text = "Color 1 "
            '
            'ColorPicker2
            '
            Me.ColorPicker2.AllowOpacity = True
            Me.ColorPicker2.ColorBase = System.Drawing.Color.Empty
            Me.ColorPicker2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ColorPicker2.LabelWidth = 45
            Me.ColorPicker2.Location = New System.Drawing.Point(256, 0)
            Me.ColorPicker2.Name = "ColorPicker2"
            Me.ColorPicker2.Opacity = 255
            Me.ColorPicker2.Size = New System.Drawing.Size(160, 24)
            Me.ColorPicker2.TabIndex = 2
            Me.ColorPicker2.Text = "Color 2 "
            '
            'PaintPicker
            '
            Me.Controls.Add(Me.ColorPicker2)
            Me.Controls.Add(Me.ColorPicker1)
            Me.Controls.Add(Me.PaintStyleList)
            Me.Name = "PaintPicker"
            Me.Size = New System.Drawing.Size(416, 24)
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private Sub PaintStyleList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaintStyleList.SelectedIndexChanged
            UpdateVisibility()
        End Sub
        Public Property Decoration() As PaintRequestEnum Implements Painting.IPaintStyle.Decoration
            Get
                Return Me.PaintStyleList.CurrentStyle
            End Get
            Set(ByVal Value As PaintRequestEnum)
                Me.PaintStyleList.CurrentStyle = Value
                UpdateVisibility()
            End Set
        End Property
        Private Sub UpdateVisibility()
            ColorPicker2.Visible = (Decoration = PaintRequestEnum.TextHighlight)
            ColorPicker1.AllowOpacity = (Not Decoration = PaintRequestEnum.TextOverlay)
        End Sub
        Public Property Color1() As Painting.IPaintColor Implements Painting.IPaintStyle.Color1
            Get
                Return Me.ColorPicker1
            End Get
            Set(ByVal Value As Painting.IPaintColor)
                Settings.Copy(Value, ColorPicker1)
            End Set
        End Property
        Public Property Color2() As Painting.IPaintColor Implements Painting.IPaintStyle.Color2
            Get
                Return Me.ColorPicker2
            End Get
            Set(ByVal Value As Painting.IPaintColor)
                Settings.Copy(Value, ColorPicker2)
            End Set
        End Property
    End Class
End Namespace