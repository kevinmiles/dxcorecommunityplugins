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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSmartEnterSplitString
            // 
            this.cbSmartEnterSplitString.Location = new System.Drawing.Point(16, 13);
            this.cbSmartEnterSplitString.Name = "cbSmartEnterSplitString";
            this.cbSmartEnterSplitString.Size = new System.Drawing.Size(473, 24);
            this.cbSmartEnterSplitString.TabIndex = 0;
            this.cbSmartEnterSplitString.Text = "Enter breaks string into two lines with \'Split String\' refactoring";
            this.cbSmartEnterSplitString.UseVisualStyleBackColor = true;
            // 
            // SplitStringOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.cbSmartEnterSplitString);
            this.Name = "SplitStringOptions";
            this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.SplitStringOptions_PreparePage);
            this.RestoreDefaults += new DevExpress.CodeRush.Core.OptionsPage.RestoreDefaultsEventHandler(this.SplitStringOptions_RestoreDefaults);
            this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.SplitStringOptions_CommitChanges);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

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
    }
}