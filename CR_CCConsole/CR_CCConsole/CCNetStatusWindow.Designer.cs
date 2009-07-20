using System.Runtime.InteropServices;

namespace CR_CCConsole
{
    [Guid("781c83b9-7588-489c-b2f7-bf38af397cbc")]
    partial class CCNetStatusWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DevExpress.DXCore.PlugInCore.DXCoreEvents events;

        public CCNetStatusWindow()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCNetStatusWindow));
            this.events = new DevExpress.DXCore.PlugInCore.DXCoreEvents(this.components);
            this.lvProjectStatus = new System.Windows.Forms.ListView();
            this.chProject = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.chLastBuild = new System.Windows.Forms.ColumnHeader();
            this.cmViewOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilBuildStatusIconsLarge = new System.Windows.Forms.ImageList(this.components);
            this.ilBuildStatusIconsSmall = new System.Windows.Forms.ImageList(this.components);
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.bwStatusUpdater = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.events)).BeginInit();
            this.cmViewOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lvProjectStatus
            // 
            this.lvProjectStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chProject,
            this.chStatus,
            this.chLastBuild});
            this.lvProjectStatus.ContextMenuStrip = this.cmViewOptions;
            this.lvProjectStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvProjectStatus.GridLines = true;
            this.lvProjectStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvProjectStatus.LargeImageList = this.ilBuildStatusIconsLarge;
            this.lvProjectStatus.Location = new System.Drawing.Point(0, 0);
            this.lvProjectStatus.Name = "lvProjectStatus";
            this.lvProjectStatus.Size = new System.Drawing.Size(407, 161);
            this.lvProjectStatus.SmallImageList = this.ilBuildStatusIconsSmall;
            this.lvProjectStatus.TabIndex = 0;
            this.lvProjectStatus.UseCompatibleStateImageBehavior = false;
            // 
            // chProject
            // 
            this.chProject.Text = "Project";
            this.chProject.Width = 150;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            // 
            // chLastBuild
            // 
            this.chLastBuild.Text = "Last Build Time";
            this.chLastBuild.Width = 100;
            // 
            // cmViewOptions
            // 
            this.cmViewOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.cmViewOptions.Name = "cmViewOptions";
            this.cmViewOptions.Size = new System.Drawing.Size(124, 48);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.largeIconsToolStripMenuItem,
            this.smallIconsToolStripMenuItem,
            this.listToolStripMenuItem,
            this.detailsToolStripMenuItem});
            this.viewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewToolStripMenuItem.Image")));
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // largeIconsToolStripMenuItem
            // 
            this.largeIconsToolStripMenuItem.Name = "largeIconsToolStripMenuItem";
            this.largeIconsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.largeIconsToolStripMenuItem.Text = "Large Icons";
            this.largeIconsToolStripMenuItem.Click += new System.EventHandler(this.largeIconsToolStripMenuItem_Click);
            // 
            // smallIconsToolStripMenuItem
            // 
            this.smallIconsToolStripMenuItem.Name = "smallIconsToolStripMenuItem";
            this.smallIconsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.smallIconsToolStripMenuItem.Text = "Small Icons";
            this.smallIconsToolStripMenuItem.Click += new System.EventHandler(this.smallIconsToolStripMenuItem_Click);
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.listToolStripMenuItem.Text = "List";
            this.listToolStripMenuItem.Click += new System.EventHandler(this.listToolStripMenuItem_Click);
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.detailsToolStripMenuItem.Text = "Details";
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.detailsToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripMenuItem.Image")));
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // ilBuildStatusIconsLarge
            // 
            this.ilBuildStatusIconsLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilBuildStatusIconsLarge.ImageStream")));
            this.ilBuildStatusIconsLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.ilBuildStatusIconsLarge.Images.SetKeyName(0, "help2.ico");
            this.ilBuildStatusIconsLarge.Images.SetKeyName(1, "check.ico");
            this.ilBuildStatusIconsLarge.Images.SetKeyName(2, "delete.ico");
            this.ilBuildStatusIconsLarge.Images.SetKeyName(3, "nav_right_green.ico");
            this.ilBuildStatusIconsLarge.Images.SetKeyName(4, "nav_right_red.ico");
            // 
            // ilBuildStatusIconsSmall
            // 
            this.ilBuildStatusIconsSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilBuildStatusIconsSmall.ImageStream")));
            this.ilBuildStatusIconsSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.ilBuildStatusIconsSmall.Images.SetKeyName(0, "help2.ico");
            this.ilBuildStatusIconsSmall.Images.SetKeyName(1, "check.ico");
            this.ilBuildStatusIconsSmall.Images.SetKeyName(2, "delete.ico");
            this.ilBuildStatusIconsSmall.Images.SetKeyName(3, "nav_right_green.ico");
            this.ilBuildStatusIconsSmall.Images.SetKeyName(4, "nav_right_red.ico");
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // bwStatusUpdater
            // 
            this.bwStatusUpdater.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwStatusUpdater_DoWork);
            // 
            // CCNetStatusWindow
            // 
            this.Controls.Add(this.lvProjectStatus);
            this.Image = ((System.Drawing.Bitmap)(resources.GetObject("$this.Image")));
            this.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Name = "CCNetStatusWindow";
            this.Size = new System.Drawing.Size(407, 161);
            this.Load += new System.EventHandler(this.CCNetStatusWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.events)).EndInit();
            this.cmViewOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #region ShowWindow
        ///
        /// Displays this tool window.
        ///
        public static EnvDTE.Window ShowWindow()
        {
            return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Show(typeof(CCNetStatusWindow).GUID);
        }
        #endregion
        #region HideWindow
        ///
        /// Hides this tool window.
        ///
        public static EnvDTE.Window HideWindow()
        {
            return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Hide(typeof(CCNetStatusWindow).GUID);
        }
        #endregion

        private System.Windows.Forms.ListView lvProjectStatus;
        private System.Windows.Forms.ImageList ilBuildStatusIconsSmall;
        private System.Windows.Forms.ImageList ilBuildStatusIconsLarge;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.ContextMenuStrip cmViewOptions;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader chProject;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chLastBuild;
        private System.ComponentModel.BackgroundWorker bwStatusUpdater;
    }
}