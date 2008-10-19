/*
 * Software License Agreement for RedGreen
 * 
 * Copyright (c) 2008 Renaissance Learning, Inc. and James Argeropoulos
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;

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
