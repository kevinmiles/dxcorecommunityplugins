using System;
using DevExpress.CodeRush.Core;

namespace CR_CCConsole
{
    partial class CCStatus_Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CCStatus_Options()
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
            this.lblBuildServer = new System.Windows.Forms.Label();
            this.tbBuildServer = new System.Windows.Forms.TextBox();
            this.gbProjects = new System.Windows.Forms.GroupBox();
            this.clbProjects = new System.Windows.Forms.CheckedListBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.nudUpdateInterval = new System.Windows.Forms.NumericUpDown();
            this.lblPoling = new System.Windows.Forms.Label();
            this.cbNotifyOnFailure = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNotifyTest = new System.Windows.Forms.Button();
            this.gbProjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpdateInterval)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBuildServer
            // 
            this.lblBuildServer.AutoSize = true;
            this.lblBuildServer.Location = new System.Drawing.Point(4, 4);
            this.lblBuildServer.Name = "lblBuildServer";
            this.lblBuildServer.Size = new System.Drawing.Size(307, 13);
            this.lblBuildServer.TabIndex = 0;
            this.lblBuildServer.Text = "Build Server: (should be name of the server hosting CCNet web)";
            // 
            // tbBuildServer
            // 
            this.tbBuildServer.Location = new System.Drawing.Point(7, 21);
            this.tbBuildServer.Name = "tbBuildServer";
            this.tbBuildServer.Size = new System.Drawing.Size(369, 20);
            this.tbBuildServer.TabIndex = 1;
            // 
            // gbProjects
            // 
            this.gbProjects.Controls.Add(this.clbProjects);
            this.gbProjects.Location = new System.Drawing.Point(7, 47);
            this.gbProjects.Name = "gbProjects";
            this.gbProjects.Size = new System.Drawing.Size(369, 128);
            this.gbProjects.TabIndex = 2;
            this.gbProjects.TabStop = false;
            this.gbProjects.Text = "Available Projects";
            // 
            // clbProjects
            // 
            this.clbProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.clbProjects.FormattingEnabled = true;
            this.clbProjects.Location = new System.Drawing.Point(7, 20);
            this.clbProjects.Name = "clbProjects";
            this.clbProjects.Size = new System.Drawing.Size(356, 94);
            this.clbProjects.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(301, 181);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // nudUpdateInterval
            // 
            this.nudUpdateInterval.Location = new System.Drawing.Point(10, 19);
            this.nudUpdateInterval.Name = "nudUpdateInterval";
            this.nudUpdateInterval.Size = new System.Drawing.Size(46, 20);
            this.nudUpdateInterval.TabIndex = 4;
            this.nudUpdateInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblPoling
            // 
            this.lblPoling.AutoSize = true;
            this.lblPoling.Location = new System.Drawing.Point(62, 21);
            this.lblPoling.Name = "lblPoling";
            this.lblPoling.Size = new System.Drawing.Size(125, 13);
            this.lblPoling.TabIndex = 5;
            this.lblPoling.Text = "Update Interval (minutes)";
            // 
            // cbNotifyOnFailure
            // 
            this.cbNotifyOnFailure.AutoSize = true;
            this.cbNotifyOnFailure.Location = new System.Drawing.Point(10, 46);
            this.cbNotifyOnFailure.Name = "cbNotifyOnFailure";
            this.cbNotifyOnFailure.Size = new System.Drawing.Size(275, 17);
            this.cbNotifyOnFailure.TabIndex = 6;
            this.cbNotifyOnFailure.Text = "Show Notification when a passing project initially fails";
            this.cbNotifyOnFailure.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNotifyTest);
            this.groupBox1.Controls.Add(this.cbNotifyOnFailure);
            this.groupBox1.Controls.Add(this.nudUpdateInterval);
            this.groupBox1.Controls.Add(this.lblPoling);
            this.groupBox1.Location = new System.Drawing.Point(4, 210);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 73);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plugin Options";
            // 
            // btnNotifyTest
            // 
            this.btnNotifyTest.Location = new System.Drawing.Point(285, 42);
            this.btnNotifyTest.Name = "btnNotifyTest";
            this.btnNotifyTest.Size = new System.Drawing.Size(42, 23);
            this.btnNotifyTest.TabIndex = 7;
            this.btnNotifyTest.Text = "Test";
            this.btnNotifyTest.UseVisualStyleBackColor = true;
            this.btnNotifyTest.Click += new System.EventHandler(this.btnNotifyTest_Click);
            // 
            // CCStatus_Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.gbProjects);
            this.Controls.Add(this.tbBuildServer);
            this.Controls.Add(this.lblBuildServer);
            this.Name = "CCStatus_Options";
            this.Size = new System.Drawing.Size(398, 304);
            this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.CCStatus_Options_PreparePage);
            this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.CCStatus_Options_CommitChanges);
            this.gbProjects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudUpdateInterval)).EndInit();
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
                return CCStatus_Options.GetCategory();
            }
        }
        ///
        /// Returns the page name of this options page.
        ///
        public override string PageName
        {
            get
            {
                return CCStatus_Options.GetPageName();
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

        private System.Windows.Forms.Label lblBuildServer;
        private System.Windows.Forms.TextBox tbBuildServer;
        private System.Windows.Forms.GroupBox gbProjects;
        private System.Windows.Forms.CheckedListBox clbProjects;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.NumericUpDown nudUpdateInterval;
        private System.Windows.Forms.Label lblPoling;
        private System.Windows.Forms.CheckBox cbNotifyOnFailure;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNotifyTest;
    }
}