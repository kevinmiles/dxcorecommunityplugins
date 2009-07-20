namespace CR_CreateTestMethod
{
    partial class CR_CreateTestMethod
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CR_CreateTestMethod()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CR_CreateTestMethod));
            this.acnCreateTestMethod = new DevExpress.CodeRush.Core.Action(this.components);
            this.spCreateTestMethods = new DevExpress.CodeRush.Extensions.SmartPasteExtension(this.components);
            this.cpInsideTestClass = new DevExpress.CodeRush.Extensions.ContextProvider(this.components);
            this.cpInsideTestMethod = new DevExpress.CodeRush.Extensions.ContextProvider(this.components);
            this.cpMoveToSetup = new DevExpress.CodeRush.Core.CodeProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.acnCreateTestMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spCreateTestMethods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpInsideTestClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpInsideTestMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpMoveToSetup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // acnCreateTestMethod
            // 
            this.acnCreateTestMethod.ActionName = "Create Test";
            this.acnCreateTestMethod.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.acnCreateTestMethod.Description = "Creates a test method from the current comment";
            this.acnCreateTestMethod.Image = ((System.Drawing.Bitmap)(resources.GetObject("acnCreateTestMethod.Image")));
            this.acnCreateTestMethod.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.acnCreateTestMethod.RegisterMenuButton = false;
            this.acnCreateTestMethod.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.acnCreateTestMethod_Execute);
            this.acnCreateTestMethod.CheckAvailability += new DevExpress.CodeRush.Core.CheckActionAvailabilityEventHandler(this.acnCreateTestMethod_CheckAvailability);
            // 
            // spCreateTestMethods
            // 
            this.spCreateTestMethods.Description = "Creates test method(s) from the contents of the clipboard";
            this.spCreateTestMethods.ExtensionName = "Create Test Method(s)";
            this.spCreateTestMethods.Register = true;
            this.spCreateTestMethods.GetSmartPasteSuggestions += new DevExpress.CodeRush.Extensions.GetSmartPasteSuggestionsHandler(this.spCreateTestMethods_GetSmartPasteSuggestions);
            // 
            // cpInsideTestClass
            // 
            this.cpInsideTestClass.Description = "Satisfied if the caret is within a Test Class";
            this.cpInsideTestClass.DisplayName = "Inside Test Class";
            this.cpInsideTestClass.ProviderName = "Editor\\Code\\Inside Test Class";
            this.cpInsideTestClass.Register = true;
            this.cpInsideTestClass.ContextSatisfied += new DevExpress.CodeRush.Core.ContextSatisfiedEventHandler(this.cpInsideTestClass_ContextSatisfied);
            // 
            // cpInsideTestMethod
            // 
            this.cpInsideTestMethod.Description = "Satisfied if the caret is inside a test method";
            this.cpInsideTestMethod.DisplayName = "Inside Test Method";
            this.cpInsideTestMethod.ProviderName = "Editor\\Code\\Inside Test Method";
            this.cpInsideTestMethod.Register = true;
            this.cpInsideTestMethod.ContextSatisfied += new DevExpress.CodeRush.Core.ContextSatisfiedEventHandler(this.cpInsideTestMethod_ContextSatisfied);
            // 
            // cpMoveToSetup
            // 
            this.cpMoveToSetup.ActionHintText = "Move to SetUp";
            this.cpMoveToSetup.AutoActivate = true;
            this.cpMoveToSetup.AutoUndo = true;
            this.cpMoveToSetup.Description = "Moves the selected code block to the SetUp method";
            this.cpMoveToSetup.Image = ((System.Drawing.Bitmap)(resources.GetObject("cpMoveToSetup.Image")));
            this.cpMoveToSetup.NeedsSelection = false;
            this.cpMoveToSetup.ProviderName = "Move to SetUp";
            this.cpMoveToSetup.Register = true;
            this.cpMoveToSetup.SupportsAsyncMode = false;
            this.cpMoveToSetup.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.cpMoveToSetup_Apply);
            this.cpMoveToSetup.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.cpMoveToSetup_CheckAvailability);
            this.cpMoveToSetup.PreparePreview += new DevExpress.Refactor.Core.PrepareRefactoringPreviewEventHandler(this.cpMoveToSetup_PreparePreview);
            // 
            // CR_CreateTestMethod
            // 
            ((System.ComponentModel.ISupportInitialize)(this.acnCreateTestMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spCreateTestMethods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpInsideTestClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpInsideTestMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpMoveToSetup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.Action acnCreateTestMethod;
        private DevExpress.CodeRush.Extensions.SmartPasteExtension spCreateTestMethods;
        private DevExpress.CodeRush.Extensions.ContextProvider cpInsideTestClass;
        private DevExpress.CodeRush.Extensions.ContextProvider cpInsideTestMethod;
        private DevExpress.CodeRush.Core.CodeProvider cpMoveToSetup;

    }
}