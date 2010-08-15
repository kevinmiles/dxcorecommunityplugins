namespace CR_EasyGoto
{
    partial class EasyGotoPlugIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public EasyGotoPlugIn()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EasyGotoPlugIn));
            this.navEasyGoto = new DevExpress.CodeRush.Library.NavigationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.navEasyGoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // navEasyGoto
            // 
            this.navEasyGoto.ActionHintText = "Navigate to class declaration";
            this.navEasyGoto.AutoActivate = true;
            this.navEasyGoto.AutoUndo = false;
            this.navEasyGoto.CodeIssueMessage = null;
            this.navEasyGoto.Description = "Enables to navigate between various places within a class";
            this.navEasyGoto.DisplayName = "Easy goto";
            this.navEasyGoto.ExclusiveAvailabilityBehavior = DevExpress.CodeRush.Core.ExclusiveAvailabilityBehavior.ShowMenu;
            this.navEasyGoto.Image = ((System.Drawing.Bitmap)(resources.GetObject("navEasyGoto.Image")));
            this.navEasyGoto.NeedsSelection = false;
            this.navEasyGoto.ProviderName = "EasyGoto";
            this.navEasyGoto.Register = true;
            this.navEasyGoto.SupportsAsyncMode = false;
            this.navEasyGoto.Navigate += new DevExpress.CodeRush.Library.NavigationEventHandler(this.navEasyGoto_Navigate);
            this.navEasyGoto.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.navEasyGoto_CheckAvailability);
            ((System.ComponentModel.ISupportInitialize)(this.navEasyGoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Library.NavigationProvider navEasyGoto;
    }
}