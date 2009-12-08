namespace CR_MoveFile
{
    partial class MoveFilePlugin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public MoveFilePlugin()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveFilePlugin));
            this.cpMoveFile = new DevExpress.CodeRush.Core.CodeProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cpMoveFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // cpMoveFile
            // 
            this.cpMoveFile.ActionHintText = "Move File";
            this.cpMoveFile.AutoActivate = true;
            this.cpMoveFile.AutoUndo = false;
            this.cpMoveFile.Description = "Moves the current file to the specified location in the solution";
            this.cpMoveFile.DisplayName = "Move File";
            this.cpMoveFile.Image = ((System.Drawing.Bitmap)(resources.GetObject("cpMoveFile.Image")));
            this.cpMoveFile.NeedsSelection = false;
            this.cpMoveFile.ProviderName = "MoveFile";
            this.cpMoveFile.Register = true;
            this.cpMoveFile.SupportsAsyncMode = false;
            this.cpMoveFile.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.codeProvider1_Apply);
            this.cpMoveFile.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.codeProvider1_CheckAvailability);
            // 
            // MoveFilePlugin
            // 
            ((System.ComponentModel.ISupportInitialize)(this.cpMoveFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.CodeProvider cpMoveFile;
    }
}