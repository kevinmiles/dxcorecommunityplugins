namespace CR_CodeBlockContentProviders
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
            this.cpStartOfBlock = new DevExpress.CodeRush.Extensions.ContextProvider(this.components);
            this.cpEndOfBlock = new DevExpress.CodeRush.Extensions.ContextProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cpStartOfBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpEndOfBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // cpStartOfBlock
            // 
            this.cpStartOfBlock.Description = "Satisfied when the caret is at the begining of a collapsable code block";
            this.cpStartOfBlock.DisplayName = "At CodeBlock Start";
            this.cpStartOfBlock.ProviderName = "Editor\\Line\\At CodeBlock Start";
            this.cpStartOfBlock.Register = true;
            this.cpStartOfBlock.ContextSatisfied += new DevExpress.CodeRush.Core.ContextSatisfiedEventHandler(this.cpStartOfBlock_ContextSatisfied);
            // 
            // cpEndOfBlock
            // 
            this.cpEndOfBlock.Description = "Satisfied when the caret is at the end of a collapsable code block";
            this.cpEndOfBlock.DisplayName = "At CodeBlock End";
            this.cpEndOfBlock.ProviderName = "Editor\\Line\\At CodeBlock End";
            this.cpEndOfBlock.Register = true;
            this.cpEndOfBlock.ContextSatisfied += new DevExpress.CodeRush.Core.ContextSatisfiedEventHandler(this.cpEndOfBlock_ContextSatisfied);
            ((System.ComponentModel.ISupportInitialize)(this.cpStartOfBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpEndOfBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Extensions.ContextProvider cpStartOfBlock;
        private DevExpress.CodeRush.Extensions.ContextProvider cpEndOfBlock;
    }
}