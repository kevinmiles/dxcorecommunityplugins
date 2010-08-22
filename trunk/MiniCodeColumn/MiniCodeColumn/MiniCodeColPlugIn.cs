using System;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.Menus;

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
            try
            {
                CodeRush.ToolWindows.Show(typeof(CodeToolWindow));
            }
            catch 
            {                
            }
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
    }
    #endregion
}
