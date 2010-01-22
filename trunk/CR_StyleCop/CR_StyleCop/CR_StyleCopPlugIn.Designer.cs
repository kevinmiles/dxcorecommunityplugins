namespace CR_StyleCop
{
    partial class CR_StyleCopPlugIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CR_StyleCopPlugIn()
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
            this.styleCopIssueProvider = new DevExpress.CodeRush.Core.IssueProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.styleCopIssueProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // styleCopIssueProvider
            // 
            this.styleCopIssueProvider.Description = "Shows StyleCop violations";
            this.styleCopIssueProvider.DisplayName = "StyleCop Issues";
            this.styleCopIssueProvider.ProviderName = "StyleCopProvider";
            this.styleCopIssueProvider.Register = true;
            this.styleCopIssueProvider.LanguageSupported += new DevExpress.CodeRush.Core.LanguageSupportedEventHandler(this.styleCopIssueProvider_LanguageSupported);
            this.styleCopIssueProvider.CheckCodeIssues += new DevExpress.CodeRush.Core.CheckCodeIssuesEventHandler(this.styleCopIssueProvider_CheckCodeIssues);
            ((System.ComponentModel.ISupportInitialize)(this.styleCopIssueProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.IssueProvider styleCopIssueProvider;
    }
}