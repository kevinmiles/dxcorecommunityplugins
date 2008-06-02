namespace Refactor_Generalize
{
    partial class RefactorGeneralizePlugin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RefactorGeneralizePlugin()
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
            DevExpress.CodeRush.Core.InsertionPoint insertionPoint1 = new DevExpress.CodeRush.Core.InsertionPoint();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RefactorGeneralizePlugin));
            this.codeProvider = new DevExpress.CodeRush.Core.CodeProvider(this.components);
            this.targetPicker = new DevExpress.CodeRush.PlugInCore.TargetPicker(this.components);
            this.action = new DevExpress.CodeRush.Core.Action(this.components);
            this.actionHint = new DevExpress.CodeRush.PlugInCore.ActionHint(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.codeProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.action)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionHint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // codeProvider
            // 
            this.codeProvider.ActionHintText = "Generalize";
            this.codeProvider.AutoActivate = true;
            this.codeProvider.AutoUndo = false;
            this.codeProvider.Description = "Generalizes, or \"pushes\", the member up to its base class.";
            this.codeProvider.DisplayName = "Generalize";
            this.codeProvider.ProviderName = "Generalize";
            this.codeProvider.Register = true;
            this.codeProvider.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.codeProvider_Apply);
            this.codeProvider.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.codeProvider_CheckAvailability);
            // 
            // targetPicker
            // 
            this.targetPicker.BigHint = null;
            insertionPoint1.ArrowFillColor = System.Drawing.Color.Red;
            insertionPoint1.ArrowFillOpacity = 30;
            insertionPoint1.ArrowLineColor = System.Drawing.Color.Red;
            insertionPoint1.LineColor = System.Drawing.Color.Red;
            insertionPoint1.LineOpacity = 200;
            this.targetPicker.InsertionPoint = insertionPoint1;
            this.targetPicker.IsModalMode = false;
            this.targetPicker.ShortcutsHint = null;
            this.targetPicker.Canceled += new System.EventHandler(this.targetPicker_Canceled);
            this.targetPicker.TargetSelected += new DevExpress.CodeRush.PlugInCore.TargetSelectedEventHandler(this.targetPicker_TargetSelected);
            // 
            // action
            // 
            this.action.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this.action.Image = ((System.Drawing.Bitmap)(resources.GetObject("action.Image")));
            this.action.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
            this.action.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.action_Execute);
            // 
            // actionHint
            // 
            this.actionHint.Color = System.Drawing.Color.DarkGray;
            this.actionHint.Feature = null;
            this.actionHint.OptionsPath = null;
            this.actionHint.ResetDisplayCountOnStartup = false;
            this.actionHint.Text = null;
            ((System.ComponentModel.ISupportInitialize)(this.codeProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.action)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionHint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.CodeProvider codeProvider;
        private DevExpress.CodeRush.PlugInCore.TargetPicker targetPicker;
        private DevExpress.CodeRush.Core.Action action;
        private DevExpress.CodeRush.PlugInCore.ActionHint actionHint;
    }
}