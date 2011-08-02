namespace CR_CreateContract
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlugIn1));
            this.cpCreateContract = new DevExpress.CodeRush.Core.CodeProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cpCreateContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // cpCreateContract
            // 
            this.cpCreateContract.ActionHintText = "Create Contract Class";
            this.cpCreateContract.AutoActivate = true;
            this.cpCreateContract.AutoUndo = false;
            this.cpCreateContract.CodeIssueMessage = "Create Contract Class";
            this.cpCreateContract.Description = "Creates a buddy contract class for this interface.";
            this.cpCreateContract.Image = ((System.Drawing.Bitmap)(resources.GetObject("cpCreateContract.Image")));
            this.cpCreateContract.NeedsSelection = false;
            this.cpCreateContract.ProviderName = "Create Contract Class";
            this.cpCreateContract.Register = true;
            this.cpCreateContract.SupportsAsyncMode = false;
            this.cpCreateContract.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.cpCreateContract_CheckAvailability);
            this.cpCreateContract.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.cpCreateContract_Apply);
            ((System.ComponentModel.ISupportInitialize)(this.cpCreateContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.CodeProvider cpCreateContract;
    }
}