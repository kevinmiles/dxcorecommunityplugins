namespace RedGreen
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
            this.actBuildProject = new DevExpress.CodeRush.Core.Action(this.components);
            this.testActions = new DevExpress.CodeRush.Core.SmartTagProvider(this.components);
            this.actRunTests = new DevExpress.CodeRush.Core.Action(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.actBuildProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actRunTests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // actBuildProject
            // 
            this.actBuildProject.ActionName = "Build";
            this.actBuildProject.ButtonText = "Build";
            this.actBuildProject.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.EditorContext;
            this.actBuildProject.Description = "Build the current project";
            this.actBuildProject.Image = ((System.Drawing.Bitmap)(resources.GetObject("actBuildProject.Image")));
            this.actBuildProject.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.actBuildProject.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actBuildProject_Execute);
            // 
            // testActions
            // 
            this.testActions.Description = "Actions available for Test";
            this.testActions.DisplayName = "Test";
            this.testActions.MenuOrder = 0;
            this.testActions.ProviderName = "UnitTest";
            this.testActions.Register = true;
            this.testActions.ShowInContextMenu = false;
            this.testActions.ShowInPopupMenu = true;
            this.testActions.GetSmartTagItemColors += new DevExpress.CodeRush.Core.GetSmartTagItemColorsEventHandler(this.testActions_GetSmartTagItemColors);
            this.testActions.CheckSmartTagAvailability += new DevExpress.CodeRush.Core.CheckSmartTagAvailabilityEventHandler(this.testActions_CheckSmartTagAvailability);
            this.testActions.GetSmartTagItems += new DevExpress.CodeRush.Core.GetSmartTagItemsEventHandler(this.testActions_GetSmartTagItems);
            // 
            // actRunTests
            // 
            this.actRunTests.ActionName = "Run Test(s)";
            this.actRunTests.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.actRunTests.Image = ((System.Drawing.Bitmap)(resources.GetObject("actRunTests.Image")));
            this.actRunTests.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.actRunTests.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actRunTests_Execute);
            // 
            // PlugIn1
            // 
            this.TileMouseEnter += new DevExpress.CodeRush.Core.TileEventHandler(this.PlugIn1_TileMouseEnter);
            this.TileSetCursor += new DevExpress.CodeRush.Core.TileSetCursorEventHandler(this.PlugIn1_TileSetCursor);
            this.EditorValidateLanguageElementClipRegion += new DevExpress.CodeRush.Core.EditorValidateLanguageElementClipRegionEventHandler(this.PlugIn1_EditorValidateLanguageElementClipRegion);
            this.EditorMouseHover += new DevExpress.CodeRush.Core.EditorEventHandler(this.PlugIn1_EditorMouseHover);
            this.SolutionOpened += new DevExpress.CodeRush.Core.DefaultHandler(this.PlugIn1_SolutionOpened);
            this.BuildDone += new DevExpress.CodeRush.Core.BuildEventHandler(this.PlugIn1_BuildDone);
            this.EditorPaintLanguageElement += new DevExpress.CodeRush.Core.EditorPaintLanguageElementEventHandler(this.PlugIn1_EditorPaintLanguageElement);
            this.TileMouseLeave += new DevExpress.CodeRush.Core.TileEventHandler(this.PlugIn1_TileMouseLeave);
            this.TextChanged += new DevExpress.CodeRush.Core.TextChangedEventHandler(this.PlugIn1_TextChanged);
            this.LanguageElementActivated += new DevExpress.CodeRush.Core.LanguageElementActivatedEventHandler(this.PlugIn1_LanguageElementActivated);
            ((System.ComponentModel.ISupportInitialize)(this.actBuildProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testActions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actRunTests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.Action actBuildProject;
        private DevExpress.CodeRush.Core.SmartTagProvider testActions;
        private DevExpress.CodeRush.Core.Action actRunTests;

    }
}