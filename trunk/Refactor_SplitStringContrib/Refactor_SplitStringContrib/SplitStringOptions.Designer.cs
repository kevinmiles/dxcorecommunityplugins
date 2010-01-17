using System;
using DevExpress.CodeRush.Core;

namespace Refactor_SplitStringContrib
{
    partial class SplitStringOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public SplitStringOptions()
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbSmartEnterSplitString = new System.Windows.Forms.CheckBox();
            this.rbLeaveOperator = new System.Windows.Forms.RadioButton();
            this.rbMoveOperatorToNextLine = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbUseAmpersand = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSmartEnterSplitString
            // 
            this.cbSmartEnterSplitString.Location = new System.Drawing.Point(6, -5);
            this.cbSmartEnterSplitString.Name = "cbSmartEnterSplitString";
            this.cbSmartEnterSplitString.Size = new System.Drawing.Size(311, 24);
            this.cbSmartEnterSplitString.TabIndex = 0;
            this.cbSmartEnterSplitString.Text = "Enter breaks string into two lines with \'Split String\' refactoring";
            this.cbSmartEnterSplitString.UseVisualStyleBackColor = true;
            this.cbSmartEnterSplitString.CheckedChanged += new System.EventHandler(this.SmartEnterSplitString_CheckedChanged);
            // 
            // rbLeaveOperator
            // 
            this.rbLeaveOperator.AutoSize = true;
            this.rbLeaveOperator.Location = new System.Drawing.Point(9, 38);
            this.rbLeaveOperator.Name = "rbLeaveOperator";
            this.rbLeaveOperator.Size = new System.Drawing.Size(123, 17);
            this.rbLeaveOperator.TabIndex = 1;
            this.rbLeaveOperator.TabStop = true;
            this.rbLeaveOperator.Text = "at the end of first line";
            this.rbLeaveOperator.UseVisualStyleBackColor = true;
            // 
            // rbMoveOperatorToNextLine
            // 
            this.rbMoveOperatorToNextLine.AutoSize = true;
            this.rbMoveOperatorToNextLine.Location = new System.Drawing.Point(9, 61);
            this.rbMoveOperatorToNextLine.Name = "rbMoveOperatorToNextLine";
            this.rbMoveOperatorToNextLine.Size = new System.Drawing.Size(170, 17);
            this.rbMoveOperatorToNextLine.TabIndex = 2;
            this.rbMoveOperatorToNextLine.TabStop = true;
            this.rbMoveOperatorToNextLine.Text = "at the beginning of second line";
            this.rbMoveOperatorToNextLine.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Place concatenation operator:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbSmartEnterSplitString);
            this.groupBox1.Controls.Add(this.rbMoveOperatorToNextLine);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rbLeaveOperator);
            this.groupBox1.Location = new System.Drawing.Point(13, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 86);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // cbUseAmpersand
            // 
            this.cbUseAmpersand.AutoSize = true;
            this.cbUseAmpersand.Location = new System.Drawing.Point(19, 106);
            this.cbUseAmpersand.Name = "cbUseAmpersand";
            this.cbUseAmpersand.Size = new System.Drawing.Size(86, 17);
            this.cbUseAmpersand.TabIndex = 5;
            this.cbUseAmpersand.Text = "Use \'&&\' in VB";
            this.cbUseAmpersand.UseVisualStyleBackColor = true;
            // 
            // SplitStringOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.cbUseAmpersand);
            this.Controls.Add(this.groupBox1);
            this.Name = "SplitStringOptions";
            this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.SplitStringOptions_PreparePage);
            this.RestoreDefaults += new DevExpress.CodeRush.Core.OptionsPage.RestoreDefaultsEventHandler(this.SplitStringOptions_RestoreDefaults);
            this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.SplitStringOptions_CommitChanges);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        ///
        /// Gets a DecoupledStorage instance for this options page.
        ///
        public static DecoupledStorage Storage
        {
            get
            {
                return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName());
            }
        }
        ///
        /// Returns the category of this options page.
        ///
        public override string Category
        {
            get
            {
                return SplitStringOptions.GetCategory();
            }
        }
        ///
        /// Returns the page name of this options page.
        ///
        public override string PageName
        {
            get
            {
                return SplitStringOptions.GetPageName();
            }
        }
        ///
        /// Returns the full path (Category + PageName) of this options page.
        ///
        public static string FullPath
        {
            get
            {
                return GetCategory() + "\\" + GetPageName();
            }
        }

        ///
        /// Displays the DXCore options dialog and selects this page.
        ///
        public new static void Show()
        {
            DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath);
        }

        private System.Windows.Forms.CheckBox cbSmartEnterSplitString;
        private System.Windows.Forms.RadioButton rbLeaveOperator;
        private System.Windows.Forms.RadioButton rbMoveOperatorToNextLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbUseAmpersand;
    }
}