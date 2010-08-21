namespace Impromptu
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
            this.impromptuActions = new DevExpress.CodeRush.Core.Action(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.impromptuActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // impromptuActions
            // 
            this.impromptuActions.ActionName = "Impromptu Run Method";
            this.impromptuActions.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.impromptuActions.Image = null;
            this.impromptuActions.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.impromptuActions.ToolbarItem.ButtonIsPressed = false;
            this.impromptuActions.ToolbarItem.Caption = null;
            this.impromptuActions.ToolbarItem.Image = null;
            this.impromptuActions.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.impromptuActions_Execute);
            // 
            // PlugIn1
            // 
            this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.PlugIn1_OptionsChanged);
            this.DecorateLanguageElement += new DevExpress.CodeRush.Core.DecorateLanguageElementEventHandler(this.PlugIn1_DecorateLanguageElement);
            ((System.ComponentModel.ISupportInitialize)(this.impromptuActions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.Action impromptuActions;
    }
}