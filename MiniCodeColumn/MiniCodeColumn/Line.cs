using System;
using System.Collections.Generic;
using System.Text;


namespace MiniCodeColumn
{
    public class Line
    {
        public int Number;
        public int Start;
        public int End;
        public int StartOfComment;
        public int EndOfComment;
        public int StartOfWord;

        public bool IsEmpty { get { return Start > End || (Start < 0 && End < 0); } }
        public bool HasWord { get { return StartOfWord>=0; } }

        public bool HasBreakpoint;

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
            : this(0, start, end, start_of_comment, end_of_comment, start_of_word)
        {
        }

        public Line(int number, int start, int end, int start_of_comment, int end_of_comment, int start_of_word)
        {
            Number = number;
            Start = start;
            End = end;
            StartOfComment = start_of_comment;
            EndOfComment = end_of_comment;
            StartOfWord = start_of_word;
        }

        public void DivideWidth(int divisor)
        {
            if (divisor <= 1)
                return;

            if (Start > 1) Start /= divisor;
            if (End > 1) End /= divisor;
            if (StartOfComment > 1) StartOfComment /= divisor;
            if (EndOfComment > 1) EndOfComment /= divisor;
            if (StartOfWord > 1) StartOfWord /= divisor;
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
            if (StartOfWord > max_width)
                StartOfWord = max_width - 6;
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
                items.Add(new Line(8, 17));
                items.Add(new Line(4, 5));
                items.Add(new Line());
                items.Add(new Line(-1, -1, 4, 30));
                items.Add(new Line(-1, -1, 4, 35));
                items.Add(new Line());
                items.Add(new Line(4, 20));
                items.Add(new Line(4, 5));
                items.Add(new Line(8, 20));
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
