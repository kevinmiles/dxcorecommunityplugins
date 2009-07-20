namespace DX_JQuery
{
    partial class JqueryPlugin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public JqueryPlugin()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JqueryPlugin));
            this.cpConvertToJQuery = new DevExpress.CodeRush.Core.CodeProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cpConvertToJQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // cpConvertToJQuery
            // 
            this.cpConvertToJQuery.ActionHintText = "Convert To JQuery";
            this.cpConvertToJQuery.AutoActivate = true;
            this.cpConvertToJQuery.AutoUndo = true;
            this.cpConvertToJQuery.Description = "Converts the current expression to an equivelent jQuery expression";
            this.cpConvertToJQuery.Image = ((System.Drawing.Bitmap)(resources.GetObject("cpConvertToJQuery.Image")));
            this.cpConvertToJQuery.NeedsSelection = false;
            this.cpConvertToJQuery.ProviderName = "Convert To JQuery";
            this.cpConvertToJQuery.Register = true;
            this.cpConvertToJQuery.SupportsAsyncMode = false;
            this.cpConvertToJQuery.LanguageSupported += new DevExpress.CodeRush.Core.LanguageSupportedEventHandler(this.cpConvertToJQuery_LanguageSupported);
            this.cpConvertToJQuery.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.cpConvertToJQuery_Apply);
            this.cpConvertToJQuery.HidePreview += new DevExpress.Refactor.Core.HideRefactoringPreviewEventHandler(this.cpConvertToJQuery_HidePreview);
            this.cpConvertToJQuery.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.cpConvertToJQuery_CheckAvailability);
            this.cpConvertToJQuery.PreparePreview += new DevExpress.Refactor.Core.PrepareRefactoringPreviewEventHandler(this.cpConvertToJQuery_PreparePreview);
            ((System.ComponentModel.ISupportInitialize)(this.cpConvertToJQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.CodeProvider cpConvertToJQuery;
    }
}