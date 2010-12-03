<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EasyFields_Options

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

    End Sub

    'OptionsPage overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EasyFields_Options))
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GrowLabel1 = New XPO_EasyFields.GrowLabel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New XPO_EasyFields.GrowLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkIncludeFieldConstants = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtVariableName = New System.Windows.Forms.TextBox()
        Me.chkIncludedInheritedMembers = New System.Windows.Forms.CheckBox()
        Me.chkIncludeNonPersistent = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkReplaceClassOnly = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkUseCollectionsFieldsClass = New System.Windows.Forms.CheckBox()
        Me.txtCommentFormat = New System.Windows.Forms.TextBox()
        Me.chkUseComment = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkUpdateOnDocumentSave = New System.Windows.Forms.CheckBox()
        Me.chkAvailableWithinEntireClass = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(-15, -15)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton1.TabIndex = 10
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "RadioButton1"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrowLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LinkLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PictureBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer1.Size = New System.Drawing.Size(541, 480)
        Me.SplitContainer1.SplitterDistance = 65
        Me.SplitContainer1.TabIndex = 16
        '
        'GrowLabel1
        '
        Me.GrowLabel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GrowLabel1.ForeColor = System.Drawing.Color.Yellow
        Me.GrowLabel1.HeightPadding = 5
        Me.GrowLabel1.Location = New System.Drawing.Point(189, 40)
        Me.GrowLabel1.Name = "GrowLabel1"
        Me.GrowLabel1.Size = New System.Drawing.Size(352, 18)
        Me.GrowLabel1.TabIndex = 4
        Me.GrowLabel1.Text = "THIS IS NOT A DEVEXPRESS OFFICIAL PLUGIN"
        Me.GrowLabel1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.LinkLabel1.LinkColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.LinkLabel1.Location = New System.Drawing.Point(189, 14)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(256, 26)
        Me.LinkLabel1.TabIndex = 1
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "XPO_EasyFields Home" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "http://code.google.com/p/dxcorecommunityplugins/"
        Me.LinkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(248, Byte), Integer))
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.HeightPadding = 1
        Me.Label1.Location = New System.Drawing.Point(189, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(352, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "For more information on what these options please visit"
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(179, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(10, 65)
        Me.Panel1.TabIndex = 5
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(179, 65)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.chkIncludeFieldConstants)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.txtVariableName)
        Me.GroupBox3.Controls.Add(Me.chkIncludedInheritedMembers)
        Me.GroupBox3.Controls.Add(Me.chkIncludeNonPersistent)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.chkReplaceClassOnly)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.chkUseCollectionsFieldsClass)
        Me.GroupBox3.Controls.Add(Me.txtCommentFormat)
        Me.GroupBox3.Controls.Add(Me.chkUseComment)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.GroupBox3.Location = New System.Drawing.Point(9, 88)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(523, 294)
        Me.GroupBox3.TabIndex = 20
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "FieldsClass Options"
        '
        'chkIncludeFieldConstants
        '
        Me.chkIncludeFieldConstants.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkIncludeFieldConstants.AutoSize = True
        Me.chkIncludeFieldConstants.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkIncludeFieldConstants.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludeFieldConstants.ForeColor = System.Drawing.Color.Black
        Me.chkIncludeFieldConstants.Location = New System.Drawing.Point(6, 70)
        Me.chkIncludeFieldConstants.Name = "chkIncludeFieldConstants"
        Me.chkIncludeFieldConstants.Size = New System.Drawing.Size(255, 17)
        Me.chkIncludeFieldConstants.TabIndex = 23
        Me.chkIncludeFieldConstants.Text = "Include FieldsClass Constants for Attribute usage"
        Me.ToolTip1.SetToolTip(Me.chkIncludeFieldConstants, "This will generate a Constant <MyProperty>FieldName for each property on your Per" & _
                "sistent Object, this can then be used for Attributes where they require a consta" & _
                "nt and can't use an expression")
        Me.chkIncludeFieldConstants.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(30, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(134, 13)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "FieldsClass Variable Name:"
        '
        'txtVariableName
        '
        Me.txtVariableName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVariableName.Location = New System.Drawing.Point(170, 44)
        Me.txtVariableName.Name = "txtVariableName"
        Me.txtVariableName.Size = New System.Drawing.Size(100, 20)
        Me.txtVariableName.TabIndex = 21
        Me.ToolTip1.SetToolTip(Me.txtVariableName, "This will be the name of the Variable generated for your FieldsClass, this allows" & _
                " you to specify something that matches your coding style")
        '
        'chkIncludedInheritedMembers
        '
        Me.chkIncludedInheritedMembers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkIncludedInheritedMembers.AutoSize = True
        Me.chkIncludedInheritedMembers.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkIncludedInheritedMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludedInheritedMembers.ForeColor = System.Drawing.Color.Black
        Me.chkIncludedInheritedMembers.Location = New System.Drawing.Point(6, 139)
        Me.chkIncludedInheritedMembers.Name = "chkIncludedInheritedMembers"
        Me.chkIncludedInheritedMembers.Size = New System.Drawing.Size(439, 17)
        Me.chkIncludedInheritedMembers.TabIndex = 20
        Me.chkIncludedInheritedMembers.Text = "Include Inherited Members? (Inherit from ancestor FieldsClass instead of Persiste" & _
            "ntBase)"
        Me.ToolTip1.SetToolTip(Me.chkIncludedInheritedMembers, "When your Object inherits from another Object, you can have XPO_EasyFields includ" & _
                "e the inherited classes Fields as well")
        Me.chkIncludedInheritedMembers.UseVisualStyleBackColor = True
        '
        'chkIncludeNonPersistent
        '
        Me.chkIncludeNonPersistent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkIncludeNonPersistent.AutoSize = True
        Me.chkIncludeNonPersistent.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkIncludeNonPersistent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludeNonPersistent.ForeColor = System.Drawing.Color.Black
        Me.chkIncludeNonPersistent.Location = New System.Drawing.Point(6, 116)
        Me.chkIncludeNonPersistent.Name = "chkIncludeNonPersistent"
        Me.chkIncludeNonPersistent.Size = New System.Drawing.Size(249, 17)
        Me.chkIncludeNonPersistent.TabIndex = 19
        Me.chkIncludeNonPersistent.Text = "Include Non-Persistent Properties in FieldsClass"
        Me.ToolTip1.SetToolTip(Me.chkIncludeNonPersistent, "Allows you to have Non-Persistent properties included in your FieldsClass, this c" & _
                "an be handy for Binding scenarios where you want to have references to PropertyN" & _
                "ames")
        Me.chkIncludeNonPersistent.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(113, 208)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(222, 65)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "{computername} = My.Computer.Name" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "{currentuser} = My.User.Name (Domain\User)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "{d" & _
            "ateshort} = Now.ToShortDateString" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "{timeshort} = Now.ToShortTimeString" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "{date:fo" & _
            "rmat} Now.ToString(<format>)"
        '
        'chkReplaceClassOnly
        '
        Me.chkReplaceClassOnly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkReplaceClassOnly.AutoSize = True
        Me.chkReplaceClassOnly.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkReplaceClassOnly.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReplaceClassOnly.ForeColor = System.Drawing.Color.Black
        Me.chkReplaceClassOnly.Location = New System.Drawing.Point(6, 23)
        Me.chkReplaceClassOnly.Name = "chkReplaceClassOnly"
        Me.chkReplaceClassOnly.Size = New System.Drawing.Size(220, 17)
        Me.chkReplaceClassOnly.TabIndex = 16
        Me.chkReplaceClassOnly.Text = "Don't replace Fields Property or Variable?"
        Me.ToolTip1.SetToolTip(Me.chkReplaceClassOnly, resources.GetString("chkReplaceClassOnly.ToolTip"))
        Me.chkReplaceClassOnly.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(24, 188)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Comment Format:"
        '
        'chkUseCollectionsFieldsClass
        '
        Me.chkUseCollectionsFieldsClass.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkUseCollectionsFieldsClass.AutoSize = True
        Me.chkUseCollectionsFieldsClass.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkUseCollectionsFieldsClass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseCollectionsFieldsClass.ForeColor = System.Drawing.Color.DarkGray
        Me.chkUseCollectionsFieldsClass.Location = New System.Drawing.Point(6, 93)
        Me.chkUseCollectionsFieldsClass.Name = "chkUseCollectionsFieldsClass"
        Me.chkUseCollectionsFieldsClass.Size = New System.Drawing.Size(380, 17)
        Me.chkUseCollectionsFieldsClass.TabIndex = 18
        Me.chkUseCollectionsFieldsClass.Text = "Use XPO_EasyFields CollectionFieldsClass for XPCollections (not ready yet)"
        Me.ToolTip1.SetToolTip(Me.chkUseCollectionsFieldsClass, "Not in use yet, Will allow for generating Aggregate Operands for common scenarios" & _
                " such as Count/Sum/Min/Max/Exist")
        Me.chkUseCollectionsFieldsClass.UseVisualStyleBackColor = True
        '
        'txtCommentFormat
        '
        Me.txtCommentFormat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCommentFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommentFormat.Location = New System.Drawing.Point(116, 185)
        Me.txtCommentFormat.Name = "txtCommentFormat"
        Me.txtCommentFormat.Size = New System.Drawing.Size(401, 20)
        Me.txtCommentFormat.TabIndex = 9
        Me.txtCommentFormat.Text = "Created/Updated: {computername}\{currentuser} {dateshort} {timeshort}"
        Me.ToolTip1.SetToolTip(Me.txtCommentFormat, "Format of generated comment")
        '
        'chkUseComment
        '
        Me.chkUseComment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkUseComment.AutoSize = True
        Me.chkUseComment.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkUseComment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseComment.ForeColor = System.Drawing.Color.Black
        Me.chkUseComment.Location = New System.Drawing.Point(6, 162)
        Me.chkUseComment.Name = "chkUseComment"
        Me.chkUseComment.Size = New System.Drawing.Size(233, 17)
        Me.chkUseComment.TabIndex = 8
        Me.chkUseComment.Text = "Place comment when updating FieldsClass?"
        Me.ToolTip1.SetToolTip(Me.chkUseComment, "Simple comment placed on top of the class indicating when it was last generated")
        Me.chkUseComment.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.chkUpdateOnDocumentSave)
        Me.GroupBox2.Controls.Add(Me.chkAvailableWithinEntireClass)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(9, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(523, 70)
        Me.GroupBox2.TabIndex = 19
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Availability"
        '
        'chkUpdateOnDocumentSave
        '
        Me.chkUpdateOnDocumentSave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkUpdateOnDocumentSave.AutoSize = True
        Me.chkUpdateOnDocumentSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUpdateOnDocumentSave.ForeColor = System.Drawing.Color.Black
        Me.chkUpdateOnDocumentSave.Location = New System.Drawing.Point(6, 46)
        Me.chkUpdateOnDocumentSave.Name = "chkUpdateOnDocumentSave"
        Me.chkUpdateOnDocumentSave.Size = New System.Drawing.Size(149, 17)
        Me.chkUpdateOnDocumentSave.TabIndex = 15
        Me.chkUpdateOnDocumentSave.Text = "Perform Update on Save?"
        Me.ToolTip1.SetToolTip(Me.chkUpdateOnDocumentSave, "When the document is Saved, XPO_EasyFields can be triggered to update the FieldsC" & _
                "lass")
        Me.chkUpdateOnDocumentSave.UseVisualStyleBackColor = True
        '
        'chkAvailableWithinEntireClass
        '
        Me.chkAvailableWithinEntireClass.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkAvailableWithinEntireClass.AutoSize = True
        Me.chkAvailableWithinEntireClass.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkAvailableWithinEntireClass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAvailableWithinEntireClass.ForeColor = System.Drawing.Color.Black
        Me.chkAvailableWithinEntireClass.Location = New System.Drawing.Point(6, 23)
        Me.chkAvailableWithinEntireClass.Name = "chkAvailableWithinEntireClass"
        Me.chkAvailableWithinEntireClass.Size = New System.Drawing.Size(347, 17)
        Me.chkAvailableWithinEntireClass.TabIndex = 14
        Me.chkAvailableWithinEntireClass.Text = "Action visible in Refactor Menu anywhere within the Persistent Class"
        Me.ToolTip1.SetToolTip(Me.chkAvailableWithinEntireClass, "With this on the Action to Update FieldsClass can be available anywhere within th" & _
                "e class, otherwise it will only be available when your cursor is on the Class de" & _
                "finition")
        Me.chkAvailableWithinEntireClass.UseVisualStyleBackColor = True
        '
        'EasyFields_Options
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadioButton1)
        Me.Name = "EasyFields_Options"
        Me.Size = New System.Drawing.Size(541, 480)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public Overrides ReadOnly Property Category() As String
        Get
            Return EasyFields_Options.GetCategory()
        End Get
    End Property

    Public Overrides ReadOnly Property PageName() As String
        Get
            Return EasyFields_Options.GetPageName()
        End Get
    End Property

    Public Shared Shadows Sub Show()
        DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath)
    End Sub
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkAvailableWithinEntireClass As System.Windows.Forms.CheckBox
    Friend WithEvents chkUpdateOnDocumentSave As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCommentFormat As System.Windows.Forms.TextBox
    Friend WithEvents chkUseComment As System.Windows.Forms.CheckBox
    Friend WithEvents chkReplaceClassOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseCollectionsFieldsClass As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As XPO_EasyFields.GrowLabel
    Friend WithEvents chkIncludeNonPersistent As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkIncludedInheritedMembers As System.Windows.Forms.CheckBox
    Friend WithEvents GrowLabel1 As XPO_EasyFields.GrowLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtVariableName As System.Windows.Forms.TextBox
    Friend WithEvents chkIncludeFieldConstants As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    Public Shared ReadOnly Property FullPath() As String
        Get
            Return GetCategory() + "\" + GetPageName()
        End Get
    End Property

End Class
