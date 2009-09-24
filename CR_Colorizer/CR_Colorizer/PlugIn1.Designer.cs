namespace CR_Colorizer
{
    partial class Colorizer_Plugin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Colorizer_Plugin()
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Colorizer_Plugin
            // 
            this.DocumentActivated += new DevExpress.CodeRush.Core.DocumentEventHandler(this.Colorizer_Plugin_DocumentActivated);
            this.AfterParse += new DevExpress.CodeRush.Core.AfterParseEventHandler(this.Colorizer_Plugin_AfterParse);
            this.EditorPaint += new DevExpress.CodeRush.Core.EditorPaintEventHandler(this.Colorizer_Plugin_EditorPaint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}