using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace MiniCodeColumn
{
    [UserLevel(UserLevel.NewUser)]
    public partial class PluginOptions : OptionsPage
    {
        public static bool MiniCodeColumnEnabled = true;
        public static bool WordDoubleClickEnabled = true;
        public static int ColumnWidth = 40;

        public static Color ColumnBackgroundColor;
        public static Color ColumnVisibleLinesColor;

        public static Color CodeColorNormalLine;
        public static Color CodeColorCommentLine;

        public static Color ColumnBackgroundColorSelectedWord;
        public static Color CodeColorSelectedWord;


        // DXCore-generated code...
        #region Initialize
        protected override void Initialize()
        {
            base.Initialize();

            LoadSettings();
            UpdateOptionsDialog();
        }

        protected void UpdateOptionsDialog()
        {
            trackWidth.Value = ColumnWidth;

            btnBackColor.BackColor = ColumnBackgroundColor;
            trackBackColor.Value = ColumnVisibleLinesColor.A;
            trackBackColor.Tag = btnBackColor;

            btnVisibleRangeColor.BackColor = ColumnVisibleLinesColor;
            trackVisibleRangeColor.Value = ColumnVisibleLinesColor.A;
            trackVisibleRangeColor.Tag = btnVisibleRangeColor;

            btnLineColor.BackColor = CodeColorNormalLine;
            trackLineColor.Value = CodeColorNormalLine.A;
            trackLineColor.Tag = btnLineColor;

            btnCommentColor.BackColor = CodeColorCommentLine;
            trackCommentColor.Value = CodeColorCommentLine.A;
            trackCommentColor.Tag = btnCommentColor;

            btnColumnBackgroundColorSelectedWord.BackColor = ColumnBackgroundColorSelectedWord;
            trackColumnBackgroundColorSelectedWord.Value = ColumnBackgroundColorSelectedWord.A;
            trackColumnBackgroundColorSelectedWord.Tag = btnColumnBackgroundColorSelectedWord;

            btnCodeColorSelectedWord.BackColor = CodeColorSelectedWord;
            trackCodeColorSelectedWord.Value = CodeColorSelectedWord.A;
            trackCodeColorSelectedWord.Tag = btnCodeColorSelectedWord;
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
            return @"Mini Code Column";
        }
        #endregion

        public static void LoadSettings()
        {
            DecoupledStorage store = DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName());
            if (store != null)
            {
                MiniCodeColumnEnabled = store.ReadBoolean("Config", "Enabled", true);
                WordDoubleClickEnabled = store.ReadBoolean("Config", "WordDoubleClickEnabled", true);

                ColumnWidth = store.ReadInt32("Config", "ColumnWidth", 40);

                ColumnBackgroundColor = Color.FromArgb(
                    store.ReadInt32(
                                    "Config", 
                                    "ColumnBackgroundColor", 
                                    CodeRush.Color.VSLight.ToArgb()
                                   ));
                ColumnBackgroundColorSelectedWord = Color.FromArgb(
                    store.ReadInt32(
                                    "Config",
                                    "ColumnBackgroundColorSelectedWord",
                                    Color.FromArgb(40, Color.Blue).ToArgb()
                                   ));
                ColumnVisibleLinesColor = Color.FromArgb(
                    store.ReadInt32(
                                    "Config",
                                    "ColumnVisibleLinesColor",
                                    Color.FromArgb(70, Color.DarkBlue).ToArgb()
                                   ));

                CodeColorNormalLine = Color.FromArgb(
                    store.ReadInt32(
                                    "Config",
                                    "CodeColorNormalLine",
                                    Color.FromArgb(70, Color.Black).ToArgb()
                                   ));
                CodeColorSelectedWord = Color.FromArgb(
                    store.ReadInt32(
                                    "Config",
                                    "CodeColorPenSelectedWord",
                                    Color.FromArgb(100, Color.Red).ToArgb()
                                   ));
                CodeColorCommentLine = Color.FromArgb(
                    store.ReadInt32(
                                    "Config",
                                    "CodeColorCommentLine",
                                    Color.FromArgb(70, Color.Green).ToArgb()
                                   ));
            }
        }

        public void ReadOptionsFromDialog()
        {
            ColumnWidth = trackWidth.Value;

            ColumnBackgroundColor = btnBackColor.BackColor;
            ColumnVisibleLinesColor = btnVisibleRangeColor.BackColor;
            CodeColorNormalLine = btnLineColor.BackColor;
            CodeColorCommentLine = btnCommentColor.BackColor;
            ColumnBackgroundColorSelectedWord = btnColumnBackgroundColorSelectedWord.BackColor;
            CodeColorSelectedWord = btnCodeColorSelectedWord.BackColor;
        }

        public static void SaveSettings()
        {
            DecoupledStorage store = DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName());
            if (store != null)
            {
                store.WriteBoolean("Config", "Enabled", MiniCodeColumnEnabled);
                store.WriteBoolean("Config", "WordDoubleClickEnabled", WordDoubleClickEnabled);

                store.WriteInt32("Config", "ColumnWidth", ColumnWidth);

                store.WriteInt32("Config", "ColumnBackgroundColor", 
                                    ColumnBackgroundColor.ToArgb());
                store.WriteInt32("Config", "ColumnBackgroundColorSelectedWord",
                                    ColumnBackgroundColorSelectedWord.ToArgb());
                store.WriteInt32("Config", "ColumnVisibleLinesColor",
                                    ColumnVisibleLinesColor.ToArgb());

                store.WriteInt32("Config", "CodeColorNormalLine",
                                    CodeColorNormalLine.ToArgb());
                store.WriteInt32("Config", "CodeColorPenSelectedWord",
                                    CodeColorSelectedWord.ToArgb());
                store.WriteInt32("Config", "CodeColorCommentLine",
                                    CodeColorCommentLine.ToArgb());
            }
        }

        private void trackWidth_Scroll(object sender, EventArgs e)
        {
            lblWidth.Text = string.Format("{0} Pixel", trackWidth.Value);
            ReadOptionsFromDialog();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog { AnyColor = true, FullOpen = true, Color = ((Button)sender).BackColor })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ((Button)sender).BackColor = dlg.Color;
                    ReadOptionsFromDialog();
                }
            }
        }

        private void trackBackColor_Scroll(object sender, EventArgs e)
        {
            TrackBar tracker = ((TrackBar)sender);
            Button btn = tracker.Tag as Button;
            if (btn == null)
                return;

            btn.BackColor = Color.FromArgb(
                    tracker.Value,
                    btn.BackColor.R, btn.BackColor.G, btn.BackColor.B);
            ReadOptionsFromDialog();
        }

        private void PluginOptions_CommitChanges(object sender, CommitChangesEventArgs ea)
        {
            SaveSettings();
        }

        private void btnResetSettings_Click(object sender, EventArgs e)
        {
            DecoupledStorage store = DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName());
            if (store != null)
            {
                store.Clear();
            }
            LoadSettings();
            UpdateOptionsDialog();
        }

        private void panelSample_Paint(object sender, PaintEventArgs e)
        {
            
        }


    }
}