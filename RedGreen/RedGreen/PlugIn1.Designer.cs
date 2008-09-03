namespace RedGreen
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
            this.actRunTest = new DevExpress.CodeRush.Core.Action(this.components);
            this.actNextError = new DevExpress.CodeRush.Core.Action(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.actRunTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actNextError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // actRunTest
            // 
            this.actRunTest.ActionName = "Run Test(s)";
            this.actRunTest.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.actRunTest.Description = "Runs the currently selected unit tests";
            this.actRunTest.Image = ((System.Drawing.Bitmap)(resources.GetObject("actRunTest.Image")));
            this.actRunTest.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.actRunTest.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actRunTest_Execute);
            // 
            // actNextError
            // 
            this.actNextError.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.actNextError.Image = ((System.Drawing.Bitmap)(resources.GetObject("actNextError.Image")));
            this.actNextError.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.actNextError.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actNextError_Execute);
            // 
            // PlugIn1
            // 
            this.SolutionOpened += new DevExpress.CodeRush.Core.DefaultHandler(this.PlugIn1_SolutionOpened);
            this.EditorPaintLanguageElement += new DevExpress.CodeRush.Core.EditorPaintLanguageElementEventHandler(this.PlugIn1_EditorPaintLanguageElement);
            ((System.ComponentModel.ISupportInitialize)(this.actRunTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actNextError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.Action actRunTest;
        private DevExpress.CodeRush.Core.Action actNextError;
    }
}