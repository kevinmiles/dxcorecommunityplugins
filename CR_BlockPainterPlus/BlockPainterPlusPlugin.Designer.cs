namespace CR_BlockPainterPlus
{
    partial class BlockPainterPlusPlugin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public BlockPainterPlusPlugin()
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
            this.sourceRangeHighlight = new DevExpress.CodeRush.PlugInCore.SourceRangeHighlight(this.components);
            this.animatedArrow = new DevExpress.CodeRush.PlugInCore.AnimatedArrow(this.components);
            this.actionHint = new DevExpress.CodeRush.PlugInCore.ActionHint(this.components);
            this.locatorBeacon = new DevExpress.CodeRush.PlugInCore.LocatorBeacon(this.components);
            this.animationFrame1 = new DevExpress.CodeRush.PlugInCore.AnimationFrame(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.sourceRangeHighlight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.animatedArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionHint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locatorBeacon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.animationFrame1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sourceRangeHighlight
            // 
            this.sourceRangeHighlight.Duration = 1000;
            this.sourceRangeHighlight.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            // 
            // animatedArrow
            // 
            this.animatedArrow.BorderColor = System.Drawing.Color.Empty;
            this.animatedArrow.BorderOpacity = 0;
            this.animatedArrow.FillColor = System.Drawing.Color.Empty;
            this.animatedArrow.FillOpacity = 0;
            this.animatedArrow.PixelsPerSecond = 200;
            this.animatedArrow.StopAnimationOnOutOfBounds = false;
            // 
            // actionHint
            // 
            this.actionHint.Color = System.Drawing.Color.DarkGray;
            this.actionHint.Feature = null;
            this.actionHint.OptionsPath = null;
            this.actionHint.ResetDisplayCountOnStartup = false;
            this.actionHint.Text = "test";
            // 
            // locatorBeacon
            // 
            this.locatorBeacon.Color = System.Drawing.Color.SlateBlue;
            // 
            // BlockPainterPlusPlugin
            // 
            this.EditorPaintBackground += new DevExpress.CodeRush.Core.EditorPaintEventHandler(this.BlockPainterPlusPlugin_EditorPaintBackground);
            this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.BlockPainterPlusPlugin_OptionsChanged);
            this.EditorPaintLanguageElement += new DevExpress.CodeRush.Core.EditorPaintLanguageElementEventHandler(this.BlockPainterPlusPlugin_EditorPaintLanguageElement);
            ((System.ComponentModel.ISupportInitialize)(this.sourceRangeHighlight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.animatedArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionHint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locatorBeacon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.animationFrame1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.PlugInCore.SourceRangeHighlight sourceRangeHighlight;
        private DevExpress.CodeRush.PlugInCore.AnimatedArrow animatedArrow;
        private DevExpress.CodeRush.PlugInCore.ActionHint actionHint;
        private DevExpress.CodeRush.PlugInCore.LocatorBeacon locatorBeacon;
        private DevExpress.CodeRush.PlugInCore.AnimationFrame animationFrame1;
    }
}