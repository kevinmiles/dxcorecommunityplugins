using System;
using System.Collections.Generic;
using System.Text;


namespace MiniCodeColumn
{
    /// <summary>
    /// class to hold information about start and end of comments, text and the double-clicked-word
    /// </summary>
    public class Line
    {
        public int Number;
        public int Start;
        public int End;
        public int StartOfComment;
        public int EndOfComment;

        // more than one occurence of double-clicked word per line !
        public List<int> StartOfWords;

        public bool IsEmpty { get { return Start > End || (Start < 0 && End < 0); } }
        public bool HasWord { get { if (StartOfWords == null) return false; return StartOfWords.Count > 0; } }

        public bool HasBreakpoint;
        public int MarkerPosition;

        public Line()
            : this(-1, -1, -1, -1, -1)
        {
        }

        public Line(int start, int end)
            : this(start, end, -1, -1, -1)
        {
        }

        public Line(int start, int end, int start_of_comment, int end_of_comment)
            : this(start, end, start_of_comment, end_of_comment, -1)
        {
        }

        public Line(int start, int end, int start_of_comment, int end_of_comment, int start_of_word)
        {
            Number = 0;
            Start = start;
            End = end;
            StartOfComment = start_of_comment;
            EndOfComment = end_of_comment;
            StartOfWords = new List<int>();
            if (start_of_word>=0)
                StartOfWords.Add(start_of_word);
        }

        public Line(int number, int start, int end, int start_of_comment, int end_of_comment, List<int> start_of_words)
        {
            Number = number;
            Start = start;
            End = end;
            StartOfComment = start_of_comment;
            EndOfComment = end_of_comment;
            StartOfWords = start_of_words;
            MarkerPosition = -1;
        }

        public void DivideWidth(int divisor)
        {
            if (divisor <= 1)
                return;

            if (Start > 1) Start /= divisor;
            if (End > 1) End /= divisor;
            if (StartOfComment > 1) StartOfComment /= divisor;
            if (EndOfComment > 1) EndOfComment /= divisor;
        }

        public void PressIntoWidth(int max_width)
        {
            if (Start > max_width)
                Start = max_width - 2;
            if (End > max_width)
                End = max_width;
            if (StartOfComment > max_width)
                StartOfComment = max_width - 2;
            if (EndOfComment > max_width)
                EndOfComment = max_width;
        }

        public static List<Line> SampleLines
        {
            get
            {
                List<Line> items = new List<Line>();

                items.Add(new Line(-1, -1, 4, 30));
                items.Add(new Line(-1, -1, 4, 35));
                items.Add(new Line());
                items.Add(new Line(4, 20));
                items.Add(new Line(4, 5));
                items.Add(new Line(8, 20));
                items.Add(new Line());
                items.Add(new Line(8, 25));
                items.Add(new Line(8, 20, -1, -1, 10));
                items.Add(new Line(4, 5));
                items.Add(new Line());
                items.Add(new Line(-1, -1, 4, 38));
                items.Add(new Line(-1, -1, 4, 30));
                items.Add(new Line());
                items.Add(new Line(4, 28));
                items.Add(new Line(4, 5));
                items.Add(new Line(8, 22, -1, -1, 12));
                items.Add(new Line(8, 29));
                items.Add(new Line(8, 17) { HasBreakpoint = true });
                items.Add(new Line(4, 5));
                items.Add(new Line());
                items.Add(new Line(-1, -1, 4, 30));
                items.Add(new Line(-1, -1, 4, 35));
                items.Add(new Line());
                items.Add(new Line(4, 20));
                items.Add(new Line(4, 5));
                items.Add(new Line(8, 20) { MarkerPosition = 8 });
                items.Add(new Line(8, 25));
                items.Add(new Line(8, 20, -1, -1, 10));
                items.Add(new Line(4, 5));
                items.Add(new Line());
                items.Add(new Line(-1, -1, 4, 38));
                items.Add(new Line(-1, -1, 4, 30));
                items.Add(new Line());
                items.Add(new Line(4, 28));
                items.Add(new Line(4, 5));
                items.Add(new Line(8, 22, -1, -1, 12));
                items.Add(new Line(8, 29));
                items.Add(new Line(8, 17));
                items.Add(new Line(4, 5));
                items.Add(new Line());
                items.Add(new Line(-1, -1, 4, 30));
                items.Add(new Line(-1, -1, 4, 35));
                items.Add(new Line());
                items.Add(new Line(4, 20));
                items.Add(new Line(4, 5));
                items.Add(new Line());
                items.Add(new Line(8, 22));
                items.Add(new Line(8, 27));
                items.Add(new Line(8, 20, -1, -1, 10));
                items.Add(new Line(4, 5));
                items.Add(new Line());
                items.Add(new Line(-1, -1, 4, 38));
                items.Add(new Line(-1, -1, 4, 30));
                items.Add(new Line());
                items.Add(new Line(4, 28));
                items.Add(new Line());
                items.Add(new Line(4, 5));
                items.Add(new Line(8, 22, -1, -1, 8));
                items.Add(new Line(8, 29));
                items.Add(new Line(4, 5));
                items.Add(new Line());
                items.Add(new Line(8, 17));
                items.Add(new Line(4, 5));

                return items;
            }
        }
    }
}
