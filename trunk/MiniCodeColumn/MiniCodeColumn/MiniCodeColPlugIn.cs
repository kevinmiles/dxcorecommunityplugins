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
using System.Collections.Generic;

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
        private bool invalid = false;

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
            //HighlightSelectedText();
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
            HighlightSelectedText();
        }

        private void PlugIn1_EditorPaintBackground(EditorPaintEventArgs ea)
        {
            
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

        List<Line> CollectLines(TextView textView, int width_divisor)
        {
            List<Line> lines = new List<Line>();

            int start = 0;
            int end = 0;
            string tabs = new string(' ', textView.TextDocument.TabSize);

            string line_comment_start = "//";

            if (textView.TextDocument.Language == "Basic")
                line_comment_start = "'";

            for (int l = 0; l < textView.TextDocument.LineCount; l++)
            {
                string txt = textView.TextDocument.GetLine(l).TrimEnd();
                if (txt.IndexOf('\t') >= 0)
                    txt = txt.Replace("\t", tabs);

                string ltr = txt.TrimStart();
                start = (txt.Length - ltr.Length);
                end = textView.TextDocument.GetLineLength(l);

                int start_of_comment = txt.IndexOf(line_comment_start);
                int end_of_comment = -2;
                if (start_of_comment >= 0)
                {
                    end_of_comment = end;
                    end = start_of_comment - 1;
                }
                int word_start = txt.IndexOf(selected_double_click, StringComparison.InvariantCultureIgnoreCase);
                Line line = new Line(l, start, end, start_of_comment, end_of_comment, word_start);

                line.DivideWidth(width_divisor);
                line.PressIntoWidth(PluginOptions.ColumnWidth);

                lines.Add(line);
            }

            return lines;
        }

        bool LinesAreEqual(List<Line> l1, List<Line> l2)
        {
            if (l1 == null || l2 == null)
                return false;

            if (l1.Count != l2.Count)
                return false;

            for (int l=0; l < l1.Count; l++)
            {
                Line line1 = l1[l];
                Line line2 = l2[l];

                if (line1.Start != line2.Start || line1.End != line2.End)
                    return false;

                if (line1.StartOfComment != line2.StartOfComment || line1.EndOfComment != line2.EndOfComment)
                    return false;

                if (line1.StartOfWord != line2.StartOfWord)
                    return false;
            }

            return true;
        }

        bool drawing = false;
        private Bitmap _backBuffer;
        private List<Line> last_lines;
        private void DrawCodeColumn()
        {
            TextView textView = CodeRush.TextViews.Active;

            if (!PluginOptions.MiniCodeColumnEnabled || (textView == null) || drawing)
                return;

            drawing = true;

            CreateGraphicElements();

            //SmoothingMode oldMode = graphics.SmoothingMode;
            try
            {
                //graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = GetCodeColumnRect(textView);

                if (rect != last_code_rect)
                {
                    textView.Invalidate(last_code_rect);
                    if (_backBuffer != null)
                        _backBuffer.Dispose();
                    _backBuffer = null;
                    last_lines = null;
                }
                last_code_rect = rect;

                Rectangle bmp_rect = new Rectangle(0, 0, PluginOptions.ColumnWidth, textView.Height); 

                if (_backBuffer==null)
                    _backBuffer = new Bitmap(PluginOptions.ColumnWidth, textView.Height);

                Graphics graphics = Graphics.FromImage(_backBuffer);


                int width_divisor = 2;
                // falls die Höhe nicht reicht, Teiler ermitteln
                int height_divisor = 1;
                while ((textView.TextDocument.LineCount / height_divisor) > textView.Height)
                {
                    height_divisor++;
                }
                if (last_height_divisor != height_divisor)
                    last_lines = null;

                last_height_divisor = height_divisor;

                List<Line> lines = CollectLines(textView, width_divisor);
                if (!LinesAreEqual(lines, last_lines))
                {
                    last_lines = lines;

                    graphics.SmoothingMode = SmoothingMode.None;
                    graphics.Clear(PluginOptions.ColumnBackgroundColor);

                    foreach (Line line in lines)
                    {
                        if (height_divisor > 4 && (line.Number % height_divisor) != 0)
                            continue;

                        int y = line.Number / height_divisor;

                        if (line.Start < line.End)
                            graphics.DrawLine(CodePenNormalLine, new Point(line.Start, y), new Point(line.End, y));
                        if (line.StartOfComment < line.EndOfComment)
                            graphics.DrawLine(CodePenCommentLine, new Point(line.StartOfComment, y), new Point(line.EndOfComment, y));

                    }

                    int length = selected_double_click.Length;
                    if (length > 2)
                    {
                        length /= width_divisor;
                        foreach (Line line in lines)
                        {
                            int y = line.Number / height_divisor;

                            if (line.StartOfWord >= 0)
                            {
                                int end = line.StartOfWord + length;
                                if (end > PluginOptions.ColumnWidth)
                                    end = PluginOptions.ColumnWidth;
                                graphics.DrawLine(
                                    CodePenSelectedWord,
                                    new Point(line.StartOfWord, y),
                                    new Point(end, y));
                            }
                        }
                    }
                }
                graphics.Dispose();
                textView.Graphics.DrawImageUnscaled(_backBuffer, textView.Width - PluginOptions.ColumnWidth, 0);

                // den sichtbaren Bereich markieren
                Rectangle visible_rect = new Rectangle(
                    textView.Width - PluginOptions.ColumnWidth,
                    (textView.TopLine / height_divisor),
                    PluginOptions.ColumnWidth,
                    (textView.BottomLine - textView.TopLine) / height_divisor);
                textView.Graphics.FillRectangle(ColumnBrushVisibleLines, visible_rect);

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
            invalid = false;
        }
        private void InvalidateColumn()
        {
            if (invalid)
                return;

            TextView textView = CodeRush.TextViews.Active;
            if (textView == null)
                return;

            invalid = true;
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
                    invalid = true;
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
                ea.TextView.CenterLine(line);
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
                invalid = true;
                textView.Invalidate();
            }
        }
    }
}