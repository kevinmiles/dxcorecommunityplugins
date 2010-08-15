namespace Refactor_ConvertToXPOProperty
{
    partial class ConvertToXPOPropertyPlugin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ConvertToXPOPropertyPlugin()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvertToXPOPropertyPlugin));
            this.refactoringProvider = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.refactoringProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // refactoringProvider
            // 
            this.refactoringProvider.ActionHintText = "Convert to XPO property";
            this.refactoringProvider.AutoActivate = true;
            this.refactoringProvider.AutoUndo = false;
            this.refactoringProvider.CodeIssueMessage = null;
            this.refactoringProvider.Description = "Enables properties to be converted into XPO properties";
            this.refactoringProvider.DisplayName = "Convert into XPO property";
            this.refactoringProvider.ExclusiveAvailabilityBehavior = DevExpress.CodeRush.Core.ExclusiveAvailabilityBehavior.ShowMenu;
            this.refactoringProvider.Image = ((System.Drawing.Bitmap)(resources.GetObject("refactoringProvider.Image")));
            this.refactoringProvider.NeedsSelection = false;
            this.refactoringProvider.ProviderName = "ConvertToXPOProperty";
            this.refactoringProvider.Register = true;
            this.refactoringProvider.SupportsAsyncMode = false;
            this.refactoringProvider.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.refactoringProvider_CheckAvailability);
            this.refactoringProvider.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.refactoringProvider_Apply);
            this.refactoringProvider.PreparePreview += new DevExpress.Refactor.Core.PrepareRefactoringPreviewEventHandler(this.refactoringProvider_PreparePreview);
            ((System.ComponentModel.ISupportInitialize)(this.refactoringProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.Refactor.Core.RefactoringProvider refactoringProvider;
    }
}