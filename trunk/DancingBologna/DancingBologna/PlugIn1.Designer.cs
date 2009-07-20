namespace DancingBologna
{
    partial class PlugIn1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public PlugIn1()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlugIn1));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.action1 = new DevExpress.CodeRush.Core.Action(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.action1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Dancing_Bologna_006.gif");
            this.imageList1.Images.SetKeyName(1, "Dancing_Bologna_001.gif");
            this.imageList1.Images.SetKeyName(2, "Dancing_Bologna_002.gif");
            this.imageList1.Images.SetKeyName(3, "Dancing_Bologna_003.gif");
            this.imageList1.Images.SetKeyName(4, "Dancing_Bologna_004.gif");
            this.imageList1.Images.SetKeyName(5, "Dancing_Bologna_005.gif");
            // 
            // action1
            // 
            this.action1.ActionName = "DancingBologna";
            this.action1.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.action1.Description = "Renders Dancing Bologna in the IDE";
            this.action1.Image = ((System.Drawing.Bitmap)(resources.GetObject("action1.Image")));
            this.action1.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.action1.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.action1_Execute);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PlugIn1
            // 
            this.EditorPaint += new DevExpress.CodeRush.Core.EditorPaintEventHandler(this.PlugIn1_EditorPaint);
            ((System.ComponentModel.ISupportInitialize)(this.action1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.CodeRush.Core.Action action1;
        private System.Windows.Forms.Timer timer1;
    }
}