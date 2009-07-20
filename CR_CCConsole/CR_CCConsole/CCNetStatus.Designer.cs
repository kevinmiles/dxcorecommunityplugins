namespace CR_CCConsole
{
    partial class CCNetStatus
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CCNetStatus()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCNetStatus));
            this.ccNetAction = new DevExpress.CodeRush.Core.Action(this.components);
            this.ilButtonBar = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ccNetAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // ccNetAction
            // 
            this.ccNetAction.ActionName = "Show CCNet Projects";
            this.ccNetAction.ButtonText = "Show CCNet";
            this.ccNetAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.ccNetAction.Description = "Displays the status of configured CCNet projects";
            this.ccNetAction.Image = ((System.Drawing.Bitmap)(resources.GetObject("ccNetAction.Image")));
            this.ccNetAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(220)))));
            this.ccNetAction.ParentMenu = "Tools";
            this.ccNetAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.ccNetAction_Execute);
            // 
            // ilButtonBar
            // 
            this.ilButtonBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilButtonBar.ImageStream")));
            this.ilButtonBar.TransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(220)))));
            this.ilButtonBar.Images.SetKeyName(0, "CCTray.bmp");
            // 
            // CCNetStatus
            // 
            this.SolutionOpened += new DevExpress.CodeRush.Core.DefaultHandler(this.CCNetStatus_SolutionOpened);
            ((System.ComponentModel.ISupportInitialize)(this.ccNetAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.Action ccNetAction;
        private System.Windows.Forms.ImageList ilButtonBar;
    }
}