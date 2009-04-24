namespace Refactor_Comments
{
    partial class CollapseXmlComment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CollapseXmlComment()
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
            this.XmlSummaryCollapseProvider = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.XmlSummaryCollapseProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // XmlSummaryCollapseProvider
            // 
            this.XmlSummaryCollapseProvider.ActionHintText = "";
            this.XmlSummaryCollapseProvider.AutoActivate = true;
            this.XmlSummaryCollapseProvider.AutoUndo = false;
            this.XmlSummaryCollapseProvider.Description = "Collapse Xml Comment tags to one line.";
            this.XmlSummaryCollapseProvider.DisplayName = "Collapse Summary";
            this.XmlSummaryCollapseProvider.ProviderName = "";
            this.XmlSummaryCollapseProvider.Register = true;
            this.XmlSummaryCollapseProvider.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.XmlSummaryCollapseProvider_Apply);
            this.XmlSummaryCollapseProvider.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.XmlSummaryCollapseProvider_CheckAvailability);
            ((System.ComponentModel.ISupportInitialize)(this.XmlSummaryCollapseProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.Refactor.Core.RefactoringProvider XmlSummaryCollapseProvider;

    }
}