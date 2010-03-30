namespace CR_Initials
{
    partial class CR_Initials
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CR_Initials()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CR_Initials));
            this.actInitials = new DevExpress.CodeRush.Core.Action(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.actInitials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // actInitials
            // 
            this.actInitials.ActionName = "Add Initials";
            this.actInitials.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.actInitials.Description = "Insert initials + date";
            this.actInitials.Image = ((System.Drawing.Bitmap)(resources.GetObject("actInitials.Image")));
            this.actInitials.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.actInitials.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actInitials_Execute);
            this.actInitials.CheckAvailability += new DevExpress.CodeRush.Core.CheckActionAvailabilityEventHandler(this.actInitials_CheckAvailability);
            // 
            // CR_Initials
            // 
            this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.CR_Initials_OptionsChanged);
            ((System.ComponentModel.ISupportInitialize)(this.actInitials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.Action actInitials;

    }
}