using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using System.Collections.Generic;
using DevExpress.CodeRush.UserControls;

namespace CR_Colorizer
{
    [UserLevel(UserLevel.NewUser)]
    public partial class Colorizer_Options : OptionsPage
    {
        private Dictionary<string, ColorSwatch> _colors = new Dictionary<string,ColorSwatch>();
        private static Color _DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
        public static List<string> SupportedElements = new List<string>
            {
                "Public Methods",
                "Private Methods",
                "Fields",
                "Public Properties",
                "Private Properties",
                "Local Fields",
                "Parameters"
            };

        // DXCore-generated code...
        #region Initialize
        protected override void Initialize()
        {
            base.Initialize();
            var baseLabelY = 23;
            var baseCheckboxY = 20;
            var labelYOffset = 20;
            var baseSwatchY = 20;
            foreach (string element in SupportedElements)
            {
                var index = SupportedElements.IndexOf(element);
                var label = new System.Windows.Forms.Label
                {
                    AutoSize = true,
                    Location = new Point(3, baseLabelY + (index * labelYOffset)),
                    Name = "lbl" + element.Replace(" ", ""),
                    Text = element + ":"
                };
                this.Controls.Add(label);
                var checkbox = new CheckBox() { Text = "Enabled", Checked = true };
                checkbox.Location = new Point(250, baseCheckboxY + (index * labelYOffset));
                var swatch = new ColorSwatch
                {
                    Color = _DefaultColor,
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    DropDownOnMouseHover = false,
                    InternalCloseOnLostFocus = true,
                    InternalCloseOnOuterMouseClick = true,
                    Location = new Point(105, baseSwatchY + (index * labelYOffset)),
                    Name = "cs" + element.Replace(" ", ""),
                    RightToLeft = System.Windows.Forms.RightToLeft.Yes,
                    ShowColorName = false,
                    Size = new System.Drawing.Size(137, 20),
                    ToolTip = null,
                    Tag = checkbox
                };
                this._colors.Add(element, swatch);
                this.Controls.Add(swatch);
                this.Controls.Add(checkbox);
            }
        }
        #endregion

        #region GetCategory
        public static string GetCategory()
        {
            return @"Editor\Painting";
        }
        #endregion
        #region GetPageName
        public static string GetPageName()
        {
            return @"Colorizer";
        }
        #endregion

        private void Colorizer_Options_PreparePage(object sender, OptionsPageStorageEventArgs ea)
        {
            using (var storage = ea.Storage)
            {
                this.checkBox1.Checked = storage.ReadBoolean("Settings", "Enabled", false);
                foreach (string element in SupportedElements)
                {
                    _colors[element].Color = storage.ReadColor("Settings", element, _DefaultColor);
                    ((CheckBox)_colors[element].Tag).Checked = storage.ReadBoolean("Settings", element + "Enabled", true);
                }
            }
        }

        private void Colorizer_Options_CommitChanges(object sender, CommitChangesEventArgs ea)
        {
            using (var storage = ea.Storage)
            {
                storage.WriteBoolean("Settings", "Enabled", checkBox1.Checked);
                foreach (var element in SupportedElements)
                {
                    storage.WriteColor("Settings", element, _colors[element].Color);
                    storage.WriteBoolean("Settings", element + "Enabled", ((CheckBox)_colors[element].Tag).Checked);
                }
            }
            if (UpdateCurrentDocument != null)
                UpdateCurrentDocument(this,EventArgs.Empty);
        }

        internal static Color GetColorFor(string element)
        {
            using (var storage = Colorizer_Options.Storage)
            {
                return storage.ReadColor("Settings", element, _DefaultColor);
            }
        }

        internal static bool IsEnabled()
        {
            using (var storage = Colorizer_Options.Storage)
            {
                return storage.ReadBoolean("Settings", "Enabled", false);
            }
        }

        internal static event EventHandler UpdateCurrentDocument;

        public static bool ElementIsEnabled(string element)
        {
            using (var storage = Colorizer_Options.Storage)
            {
                return storage.ReadBoolean("Settings", element + "Enabled", true);
            }
        }

    }
}