using System;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;

namespace MiniCodeColumn
{
    #region plugin - starts Tool-Window now
    // only remove button !
    public class MiniCodeColPlugIn : StandardPlugIn
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

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // MiniCodeColPlugIn
            // 
            // this.TextViewActivated += new DevExpress.CodeRush.Core.TextViewHandler(this.MiniCodeColPlugIn_TextViewActivated);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        private void MiniCodeColPlugIn_TextViewActivated(TextViewEventArgs ea)
        {
            //if (!CodeToolWindow.OnAir)
            //    try
            //    {
            //        CodeRush.ToolWindows.Show(typeof(CodeToolWindow));
            //    }
            //    catch
            //    {
            //    }

        }
    }
    #endregion
}
