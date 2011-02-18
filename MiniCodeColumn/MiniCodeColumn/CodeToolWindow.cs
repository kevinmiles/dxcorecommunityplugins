#region Copyright (C) 2009 by Ralf Warnat
/*
        Copyright (C) 2009 by Ralf Warnat
        This software is provided "as is" without express or implied warranty of any kind.
        It is labeled as "Works on my machine".        
*/
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using DevExpress.CodeRush.Menus;

namespace MiniCodeColumn
{

    /// <summary>
    /// Plugin for visualizing complete codefile as small column left of the scrollbar
    /// </summary>
    [Title("MiniCodeColumn")]
    public partial class CodeToolWindow : ToolWindowPlugIn
    {
        private bool shutting_down = false;

        public static bool OnAir;
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

        System.Timers.Timer repaint_tool_window_timer;

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            LoadSettings();
            SetButtonImage();

            this.WindowShow += new EventHandler(CodeToolWindow_WindowShow);

            repaint_tool_window_timer = new System.Timers.Timer(250) { Enabled = false, AutoReset = false };
            repaint_tool_window_timer.Elapsed += repaint_timer_Elapsed;
        }

        void RestartTimer()
        {
            if (shutting_down)
            {
                repaint_tool_window_timer.Stop();
                return;
            }
            repaint_tool_window_timer.Stop();
            repaint_tool_window_timer.Start();
        }

        void repaint_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            repaint_tool_window_timer.Stop();
            if (shutting_down) return;
            Invalidate();
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //
            try
            {
                DisposeGraphicElements();
                //HideWindow().Close(EnvDTE.vsSaveChanges.vsSaveChangesYes);
            }
            catch //(Exception ex)
            {
                
            }
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
                            IMenuButton btn = item as IMenuButton;

                            btn.Face = Image;
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
            if (shutting_down) return lines;

            int start = 0;
            int end = 0;
            string tabs = new string(' ', textView.TextDocument.TabSize);

            string line_comment_start;
            if (textView.TextDocument.Language == "Basic")
                line_comment_start = "'";
            else
                line_comment_start = "//";

            for (int l = 0; l < textView.TextDocument.LineCount; l++)
            {
                string txt = textView.TextDocument.GetLine(l).TrimEnd();
                string org_txt = txt;
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
                //int word_start = txt.IndexOf(selected_double_click, StringComparison.InvariantCultureIgnoreCase);
                Line line = new Line(l, start, end, start_of_comment, end_of_comment, CollectWordIndexes(ref org_txt));

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

        List<int> CollectWordIndexes(ref string line)
        {
            List<int> pos = new List<int>();

            if (!string.IsNullOrEmpty(selected_double_click))
            {
                int p = line.IndexOf(selected_double_click, StringComparison.InvariantCultureIgnoreCase);

                while (p >= 0)
                {
                    pos.Add(p);
                    p = line.IndexOf(selected_double_click, p + 1, StringComparison.InvariantCultureIgnoreCase);
                }
            }
            return pos;
        }

        static bool LinesAreEqual(List<Line> l1, List<Line> l2)
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

                if (line1.HasWord != line2.HasWord)
                    return false;

                if (line1.HasBreakpoint != line2.HasBreakpoint)
                    return false;

                if (line1.MarkerPosition != line2.MarkerPosition)
                    return false;
            }

            return true;
        }


        bool drawing;
        private Bitmap _backBuffer;
        private List<Line> last_lines;
        private void DrawCodeColumn(Graphics my_graphics)
        {
            TextView textView = CodeRush.TextViews.Active;

            if ((textView == null) || drawing || shutting_down)
                return;

            drawing = true;

            //textView.Graphics
            CreateGraphicElements();

            //SmoothingMode oldMode = graphics.SmoothingMode;
            try
            {
                //graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, Width, Height);
                if (rect != last_code_rect)
                {
                    if (_backBuffer != null)
                        _backBuffer.Dispose();
                    _backBuffer = null;
                    last_lines = null;
                }
                last_code_rect = rect;

                //Rectangle bmp_rect = new Rectangle(0, 0, Width, Height);

                if (_backBuffer == null)
                    _backBuffer = new Bitmap(Width, Height);

                Graphics graphics = Graphics.FromImage(_backBuffer);

                int width_divisor = 2;
                // falls die Höhe nicht reicht, Teiler ermitteln
                int height_divisor = 1;
                while ((textView.TextDocument.LineCount / height_divisor) > Height)
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

                            if (line.HasWord)
                            {
                                foreach (int st in line.StartOfWords)
                                {
                                    int start = st;
                                    if (start > Width)
                                        start = Width - 6;
                                    int end = start / width_divisor + length;
                                    if (end > Width)
                                        end = Width;
                                    graphics.DrawLine(
                                        CodePenSelectedWord,
                                        new Point(start / width_divisor, y),
                                        new Point(end, y));
                                }
                            }
                        }
                    }
                    foreach (Line line in lines)
                    {
                        if (line.HasBreakpoint)
                            graphics.FillEllipse(BreakPointBrush, 0f, line.Number / height_divisor - 5f, 10f, 12f);

                        if (line.MarkerPosition >= 0)
                            graphics.FillEllipse(ColumnBackgroundBrushSelectedWord, 0f, line.Number / height_divisor - 5f, 10f, 12f);
                            //CodeRush.Graphics..Markers.Draw(graphics, new PointF(line.MarkerPosition, line.Number / height_divisor), PluginOptions.BreakPointColor, 5f, 3f);
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
            drawing = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (shutting_down)
                base.OnPaint(e);
            else
                DrawCodeColumn(e.Graphics);
        }

        private void events_TextChanged(TextChangedEventArgs ea)
        {
            RestartTimer();
        }

        private void events_TextViewActivated(TextViewEventArgs ea)
        {
            RestartTimer();
        }

        private void events_EditorScrolled(EditorScrolledEventArgs ea)
        {
            RestartTimer();
        }

        void events_EditorPaint(EditorPaintEventArgs ea)
        {
            if (shutting_down) return;
            HighlightSelectedText(ea.Graphics);
        }


        private bool highlighting;
        private void HighlightSelectedText(Graphics graphics)
        {
            if (!PluginOptions.WordDoubleClickEnabled || highlighting || last_lines == null || last_lines.Count<= 0 || shutting_down)
                return;


            TextView textView = CodeRush.TextViews.Active;
            if (textView == null || (selected_double_click.Length <= 2))
                return;

            highlighting = true;
            CreateGraphicElements();

            //SmoothingMode oldMode = graphics.SmoothingMode;
            try
            {                
                //TextViewLines items = textView.Lines;
                for (int l = textView.TopLine; l < textView.BottomLine && l < last_lines.Count; l++)
                {
                    if (!last_lines[l].HasWord) continue;

                    foreach (int start in last_lines[l].StartOfWords)
                    {
                        var rect = textView.GetRectangleFromSourceRange(new SourceRange(l, start + 1, l, start + selected_double_click.Length + 1));
                        graphics.FillRectangle(ColumnBackgroundBrushSelectedWord, rect.ConvertTo<Rectangle>());                        
                    }
                }
            }
            catch // (Exception ex)
            {
                // System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                highlighting = false;
            }
        }

        private void events_EditorMouseDoubleClick(EditorMouseEventArgs ea)
        {
            if (!PluginOptions.WordDoubleClickEnabled || shutting_down)
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
                    last_lines = CollectLines(textView, 1);
                    textView.Invalidate();
                    
                    //HighlightSelectedText();
                    RestartTimer();
                    Invalidate();
                }
                catch //(Exception ex)
                {
                }
            }
        }

        bool mouse_is_down;
        private void CodeToolWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (shutting_down) return;
            mouse_is_down = true;
            CodeToolWindow_MouseMove(sender, e);
        }

        private void CodeToolWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouse_is_down || !PluginOptions.MiniCodeColumnEnabled || shutting_down)
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
                Invalidate();
            }
        }

        private void CodeToolWindow_MouseUp(object sender, MouseEventArgs e)
        {
            RestartTimer();
            mouse_is_down = false;
        }

        private void CodeToolWindow_MouseLeave(object sender, EventArgs e)
        {
            mouse_is_down = false;
        }

        private void events_OptionsChanged(OptionsChangedEventArgs ea)
        {
            DisposeGraphicElements();
            RestartTimer();
            PluginOptions.MiniCodeColumnEnabled = true;
        }

        private void events_MarkerCollected(MarkerEventArgs ea)
        {
            RestartTimer();
        }

        void CodeToolWindow_WindowShow(object sender, EventArgs e)
        {
            if (shutting_down) return;
            if (OnAir == false && CodeRush.TextViews.Active!=null)
            {
                EnvDTE.Window wnd = null;
                try
                {
                    wnd = this.Window; // CodeRush.ToolWindows.Show(typeof(CodeToolWindow));
                    if (wnd != null && wnd.IsFloating)
                    {
                        //wnd.Visible = true;
                        //wnd.IsFloating = true;
                        EnvDTE.Window prop_wnd = CodeRush.ApplicationObject.Windows.Item(EnvDTE.Constants.vsWindowKindProperties);
                        if (prop_wnd == null) return;

                        wnd.Width = 60;
                        wnd.Top = prop_wnd.Top; // ea.TextView.ScreenBounds.Top;
                        wnd.Height = prop_wnd.Height; // ea.TextView.ScreenBounds.Height;
                        wnd.Left = prop_wnd.Left;// +code_wnd.Width + 1; // ea.TextView.ScreenBounds.Right - wnd.Width;
                        
                        //wnd.IsFloating = false;
                        if (false) //(prop_wnd != null && prop_wnd.Linkable && prop_wnd.LinkedWindowFrame != null && prop_wnd.LinkedWindowFrame.LinkedWindows != null)
                        {
                            //var frame = wnd.DTE.Windows.CreateLinkedWindowFrame(wnd, prop_wnd, EnvDTE.vsLinkedWindowType.vsLinkedWindowTypeVertical);
                            prop_wnd.LinkedWindowFrame.LinkedWindows.Add(wnd);
                        }
                        else
                        {
                            int width = prop_wnd.Width + 60;
                            int height = prop_wnd.Height;
                            prop_wnd.LinkedWindowFrame.LinkedWindows.Remove(prop_wnd);
                            //prop_wnd.IsFloating = true;
                            EnvDTE80.Window2 Frame = (EnvDTE80.Window2)CodeRush.ApplicationObject.Windows.CreateLinkedWindowFrame(wnd, prop_wnd,
                                    EnvDTE.vsLinkedWindowType.vsLinkedWindowTypeVertical);
                            Frame.Caption = "Properties && Code";
                            
                            Frame.SetKind(EnvDTE.vsWindowType.vsWindowTypeToolWindow);
                            Frame.Width = width;
                            Frame.Height = height;
                            EnvDTE.Window main_wnd = ((EnvDTE80.DTE2)CodeRush.ApplicationObject).MainWindow;
                            //EnvDTE.Window main_wnd = CodeRush.ApplicationObject.Windows.Item(EnvDTE.Constants.vsWindowKindDocumentOutline);
                            //main_wnd.LinkedWindowFrame.LinkedWindows.Add(Frame);
                            main_wnd.LinkedWindows.Add(Frame);
                        }

                        OnAir = true;
                    }
                }
                catch
                {
                }
            }
        }

        private void events_BeginShutdown()
        {
            shutting_down = true;
        }


    }
}