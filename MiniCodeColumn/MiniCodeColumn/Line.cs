using System;
using System.Collections.Generic;
using System.Text;


namespace MiniCodeColumn
{
    public class Line
    {
        public int Start;
        public int End;
        public int StartOfComment;
        public int EndOfComment;
        public int StartOfWord;

        public bool IsEmpty { get { return Start > End || (Start < 0 && End < 0); } }
        public bool HasWord { get { return StartOfWord>=0; } }

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
            Start = start;
            End = end;
            StartOfComment = start_of_comment;
            EndOfComment = end_of_comment;
            StartOfWord = start_of_word;
        }
    }
}
