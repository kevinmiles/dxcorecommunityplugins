using System;
using DevExpress.CodeRush.Core;

namespace CR_SmartQuotes
{
    partial class SmartQuoteOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public SmartQuoteOptions()
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
            this.gbSmartDoubleQuotes = new System.Windows.Forms.GroupBox();
            this.cbDoubleQuotesIgnoreClosingQuote = new System.Windows.Forms.CheckBox();
            this.cbDoubleQuotesEasyDelete = new System.Windows.Forms.CheckBox();
            this.cbDoubleQuotesUseTextFields = new System.Windows.Forms.CheckBox();
            this.cbDoubleQuotesAutoComplete = new System.Windows.Forms.CheckBox();
            this.cbSmartDoubleQuotes = new System.Windows.Forms.CheckBox();
            this.gbSmartQuotes = new System.Windows.Forms.GroupBox();
            this.cbQuotesIgnoreClosingQuote = new System.Windows.Forms.CheckBox();
            this.cbQuotesUseTextFields = new System.Windows.Forms.CheckBox();
            this.cbQuotesEasyDelete = new System.Windows.Forms.CheckBox();
            this.cbQuotesAutoComplete = new System.Windows.Forms.CheckBox();
            this.cbSmartQuotes = new System.Windows.Forms.CheckBox();
            this.gbSmartDoubleQuotes.SuspendLayout();
            this.gbSmartQuotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // gbSmartDoubleQuotes
            // 
            this.gbSmartDoubleQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSmartDoubleQuotes.Controls.Add(this.cbDoubleQuotesIgnoreClosingQuote);
            this.gbSmartDoubleQuotes.Controls.Add(this.cbDoubleQuotesEasyDelete);
            this.gbSmartDoubleQuotes.Controls.Add(this.cbDoubleQuotesUseTextFields);
            this.gbSmartDoubleQuotes.Controls.Add(this.cbDoubleQuotesAutoComplete);
            this.gbSmartDoubleQuotes.Controls.Add(this.cbSmartDoubleQuotes);
            this.gbSmartDoubleQuotes.Location = new System.Drawing.Point(13, 14);
            this.gbSmartDoubleQuotes.Name = "gbSmartDoubleQuotes";
            this.gbSmartDoubleQuotes.Size = new System.Drawing.Size(500, 137);
            this.gbSmartDoubleQuotes.TabIndex = 0;
            this.gbSmartDoubleQuotes.TabStop = false;
            // 
            // cbDoubleQuotesIgnoreClosingQuote
            // 
            this.cbDoubleQuotesIgnoreClosingQuote.Location = new System.Drawing.Point(20, 100);
            this.cbDoubleQuotesIgnoreClosingQuote.Name = "cbDoubleQuotesIgnoreClosingQuote";
            this.cbDoubleQuotesIgnoreClosingQuote.Size = new System.Drawing.Size(464, 24);
            this.cbDoubleQuotesIgnoreClosingQuote.TabIndex = 4;
            this.cbDoubleQuotesIgnoreClosingQuote.Text = "Ignore closing double quote typed in the end of string";
            this.cbDoubleQuotesIgnoreClosingQuote.UseVisualStyleBackColor = true;
            // 
            // cbDoubleQuotesEasyDelete
            // 
            this.cbDoubleQuotesEasyDelete.Location = new System.Drawing.Point(20, 75);
            this.cbDoubleQuotesEasyDelete.Name = "cbDoubleQuotesEasyDelete";
            this.cbDoubleQuotesEasyDelete.Size = new System.Drawing.Size(464, 24);
            this.cbDoubleQuotesEasyDelete.TabIndex = 3;
            this.cbDoubleQuotesEasyDelete.Text = "Easy-delete empty double quotes";
            this.cbDoubleQuotesEasyDelete.UseVisualStyleBackColor = true;
            // 
            // cbDoubleQuotesUseTextFields
            // 
            this.cbDoubleQuotesUseTextFields.Location = new System.Drawing.Point(32, 50);
            this.cbDoubleQuotesUseTextFields.Name = "cbDoubleQuotesUseTextFields";
            this.cbDoubleQuotesUseTextFields.Size = new System.Drawing.Size(452, 24);
            this.cbDoubleQuotesUseTextFields.TabIndex = 2;
            this.cbDoubleQuotesUseTextFields.Text = "Use text fields";
            this.cbDoubleQuotesUseTextFields.UseVisualStyleBackColor = true;
            // 
            // cbDoubleQuotesAutoComplete
            // 
            this.cbDoubleQuotesAutoComplete.Location = new System.Drawing.Point(20, 25);
            this.cbDoubleQuotesAutoComplete.Name = "cbDoubleQuotesAutoComplete";
            this.cbDoubleQuotesAutoComplete.Size = new System.Drawing.Size(464, 24);
            this.cbDoubleQuotesAutoComplete.TabIndex = 1;
            this.cbDoubleQuotesAutoComplete.Text = "Auto-complete double quotes";
            this.cbDoubleQuotesAutoComplete.UseVisualStyleBackColor = true;
            this.cbDoubleQuotesAutoComplete.CheckedChanged += new System.EventHandler(this.DoubleQuotesAutoCompleteCheckedChanged);
            // 
            // cbSmartDoubleQuotes
            // 
            this.cbSmartDoubleQuotes.Location = new System.Drawing.Point(10, -5);
            this.cbSmartDoubleQuotes.Name = "cbSmartDoubleQuotes";
            this.cbSmartDoubleQuotes.Size = new System.Drawing.Size(223, 24);
            this.cbSmartDoubleQuotes.TabIndex = 0;
            this.cbSmartDoubleQuotes.Text = "Use Smart Double Quotes";
            this.cbSmartDoubleQuotes.UseVisualStyleBackColor = true;
            this.cbSmartDoubleQuotes.CheckedChanged += new System.EventHandler(this.SmartDoubleQuotesCheckedChanged);
            // 
            // gbSmartQuotes
            // 
            this.gbSmartQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSmartQuotes.Controls.Add(this.cbQuotesIgnoreClosingQuote);
            this.gbSmartQuotes.Controls.Add(this.cbQuotesUseTextFields);
            this.gbSmartQuotes.Controls.Add(this.cbQuotesEasyDelete);
            this.gbSmartQuotes.Controls.Add(this.cbQuotesAutoComplete);
            this.gbSmartQuotes.Controls.Add(this.cbSmartQuotes);
            this.gbSmartQuotes.Location = new System.Drawing.Point(13, 170);
            this.gbSmartQuotes.Name = "gbSmartQuotes";
            this.gbSmartQuotes.Size = new System.Drawing.Size(500, 137);
            this.gbSmartQuotes.TabIndex = 1;
            this.gbSmartQuotes.TabStop = false;
            // 
            // cbQuotesIgnoreClosingQuote
            // 
            this.cbQuotesIgnoreClosingQuote.Location = new System.Drawing.Point(20, 100);
            this.cbQuotesIgnoreClosingQuote.Name = "cbQuotesIgnoreClosingQuote";
            this.cbQuotesIgnoreClosingQuote.Size = new System.Drawing.Size(464, 24);
            this.cbQuotesIgnoreClosingQuote.TabIndex = 4;
            this.cbQuotesIgnoreClosingQuote.Text = "Ignore closing quote typed in the end of char";
            this.cbQuotesIgnoreClosingQuote.UseVisualStyleBackColor = true;
            // 
            // cbQuotesUseTextFields
            // 
            this.cbQuotesUseTextFields.Location = new System.Drawing.Point(32, 50);
            this.cbQuotesUseTextFields.Name = "cbQuotesUseTextFields";
            this.cbQuotesUseTextFields.Size = new System.Drawing.Size(452, 24);
            this.cbQuotesUseTextFields.TabIndex = 2;
            this.cbQuotesUseTextFields.Text = "Use text fields";
            this.cbQuotesUseTextFields.UseVisualStyleBackColor = true;
            // 
            // cbQuotesEasyDelete
            // 
            this.cbQuotesEasyDelete.Location = new System.Drawing.Point(20, 75);
            this.cbQuotesEasyDelete.Name = "cbQuotesEasyDelete";
            this.cbQuotesEasyDelete.Size = new System.Drawing.Size(464, 24);
            this.cbQuotesEasyDelete.TabIndex = 3;
            this.cbQuotesEasyDelete.Text = "Easy-delete empty quotes";
            this.cbQuotesEasyDelete.UseVisualStyleBackColor = true;
            // 
            // cbQuotesAutoComplete
            // 
            this.cbQuotesAutoComplete.Location = new System.Drawing.Point(20, 25);
            this.cbQuotesAutoComplete.Name = "cbQuotesAutoComplete";
            this.cbQuotesAutoComplete.Size = new System.Drawing.Size(464, 24);
            this.cbQuotesAutoComplete.TabIndex = 1;
            this.cbQuotesAutoComplete.Text = "Auto-complete quotes";
            this.cbQuotesAutoComplete.UseVisualStyleBackColor = true;
            this.cbQuotesAutoComplete.CheckedChanged += new System.EventHandler(this.QuotesAutoCompleteCheckedChanged);
            // 
            // cbSmartQuotes
            // 
            this.cbSmartQuotes.Location = new System.Drawing.Point(10, -5);
            this.cbSmartQuotes.Name = "cbSmartQuotes";
            this.cbSmartQuotes.Size = new System.Drawing.Size(223, 24);
            this.cbSmartQuotes.TabIndex = 0;
            this.cbSmartQuotes.Text = "Use Smart Quotes";
            this.cbSmartQuotes.UseVisualStyleBackColor = true;
            this.cbSmartQuotes.CheckedChanged += new System.EventHandler(this.SmartQuotesCheckedChanged);
            // 
            // SmartQuoteOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.gbSmartQuotes);
            this.Controls.Add(this.gbSmartDoubleQuotes);
            this.Name = "SmartQuoteOptions";
            this.Title = "Auto Complete - Quotes & Double Quotes";
            this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.SmartQuoteOptionsCommitChanges);
            this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.SmartQuoteOptionsPreparePage);
            this.RestoreDefaults += new DevExpress.CodeRush.Core.OptionsPage.RestoreDefaultsEventHandler(this.SmartQuoteOptionsRestoreDefaults);
            this.gbSmartDoubleQuotes.ResumeLayout(false);
            this.gbSmartQuotes.ResumeLayout(false);
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
                return SmartQuoteOptions.GetCategory();
            }
        }
        ///
        /// Returns the page name of this options page.
        ///
        public override string PageName
        {
            get
            {
                return SmartQuoteOptions.GetPageName();
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

        private System.Windows.Forms.GroupBox gbSmartDoubleQuotes;
        private System.Windows.Forms.CheckBox cbDoubleQuotesAutoComplete;
        private System.Windows.Forms.CheckBox cbSmartDoubleQuotes;
        private System.Windows.Forms.CheckBox cbDoubleQuotesEasyDelete;
        private System.Windows.Forms.CheckBox cbDoubleQuotesUseTextFields;
        private System.Windows.Forms.CheckBox cbDoubleQuotesIgnoreClosingQuote;
        private System.Windows.Forms.GroupBox gbSmartQuotes;
        private System.Windows.Forms.CheckBox cbQuotesIgnoreClosingQuote;
        private System.Windows.Forms.CheckBox cbQuotesUseTextFields;
        private System.Windows.Forms.CheckBox cbQuotesEasyDelete;
        private System.Windows.Forms.CheckBox cbQuotesAutoComplete;
        private System.Windows.Forms.CheckBox cbSmartQuotes;
    }
}