namespace CR_StackOverflowIssues
{
    partial class StackOverflowIssuesPlugin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public StackOverflowIssuesPlugin()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StackOverflowIssuesPlugin));
            this.StackOverflowInGetterIssueProvider = new DevExpress.CodeRush.Core.IssueProvider(this.components);
            this.changeToBaseCallRefactoringProvider = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.StackOverflowInGetterIssueProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.changeToBaseCallRefactoringProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // StackOverflowInGetterIssueProvider
            // 
            this.StackOverflowInGetterIssueProvider.Description = "This call will result in StackOverflowException in runtime";
            this.StackOverflowInGetterIssueProvider.DisplayName = "StackOverflow in runtime";
            this.StackOverflowInGetterIssueProvider.ProviderName = "StackOverflowIssueProvider";
            this.StackOverflowInGetterIssueProvider.Register = true;
            this.StackOverflowInGetterIssueProvider.CheckCodeIssues += new DevExpress.CodeRush.Core.CheckCodeIssuesEventHandler(this.StackOverflowInGetterIssueProvider_CheckCodeIssues);
            // 
            // changeToBaseCallRefactoringProvider
            // 
            this.changeToBaseCallRefactoringProvider.ActionHintText = "";
            this.changeToBaseCallRefactoringProvider.AutoActivate = true;
            this.changeToBaseCallRefactoringProvider.AutoUndo = false;
            this.changeToBaseCallRefactoringProvider.Description = "Introduces call to base implementation of property";
            this.changeToBaseCallRefactoringProvider.Image = ((System.Drawing.Bitmap)(resources.GetObject("changeToBaseCallRefactoringProvider.Image")));
            this.changeToBaseCallRefactoringProvider.NeedsSelection = false;
            this.changeToBaseCallRefactoringProvider.ProviderName = "Change to \'base\' call";
            this.changeToBaseCallRefactoringProvider.Register = true;
            this.changeToBaseCallRefactoringProvider.SupportsAsyncMode = false;
            this.changeToBaseCallRefactoringProvider.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.ChangeToBaseCallRefactoringProvider_Apply);
            this.changeToBaseCallRefactoringProvider.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.ChangeToBaseCallRefactoringProvider_CheckAvailability);
            this.changeToBaseCallRefactoringProvider.PreparePreview += new DevExpress.Refactor.Core.PrepareRefactoringPreviewEventHandler(this.ChangeToBaseCallRefactoringProvider_PreparePreview);
            ((System.ComponentModel.ISupportInitialize)(this.StackOverflowInGetterIssueProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.changeToBaseCallRefactoringProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.IssueProvider StackOverflowInGetterIssueProvider;
        private DevExpress.Refactor.Core.RefactoringProvider changeToBaseCallRefactoringProvider;
    }
}