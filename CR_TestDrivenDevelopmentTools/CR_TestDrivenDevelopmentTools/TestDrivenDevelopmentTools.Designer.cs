namespace CR_TestDrivenDevelopmentTools
{
    partial class TestDrivenDevelopmentTools
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public TestDrivenDevelopmentTools()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestDrivenDevelopmentTools));
            this.MoveTypeToFileInSpecificProjectProvider = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
            this.DeclareClassInSpecificProject = new DevExpress.CodeRush.Core.CodeProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MoveTypeToFileInSpecificProjectProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeclareClassInSpecificProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // MoveTypeToFileInSpecificProjectProvider
            // 
            this.MoveTypeToFileInSpecificProjectProvider.ActionHintText = "Im the ActionHintText Property";
            this.MoveTypeToFileInSpecificProjectProvider.AutoActivate = true;
            this.MoveTypeToFileInSpecificProjectProvider.AutoUndo = false;
            this.MoveTypeToFileInSpecificProjectProvider.CodeIssueMessage = null;
            this.MoveTypeToFileInSpecificProjectProvider.Description = "Move this type declaration to a new file with the same name in the selected proje" +
                "ct";
            this.MoveTypeToFileInSpecificProjectProvider.DisplayName = "Move Type to File in Project";
            this.MoveTypeToFileInSpecificProjectProvider.Image = ((System.Drawing.Bitmap)(resources.GetObject("MoveTypeToFileInSpecificProjectProvider.Image")));
            this.MoveTypeToFileInSpecificProjectProvider.NeedsSelection = false;
            this.MoveTypeToFileInSpecificProjectProvider.ProviderName = "MoveTypeToFileInSpecificProjectProvider";
            this.MoveTypeToFileInSpecificProjectProvider.Register = true;
            this.MoveTypeToFileInSpecificProjectProvider.SupportsAsyncMode = false;
            this.MoveTypeToFileInSpecificProjectProvider.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.MoveTypeToFileInSpecificProjectProvider_Apply);
            this.MoveTypeToFileInSpecificProjectProvider.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.MoveTypeToFileInSpecificProjectProvider_CheckAvailability);
            // 
            // DeclareClassInSpecificProject
            // 
            this.DeclareClassInSpecificProject.ActionHintText = "Im the ActionHint";
            this.DeclareClassInSpecificProject.AutoActivate = true;
            this.DeclareClassInSpecificProject.AutoUndo = false;
            this.DeclareClassInSpecificProject.CodeIssueMessage = null;
            this.DeclareClassInSpecificProject.Description = "Im the Description";
            this.DeclareClassInSpecificProject.DisplayName = "Im the Display Name";
            this.DeclareClassInSpecificProject.Image = ((System.Drawing.Bitmap)(resources.GetObject("DeclareClassInSpecificProject.Image")));
            this.DeclareClassInSpecificProject.NeedsSelection = false;
            this.DeclareClassInSpecificProject.ProviderName = "DeclareClassInSpecificProject";
            this.DeclareClassInSpecificProject.Register = true;
            this.DeclareClassInSpecificProject.SupportsAsyncMode = false;
            this.DeclareClassInSpecificProject.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.DeclareClassInSpecificProject_Apply);
            this.DeclareClassInSpecificProject.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.DeclareClassInSpecificProject_CheckAvailability);
            ((System.ComponentModel.ISupportInitialize)(this.MoveTypeToFileInSpecificProjectProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeclareClassInSpecificProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.Refactor.Core.RefactoringProvider MoveTypeToFileInSpecificProjectProvider;
        private DevExpress.CodeRush.Core.CodeProvider DeclareClassInSpecificProject;
    }
}