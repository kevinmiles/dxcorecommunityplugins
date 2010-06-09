using System;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_DrawLinesBetweenMethods
{
    public partial class OptDrawLinesBetweenMethods
    {
        private CheckBox _fullWidthChk;
        private Label label2;
        private ComboBox _lineWidthLst;
        private Label label3;
        private CheckBox _drawLineAtEndChk;
        private CheckBox _drawShadowChk;
        private Button _lineColorBtn;
        private System.ComponentModel.Container components;

        private void InitializeComponent()
        {
            this._fullWidthChk = new System.Windows.Forms.CheckBox();
            this._lineWidthLst = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._drawLineAtEndChk = new System.Windows.Forms.CheckBox();
            this._drawShadowChk = new System.Windows.Forms.CheckBox();
            this._lineColorBtn = new System.Windows.Forms.Button();
            this._mainPanel = new System.Windows.Forms.Panel();
            this._shadowHeightNUD = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this._drawLineAtStartChk = new System.Windows.Forms.CheckBox();
            this._lineSpaceNUD = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this._enableOnMemberCheckList = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this._enabledChk = new System.Windows.Forms.CheckBox();
            this._mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._shadowHeightNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._lineSpaceNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // _fullWidthChk
            // 
            this._fullWidthChk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._fullWidthChk.Location = new System.Drawing.Point(3, 4);
            this._fullWidthChk.Name = "_fullWidthChk";
            this._fullWidthChk.Size = new System.Drawing.Size(184, 24);
            this._fullWidthChk.TabIndex = 0;
            this._fullWidthChk.Text = "Draw line across full width of page";
            this._fullWidthChk.Visible = false;
            // 
            // _lineWidthLst
            // 
            this._lineWidthLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lineWidthLst.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this._lineWidthLst.Location = new System.Drawing.Point(81, 30);
            this._lineWidthLst.Name = "_lineWidthLst";
            this._lineWidthLst.Size = new System.Drawing.Size(121, 21);
            this._lineWidthLst.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Location = new System.Drawing.Point(3, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Line width:";
            // 
            // label3
            // 
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Location = new System.Drawing.Point(3, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Line color:";
            // 
            // _drawLineAtEndChk
            // 
            this._drawLineAtEndChk.Location = new System.Drawing.Point(3, 186);
            this._drawLineAtEndChk.Name = "_drawLineAtEndChk";
            this._drawLineAtEndChk.Size = new System.Drawing.Size(193, 24);
            this._drawLineAtEndChk.TabIndex = 3;
            this._drawLineAtEndChk.Text = "Draw line at end of method";
            // 
            // _drawShadowChk
            // 
            this._drawShadowChk.Location = new System.Drawing.Point(3, 113);
            this._drawShadowChk.Name = "_drawShadowChk";
            this._drawShadowChk.Size = new System.Drawing.Size(104, 24);
            this._drawShadowChk.TabIndex = 3;
            this._drawShadowChk.Text = "Shadow";
            this._drawShadowChk.Visible = false;
            this._drawShadowChk.CheckedChanged += new System.EventHandler(this._drawShadowChk_CheckedChanged);
            // 
            // _lineColorBtn
            // 
            this._lineColorBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._lineColorBtn.Location = new System.Drawing.Point(81, 84);
            this._lineColorBtn.Name = "_lineColorBtn";
            this._lineColorBtn.Size = new System.Drawing.Size(121, 23);
            this._lineColorBtn.TabIndex = 4;
            this._lineColorBtn.UseVisualStyleBackColor = true;
            this._lineColorBtn.Click += new System.EventHandler(this._lineColorBtn_Click);
            // 
            // _mainPanel
            // 
            this._mainPanel.Controls.Add(this._shadowHeightNUD);
            this._mainPanel.Controls.Add(this.label6);
            this._mainPanel.Controls.Add(this._drawLineAtStartChk);
            this._mainPanel.Controls.Add(this._lineSpaceNUD);
            this._mainPanel.Controls.Add(this.label4);
            this._mainPanel.Controls.Add(this._enableOnMemberCheckList);
            this._mainPanel.Controls.Add(this._drawLineAtEndChk);
            this._mainPanel.Controls.Add(this._lineColorBtn);
            this._mainPanel.Controls.Add(this._drawShadowChk);
            this._mainPanel.Controls.Add(this.label3);
            this._mainPanel.Controls.Add(this.label5);
            this._mainPanel.Controls.Add(this.label2);
            this._mainPanel.Controls.Add(this._lineWidthLst);
            this._mainPanel.Controls.Add(this._fullWidthChk);
            this._mainPanel.Location = new System.Drawing.Point(13, 27);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Size = new System.Drawing.Size(333, 351);
            this._mainPanel.TabIndex = 5;
            // 
            // _shadowHeightNUD
            // 
            this._shadowHeightNUD.Location = new System.Drawing.Point(75, 134);
            this._shadowHeightNUD.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this._shadowHeightNUD.Name = "_shadowHeightNUD";
            this._shadowHeightNUD.Size = new System.Drawing.Size(37, 20);
            this._shadowHeightNUD.TabIndex = 12;
            this._shadowHeightNUD.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Height:";
            this.label6.Visible = false;
            // 
            // _drawLineAtStartChk
            // 
            this._drawLineAtStartChk.Location = new System.Drawing.Point(3, 160);
            this._drawLineAtStartChk.Name = "_drawLineAtStartChk";
            this._drawLineAtStartChk.Size = new System.Drawing.Size(193, 24);
            this._drawLineAtStartChk.TabIndex = 10;
            this._drawLineAtStartChk.Text = "Draw line at start of method";
            // 
            // _lineSpaceNUD
            // 
            this._lineSpaceNUD.Location = new System.Drawing.Point(81, 58);
            this._lineSpaceNUD.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this._lineSpaceNUD.Name = "_lineSpaceNUD";
            this._lineSpaceNUD.Size = new System.Drawing.Size(37, 20);
            this._lineSpaceNUD.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Enable on members...";
            // 
            // _enableOnMemberCheckList
            // 
            this._enableOnMemberCheckList.FormattingEnabled = true;
            this._enableOnMemberCheckList.Items.AddRange(new object[] {
            "Class",
            "Property",
            "Method",
            "Enum"});
            this._enableOnMemberCheckList.Location = new System.Drawing.Point(3, 241);
            this._enableOnMemberCheckList.Name = "_enableOnMemberCheckList";
            this._enableOnMemberCheckList.Size = new System.Drawing.Size(193, 64);
            this._enableOnMemberCheckList.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Location = new System.Drawing.Point(3, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "Leave space:";
            // 
            // _enabledChk
            // 
            this._enabledChk.AutoSize = true;
            this._enabledChk.Checked = true;
            this._enabledChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this._enabledChk.Location = new System.Drawing.Point(4, 4);
            this._enabledChk.Name = "_enabledChk";
            this._enabledChk.Size = new System.Drawing.Size(65, 17);
            this._enabledChk.TabIndex = 6;
            this._enabledChk.Text = "Enabled";
            this._enabledChk.UseVisualStyleBackColor = true;
            // 
            // OptDrawLinesBetweenMethods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this._enabledChk);
            this.Controls.Add(this._mainPanel);
            this.Name = "OptDrawLinesBetweenMethods";
            this.Size = new System.Drawing.Size(661, 617);
            this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.OptDrawLinesBetweenMethods_CommitChanges);
            this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.OptDrawLinesBetweenMethods_PreparePage);
            this.Load += new System.EventHandler(this.OptDrawLinesBetweenMethods_Load);
            this._mainPanel.ResumeLayout(false);
            this._mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._shadowHeightNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._lineSpaceNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region CodeRush generated code
        protected override void Initialize()
        {
            base.Initialize();

            //
            // TODO: Add your initialization code here.
            //
        }

        public static string GetCategory()
        {
            return @"Editor\Painting";
        }

        public static string GetPageName()
        {
            return @"Draw Lines Between Methods";
        }

        public static DecoupledStorage Storage
        {
            get
            {
                return CodeRush.Options.GetStorage(GetCategory(), GetPageName());
            }
        }

        public override string Category
        {
            get
            {
                return OptDrawLinesBetweenMethods.GetCategory();
            }
        }

        public override string PageName
        {
            get
            {
                return OptDrawLinesBetweenMethods.GetPageName();
            }
        }

        public new static void Show()
        {
            CodeRush.Command.Execute("Options", FullPath);
        }

        private Panel _mainPanel;
        private CheckBox _enabledChk;
        private Label label4;
        private CheckedListBox _enableOnMemberCheckList;
        private NumericUpDown _lineSpaceNUD;
        private CheckBox _drawLineAtStartChk;
        private NumericUpDown _shadowHeightNUD;
        private Label label6;
        private Label label5;

        public static string FullPath
        {
            get
            {
                return GetCategory() + "\\" + GetPageName();
            }
        }
        #endregion

    }
}
