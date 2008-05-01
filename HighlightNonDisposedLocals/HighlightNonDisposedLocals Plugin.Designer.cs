namespace HighlightNonDisposedLocals {
    partial class PlugIn1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public PlugIn1() {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlugIn1));
            this.actionIDisposableHighlightToggle = new DevExpress.CodeRush.Core.Action(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.actionIDisposableHighlightToggle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // actionIDisposableHighlightToggle
            // 
            this.actionIDisposableHighlightToggle.ActionName = "IDisposableHighlightToggle";
            this.actionIDisposableHighlightToggle.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.actionIDisposableHighlightToggle.Description = "Toggles on or off a feature that highlights locals that are not explicitly freed " +
                "or disposed.";
            this.actionIDisposableHighlightToggle.Image = ((System.Drawing.Bitmap)(resources.GetObject("actionIDisposableHighlightToggle.Image")));
            this.actionIDisposableHighlightToggle.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.actionIDisposableHighlightToggle.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actionIDisposableHighlightToggle_Execute);
            // 
            // PlugIn1
            // 
            this.TextChanged += new DevExpress.CodeRush.Core.TextChangedEventHandler(this.PlugIn1_TextChanged);
            this.DocumentActivated += new DevExpress.CodeRush.Core.DocumentEventHandler(this.PlugIn1_DocumentActivated);
            this.EditorPaintLanguageElement += new DevExpress.CodeRush.Core.EditorPaintLanguageElementEventHandler(this.PlugIn1_EditorPaintLanguageElement);
            this.AfterParse += new DevExpress.CodeRush.Core.AfterParseEventHandler(this.PlugIn1_AfterParse);
            ((System.ComponentModel.ISupportInitialize)(this.actionIDisposableHighlightToggle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.Action actionIDisposableHighlightToggle;
    }
}