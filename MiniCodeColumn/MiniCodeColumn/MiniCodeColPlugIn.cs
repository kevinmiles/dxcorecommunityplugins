#region Copyright (C) 2009 by Ralf Warnat
/*
        Copyright (C) 2009 by Ralf Warnat
        This software is provided "as is" without express or implied warranty of any kind.
        It is labeled as "Works on my machine".        
*/
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Menus;
using System.Drawing.Drawing2D;

namespace MiniCodeColumn
{
    /// <summary>
    /// Plugin for visualizing complete codefile as small column left of the scrollbar
    /// </summary>
    public partial class MiniCodeColPlugIn : StandardPlugIn
    {
        int last_top_line = -1;
        IMenuButton _VisualizeButton;

        public int last_height_divisor = 1;

        internal static SolidBrush ColumnBackgroundBrushCodeColumn;
        internal static SolidBrush ColumnBackgroundBrushSelectedWord;
        internal static SolidBrush ColumnBrushVisibleLines;

        internal static Pen CodePenNormalLine;
        internal static Pen CodePenSelectedWord;
        internal static Pen CodePenCommentLine;

        string selected_double_click = "";

        Rectangle last_code_rect = new Rectangle();

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            CreateGraphicElements();

            base.InitializePlugIn();
            CreateVisualizeButton();

            LoadSettings();
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            base.FinalizePlugIn();
        }
        #endregion

        void CreateVisualizeButton()
        {

            if (_VisualizeButton != null)
                return;

            CreateGraphicElements();

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
                                    if (menu_item.Caption == "Mini Code Column")
                                        index = menu_item.Index;
                                }
                            }
                            if (index >= 0)
                            {
                                mb.RemoveAt(index);     // Die Buttons wurden immer mehr !!!
                                repeat = true;
                            }
                        } while (repeat);
                    }
                    foreach (MenuBar mb in CodeRush.Menus.Bars)
                    {
                        if (mb.Name.ToUpperInvariant().IndexOf("DXCORE") >= 0)
                        {
                            _VisualizeButton = mb.AddButton();

                            _VisualizeButton.Caption = "Mini Code Column";

                            // .Face = scheint nicht zu funktionieren :-(
                            //_VisualizeButton.Face = Properties.Resources.Button;

                            _VisualizeButton.SetFace(Properties.Resources.Button);
                            _VisualizeButton.TooltipText = "Toggle Mini Code Column on/off";
                            _VisualizeButton.DescriptionText = "Toggle Mini Code Column on/off";
                            _VisualizeButton.IsDown = PluginOptions.MiniCodeColumnEnabled;
                            _VisualizeButton.Enabled = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in CreateVisualizeButton : " + ex.Message);
            }

            if (_VisualizeButton == null)
                return;

            _VisualizeButton.Click += VisualizeButton_Click;
        }

        void UpdateVisualizeButtonState()
        {
            if (_VisualizeButton == null)
                return;

            _VisualizeButton.IsDown = PluginOptions.MiniCodeColumnEnabled;
        }

        void VisualizeButton_Click(object sender, MenuButtonClickEventArgs e)
        {
            if (_VisualizeButton == null)
                return;

            PluginOptions.MiniCodeColumnEnabled = !PluginOptions.MiniCodeColumnEnabled;
            SaveSettings();
            UpdateVisualizeButtonState();
            CodeRush.TextViews.Refresh();
        }

        private void LoadSettings()
        {
            PluginOptions.LoadSettings();

            DisposeGraphicElements();
            CreateGraphicElements();
            UpdateVisualizeButtonState();
        }

        private void SaveSettings()
        {
            PluginOptions.SaveSettings();
        }

        private void PlugIn1_EditorScrolled(EditorScrolledEventArgs ea)
        {
            InvalidateColumn();
            HighlightSelectedText();
        }

        private void PlugIn1_CaretMoved(CaretMovedEventArgs ea)
        {
            TextView view = CodeRush.TextViews.Active;
            if (view != null)
            {
                if (view.TopLine != last_top_line)
                {
                    last_top_line = view.TopLine;
                    InvalidateColumn();
                }
            }
        }

        internal static void CreateGraphicElements()
        {
            if (ColumnBackgroundBrushCodeColumn==null)
                ColumnBackgroundBrushCodeColumn = new SolidBrush(PluginOptions.ColumnBackgroundColor);
            if (ColumnBackgroundBrushSelectedWord == null)
                ColumnBackgroundBrushSelectedWord = new SolidBrush(PluginOptions.ColumnBackgroundColorSelectedWord);
            if (ColumnBrushVisibleLines == null)
                ColumnBrushVisibleLines = new SolidBrush(PluginOptions.ColumnVisibleLinesColor);

            if (CodePenNormalLine == null)
                CodePenNormalLine = new Pen(new SolidBrush(PluginOptions.CodeColorNormalLine));
            if (CodePenSelectedWord == null)
                CodePenSelectedWord = new Pen(new SolidBrush(PluginOptions.CodeColorSelectedWord), 4.0f);
            if (CodePenCommentLine == null)
                CodePenCommentLine = new Pen(new SolidBrush(PluginOptions.CodeColorCommentLine));
        }

        internal static void DisposeGraphicElements()
        {
            if (ColumnBackgroundBrushCodeColumn != null)
                ColumnBackgroundBrushCodeColumn.Dispose();
            if (ColumnBackgroundBrushSelectedWord != null)
                ColumnBackgroundBrushSelectedWord.Dispose();
            if (ColumnBrushVisibleLines != null)
                ColumnBrushVisibleLines.Dispose();

            if (CodePenNormalLine != null)
                CodePenNormalLine.Dispose();
            if (CodePenSelectedWord != null)
                CodePenSelectedWord.Dispose();
            if (CodePenCommentLine != null)
                CodePenCommentLine.Dispose();


            ColumnBackgroundBrushCodeColumn = null;
            ColumnBackgroundBrushSelectedWord = null;
            ColumnBrushVisibleLines = null;
            CodePenNormalLine = null;
            CodePenSelectedWord = null;
            CodePenCommentLine = null;
        }

        private Rectangle GetCodeColumnRect(TextView textView)
        {
            Rectangle rect = new Rectangle(0, 0, textView.Width, textView.Height); 
            //.GetRectangleFromRange(new SourceRange(textView.TopLine, 0, textView.BottomLine, 0));

            rect.X = rect.Right - PluginOptions.ColumnWidth;
            rect.Width = PluginOptions.ColumnWidth;

            return rect;
        }

        private void PlugIn1_EditorPaint(EditorPaintEventArgs ea)
        {
            DrawCodeColumn();
        }

        private void PlugIn1_EditorPaintBackground(EditorPaintEventArgs ea)
        {
            HighlightSelectedText();
        }

        private void HighlightSelectedText()
        {
            if (!PluginOptions.WordDoubleClickEnabled)
                return;

            TextView textView = CodeRush.TextViews.Active;

            if (!PluginOptions.MiniCodeColumnEnabled || textView == null || (selected_double_click.Length <= 2))
                return;

            CreateGraphicElements();

            Graphics graphics = textView.Graphics;
            //SmoothingMode oldMode = graphics.SmoothingMode;
            try
            {
                TextViewLines items = textView.Lines;
                for (int l = 0; l < items.Count; l++)
                {
                    string txt = items.GetText(l).TrimEnd().ToUpperInvariant();

                    if (txt.IndexOf(selected_double_click) >= 0)
                    {
                        if (items.InView(l))
                        {
                            int start_index = txt.IndexOf(selected_double_click) + 1;
                            //SourceRange range = new SourceRange(l, txt.IndexOf(selected_double_click) + 1, l, txt.IndexOf(selected_double_click) + selected_double_click.Length + 1);
                            //graphics.FillRectangle(ColumnBrushVisibleLines, textView.GetRectangleFromRange(range));
                            textView.HighlightCode(
                                l, 
                                start_index, 
                                l, 
                                start_index + selected_double_click.Length,
                                ColumnBrushVisibleLines.Color, 
                                ColumnBackgroundBrushSelectedWord.Color, 
                                Color.White);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                //graphics.SmoothingMode = oldMode;
            }
        }

        bool drawing = false;
        private void DrawCodeColumn()
        {
            TextView textView = CodeRush.TextViews.Active;

            if (!PluginOptions.MiniCodeColumnEnabled || (textView == null) || drawing)
                return;

            drawing = true;

            Graphics graphics = textView.Graphics;
            CreateGraphicElements();

            //SmoothingMode oldMode = graphics.SmoothingMode;
            try
            {
                //graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = GetCodeColumnRect(textView);
                //graphics.FillRectangle(ColumnBackgroundBrush, rect);

                if (rect != last_code_rect)
                {
                    textView.Invalidate(last_code_rect);
                }
                last_code_rect = rect;

                // alle Zeilen holen
                TextViewLines items = textView.Lines;

                graphics.FillRectangle(ColumnBackgroundBrushCodeColumn, rect);


                int width_divisor = 2;
                // falls die Höhe nicht reicht, Teiler ermitteln
                int height_divisor = 1;
                while ((items.Count / height_divisor) > textView.Height)
                {
                    height_divisor++;
                }
                last_height_divisor = height_divisor;

                // den sichtbaren Bereich markieren
                Rectangle visible_rect = new Rectangle(
                    rect.X, rect.Y + (textView.TopLine / height_divisor), 
                    PluginOptions.ColumnWidth, (textView.BottomLine - textView.TopLine) / height_divisor);
                graphics.FillRectangle(ColumnBrushVisibleLines, visible_rect);


                int left = rect.X;
                int start = 0;
                int end = 0;
                for (int l = 0; l < items.Count; l++)
                {
                    int y = l / height_divisor;
                    string txt = items.GetText(l).TrimEnd().Replace("\t", "    ");
                    string ltr = txt.TrimStart();
                    start = (txt.Length - ltr.Length) / width_divisor;
                    if (start > PluginOptions.ColumnWidth)
                        start = PluginOptions.ColumnWidth - 6;
                    end = txt.Length / width_divisor;
                    if (end > PluginOptions.ColumnWidth)
                        end = PluginOptions.ColumnWidth;

                    int start_of_comment = txt.IndexOf("//");
                    int end_of_comment = -2;
                    if (start_of_comment >= 0)
                    {
                        start_of_comment = start_of_comment / width_divisor;
                        end_of_comment = end;
                        end = start_of_comment - 1;
                    }

                    if (start_of_comment < end_of_comment)
                        graphics.DrawLine(CodePenCommentLine, new Point(left + start_of_comment, y), new Point(left + end_of_comment, y));

                    if (start < end)
                        graphics.DrawLine(CodePenNormalLine, new Point(left + start, y), new Point(left + end, y));
                }

                if (selected_double_click.Length > 2)
                {
                    for (int l = 0; l < items.Count; l++)
                    {
                        int y = l / height_divisor;
                        string txt = items.GetText(l).TrimEnd().ToUpperInvariant();
                        start = 0;
                        end = 0;

                        if (txt.IndexOf(selected_double_click) >= 0)
                        {
                            int start_index = txt.IndexOf(selected_double_click);
                            start = start_index / width_divisor;
                            if (start > PluginOptions.ColumnWidth)
                                start = PluginOptions.ColumnWidth - 2;
                            end = (start_index + selected_double_click.Length) / width_divisor;
                            if (end > PluginOptions.ColumnWidth)
                                end = PluginOptions.ColumnWidth;
                            graphics.DrawLine(
                                CodePenSelectedWord, 
                                new Point(left + start, y - 1), 
                                new Point(left + end, y - 1));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                //graphics.SmoothingMode = oldMode;
            }
            drawing = false;
        }
        private void InvalidateColumn()
        {
            TextView textView = CodeRush.TextViews.Active;
            if (textView == null)
                return;

            textView.Invalidate(GetCodeColumnRect(textView));
        }

        /// <summary>
        /// das gedoppeltklickte Wort in selected_double_click merken.
        /// </summary>
        /// <param name="ea"></param>
        private void PlugIn1_EditorMouseDoubleClick(EditorMouseEventArgs ea)
        {
            if (!PluginOptions.WordDoubleClickEnabled)
                return;

            TextView textView = CodeRush.TextViews.Active;
            if (textView != null)
            {
                try
                {
                    selected_double_click = "";
                    Word word;
                    if (textView.GetWordAt(textView.GetSourcePointUnderMouse(), out word) == GetWordResult.Success)
                    {
                        selected_double_click = word.Text.Trim().ToUpperInvariant();
                    }
                    textView.Invalidate();
                    HighlightSelectedText();
                }
                catch //(Exception ex)
                {
                }
            }
        }

        private void PlugIn1_EditorMouseDown(EditorMouseEventArgs ea)
        {
            if (!PluginOptions.MiniCodeColumnEnabled)
                return;

            if ((ea.TextView != null) && (ea.X > ea.TextView.Width - PluginOptions.ColumnWidth))
            {
                int line = ea.Y * last_height_divisor;
                if (line > 10)
                    line -= 10;
                ea.TextView.SetTopLine(line);
                ea.Cancel = true;

                HighlightSelectedText();
            }
        }

        private void MiniCodeColPlugIn_TextDocumentActivated(TextDocumentEventArgs ea)
        {
            if (_VisualizeButton!=null)
                _VisualizeButton.Enabled = true;
        }

        private void MiniCodeColPlugIn_TextDocumentDeactivated(TextDocumentEventArgs ea)
        {
            if (_VisualizeButton!=null)
                _VisualizeButton.Enabled = false;

        }

        private void MiniCodeColPlugIn_EditorValidateClipRegion(EditorValidateClipRegionEventArgs ea)
        {

        }

        private void MiniCodeColPlugIn_OptionsChanged(OptionsChangedEventArgs ea)
        {           
            // LoadSettings();
            TextView textView = CodeRush.TextViews.Active;
            if (textView != null)
            {
                textView.Invalidate();
            }
        }
    }
}