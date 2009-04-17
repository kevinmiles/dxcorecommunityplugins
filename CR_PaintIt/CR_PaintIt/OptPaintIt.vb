Option Strict On
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports System.Diagnostics
Imports CR_PaintIt.Controls
Imports CR_PaintIt.Painting
Imports CR_PaintIt.Painting.Settings

''' <summary>
''' Summary description for OptPaintIt.
''' </summary>
Public Class OptPaintIt
    Inherits OptionsPage
#Region "Constants"
    Public Const SECTION_CURRENT_MEMBER As String = "CurrentMember"
    Public Const SECTION_MEMBER_COLORS As String = "MemberColors"
    Public Const SETTING_COLOR_PUBLIC As String = "PublicColor"
    Public Const SETTING_COLOR_PROTECTED As String = "ProtectedColor"
    Public Const SETTING_COLOR_PROTECTED_INTERNAL As String = "ProtectedInternalColor"
    Public Const SETTING_COLOR_INTERNAL As String = "InternalColor"
    Public Const SETTING_COLOR_PRIVATE As String = "PrivateColor"
    Public Const SETTING_COLOR_LOCAL As String = "LocalColor"
    Public Const SETTING_COLOR_PARAM_IN As String = "ParamInColor"
    Public Const SETTING_COLOR_PARAM_REF As String = "ParamRefColor"
    Public Const SETTING_COLOR_PARAM_OUT As String = "ParamOutColor"
    Public Const SETTING_COLOR_PARAM_ARRAY As String = "ParamArrayColor"
    Public Const SETTING_ENABLED_PUBLIC As String = "PublicEnabled"
    Public Const SETTING_ENABLED_PROTECTED As String = "ProtectedEnabled"
    Public Const SETTING_ENABLED_PROTECTED_INTERNAL As String = "ProtectedInternalEnabled"
    Public Const SETTING_ENABLED_INTERNAL As String = "InternalEnabled"
    Public Const SETTING_ENABLED_PRIVATE As String = "PrivateEnabled"
    Public Const SETTING_ENABLED_LOCAL As String = "LocalEnabled"
    Public Const SETTING_ENABLED_PARAM_IN As String = "ParamInEnabled"
    Public Const SETTING_ENABLED_PARAM_OUT As String = "ParamOutEnabled"
    Public Const SETTING_ENABLED_PARAM_REF As String = "ParamRefEnabled"
    Public Const SETTING_ENABLED_PARAM_ARRAY As String = "ParamArrayEnabled"
#End Region
#Region " private fields... "
    Private components As System.ComponentModel.Container = Nothing
#End Region

    ' constructor...
#Region " OptPaintIt "
    ''' <summary>
    ''' Required for Windows.Forms Class Composition Designer support
    ''' </summary>
    Public Sub New()
        MyBase.New()
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
        Return "NPS"
    End Function

    Public Shared Function GetPageName() As String
        Return "PaintIt"
    End Function

    Public Shared ReadOnly Property Storage() As DecoupledStorage
        Get
            Return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName())
        End Get
    End Property

    Public Overrides ReadOnly Property Category() As String
        Get
            Return OptPaintIt.GetCategory()
        End Get
    End Property

    Public Overrides ReadOnly Property PageName() As String
        Get
            Return OptPaintIt.GetPageName()
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
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents clprotected As ColorPicker
    Friend WithEvents clProtectedInternal As ColorPicker
    Friend WithEvents clPublic As ColorPicker
    Friend WithEvents clPrivate As ColorPicker
    Friend WithEvents clInternal As ColorPicker
    Friend WithEvents clParamIn As ColorPicker
    Friend WithEvents clParamOut As ColorPicker
    Friend WithEvents clParamArray As ColorPicker
    Friend WithEvents clParamRef As ColorPicker
    Friend WithEvents tpMemberColors As System.Windows.Forms.TabPage
    Friend WithEvents tpCurrentMember As System.Windows.Forms.TabPage
    Friend WithEvents optCurrentisCursor As System.Windows.Forms.RadioButton
    Friend WithEvents optCurrentisCaret As System.Windows.Forms.RadioButton
    Friend WithEvents clLocal As ColorPicker
    Friend WithEvents chkPublicEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkProtectedEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkProtectedInternalEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkInternalEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrivateEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkLocalEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkParamInEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkParamOutEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkParamRefEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkParamArrayEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkCurrentMemberEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkHighlightCurrentItem As System.Windows.Forms.CheckBox
    Friend WithEvents CurrentPaintPicker As PaintPicker
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tpCurrentMember = New System.Windows.Forms.TabPage
        Me.chkHighlightCurrentItem = New System.Windows.Forms.CheckBox
        Me.chkCurrentMemberEnabled = New System.Windows.Forms.CheckBox
        Me.optCurrentisCaret = New System.Windows.Forms.RadioButton
        Me.optCurrentisCursor = New System.Windows.Forms.RadioButton
        Me.CurrentPaintPicker = New PaintPicker
        Me.tpMemberColors = New System.Windows.Forms.TabPage
        Me.chkParamArrayEnabled = New System.Windows.Forms.CheckBox
        Me.chkParamRefEnabled = New System.Windows.Forms.CheckBox
        Me.chkParamOutEnabled = New System.Windows.Forms.CheckBox
        Me.chkParamInEnabled = New System.Windows.Forms.CheckBox
        Me.chkLocalEnabled = New System.Windows.Forms.CheckBox
        Me.chkPrivateEnabled = New System.Windows.Forms.CheckBox
        Me.chkInternalEnabled = New System.Windows.Forms.CheckBox
        Me.chkProtectedInternalEnabled = New System.Windows.Forms.CheckBox
        Me.chkProtectedEnabled = New System.Windows.Forms.CheckBox
        Me.chkPublicEnabled = New System.Windows.Forms.CheckBox
        Me.clParamIn = New ColorPicker
        Me.clParamOut = New ColorPicker
        Me.clLocal = New ColorPicker
        Me.clParamArray = New ColorPicker
        Me.clParamRef = New ColorPicker
        Me.clprotected = New ColorPicker
        Me.clProtectedInternal = New ColorPicker
        Me.clPublic = New ColorPicker
        Me.clPrivate = New ColorPicker
        Me.clInternal = New ColorPicker
        Me.TabControl1.SuspendLayout()
        Me.tpCurrentMember.SuspendLayout()
        Me.tpMemberColors.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tpCurrentMember)
        Me.TabControl1.Controls.Add(Me.tpMemberColors)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(530, 480)
        Me.TabControl1.TabIndex = 5
        '
        'tpCurrentMember
        '
        Me.tpCurrentMember.Controls.Add(Me.chkHighlightCurrentItem)
        Me.tpCurrentMember.Controls.Add(Me.chkCurrentMemberEnabled)
        Me.tpCurrentMember.Controls.Add(Me.optCurrentisCaret)
        Me.tpCurrentMember.Controls.Add(Me.optCurrentisCursor)
        Me.tpCurrentMember.Controls.Add(Me.CurrentPaintPicker)
        Me.tpCurrentMember.Location = New System.Drawing.Point(4, 22)
        Me.tpCurrentMember.Name = "tpCurrentMember"
        Me.tpCurrentMember.Size = New System.Drawing.Size(522, 454)
        Me.tpCurrentMember.TabIndex = 2
        Me.tpCurrentMember.Text = "Current Member"
        '
        'chkHighlightCurrentItem
        '
        Me.chkHighlightCurrentItem.Location = New System.Drawing.Point(56, 136)
        Me.chkHighlightCurrentItem.Name = "chkHighlightCurrentItem"
        Me.chkHighlightCurrentItem.Size = New System.Drawing.Size(200, 24)
        Me.chkHighlightCurrentItem.TabIndex = 6
        Me.chkHighlightCurrentItem.Text = "Highlight current item"
        '
        'chkCurrentMemberEnabled
        '
        Me.chkCurrentMemberEnabled.Location = New System.Drawing.Point(40, 24)
        Me.chkCurrentMemberEnabled.Name = "chkCurrentMemberEnabled"
        Me.chkCurrentMemberEnabled.TabIndex = 4
        Me.chkCurrentMemberEnabled.Text = "Enabled"
        '
        'optCurrentisCaret
        '
        Me.optCurrentisCaret.Location = New System.Drawing.Point(72, 56)
        Me.optCurrentisCaret.Name = "optCurrentisCaret"
        Me.optCurrentisCaret.Size = New System.Drawing.Size(208, 24)
        Me.optCurrentisCaret.TabIndex = 1
        Me.optCurrentisCaret.Text = "Current Under Caret"
        '
        'optCurrentisCursor
        '
        Me.optCurrentisCursor.Location = New System.Drawing.Point(72, 80)
        Me.optCurrentisCursor.Name = "optCurrentisCursor"
        Me.optCurrentisCursor.Size = New System.Drawing.Size(208, 24)
        Me.optCurrentisCursor.TabIndex = 0
        Me.optCurrentisCursor.Text = "Current Under Cursor"
        '
        'CurrentPaintPicker
        '
        Me.CurrentPaintPicker.Color1.Base = System.Drawing.Color.Blue
        Me.CurrentPaintPicker.Color1.Opacity = 255
        Me.CurrentPaintPicker.Color2.Base = System.Drawing.Color.Blue
        Me.CurrentPaintPicker.Color2.Opacity = 100
        Me.CurrentPaintPicker.Location = New System.Drawing.Point(56, 112)
        Me.CurrentPaintPicker.Name = "CurrentPaintPicker"
        Me.CurrentPaintPicker.Size = New System.Drawing.Size(432, 24)
        Me.CurrentPaintPicker.Decoration = PaintRequestEnum.BrushStroke
        Me.CurrentPaintPicker.TabIndex = 8
        '
        'tpMemberColors
        '
        Me.tpMemberColors.Controls.Add(Me.chkParamArrayEnabled)
        Me.tpMemberColors.Controls.Add(Me.chkParamRefEnabled)
        Me.tpMemberColors.Controls.Add(Me.chkParamOutEnabled)
        Me.tpMemberColors.Controls.Add(Me.chkParamInEnabled)
        Me.tpMemberColors.Controls.Add(Me.chkLocalEnabled)
        Me.tpMemberColors.Controls.Add(Me.chkPrivateEnabled)
        Me.tpMemberColors.Controls.Add(Me.chkInternalEnabled)
        Me.tpMemberColors.Controls.Add(Me.chkProtectedInternalEnabled)
        Me.tpMemberColors.Controls.Add(Me.chkProtectedEnabled)
        Me.tpMemberColors.Controls.Add(Me.chkPublicEnabled)
        Me.tpMemberColors.Controls.Add(Me.clParamIn)
        Me.tpMemberColors.Controls.Add(Me.clParamOut)
        Me.tpMemberColors.Controls.Add(Me.clLocal)
        Me.tpMemberColors.Controls.Add(Me.clParamArray)
        Me.tpMemberColors.Controls.Add(Me.clParamRef)
        Me.tpMemberColors.Controls.Add(Me.clprotected)
        Me.tpMemberColors.Controls.Add(Me.clProtectedInternal)
        Me.tpMemberColors.Controls.Add(Me.clPublic)
        Me.tpMemberColors.Controls.Add(Me.clPrivate)
        Me.tpMemberColors.Controls.Add(Me.clInternal)
        Me.tpMemberColors.Location = New System.Drawing.Point(4, 22)
        Me.tpMemberColors.Name = "tpMemberColors"
        Me.tpMemberColors.Size = New System.Drawing.Size(522, 454)
        Me.tpMemberColors.TabIndex = 1
        Me.tpMemberColors.Text = "Member Colors"
        '
        'chkParamArrayEnabled
        '
        Me.chkParamArrayEnabled.Location = New System.Drawing.Point(16, 312)
        Me.chkParamArrayEnabled.Name = "chkParamArrayEnabled"
        Me.chkParamArrayEnabled.Size = New System.Drawing.Size(16, 24)
        Me.chkParamArrayEnabled.TabIndex = 24
        Me.chkParamArrayEnabled.Text = "CheckBox10"
        '
        'chkParamRefEnabled
        '
        Me.chkParamRefEnabled.Location = New System.Drawing.Point(16, 280)
        Me.chkParamRefEnabled.Name = "chkParamRefEnabled"
        Me.chkParamRefEnabled.Size = New System.Drawing.Size(16, 24)
        Me.chkParamRefEnabled.TabIndex = 23
        Me.chkParamRefEnabled.Text = "CheckBox9"
        '
        'chkParamOutEnabled
        '
        Me.chkParamOutEnabled.Location = New System.Drawing.Point(16, 248)
        Me.chkParamOutEnabled.Name = "chkParamOutEnabled"
        Me.chkParamOutEnabled.Size = New System.Drawing.Size(16, 24)
        Me.chkParamOutEnabled.TabIndex = 22
        Me.chkParamOutEnabled.Text = "CheckBox8"
        '
        'chkParamInEnabled
        '
        Me.chkParamInEnabled.Location = New System.Drawing.Point(16, 216)
        Me.chkParamInEnabled.Name = "chkParamInEnabled"
        Me.chkParamInEnabled.Size = New System.Drawing.Size(16, 24)
        Me.chkParamInEnabled.TabIndex = 21
        Me.chkParamInEnabled.Text = "CheckBox7"
        '
        'chkLocalEnabled
        '
        Me.chkLocalEnabled.Location = New System.Drawing.Point(16, 184)
        Me.chkLocalEnabled.Name = "chkLocalEnabled"
        Me.chkLocalEnabled.Size = New System.Drawing.Size(16, 24)
        Me.chkLocalEnabled.TabIndex = 20
        Me.chkLocalEnabled.Text = "CheckBox6"
        '
        'chkPrivateEnabled
        '
        Me.chkPrivateEnabled.Location = New System.Drawing.Point(16, 136)
        Me.chkPrivateEnabled.Name = "chkPrivateEnabled"
        Me.chkPrivateEnabled.Size = New System.Drawing.Size(16, 24)
        Me.chkPrivateEnabled.TabIndex = 19
        Me.chkPrivateEnabled.Text = "CheckBox5"
        '
        'chkInternalEnabled
        '
        Me.chkInternalEnabled.Location = New System.Drawing.Point(16, 104)
        Me.chkInternalEnabled.Name = "chkInternalEnabled"
        Me.chkInternalEnabled.Size = New System.Drawing.Size(16, 24)
        Me.chkInternalEnabled.TabIndex = 18
        Me.chkInternalEnabled.Text = "CheckBox4"
        '
        'chkProtectedInternalEnabled
        '
        Me.chkProtectedInternalEnabled.Location = New System.Drawing.Point(16, 72)
        Me.chkProtectedInternalEnabled.Name = "chkProtectedInternalEnabled"
        Me.chkProtectedInternalEnabled.Size = New System.Drawing.Size(16, 24)
        Me.chkProtectedInternalEnabled.TabIndex = 17
        Me.chkProtectedInternalEnabled.Text = "CheckBox3"
        '
        'chkProtectedEnabled
        '
        Me.chkProtectedEnabled.Location = New System.Drawing.Point(16, 40)
        Me.chkProtectedEnabled.Name = "chkProtectedEnabled"
        Me.chkProtectedEnabled.Size = New System.Drawing.Size(16, 24)
        Me.chkProtectedEnabled.TabIndex = 16
        Me.chkProtectedEnabled.Text = "CheckBox2"
        '
        'chkPublicEnabled
        '
        Me.chkPublicEnabled.Location = New System.Drawing.Point(16, 8)
        Me.chkPublicEnabled.Name = "chkPublicEnabled"
        Me.chkPublicEnabled.Size = New System.Drawing.Size(16, 24)
        Me.chkPublicEnabled.TabIndex = 15
        Me.chkPublicEnabled.Text = "CheckBox1"
        '
        'clParamIn
        '
        Me.clParamIn.ColorBase = System.Drawing.Color.Black
        Me.clParamIn.LabelWidth = 104
        Me.clParamIn.Location = New System.Drawing.Point(32, 216)
        Me.clParamIn.Name = "clParamIn"
        Me.clParamIn.Opacity = 255
        Me.clParamIn.AllowOpacity = False
        Me.clParamIn.Size = New System.Drawing.Size(336, 24)
        Me.clParamIn.TabIndex = 11
        Me.clParamIn.Text = "Param In "
        '
        'clParamOut
        '
        Me.clParamOut.ColorBase = System.Drawing.Color.Black
        Me.clParamOut.LabelWidth = 104
        Me.clParamOut.Location = New System.Drawing.Point(32, 248)
        Me.clParamOut.Name = "clParamOut"
        Me.clParamOut.Opacity = 255
        Me.clParamOut.AllowOpacity = False
        Me.clParamOut.Size = New System.Drawing.Size(336, 24)
        Me.clParamOut.TabIndex = 12
        Me.clParamOut.Text = "Param Out "
        '
        'clLocal
        '
        Me.clLocal.ColorBase = System.Drawing.Color.Black
        Me.clLocal.LabelWidth = 104
        Me.clLocal.Location = New System.Drawing.Point(32, 184)
        Me.clLocal.Name = "clLocal"
        Me.clLocal.Opacity = 255
        Me.clLocal.AllowOpacity = False
        Me.clLocal.Size = New System.Drawing.Size(336, 24)
        Me.clLocal.TabIndex = 10
        Me.clLocal.Text = "Local"
        '
        'clParamArray
        '
        Me.clParamArray.ColorBase = System.Drawing.Color.Black
        Me.clParamArray.LabelWidth = 104
        Me.clParamArray.Location = New System.Drawing.Point(32, 312)
        Me.clParamArray.Name = "clParamArray"
        Me.clParamArray.Opacity = 255
        Me.clParamArray.AllowOpacity = False
        Me.clParamArray.Size = New System.Drawing.Size(336, 24)
        Me.clParamArray.TabIndex = 14
        Me.clParamArray.Text = "Param Array "
        '
        'clParamRef
        '
        Me.clParamRef.ColorBase = System.Drawing.Color.Black
        Me.clParamRef.LabelWidth = 104
        Me.clParamRef.Location = New System.Drawing.Point(32, 280)
        Me.clParamRef.Name = "clParamRef"
        Me.clParamRef.Opacity = 255
        Me.clParamRef.AllowOpacity = False
        Me.clParamRef.Size = New System.Drawing.Size(336, 24)
        Me.clParamRef.TabIndex = 13
        Me.clParamRef.Text = "Param Ref "
        '
        'clprotected
        '
        Me.clprotected.ColorBase = System.Drawing.Color.Black
        Me.clprotected.LabelWidth = 104
        Me.clprotected.Location = New System.Drawing.Point(32, 40)
        Me.clprotected.Name = "clprotected"
        Me.clprotected.Opacity = 255
        Me.clprotected.AllowOpacity = False
        Me.clprotected.Size = New System.Drawing.Size(336, 24)
        Me.clprotected.TabIndex = 6
        Me.clprotected.Text = "Protected "
        '
        'clProtectedInternal
        '
        Me.clProtectedInternal.ColorBase = System.Drawing.Color.Black
        Me.clProtectedInternal.LabelWidth = 104
        Me.clProtectedInternal.Location = New System.Drawing.Point(32, 72)
        Me.clProtectedInternal.Name = "clProtectedInternal"
        Me.clProtectedInternal.Opacity = 255
        Me.clProtectedInternal.AllowOpacity = False
        Me.clProtectedInternal.Size = New System.Drawing.Size(336, 24)
        Me.clProtectedInternal.TabIndex = 7
        Me.clProtectedInternal.Text = "Protected Internal "
        '
        'clPublic
        '
        Me.clPublic.ColorBase = System.Drawing.Color.Black
        Me.clPublic.LabelWidth = 104
        Me.clPublic.Location = New System.Drawing.Point(32, 8)
        Me.clPublic.Name = "clPublic"
        Me.clPublic.Opacity = 255
        Me.clPublic.AllowOpacity = False
        Me.clPublic.Size = New System.Drawing.Size(336, 24)
        Me.clPublic.TabIndex = 5
        Me.clPublic.Text = "Public "
        '
        'clPrivate
        '
        Me.clPrivate.ColorBase = System.Drawing.Color.Black
        Me.clPrivate.LabelWidth = 104
        Me.clPrivate.Location = New System.Drawing.Point(32, 136)
        Me.clPrivate.Name = "clPrivate"
        Me.clPrivate.Opacity = 255
        Me.clPrivate.AllowOpacity = False
        Me.clPrivate.Size = New System.Drawing.Size(336, 24)
        Me.clPrivate.TabIndex = 9
        Me.clPrivate.Text = "Private "
        '
        'clInternal
        '
        Me.clInternal.ColorBase = System.Drawing.Color.Black
        Me.clInternal.LabelWidth = 104
        Me.clInternal.Location = New System.Drawing.Point(32, 104)
        Me.clInternal.Name = "clInternal"
        Me.clInternal.Opacity = 255
        Me.clInternal.AllowOpacity = False
        Me.clInternal.Size = New System.Drawing.Size(336, 24)
        Me.clInternal.TabIndex = 8
        Me.clInternal.Text = "Internal "
        '
        'OptPaintIt
        '
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "OptPaintIt"
        Me.TabControl1.ResumeLayout(False)
        Me.tpCurrentMember.ResumeLayout(False)
        Me.tpMemberColors.ResumeLayout(False)
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
    Public Shared Function DefaultHighlightStyle() As Painting.PaintOptions
        Return New Painting.PaintOptions(PaintRequestEnum.BrushStroke, Color.Blue, 100, Color.Blue, 255)
    End Function
    Public Shared Function DefaultMemberStyle() As Painting.PaintOptions
        Return New Painting.PaintOptions(PaintRequestEnum.TextOverlay, Color.Black, 255, Color.Gray, 255)
    End Function

    Private Sub OptPaintIt_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles MyBase.PreparePage
        Try
            chkCurrentMemberEnabled.Checked = Storage.ReadBoolean(SECTION_CURRENT_MEMBER, "Enabled", True)
            optCurrentisCaret.Checked = ea.Storage.ReadBoolean(SECTION_CURRENT_MEMBER, "CurrentUnderCaret", True)
            optCurrentisCursor.Checked = ea.Storage.ReadBoolean(SECTION_CURRENT_MEMBER, "CurrentUnderCursor", False)
            chkHighlightCurrentItem.Checked = ea.Storage.ReadBoolean(SECTION_CURRENT_MEMBER, "HighlightCurrentItem", True)

            LoadStyle(Storage, SECTION_CURRENT_MEMBER, "", CurrentPaintPicker, DefaultHighlightStyle())
            'Settings.Copy(DefaultStyle, CurrentPaintPicker)

            Dim HighlightStyle As Painting.PaintOptions = DefaultMemberStyle()
            LoadIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PUBLIC, clPublic, HighlightStyle.Color1)
            LoadIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PROTECTED, clprotected, HighlightStyle.Color1)
            LoadIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PROTECTED_INTERNAL, clProtectedInternal, HighlightStyle.Color1)
            LoadIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_INTERNAL, clInternal, HighlightStyle.Color1)
            LoadIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PRIVATE, clPrivate, HighlightStyle.Color1)

            LoadIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_LOCAL, clLocal, HighlightStyle.Color1)
            LoadIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PARAM_IN, clParamIn, HighlightStyle.Color1)
            LoadIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PARAM_OUT, clParamOut, HighlightStyle.Color1)
            LoadIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PARAM_REF, clParamRef, HighlightStyle.Color1)
            LoadIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PARAM_ARRAY, clParamArray, HighlightStyle.Color1)

            chkPublicEnabled.Checked = ea.Storage.ReadBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PUBLIC, False)
            chkProtectedEnabled.Checked = ea.Storage.ReadBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PROTECTED, False)
            chkProtectedInternalEnabled.Checked = ea.Storage.ReadBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PROTECTED_INTERNAL, False)
            chkInternalEnabled.Checked = ea.Storage.ReadBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_INTERNAL, False)
            chkPrivateEnabled.Checked = ea.Storage.ReadBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PRIVATE, False)
            chkLocalEnabled.Checked = ea.Storage.ReadBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_LOCAL, False)
            chkParamInEnabled.Checked = ea.Storage.ReadBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PARAM_IN, False)
            chkParamOutEnabled.Checked = ea.Storage.ReadBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PARAM_OUT, False)
            chkParamRefEnabled.Checked = ea.Storage.ReadBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PARAM_REF, False)
            chkParamArrayEnabled.Checked = ea.Storage.ReadBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PARAM_ARRAY, False)
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Throw
        End Try

    End Sub
    Private Sub OptPaintIt_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles MyBase.CommitChanges
        Try
            ea.Storage.WriteBoolean(SECTION_CURRENT_MEMBER, "HighlightCurrentItem", chkHighlightCurrentItem.Checked)
            ea.Storage.WriteBoolean(SECTION_CURRENT_MEMBER, "Enabled", chkCurrentMemberEnabled.Checked)
            ea.Storage.WriteBoolean(SECTION_CURRENT_MEMBER, "CurrentUnderCaret", optCurrentisCaret.Checked)
            ea.Storage.WriteBoolean(SECTION_CURRENT_MEMBER, "CurrentUnderCursor", optCurrentisCursor.Checked)

            Call SaveStyle(ea.Storage, SECTION_CURRENT_MEMBER, "", CurrentPaintPicker)

            SaveIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PUBLIC, clPublic)
            SaveIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PROTECTED, clprotected)
            SaveIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PROTECTED_INTERNAL, clProtectedInternal)
            SaveIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_INTERNAL, clInternal)
            SaveIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PRIVATE, clPrivate)

            SaveIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_LOCAL, clLocal)
            SaveIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PARAM_IN, clParamIn)
            SaveIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PARAM_OUT, clParamOut)
            SaveIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PARAM_REF, clParamRef)
            SaveIColor(ea.Storage, SECTION_MEMBER_COLORS, SETTING_COLOR_PARAM_ARRAY, clParamArray)

            ea.Storage.WriteBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PUBLIC, chkPublicEnabled.Checked)
            ea.Storage.WriteBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PROTECTED, chkProtectedEnabled.Checked)
            ea.Storage.WriteBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PROTECTED_INTERNAL, chkProtectedInternalEnabled.Checked)
            ea.Storage.WriteBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_INTERNAL, chkInternalEnabled.Checked)
            ea.Storage.WriteBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PRIVATE, chkPrivateEnabled.Checked)
            ea.Storage.WriteBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_LOCAL, chkLocalEnabled.Checked)
            ea.Storage.WriteBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PARAM_IN, chkParamInEnabled.Checked)
            ea.Storage.WriteBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PARAM_OUT, chkParamOutEnabled.Checked)
            ea.Storage.WriteBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PARAM_REF, chkParamRefEnabled.Checked)
            ea.Storage.WriteBoolean(SECTION_MEMBER_COLORS, SETTING_ENABLED_PARAM_ARRAY, chkParamArrayEnabled.Checked)
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Throw
        End Try
    End Sub
    Private Sub OptPaintIt_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles MyBase.RestoreDefaults
        chkCurrentMemberEnabled.Checked = True
        chkHighlightCurrentItem.Checked = True

        Settings.Copy(DefaultMemberStyle(), CurrentPaintPicker)

        optCurrentisCaret.Checked = True
        optCurrentisCursor.Checked = False

        Dim HighlightStyle As Painting.PaintOptions = DefaultMemberStyle()
        Settings.Copy(HighlightStyle.Color1, clPublic)
        Settings.Copy(HighlightStyle.Color1, clprotected)
        Settings.Copy(HighlightStyle.Color1, clProtectedInternal)
        Settings.Copy(HighlightStyle.Color1, clInternal)
        Settings.Copy(HighlightStyle.Color1, clPrivate)

        Settings.Copy(HighlightStyle.Color1, clLocal)
        Settings.Copy(HighlightStyle.Color1, clParamIn)
        Settings.Copy(HighlightStyle.Color1, clParamOut)
        Settings.Copy(HighlightStyle.Color1, clParamRef)
        Settings.Copy(HighlightStyle.Color1, clParamArray)

        clPublic.Enabled = False
        clprotected.Enabled = False
        clProtectedInternal.Enabled = False
        clInternal.Enabled = False
        clPrivate.Enabled = False
        clLocal.Enabled = False
        clParamIn.Enabled = False
        clParamOut.Enabled = False
        clParamRef.Enabled = False
        clParamArray.Enabled = False

    End Sub
End Class

