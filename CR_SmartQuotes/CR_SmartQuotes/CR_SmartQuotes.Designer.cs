namespace CR_SmartQuotes
{
    partial class CR_SmartQuotes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CR_SmartQuotes()
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            this.InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            // CR_SmartQuotes
            // 
            this.EditorCharacterTyping += new DevExpress.CodeRush.Core.EditorCharacterTypingEventHandler(this.CR_SmartQuotes_EditorCharacterTyping);
            this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.CR_SmartQuotes_OptionsChanged);
            this.EditorCharacterTyped += new DevExpress.CodeRush.Core.EditorCharacterTypedEventHandler(this.CR_SmartQuotes_EditorCharacterTyped);
            this.KeyPressed += new DevExpress.CodeRush.Core.KeyPressedEventHandler(this.CR_SmartQuotes_KeyPressed);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}