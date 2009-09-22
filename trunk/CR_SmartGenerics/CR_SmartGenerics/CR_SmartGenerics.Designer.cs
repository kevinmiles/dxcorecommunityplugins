namespace CR_SmartGenerics
{
    partial class CR_SmartGenerics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CR_SmartGenerics()
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
            // CR_SmartGenerics
            // 
            this.EditorCharacterTyping += new DevExpress.CodeRush.Core.EditorCharacterTypingEventHandler(this.CR_SmartGenerics_EditorCharacterTyping);
            this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.CR_SmartGenerics_OptionsChanged);
            this.EditorCharacterTyped += new DevExpress.CodeRush.Core.EditorCharacterTypedEventHandler(this.CR_SmartGenerics_EditorCharacterTyped);
            this.KeyPressed += new DevExpress.CodeRush.Core.KeyPressedEventHandler(this.CR_SmartGenerics_KeyPressed);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}