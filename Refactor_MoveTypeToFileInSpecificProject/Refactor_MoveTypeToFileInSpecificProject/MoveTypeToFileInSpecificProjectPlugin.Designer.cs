namespace Refactor_MoveTypeToFileInSpecificProject
{
    partial class MoveTypeToFileInSpecificProjectPlugin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public MoveTypeToFileInSpecificProjectPlugin()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveTypeToFileInSpecificProjectPlugin));
            this.MoveTypeToFileInSpecificProjectProvider = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MoveTypeToFileInSpecificProjectProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // MoveTypeToFileInSpecificProjectProvider
            // 
            this.MoveTypeToFileInSpecificProjectProvider.ActionHintText = "Im the ActionHintText Property";
            this.MoveTypeToFileInSpecificProjectProvider.AutoActivate = true;
            this.MoveTypeToFileInSpecificProjectProvider.AutoUndo = false;
            this.MoveTypeToFileInSpecificProjectProvider.CodeIssueMessage = null;
            this.MoveTypeToFileInSpecificProjectProvider.Description = "Im the Description Property";
            this.MoveTypeToFileInSpecificProjectProvider.DisplayName = "Im the DisplayName Property";
            this.MoveTypeToFileInSpecificProjectProvider.Image = ((System.Drawing.Bitmap)(resources.GetObject("MoveTypeToFileInSpecificProjectProvider.Image")));
            this.MoveTypeToFileInSpecificProjectProvider.NeedsSelection = false;
            this.MoveTypeToFileInSpecificProjectProvider.ProviderName = "MoveTypeToFileInSpecificProjectProvider";
            this.MoveTypeToFileInSpecificProjectProvider.Register = true;
            this.MoveTypeToFileInSpecificProjectProvider.SupportsAsyncMode = false;
            this.MoveTypeToFileInSpecificProjectProvider.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.MoveTypeToFileInSpecificProjectProvider_Apply);
            this.MoveTypeToFileInSpecificProjectProvider.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.MoveTypeToFileInSpecificProjectProvider_CheckAvailability);
            ((System.ComponentModel.ISupportInitialize)(this.MoveTypeToFileInSpecificProjectProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.Refactor.Core.RefactoringProvider MoveTypeToFileInSpecificProjectProvider;
    }
}