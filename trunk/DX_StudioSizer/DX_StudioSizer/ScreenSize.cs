using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Menus;
using System.Collections.Generic;
using DevExpress.CodeRush.Win32;

namespace DX_StudioSizer
{
    public class ScreenSize
    {
        #region Fields
        private int _Width;
        private int _Height;
        #endregion
        #region Simple Properties
        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        #endregion
        public ScreenSize(string Spec)
        {
            var split = Spec.Split("xX".ToCharArray());
            _Width = int.Parse(split[0]);
            _Height = int.Parse(split[1]);
        }
        public ScreenSize(int Width, int Height)
        {
            _Width = Width;
            _Height = Height;
        }
        public void ChangeSize_Click(object sender, MenuButtonClickEventArgs e)
        {
            var handle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            NativeMethods.SetWindowPos(handle, Constants.HWND_TOP, 0, 0, _Width, _Height, 0);
        }
    }
}
