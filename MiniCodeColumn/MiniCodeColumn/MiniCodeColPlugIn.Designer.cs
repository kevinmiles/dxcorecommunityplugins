/*
        Copyright (C) 2009 by Ralf Warnat
        This software is provided "as is" without express or implied warranty of any kind.
        It is labeled as "Works on my machine".        
*/
namespace MiniCodeColumn
{
    partial class MiniCodeColPlugIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public MiniCodeColPlugIn()
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // MiniCodeColPlugIn
            // 
            this.EditorMouseDown += new DevExpress.CodeRush.Core.EditorMouseEventHandler(this.PlugIn1_EditorMouseDown);
            this.EditorPaintBackground += new DevExpress.CodeRush.Core.EditorPaintEventHandler(this.PlugIn1_EditorPaintBackground);
            this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.MiniCodeColPlugIn_OptionsChanged);
            this.EditorValidateClipRegion += new DevExpress.CodeRush.Core.EditorValidateClipRegionEventHandler(this.MiniCodeColPlugIn_EditorValidateClipRegion);
            this.TextDocumentDeactivated += new DevExpress.CodeRush.Core.TextDocumentEventHandler(this.MiniCodeColPlugIn_TextDocumentDeactivated);
            this.EditorMouseDoubleClick += new DevExpress.CodeRush.Core.EditorMouseEventHandler(this.PlugIn1_EditorMouseDoubleClick);
            this.TextDocumentActivated += new DevExpress.CodeRush.Core.TextDocumentEventHandler(this.MiniCodeColPlugIn_TextDocumentActivated);
            this.CaretMoved += new DevExpress.CodeRush.Core.CaretMovedEventHandler(this.PlugIn1_CaretMoved);
            this.EditorPaint += new DevExpress.CodeRush.Core.EditorPaintEventHandler(this.PlugIn1_EditorPaint);
            this.EditorScrolled += new DevExpress.CodeRush.Core.EditorScrolledEventHandler(this.PlugIn1_EditorScrolled);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

    }
}