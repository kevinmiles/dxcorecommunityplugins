namespace CR_BlockPainterPlus
{
    partial class BlockPainterPlugIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public BlockPainterPlugIn()
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
            // BlockPainterPlugIn
            // 
            this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.BlockPainterPlugIn_OptionsChanged);
            this.EditorPaintBackground += new DevExpress.CodeRush.Core.EditorPaintEventHandler(this.BlockPainterPlugIn_EditorPaintBackground);
            this.EditorPaintForeground += new DevExpress.CodeRush.Core.EditorPaintEventHandler(this.BlockPainterPlugIn_EditorPaintForeground);
            this.EditorScrolled += new DevExpress.CodeRush.Core.EditorScrolledEventHandler(this.BlockPainterPlugIn_EditorScrolled);
            this.DecorateLanguageElement += new DevExpress.CodeRush.Core.DecorateLanguageElementEventHandler(this.BlockPainterPlugIn_DecorateLanguageElement);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}