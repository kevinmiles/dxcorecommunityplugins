using System;
using DevExpress.CodeRush.Core;

namespace CR_UpperCaseMenuControl
{
    partial class OptUpperCaseMenuItems
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public OptUpperCaseMenuItems()
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
			this.label1 = new System.Windows.Forms.Label();
			this.rbnALLCAPS = new System.Windows.Forms.RadioButton();
			this.chkNormalCase = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(271, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "I want my Visual Studio 2012 RC main menu text to be:";
			// 
			// rbnALLCAPS
			// 
			this.rbnALLCAPS.AutoSize = true;
			this.rbnALLCAPS.Checked = true;
			this.rbnALLCAPS.Location = new System.Drawing.Point(29, 41);
			this.rbnALLCAPS.Name = "rbnALLCAPS";
			this.rbnALLCAPS.Size = new System.Drawing.Size(205, 17);
			this.rbnALLCAPS.TabIndex = 2;
			this.rbnALLCAPS.TabStop = true;
			this.rbnALLCAPS.Text = "ALL CAPS:      FILE     EDIT     VIEW...";
			this.rbnALLCAPS.UseVisualStyleBackColor = true;
			// 
			// chkNormalCase
			// 
			this.chkNormalCase.AutoSize = true;
			this.chkNormalCase.Location = new System.Drawing.Point(29, 64);
			this.chkNormalCase.Name = "chkNormalCase";
			this.chkNormalCase.Size = new System.Drawing.Size(189, 17);
			this.chkNormalCase.TabIndex = 3;
			this.chkNormalCase.Text = "Title Case:     File     Edit     View...";
			this.chkNormalCase.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 100);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(299, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Changes will take place the next time you start Visual Studio.";
			// 
			// OptUpperCaseMenuItems
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.chkNormalCase);
			this.Controls.Add(this.rbnALLCAPS);
			this.Controls.Add(this.label1);
			this.Name = "OptUpperCaseMenuItems";
			this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.OptUpperCaseMenuItems_CommitChanges);
			this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.OptUpperCaseMenuItems_PreparePage);
			this.RestoreDefaults += new DevExpress.CodeRush.Core.OptionsPage.RestoreDefaultsEventHandler(this.OptUpperCaseMenuItems_RestoreDefaults);
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
                return OptUpperCaseMenuItems.GetCategory();
            }
        }
        ///
        /// Returns the page name of this options page.
        ///
        public override string PageName
        {
            get
            {
                return OptUpperCaseMenuItems.GetPageName();
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

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbnALLCAPS;
        private System.Windows.Forms.RadioButton chkNormalCase;
				private System.Windows.Forms.Label label2;
    }
}