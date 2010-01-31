namespace CR_ClassCleaner
{
    partial class ClassCleanerPlugIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ClassCleanerPlugIn()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassCleanerPlugIn));
            this.removeRegionsAction = new DevExpress.CodeRush.Core.Action(this.components);
            this.removeWhitespaceAction = new DevExpress.CodeRush.Core.Action(this.components);
            this.organizeWithRegions = new DevExpress.CodeRush.Core.Action(this.components);
            this.organizeWORegions = new DevExpress.CodeRush.Core.Action(this.components);
            this.cutCurrentMember = new DevExpress.CodeRush.Core.Action(this.components);
            this.selectCurrentMember = new DevExpress.CodeRush.Core.Action(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.removeRegionsAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.removeWhitespaceAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.organizeWithRegions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.organizeWORegions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cutCurrentMember)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectCurrentMember)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // removeRegionsAction
            // 
            this.removeRegionsAction.ActionName = "RemoveRegions";
            this.removeRegionsAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.removeRegionsAction.Description = "Removes the regions in the current class";
            this.removeRegionsAction.Image = ((System.Drawing.Bitmap)(resources.GetObject("removeRegionsAction.Image")));
            this.removeRegionsAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.removeRegionsAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.ExecuteRemoveRegion);
            // 
            // removeWhitespaceAction
            // 
            this.removeWhitespaceAction.ActionName = "RemoveWhitespace";
            this.removeWhitespaceAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.removeWhitespaceAction.Description = "Removes excess whitespace in the current class";
            this.removeWhitespaceAction.Image = ((System.Drawing.Bitmap)(resources.GetObject("removeWhitespaceAction.Image")));
            this.removeWhitespaceAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.removeWhitespaceAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.ExecuteRemoveWhitespace);
            // 
            // organizeWithRegions
            // 
            this.organizeWithRegions.ActionName = "OrganizeWithRegions";
            this.organizeWithRegions.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.organizeWithRegions.Description = "Organizes the current class";
            this.organizeWithRegions.Image = ((System.Drawing.Bitmap)(resources.GetObject("organizeWithRegions.Image")));
            this.organizeWithRegions.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.organizeWithRegions.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.ExecuteOrganizeWithRegions);
            // 
            // organizeWORegions
            // 
            this.organizeWORegions.ActionName = "OrganizeWORegions";
            this.organizeWORegions.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.organizeWORegions.Description = "Organizes the current class with out adding regions";
            this.organizeWORegions.Image = ((System.Drawing.Bitmap)(resources.GetObject("organizeWORegions.Image")));
            this.organizeWORegions.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.organizeWORegions.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.ExecuteOrganizeWORegions);
            // 
            // cutCurrentMember
            // 
            this.cutCurrentMember.ActionName = "CutCurrentMember";
            this.cutCurrentMember.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.cutCurrentMember.Description = "Cut the current member that the caret is currently in.";
            this.cutCurrentMember.Image = ((System.Drawing.Bitmap)(resources.GetObject("cutCurrentMember.Image")));
            this.cutCurrentMember.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.cutCurrentMember.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.ExecuteCutCurrentMember);
            // 
            // selectCurrentMember
            // 
            this.selectCurrentMember.ActionName = "SelectCurrentMember";
            this.selectCurrentMember.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.selectCurrentMember.Description = "Select the current member that the caret is currently in.";
            this.selectCurrentMember.Image = ((System.Drawing.Bitmap)(resources.GetObject("selectCurrentMember.Image")));
            this.selectCurrentMember.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.selectCurrentMember.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.ExecuteSelectCurrentMember);
            ((System.ComponentModel.ISupportInitialize)(this.removeRegionsAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.removeWhitespaceAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.organizeWithRegions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.organizeWORegions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cutCurrentMember)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectCurrentMember)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.Action removeRegionsAction;
        private DevExpress.CodeRush.Core.Action removeWhitespaceAction;
        private DevExpress.CodeRush.Core.Action organizeWithRegions;
        private DevExpress.CodeRush.Core.Action organizeWORegions;
        private DevExpress.CodeRush.Core.Action cutCurrentMember;
        private DevExpress.CodeRush.Core.Action selectCurrentMember;




    }
}