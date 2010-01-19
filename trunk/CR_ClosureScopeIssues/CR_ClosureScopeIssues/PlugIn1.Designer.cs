namespace CR_ClosureScopeIssues
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
            this.closureScopeIssueProvider = new DevExpress.CodeRush.Core.IssueProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closureScopeIssueProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // closureScopeIssueProvider
            // 
            this.closureScopeIssueProvider.Description = "Highlights possible unintended modification of variable passed to closure.";
            this.closureScopeIssueProvider.DisplayName = "Closure Scope Issue";
            this.closureScopeIssueProvider.ProviderName = "ClosureScopeIssueProvider";
            this.closureScopeIssueProvider.Register = true;
            this.closureScopeIssueProvider.CheckCodeIssues += new DevExpress.CodeRush.Core.CheckCodeIssuesEventHandler(this.closureScopeIssueProvider_CheckCodeIssues);
            ((System.ComponentModel.ISupportInitialize)(this.closureScopeIssueProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.IssueProvider closureScopeIssueProvider;
    }
}