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
using System.Collections.Generic;
using System.Text;
using Gallio.Model;
using Gallio.Runner.Extensions;
using Gallio.Runner.Reports;
using Gallio.Model.Execution;

namespace RedGreen
{
    /// <summary>
    /// Parse result data from MbUnit's format
    /// </summary>
    public class MbUnitGallioParser : IGallioResultParser
    {
        public string Framwork
        {
            get { return "MbUnit"; }
        }

        public string GetExpected(string source)
        {
            const string kExpectedStartDelimiter = "failed: [[";
            const string kExpectedEndDelimiter = "]]";
            return GallioParserUtils.GetSegment(source, String.Empty, kExpectedStartDelimiter, kExpectedEndDelimiter);
        }

        public string GetActual(string source)
        {
            const string kActualStartDelimiter = "!=[[";
            const string kActualEndDelimiter = "]]";
            return GallioParserUtils.GetSegment(source, String.Empty, kActualStartDelimiter, kActualEndDelimiter);
        }

        /// <remarks>MbUnit doesn't tell you where the difference starts. Figure it out.</remarks>
        public int GetPosition(string source, string expected, string actual)
        {
            for (int i = 0; i < Math.Min(expected.Length, actual.Length); ++i)
            {
                if (expected[i] != actual[i])
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>
        /// Gallio reports the location in a way that doesn't match what DxCore is looking for
        /// </summary>
        public string ReformatLocation(string source)
        {
            return source.Replace('/', '.');
        }

        public int GetLineNumber(string source, string testLocation)
        {
            int startFileData = source.IndexOf(testLocation);
            if (startFileData > 0)
            {
                int endFileData = source.IndexOf("\r\n", startFileData);
                int lineNumberStart = source.LastIndexOf(" ", endFileData) + 1;
                return int.Parse(source.Substring(lineNumberStart, endFileData - lineNumberStart));
            }
            return 0;
        }
    }
}
