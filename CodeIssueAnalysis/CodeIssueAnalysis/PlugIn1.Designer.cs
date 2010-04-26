namespace CodeIssueAnalysis
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
            this.action1 = new DevExpress.CodeRush.Core.Action(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.action1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // action1
            // 
            this.action1.ActionName = "OpenCodeIssueAnalysis";
            this.action1.ButtonText = "Open Code Issue Analysis";
            this.action1.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.action1.Description = "Opens Code Issue Analysis Tool Window";
            this.action1.Image = ((System.Drawing.Bitmap)(resources.GetObject("action1.Image")));
            this.action1.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.action1.ToolbarItem.ButtonIsPressed = false;
            this.action1.ToolbarItem.Caption = null;
            this.action1.ToolbarItem.Image = null;
            this.action1.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.action1_Execute);
            ((System.ComponentModel.ISupportInitialize)(this.action1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.Action action1;
    }
}