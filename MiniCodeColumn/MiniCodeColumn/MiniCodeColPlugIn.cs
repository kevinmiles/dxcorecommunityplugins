using System;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.Menus;

namespace MiniCodeColumn
{
    #region old plugin - will remove it's button only once
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
            DropVisualizeButton();
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

        void DropVisualizeButton()
        {
            try
            {
                if (CodeRush.Menus != null && CodeRush.Menus.Bars != null)
                {
                    foreach (MenuBar mb in CodeRush.Menus.Bars)
                    {
                        bool repeat = false;
                        do
                        {
                            repeat = false;
                            int index = -1;
                            if (mb.Name.ToUpperInvariant().IndexOf("DXCORE") >= 0)
                            {
                                foreach (IMenuControl menu_item in mb)
                                {
                                    System.Diagnostics.Debug.WriteLine(menu_item.Caption);
                                    if (menu_item.Caption == "Toggle Mini Code Column on/off")
                                        index = menu_item.Index;
                                    if (menu_item.Caption == "Mini Code Column")
                                        index = menu_item.Index;
                                }
                            }
                            if (index >= 0)
                            {
                                MessageBox.Show("MiniCodeColumn was moved into a Tool-Window\r\nfor better performance.\r\n\r\nStart with Menu DevExpress->Tool Windows->MiniCodeColumn\r\nand drag the tool window whereever you want.", "Dear user of MiniCodeColumn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mb.RemoveAt(index);     // Die Buttons wurden immer mehr !!!
                                repeat = true;
                            }
                        } while (repeat);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in DropVisualizeButton : " + ex.Message);
            }
        }
    }
    #endregion
}
