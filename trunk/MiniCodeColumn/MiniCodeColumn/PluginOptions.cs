using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using System.Collections.Generic;

namespace MiniCodeColumn
{
    [UserLevel(UserLevel.NewUser)]
    public partial class PluginOptions : OptionsPage
    {
        public static bool MiniCodeColumnEnabled = true;
        public static bool WordDoubleClickEnabled = true;
        // public static int ColumnWidth = 40;

        public static Color ColumnBackgroundColor;
        public static Color ColumnVisibleLinesColor;

        public static Color CodeColorNormalLine;
        public static Color CodeColorCommentLine;

        public static Color ColumnBackgroundColorSelectedWord;
        public static Color CodeColorSelectedWord;

        public static Color BreakPointColor;


        // DXCore-generated code...
        #region Initialize
        protected override void Initialize()
        {
            base.Initialize();

            LoadSettings();
            UpdateOptionsDialog();
        }
        #endregion

        #region GetCategory  "Editor\Painting"
        public static string GetCategory()
        {
            return @"Editor\Painting";
        }
        #endregion
        #region GetPageName  "Mini Code Column"
        public static string GetPageName()
        {
            return @"Mini Code Column";
        }
        #endregion

        public static void LoadSettings()
        {
            try
            {
                DecoupledStorage store = CodeRush.Options.GetStorage(GetCategory(), GetPageName());
                if (store != null)
                {
                    MiniCodeColumnEnabled = store.ReadBoolean("Config", "Enabled", true);
                    WordDoubleClickEnabled = store.ReadBoolean("Config", "WordDoubleClickEnabled", true);

                    //ColumnWidth = store.ReadInt32("Config", "ColumnWidth", 40);

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
                    BreakPointColor = Color.FromArgb(
                        store.ReadInt32(
                                        "Config",
                                        "BreakPointColor",
                                        Color.FromArgb(90, Color.Red).ToArgb()
                                       ));
                }
            }
            catch
            {
            }
        }
        //Moved out of Initialize region as I couldn't find it to start with, if it is in a region
        //probably should have the Read and Update in the same region
        protected void UpdateOptionsDialog()
        {
            chkEnabled.Checked = MiniCodeColumnEnabled; //AussieALF: Added so it shows the saved state
            chkWordHighlight.Checked = WordDoubleClickEnabled; //AussieALF: Added so it shows the saved state
            
            // trackWidth.Value = 110 - ColumnWidth;
            lblWidth.Text = string.Format("{0} Pixel", 110 - trackWidth.Value);

            btnBackColor.BackColor = ColumnBackgroundColor;
            btnBackColor.Tag = trackBackColor;
            trackBackColor.Value = ColumnVisibleLinesColor.A;
            trackBackColor.Tag = btnBackColor;

            btnVisibleRangeColor.BackColor = ColumnVisibleLinesColor;
            btnVisibleRangeColor.Tag = trackVisibleRangeColor;
            trackVisibleRangeColor.Value = ColumnVisibleLinesColor.A;
            trackVisibleRangeColor.Tag = btnVisibleRangeColor;

            btnLineColor.BackColor = CodeColorNormalLine;
            btnLineColor.Tag = trackLineColor;
            trackLineColor.Value = CodeColorNormalLine.A;
            trackLineColor.Tag = btnLineColor;

            btnCommentColor.BackColor = CodeColorCommentLine;
            btnCommentColor.Tag = trackCommentColor;
            trackCommentColor.Value = CodeColorCommentLine.A;
            trackCommentColor.Tag = btnCommentColor;

            btnColumnBackgroundColorSelectedWord.BackColor = ColumnBackgroundColorSelectedWord;
            btnColumnBackgroundColorSelectedWord.Tag = trackColumnBackgroundColorSelectedWord;
            trackColumnBackgroundColorSelectedWord.Value = ColumnBackgroundColorSelectedWord.A;
            trackColumnBackgroundColorSelectedWord.Tag = btnColumnBackgroundColorSelectedWord;

            btnCodeColorSelectedWord.BackColor = CodeColorSelectedWord;
            btnCodeColorSelectedWord.Tag = trackCodeColorSelectedWord;
            trackCodeColorSelectedWord.Value = CodeColorSelectedWord.A;
            trackCodeColorSelectedWord.Tag = btnCodeColorSelectedWord;
        }
        public void ReadOptionsFromDialog()
        {
            try
            {
                // ColumnWidth = 110 - trackWidth.Value;
                MiniCodeColumnEnabled = chkEnabled.Checked; //AussieALF: Added so it saves the updated option
                WordDoubleClickEnabled = chkWordHighlight.Checked; //AussieALF: Added so it saves the updated option
                ColumnBackgroundColor = btnBackColor.BackColor;
                ColumnVisibleLinesColor = btnVisibleRangeColor.BackColor;
                CodeColorNormalLine = btnLineColor.BackColor;
                CodeColorCommentLine = btnCommentColor.BackColor;
                ColumnBackgroundColorSelectedWord = btnColumnBackgroundColorSelectedWord.BackColor;
                CodeColorSelectedWord = btnCodeColorSelectedWord.BackColor;

                CodeToolWindow.DisposeGraphicElements();
                panelSample.Refresh();
            }
            catch
            {
            }
        }

        public static void SaveSettings()
        {
            try
            {
                DecoupledStorage store = CodeRush.Options.GetStorage(GetCategory(), GetPageName());
                if (store != null)
                {
                    store.WriteBoolean("Config", "Enabled", MiniCodeColumnEnabled);
                    store.WriteBoolean("Config", "WordDoubleClickEnabled", WordDoubleClickEnabled);

                    // store.WriteInt32("Config", "ColumnWidth", ColumnWidth);

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
            catch
            {
            }
        }

        private void trackWidth_Scroll(object sender, EventArgs e)
        {
            lblWidth.Text = string.Format("{0} Pixel",110 - trackWidth.Value);
            if (sender!=null)
                ReadOptionsFromDialog();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            int A = ((Button)sender).BackColor.A;
            using (ColorDialog dlg = new ColorDialog { AnyColor = true, FullOpen = true, Color = ((Button)sender).BackColor })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ((Button)sender).BackColor = Color.FromArgb(A,  dlg.Color);
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
            ReadOptionsFromDialog(); //AussieALF: Added this so it gets the up to date settings before saving.
            SaveSettings();
        }

        private void btnResetSettings_Click(object sender, EventArgs e)
        {
            DecoupledStorage store = CodeRush.Options.GetStorage(GetCategory(), GetPageName());
            if (store != null)
            {
                store.EraseSection("Config");
            }
            LoadSettings();
            UpdateOptionsDialog();
            ReadOptionsFromDialog();
            trackWidth_Scroll(null, null);
        }

        private void panelSample_Paint(object sender, PaintEventArgs e)
        {
            CodeToolWindow.CreateGraphicElements();
            Graphics graphics = e.Graphics;

            Rectangle rect = new Rectangle(0, sampleHeader.Height, panelSample.Width, panelSample.Height - sampleHeader.Height);

            //SmoothingMode oldMode = graphics.SmoothingMode;
            try
            {
                // alle Zeilen holen
                List<Line> items = Line.SampleLines;

                graphics.FillRectangle(CodeToolWindow.ColumnBackgroundBrushCodeColumn, rect);


                int width_divisor = 2;
                // falls die Höhe nicht reicht, Teiler ermitteln
                int height_divisor = 1;

                // den sichtbaren Bereich markieren
                Rectangle visible_rect = new Rectangle(
                    rect.X, rect.Y + (6 / height_divisor),
                    panelSample.Width, (26 - 6) / height_divisor);
                graphics.FillRectangle(CodeToolWindow.ColumnBrushVisibleLines, visible_rect);


                int left = rect.X;
                int start = 0;
                int end = 0;
                for (int l = 0; l < items.Count; l++)
                {
                    Line line = items[l];

                    int y = l / height_divisor + rect.Y;
                    start = line.Start / width_divisor;
                    if (start > panelSample.Width)
                        start = panelSample.Width - 6;
                    end = line.End / width_divisor;
                    if (end > panelSample.Width)
                        end = panelSample.Width;

                    int start_of_comment = line.StartOfComment;
                    int end_of_comment = -2;
                    if (start_of_comment >= 0)
                    {
                        start_of_comment = start_of_comment / width_divisor;
                        end_of_comment = line.EndOfComment / width_divisor;
                        end = start_of_comment - 1;
                    }

                    if (start_of_comment < end_of_comment)
                        graphics.DrawLine(CodeToolWindow.CodePenCommentLine, new Point(left + start_of_comment, y), new Point(left + end_of_comment, y));

                    if (start < end)
                        graphics.DrawLine(CodeToolWindow.CodePenNormalLine, new Point(left + start, y), new Point(left + end, y));
                }

                int selected_double_click_length = 10;
                if (selected_double_click_length > 2)
                {
                    for (int l = 0; l < items.Count; l++)
                    {
                        Line line = items[l];
                        int y = l / height_divisor + rect.Y;
                        start = 0;
                        end = 0;

                        if (line.HasWord)
                        {
                            int start_index = line.StartOfWords[0];
                            start = start_index / width_divisor;
                            if (start > panelSample.Width)
                                start = panelSample.Width - 6;
                            end = (start_index + selected_double_click_length) / width_divisor;
                            if (end > panelSample.Width)
                                end = panelSample.Width;
                            graphics.DrawLine(
                                CodeToolWindow.CodePenSelectedWord,
                                new Point(left + start, y),
                                new Point(left + end, y));
                        }
                    }
                }
            }
            catch
            {
            }
        }


    }
}