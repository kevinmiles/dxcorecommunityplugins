using System.Runtime.InteropServices;

namespace CodeIssueAnalysis
{
    [Guid("61517ca9-012f-4feb-b277-e4e945319158")]
    partial class ToolWindow1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DevExpress.DXCore.PlugInCore.DXCoreEvents events;

        public ToolWindow1()
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
            this.components = new System.ComponentModel.Container();
            DevExpress.DXCore.Controls.XtraGrid.GridGroupSummaryItem gridGroupSummaryItem2 = new DevExpress.DXCore.Controls.XtraGrid.GridGroupSummaryItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolWindow1));
            this.events = new DevExpress.DXCore.PlugInCore.DXCoreEvents(this.components);
            this.gridControl1 = new DevExpress.DXCore.Controls.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.DXCore.Controls.XtraGrid.Views.Grid.GridView();
            this.locatorBeacon1 = new DevExpress.CodeRush.PlugInCore.LocatorBeacon(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSolutionIssues = new System.Windows.Forms.ToolStripButton();
            this.btnProjectIssues = new System.Windows.Forms.ToolStripButton();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.events)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locatorBeacon1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(0, 25);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(625, 375);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.DXCore.Controls.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            gridGroupSummaryItem2.DisplayFormat = "(Count = {0})";
            gridGroupSummaryItem2.FieldName = "Type";
            gridGroupSummaryItem2.SummaryType = DevExpress.DXCore.Controls.Data.SummaryItemType.Custom;
            this.gridView1.GroupSummary.AddRange(new DevExpress.DXCore.Controls.XtraGrid.GridSummaryItem[] {
            gridGroupSummaryItem2});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFilter.MaxCheckedListItemCount = 200;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.CustomSummaryCalculate += new DevExpress.DXCore.Controls.Data.CustomSummaryEventHandler(this.gridView1_CustomSummaryCalculate);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // locatorBeacon1
            // 
            this.locatorBeacon1.Color = System.Drawing.Color.SlateBlue;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSolutionIssues,
            this.btnProjectIssues});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(625, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSolutionIssues
            // 
            this.btnSolutionIssues.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSolutionIssues.Image = ((System.Drawing.Image)(resources.GetObject("btnSolutionIssues.Image")));
            this.btnSolutionIssues.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSolutionIssues.Name = "btnSolutionIssues";
            this.btnSolutionIssues.Size = new System.Drawing.Size(23, 22);
            this.btnSolutionIssues.Text = "toolStripButton1";
            this.btnSolutionIssues.ToolTipText = "Get Solution Issues";
            this.btnSolutionIssues.Click += new System.EventHandler(this.btnSolutionIssues_Click);
            // 
            // btnProjectIssues
            // 
            this.btnProjectIssues.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProjectIssues.Image = ((System.Drawing.Image)(resources.GetObject("btnProjectIssues.Image")));
            this.btnProjectIssues.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProjectIssues.Name = "btnProjectIssues";
            this.btnProjectIssues.Size = new System.Drawing.Size(23, 22);
            this.btnProjectIssues.Text = "toolStripButton2";
            this.btnProjectIssues.ToolTipText = "Get Project Issues";
            this.btnProjectIssues.Click += new System.EventHandler(this.btnProjectIssues_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(3, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(558, 25);
            this.progressBar.TabIndex = 4;
            this.progressBar.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(562, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ToolWindow1
            // 
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gridControl1);
            this.Image = ((System.Drawing.Bitmap)(resources.GetObject("$this.Image")));
            this.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Name = "ToolWindow1";
            this.Size = new System.Drawing.Size(625, 400);
            ((System.ComponentModel.ISupportInitialize)(this.events)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locatorBeacon1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region ShowWindow
        ///
        /// Displays this tool window.
        ///
        public static EnvDTE.Window ShowWindow()
        {
            return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Show(typeof(ToolWindow1).GUID);
        }
        #endregion
        #region HideWindow
        ///
        /// Hides this tool window.
        ///
        public static EnvDTE.Window HideWindow()
        {
            return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Hide(typeof(ToolWindow1).GUID);
        }
        #endregion

        private DevExpress.DXCore.Controls.XtraGrid.GridControl gridControl1;
        private DevExpress.DXCore.Controls.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.CodeRush.PlugInCore.LocatorBeacon locatorBeacon1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSolutionIssues;
        private System.Windows.Forms.ToolStripButton btnProjectIssues;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnCancel;
    }
}