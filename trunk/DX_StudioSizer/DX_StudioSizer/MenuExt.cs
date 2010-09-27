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

namespace DX_StudioSizer
{
    public static class MenuExt
    {

        public static IMenuButton CreateAndAddButton(this IMenuContainer LinkBar, string Caption, string Description = "")
        {
            if (Description == String.Empty)
                Description = Caption;
            var Control = LinkBar.AddButton();
            Control.Caption = Caption;
            Control.Visible = true;
            Control.Enabled = true;
            Control.DescriptionText = Description;
            return Control;
        }
        public static IMenuPopup CreateAndAddDropDownButton(this IMenuContainer LinkBar, string Caption, string Description = "")
        {
            if (Description == string.Empty)
                Description = Caption;
            var Control = LinkBar.AddPopup();
            Control.Caption = Caption;
            Control.Visible = true;
            Control.Enabled = true;
            Control.DescriptionText = Description;
            return Control;
        }

    }
}
