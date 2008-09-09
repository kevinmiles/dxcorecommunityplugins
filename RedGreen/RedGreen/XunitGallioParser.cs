using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;

namespace RedGreen
{
    public class XunitGallioParser : IGallioResultParser    
    {
        public string Framwork
        {
            get { return "Xunit"; }
        }

        public string GetExpected(string source)
        {
            string kExpectedStartDelimiter = "\r\nExpected: ";
            string kExpectedEndDelimiter = "\r\nActual:";
            return GallioParserUtils.GetSegment(source, String.Empty, kExpectedStartDelimiter, kExpectedEndDelimiter);
        }

        public string GetActual(string source)
        {
            string kActualStartDelimiter = "\r\nActual:   ";
            string kActualEndDelimiter = "\n   at ";
            return GallioParserUtils.GetSegment(source, String.Empty, kActualStartDelimiter, kActualEndDelimiter);
        }

        public int GetPosition(string source, string expected, string actual)
        {
            if (string.IsNullOrEmpty(source))
            {
                return 0;
            }
            string positionStartExpression = "\r\nPosition: ";
            int positionStart = source.IndexOf(positionStartExpression) + positionStartExpression.Length;
            if (positionStart >= positionStartExpression.Length)
            {
                int positionLength = source.IndexOf("\r\nExpected:") - positionStart;
                string positionText = source.Substring(positionStart, positionLength);
                return int.Parse(positionText.Substring(positionText.LastIndexOf(" ")));
            }
            return 0;
        }

        public int GetLineNumber(string source, string testLocation)
        {
            string trimmed = source.Trim();
            return int.Parse(trimmed.Substring(trimmed.LastIndexOf(' ')));
        }

        public string ReformatLocation(string source)
        {
            return source.Replace('/', '.');
        }
    }
}
