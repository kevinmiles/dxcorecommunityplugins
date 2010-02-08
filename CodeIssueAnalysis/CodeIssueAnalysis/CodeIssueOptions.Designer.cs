using System;
using DevExpress.CodeRush.Core;

namespace CodeIssueAnalysis
{
    partial class CodeIssueOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CodeIssueOptions()
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
            this.listIncludeFiles = new System.Windows.Forms.ListBox();
            this.listExcludeFiles = new System.Windows.Forms.ListBox();
            this.txtInclude = new System.Windows.Forms.TextBox();
            this.btnAddExclusion = new System.Windows.Forms.Button();
            this.btnAddInclusion = new System.Windows.Forms.Button();
            this.btnRemoveInclusion = new System.Windows.Forms.Button();
            this.txtExclude = new System.Windows.Forms.TextBox();
            this.btnRemoveExclusion = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listExcludeContent = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRemoveContentExclusion = new System.Windows.Forms.Button();
            this.txtExcludeContent = new System.Windows.Forms.TextBox();
            this.btnAddContentExclusion = new System.Windows.Forms.Button();
            this.listIncludeContent = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRemoveContentInclusion = new System.Windows.Forms.Button();
            this.btnAddContentInclusion = new System.Windows.Forms.Button();
            this.txtIncludeContent = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // listIncludeFiles
            // 
            this.listIncludeFiles.FormattingEnabled = true;
            this.listIncludeFiles.Location = new System.Drawing.Point(3, 65);
            this.listIncludeFiles.Name = "listIncludeFiles";
            this.listIncludeFiles.Size = new System.Drawing.Size(524, 69);
            this.listIncludeFiles.TabIndex = 0;
            // 
            // listExcludeFiles
            // 
            this.listExcludeFiles.FormattingEnabled = true;
            this.listExcludeFiles.Location = new System.Drawing.Point(3, 294);
            this.listExcludeFiles.Name = "listExcludeFiles";
            this.listExcludeFiles.Size = new System.Drawing.Size(524, 69);
            this.listExcludeFiles.TabIndex = 1;
            // 
            // txtInclude
            // 
            this.txtInclude.Location = new System.Drawing.Point(3, 39);
            this.txtInclude.Name = "txtInclude";
            this.txtInclude.Size = new System.Drawing.Size(389, 20);
            this.txtInclude.TabIndex = 2;
            // 
            // btnAddExclusion
            // 
            this.btnAddExclusion.Location = new System.Drawing.Point(397, 268);
            this.btnAddExclusion.Name = "btnAddExclusion";
            this.btnAddExclusion.Size = new System.Drawing.Size(62, 21);
            this.btnAddExclusion.TabIndex = 3;
            this.btnAddExclusion.Text = "Add";
            this.btnAddExclusion.UseVisualStyleBackColor = true;
            this.btnAddExclusion.Click += new System.EventHandler(this.btnAddExclusion_Click);
            // 
            // btnAddInclusion
            // 
            this.btnAddInclusion.Location = new System.Drawing.Point(397, 38);
            this.btnAddInclusion.Name = "btnAddInclusion";
            this.btnAddInclusion.Size = new System.Drawing.Size(62, 21);
            this.btnAddInclusion.TabIndex = 4;
            this.btnAddInclusion.Text = "Add";
            this.btnAddInclusion.UseVisualStyleBackColor = true;
            this.btnAddInclusion.Click += new System.EventHandler(this.btnAddInclusion_Click);
            // 
            // btnRemoveInclusion
            // 
            this.btnRemoveInclusion.Location = new System.Drawing.Point(465, 38);
            this.btnRemoveInclusion.Name = "btnRemoveInclusion";
            this.btnRemoveInclusion.Size = new System.Drawing.Size(62, 21);
            this.btnRemoveInclusion.TabIndex = 5;
            this.btnRemoveInclusion.Text = "Remove";
            this.btnRemoveInclusion.UseVisualStyleBackColor = true;
            this.btnRemoveInclusion.Click += new System.EventHandler(this.btnRemoveInclusion_Click);
            // 
            // txtExclude
            // 
            this.txtExclude.Location = new System.Drawing.Point(3, 268);
            this.txtExclude.Name = "txtExclude";
            this.txtExclude.Size = new System.Drawing.Size(389, 20);
            this.txtExclude.TabIndex = 6;
            // 
            // btnRemoveExclusion
            // 
            this.btnRemoveExclusion.Location = new System.Drawing.Point(465, 268);
            this.btnRemoveExclusion.Name = "btnRemoveExclusion";
            this.btnRemoveExclusion.Size = new System.Drawing.Size(62, 21);
            this.btnRemoveExclusion.TabIndex = 7;
            this.btnRemoveExclusion.Text = "Remove";
            this.btnRemoveExclusion.UseVisualStyleBackColor = true;
            this.btnRemoveExclusion.Click += new System.EventHandler(this.btnRemoveExclusion_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Include Filename Pattern:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Exclude Filename Pattern:";
            // 
            // listExcludeContent
            // 
            this.listExcludeContent.FormattingEnabled = true;
            this.listExcludeContent.Location = new System.Drawing.Point(3, 408);
            this.listExcludeContent.Name = "listExcludeContent";
            this.listExcludeContent.Size = new System.Drawing.Size(524, 69);
            this.listExcludeContent.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(63, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Note: All filters are regular expressions. ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 366);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Exclude Files Containing Pattern:";
            // 
            // btnRemoveContentExclusion
            // 
            this.btnRemoveContentExclusion.Location = new System.Drawing.Point(465, 382);
            this.btnRemoveContentExclusion.Name = "btnRemoveContentExclusion";
            this.btnRemoveContentExclusion.Size = new System.Drawing.Size(62, 21);
            this.btnRemoveContentExclusion.TabIndex = 15;
            this.btnRemoveContentExclusion.Text = "Remove";
            this.btnRemoveContentExclusion.UseVisualStyleBackColor = true;
            this.btnRemoveContentExclusion.Click += new System.EventHandler(this.btnRemoveContentExclusion_Click);
            // 
            // txtExcludeContent
            // 
            this.txtExcludeContent.Location = new System.Drawing.Point(3, 382);
            this.txtExcludeContent.Name = "txtExcludeContent";
            this.txtExcludeContent.Size = new System.Drawing.Size(389, 20);
            this.txtExcludeContent.TabIndex = 14;
            // 
            // btnAddContentExclusion
            // 
            this.btnAddContentExclusion.Location = new System.Drawing.Point(398, 382);
            this.btnAddContentExclusion.Name = "btnAddContentExclusion";
            this.btnAddContentExclusion.Size = new System.Drawing.Size(62, 21);
            this.btnAddContentExclusion.TabIndex = 13;
            this.btnAddContentExclusion.Text = "Add";
            this.btnAddContentExclusion.UseVisualStyleBackColor = true;
            this.btnAddContentExclusion.Click += new System.EventHandler(this.btnAddContentExclusion_Click);
            // 
            // listIncludeContent
            // 
            this.listIncludeContent.FormattingEnabled = true;
            this.listIncludeContent.Location = new System.Drawing.Point(3, 179);
            this.listIncludeContent.Name = "listIncludeContent";
            this.listIncludeContent.Size = new System.Drawing.Size(524, 69);
            this.listIncludeContent.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Include Files Containing Pattern:";
            // 
            // btnRemoveContentInclusion
            // 
            this.btnRemoveContentInclusion.Location = new System.Drawing.Point(465, 152);
            this.btnRemoveContentInclusion.Name = "btnRemoveContentInclusion";
            this.btnRemoveContentInclusion.Size = new System.Drawing.Size(62, 21);
            this.btnRemoveContentInclusion.TabIndex = 20;
            this.btnRemoveContentInclusion.Text = "Remove";
            this.btnRemoveContentInclusion.UseVisualStyleBackColor = true;
            this.btnRemoveContentInclusion.Click += new System.EventHandler(this.btnRemoveContentInclusion_Click);
            // 
            // btnAddContentInclusion
            // 
            this.btnAddContentInclusion.Location = new System.Drawing.Point(397, 152);
            this.btnAddContentInclusion.Name = "btnAddContentInclusion";
            this.btnAddContentInclusion.Size = new System.Drawing.Size(62, 21);
            this.btnAddContentInclusion.TabIndex = 19;
            this.btnAddContentInclusion.Text = "Add";
            this.btnAddContentInclusion.UseVisualStyleBackColor = true;
            this.btnAddContentInclusion.Click += new System.EventHandler(this.btnAddContentInclusion_Click);
            // 
            // txtIncludeContent
            // 
            this.txtIncludeContent.Location = new System.Drawing.Point(3, 153);
            this.txtIncludeContent.Name = "txtIncludeContent";
            this.txtIncludeContent.Size = new System.Drawing.Size(389, 20);
            this.txtIncludeContent.TabIndex = 18;
            // 
            // CodeIssueOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnRemoveContentInclusion);
            this.Controls.Add(this.btnAddContentInclusion);
            this.Controls.Add(this.txtIncludeContent);
            this.Controls.Add(this.listIncludeContent);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnRemoveContentExclusion);
            this.Controls.Add(this.txtExcludeContent);
            this.Controls.Add(this.btnAddContentExclusion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listExcludeContent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemoveExclusion);
            this.Controls.Add(this.txtExclude);
            this.Controls.Add(this.btnRemoveInclusion);
            this.Controls.Add(this.btnAddInclusion);
            this.Controls.Add(this.btnAddExclusion);
            this.Controls.Add(this.txtInclude);
            this.Controls.Add(this.listExcludeFiles);
            this.Controls.Add(this.listIncludeFiles);
            this.Name = "CodeIssueOptions";
            this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.CodeIssueOptions_CommitChanges);
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
                return CodeIssueOptions.GetCategory();
            }
        }
        ///
        /// Returns the page name of this options page.
        ///
        public override string PageName
        {
            get
            {
                return CodeIssueOptions.GetPageName();
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

        private System.Windows.Forms.ListBox listIncludeFiles;
        private System.Windows.Forms.ListBox listExcludeFiles;
        private System.Windows.Forms.TextBox txtInclude;
        private System.Windows.Forms.Button btnAddExclusion;
        private System.Windows.Forms.Button btnAddInclusion;
        private System.Windows.Forms.Button btnRemoveInclusion;
        private System.Windows.Forms.TextBox txtExclude;
        private System.Windows.Forms.Button btnRemoveExclusion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listExcludeContent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRemoveContentExclusion;
        private System.Windows.Forms.TextBox txtExcludeContent;
        private System.Windows.Forms.Button btnAddContentExclusion;
        private System.Windows.Forms.ListBox listIncludeContent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRemoveContentInclusion;
        private System.Windows.Forms.Button btnAddContentInclusion;
        private System.Windows.Forms.TextBox txtIncludeContent;
    }
}