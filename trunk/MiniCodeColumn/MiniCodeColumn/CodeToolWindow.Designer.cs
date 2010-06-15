namespace MiniCodeColumn
{
    [System.Runtime.InteropServices.Guid("408b0e9d-8c1d-4c61-8223-550f9242877b")]
    partial class CodeToolWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DevExpress.DXCore.PlugInCore.DXCoreEvents events;

        public CodeToolWindow()
        {
            // Required for Windows.Forms Class Composition Designer support
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
            this.events = new DevExpress.DXCore.PlugInCore.DXCoreEvents(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.events)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // events
            // 
            this.events.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.events_OptionsChanged);
            this.events.MarkerCollected += new DevExpress.CodeRush.Core.MarkerEventHandler(this.events_MarkerCollected);
            this.events.EditorIdle += new DevExpress.CodeRush.Core.EditorIdleEventHandler(this.events_EditorIdle);
            this.events.EditorMouseDoubleClick += new DevExpress.CodeRush.Core.EditorMouseEventHandler(this.events_EditorMouseDoubleClick);
            this.events.EditorScrolled += new DevExpress.CodeRush.Core.EditorScrolledEventHandler(this.events_EditorScrolled);
            this.events.TextViewActivated += new DevExpress.CodeRush.Core.TextViewHandler(this.events_TextViewActivated);
            this.events.TextChanged += new DevExpress.CodeRush.Core.TextChangedEventHandler(this.events_TextChanged);
            this.events.EditorPaint += new DevExpress.CodeRush.Core.EditorPaintEventHandler(this.events_EditorPaint);
            // 
            // CodeToolWindow
            // 
            this.BackColor = System.Drawing.Color.White;
            this.DoubleBuffered = true;
            this.Image = global::MiniCodeColumn.Properties.Resources.Button;
            this.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Name = "CodeToolWindow";
            this.Size = new System.Drawing.Size(111, 264);
            this.MouseLeave += new System.EventHandler(this.CodeToolWindow_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CodeToolWindow_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CodeToolWindow_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CodeToolWindow_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.events)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #region ShowWindow
        ///
        /// Displays this tool window.
        ///
        public static EnvDTE.Window ShowWindow()
        {
            return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Show(typeof(CodeToolWindow).GUID);
        }
        #endregion
        #region HideWindow
        ///
        /// Hides this tool window.
        ///
        public static EnvDTE.Window HideWindow()
        {
            return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Hide(typeof(CodeToolWindow).GUID);
        }
        #endregion
    }
}