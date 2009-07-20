namespace CR_NavigateToTest
{
    partial class NavigateToTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public NavigateToTest()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigateToTest));
            this.navigationProvider1 = new DevExpress.CodeRush.Library.NavigationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.navigationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // navigationProvider1
            // 
            this.navigationProvider1.ActionHintText = "";
            this.navigationProvider1.AutoActivate = true;
            this.navigationProvider1.AutoUndo = false;
            this.navigationProvider1.Description = "Navigate to the test class which uses the active class";
            this.navigationProvider1.DisplayName = "Tests ";
            this.navigationProvider1.Image = ((System.Drawing.Bitmap)(resources.GetObject("navigationProvider1.Image")));
            this.navigationProvider1.NeedsSelection = false;
            this.navigationProvider1.ProviderName = "NavToTest";
            this.navigationProvider1.Register = true;
            this.navigationProvider1.SupportsAsyncMode = false;
            this.navigationProvider1.Navigate += new DevExpress.CodeRush.Library.NavigationEventHandler(this.navigationProvider1_Navigate);
            this.navigationProvider1.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.navigationProvider1_CheckAvailability);
            ((System.ComponentModel.ISupportInitialize)(this.navigationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Library.NavigationProvider navigationProvider1;
    }
}