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
using System.Runtime.InteropServices;

namespace MiniCodeColumn
{

    /// <summary>
    /// Plugin for visualizing complete codefile as small column left of the scrollbar
    /// </summary>
    [Title("MiniCodeColumn")]
    public partial class CodeToolWindow : ToolWindowPlugIn
    {
        public int last_height_divisor = 1;

        internal static SolidBrush ColumnBackgroundBrushCodeColumn;
        internal static SolidBrush ColumnBackgroundBrushSelectedWord;
        internal static SolidBrush ColumnBrushVisibleLines;

        internal static SolidBrush BreakPointBrush;

        internal static Pen CodePenNormalLine;
        internal static Pen CodePenSelectedWord;
        internal static Pen CodePenCommentLine;

        string selected_double_click = "";
        Rectangle last_code_rect = new Rectangle();

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            
            base.InitializePlugIn();
            LoadSettings();
            SetButtonImage();
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //
            DisposeGraphicElements();
            base.FinalizePlugIn();
        }
        #endregion

        private void SetButtonImage()
        {
            try
            {
                if (CodeRush.Menus != null && CodeRush.Menus.Bars != null)
                {
                    foreach (IMenuControl item in CodeRush.Menus.ToolWindows)
                    {
                        if (item.Caption.ToUpperInvariant() == "MINICODECOLUMN")
                        {
                            DevExpress.CodeRush.Menus.IMenuButton btn = item as DevExpress.CodeRush.Menus.IMenuButton;

                            btn.Face = this.Image;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void LoadSettings()
        {
            PluginOptions.LoadSettings();

            DisposeGraphicElements();
            CreateGraphicElements();
            PluginOptions.MiniCodeColumnEnabled = true;
        }

        internal static void CreateGraphicElements()
        {
            if (ColumnBackgroundBrushCodeColumn == null)
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

            if (BreakPointBrush == null)
                BreakPointBrush = new SolidBrush(PluginOptions.BreakPointColor);
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

            if (BreakPointBrush != null)
                BreakPointBrush.Dispose();

            ColumnBackgroundBrushCodeColumn = null;
            ColumnBackgroundBrushSelectedWord = null;
            ColumnBrushVisibleLines = null;
            CodePenNormalLine = null;
            CodePenSelectedWord = null;
            CodePenCommentLine = null;

            BreakPointBrush = null;
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
                end = txt.Length;

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
                line.PressIntoWidth(Width);

                try
                {
                    EnvDTE.Breakpoint bp = CodeRush.Breakpoint.Get(textView.TextDocument.FullName, l);
                    if (bp != null && bp.Enabled)
                        line.HasBreakpoint = true;
                }
                catch (Exception)
                {
                }
                lines.Add(line);
            }

            foreach (IMarker item in CodeRush.Markers)
            {
                if (textView.TextDocument.FullName.Equals(item.FileName, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (lines.Count >= item.Line && item.Hidden == false)
                        lines[item.Line].MarkerPosition = item.Column / width_divisor;
                }
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

                if (line1.HasBreakpoint != line2.HasBreakpoint)
                    return false;

                if (line1.MarkerPosition != line2.MarkerPosition)
                    return false;
            }

            return true;
        }


        bool drawing = false;
        private Bitmap _backBuffer;
        private List<Line> last_lines;
        private void DrawCodeColumn(Graphics my_graphics)
        {
            TextView textView = CodeRush.TextViews.Active;

            if (!PluginOptions.MiniCodeColumnEnabled || (textView == null) || drawing)
                return;

            drawing = true;

            //textView.Graphics
            CreateGraphicElements();

            //SmoothingMode oldMode = graphics.SmoothingMode;
            try
            {
                //graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                if (rect != last_code_rect)
                {
                    if (_backBuffer != null)
                        _backBuffer.Dispose();
                    _backBuffer = null;
                    last_lines = null;
                }
                last_code_rect = rect;

                Rectangle bmp_rect = new Rectangle(0, 0, this.Width, this.Height);

                if (_backBuffer == null)
                    _backBuffer = new Bitmap(this.Width, this.Height);

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
                                if (end > Width)
                                    end = Width;
                                graphics.DrawLine(
                                    CodePenSelectedWord,
                                    new Point(line.StartOfWord, y),
                                    new Point(end, y));
                            }
                        }
                    }
                    foreach (Line line in lines)
                    {
                        if (line.HasBreakpoint)
                            graphics.FillEllipse(BreakPointBrush, 0f, line.Number / height_divisor - 5f, 10f, 12f);

                        if (line.MarkerPosition >= 0)
                            CodeRush.Markers.Draw(graphics, new PointF(line.MarkerPosition, line.Number / height_divisor), PluginOptions.BreakPointColor, 5f, 3f);
                    }
                }
                graphics.Dispose();

                my_graphics.DrawImageUnscaled(_backBuffer, 0, 0);

                // den sichtbaren Bereich markieren
                Rectangle visible_rect = new Rectangle(0,
                    (textView.TopLine / height_divisor),
                    Width,
                    (textView.BottomLine - textView.TopLine) / height_divisor);
                my_graphics.FillRectangle(ColumnBrushVisibleLines, visible_rect);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawCodeColumn(e.Graphics);
        }

        private void events_TextChanged(TextChangedEventArgs ea)
        {
            this.Invalidate();
            redraw_reqired = true;
        }

        private void events_TextViewActivated(TextViewEventArgs ea)
        {
            redraw_reqired = true; 
            if (!mouse_is_down)
                Refresh();
        }

        private void events_EditorScrolled(EditorScrolledEventArgs ea)
        {
            TextView textView = CodeRush.TextViews.Active;
            if (textView != null || (selected_double_click.Length > 2))
            {
                redraw_reqired = true;
            }
            if (!mouse_is_down)
                Refresh();
        }

        private void HighlightSelectedText()
        {
            if (!PluginOptions.WordDoubleClickEnabled)
                return;

            TextView textView = CodeRush.TextViews.Active;

            if (textView == null || (selected_double_click.Length <= 2))
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
                            // SourceRange range = new SourceRange(l, txt.IndexOf(selected_double_click) + 1, l, txt.IndexOf(selected_double_click) + selected_double_click.Length + 1);
                            // graphics.FillRectangle(ColumnBrushVisibleLines, textView.GetRectangleFromRange(range));
                            
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

        private void events_EditorMouseDoubleClick(EditorMouseEventArgs ea)
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
                    // Tool-Window-Refresh
                    Refresh();
                    textView.Invalidate();
                    redraw_reqired = true;
                }
                catch //(Exception ex)
                {
                }
            }
        }

        bool mouse_is_down = false;
        private void CodeToolWindow_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_is_down = true;
            CodeToolWindow_MouseMove(sender, e);
        }

        private void CodeToolWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouse_is_down || !PluginOptions.MiniCodeColumnEnabled)
                return;

            TextView textView = CodeRush.TextViews.Active;
            if (textView != null)
            {
                int line = e.Y * last_height_divisor;
                int center_line = line;
                if (center_line > 10)
                    center_line -= 10;

                textView.MakeVisible(line, 0);
                textView.CenterLine(center_line);
                redraw_reqired = true;
                Refresh();
            }
        }

        private void CodeToolWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouse_is_down)
            {
                TextView textView = CodeRush.TextViews.Active;
                if (textView != null)
                    textView.Invalidate();
            }
            mouse_is_down = false;
            redraw_reqired = true;
        }

        private void CodeToolWindow_MouseLeave(object sender, EventArgs e)
        {
            mouse_is_down = false;
        }

        private void events_OptionsChanged(OptionsChangedEventArgs ea)
        {
            DisposeGraphicElements();
            Refresh();
            redraw_reqired = true;
            PluginOptions.MiniCodeColumnEnabled = true;
        }

        private void events_MarkerCollected(MarkerEventArgs ea)
        {
            redraw_reqired = true;
            Refresh();
        }

        bool redraw_reqired;
        private void events_EditorIdle(EditorIdleEventArgs ea)
        {
            // verschieben
            if (mouse_is_down)
                return;

            if (redraw_reqired)
            {
                redraw_reqired = false;
                HighlightSelectedText();
            }
        }
    }
}