using System;
using DevExpress.CodeRush.Core;

namespace CR_SmartGenerics
{
    partial class SmartGenericsOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public SmartGenericsOptions()
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
            this.gbSmartGenerics = new System.Windows.Forms.GroupBox();
            this.cbGenericsIgnoreClosingOperator = new System.Windows.Forms.CheckBox();
            this.cbGenericsEasyDelete = new System.Windows.Forms.CheckBox();
            this.cbGenericsUseTextFields = new System.Windows.Forms.CheckBox();
            this.cbGenericsAutoComplete = new System.Windows.Forms.CheckBox();
            this.cbSmartGenerics = new System.Windows.Forms.CheckBox();
            this.cbGenericsAddSpaces = new System.Windows.Forms.CheckBox();
            this.gbSmartGenerics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // gbSmartGenerics
            // 
            this.gbSmartGenerics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSmartGenerics.Controls.Add(this.cbGenericsAddSpaces);
            this.gbSmartGenerics.Controls.Add(this.cbGenericsIgnoreClosingOperator);
            this.gbSmartGenerics.Controls.Add(this.cbGenericsEasyDelete);
            this.gbSmartGenerics.Controls.Add(this.cbGenericsUseTextFields);
            this.gbSmartGenerics.Controls.Add(this.cbGenericsAutoComplete);
            this.gbSmartGenerics.Controls.Add(this.cbSmartGenerics);
            this.gbSmartGenerics.Location = new System.Drawing.Point(13, 14);
            this.gbSmartGenerics.Name = "gbSmartGenerics";
            this.gbSmartGenerics.Size = new System.Drawing.Size(500, 162);
            this.gbSmartGenerics.TabIndex = 1;
            this.gbSmartGenerics.TabStop = false;
            // 
            // cbGenericsIgnoreClosingOperator
            // 
            this.cbGenericsIgnoreClosingOperator.Location = new System.Drawing.Point(20, 125);
            this.cbGenericsIgnoreClosingOperator.Name = "cbGenericsIgnoreClosingOperator";
            this.cbGenericsIgnoreClosingOperator.Size = new System.Drawing.Size(464, 24);
            this.cbGenericsIgnoreClosingOperator.TabIndex = 4;
            this.cbGenericsIgnoreClosingOperator.Text = "Ignore closing generic operator typed in the end of list of generic parameters";
            this.cbGenericsIgnoreClosingOperator.UseVisualStyleBackColor = true;
            // 
            // cbGenericsEasyDelete
            // 
            this.cbGenericsEasyDelete.Location = new System.Drawing.Point(20, 100);
            this.cbGenericsEasyDelete.Name = "cbGenericsEasyDelete";
            this.cbGenericsEasyDelete.Size = new System.Drawing.Size(464, 24);
            this.cbGenericsEasyDelete.TabIndex = 3;
            this.cbGenericsEasyDelete.Text = "Easy-delete empty generic operators";
            this.cbGenericsEasyDelete.UseVisualStyleBackColor = true;
            // 
            // cbGenericsUseTextFields
            // 
            this.cbGenericsUseTextFields.Location = new System.Drawing.Point(32, 50);
            this.cbGenericsUseTextFields.Name = "cbGenericsUseTextFields";
            this.cbGenericsUseTextFields.Size = new System.Drawing.Size(452, 24);
            this.cbGenericsUseTextFields.TabIndex = 2;
            this.cbGenericsUseTextFields.Text = "Use text fields";
            this.cbGenericsUseTextFields.UseVisualStyleBackColor = true;
            // 
            // cbGenericsAutoComplete
            // 
            this.cbGenericsAutoComplete.Location = new System.Drawing.Point(20, 25);
            this.cbGenericsAutoComplete.Name = "cbGenericsAutoComplete";
            this.cbGenericsAutoComplete.Size = new System.Drawing.Size(464, 24);
            this.cbGenericsAutoComplete.TabIndex = 1;
            this.cbGenericsAutoComplete.Text = "Auto-complete generic operators";
            this.cbGenericsAutoComplete.UseVisualStyleBackColor = true;
            this.cbGenericsAutoComplete.CheckedChanged += new System.EventHandler(this.GenericsAutoComplete_CheckedChanged);
            // 
            // cbSmartGenerics
            // 
            this.cbSmartGenerics.Location = new System.Drawing.Point(10, -5);
            this.cbSmartGenerics.Name = "cbSmartGenerics";
            this.cbSmartGenerics.Size = new System.Drawing.Size(223, 24);
            this.cbSmartGenerics.TabIndex = 0;
            this.cbSmartGenerics.Text = "Use Smart Generic Operators (c# only)";
            this.cbSmartGenerics.UseVisualStyleBackColor = true;
            this.cbSmartGenerics.CheckedChanged += new System.EventHandler(this.SmartGenerics_CheckedChanged);
            // 
            // cbGenericsAddSpaces
            // 
            this.cbGenericsAddSpaces.AutoSize = true;
            this.cbGenericsAddSpaces.Location = new System.Drawing.Point(32, 75);
            this.cbGenericsAddSpaces.Name = "cbGenericsAddSpaces";
            this.cbGenericsAddSpaces.Size = new System.Drawing.Size(220, 17);
            this.cbGenericsAddSpaces.TabIndex = 5;
            this.cbGenericsAddSpaces.Text = "Add spaces inside generic operators < | >";
            this.cbGenericsAddSpaces.UseVisualStyleBackColor = true;
            // 
            // SmartGenericsOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.gbSmartGenerics);
            this.Name = "SmartGenericsOptions";
            this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.SmartGenericsOptions_PreparePage);
            this.RestoreDefaults += new DevExpress.CodeRush.Core.OptionsPage.RestoreDefaultsEventHandler(this.SmartGenericsOptions_RestoreDefaults);
            this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.SmartGenericsOptions_CommitChanges);
            this.gbSmartGenerics.ResumeLayout(false);
            this.gbSmartGenerics.PerformLayout();
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
                return SmartGenericsOptions.GetCategory();
            }
        }
        ///
        /// Returns the page name of this options page.
        ///
        public override string PageName
        {
            get
            {
                return SmartGenericsOptions.GetPageName();
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

        private System.Windows.Forms.GroupBox gbSmartGenerics;
        private System.Windows.Forms.CheckBox cbGenericsIgnoreClosingOperator;
        private System.Windows.Forms.CheckBox cbGenericsEasyDelete;
        private System.Windows.Forms.CheckBox cbGenericsUseTextFields;
        private System.Windows.Forms.CheckBox cbGenericsAutoComplete;
        private System.Windows.Forms.CheckBox cbSmartGenerics;
        private System.Windows.Forms.CheckBox cbGenericsAddSpaces;
    }
}