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
using Gallio.Framework;

namespace RedGreen
{
    /// <summary>
    /// Tools used by more than one of the Gallio parsers
    /// </summary>
    internal static class GallioParserUtils
    {
        /// <summary>
        /// Gets a segment of a result using the delimiters passed in
        /// </summary>
        /// <param name="source">Where to find the segment</param>
        /// <param name="defaultValue">What to pass back if no segment is found</param>
        /// <param name="startDelimiter">How to locate segment start</param>
        /// <param name="endSegmentDelimiters">How to locate segment end</param>
        /// <returns>segment or default</returns>
        public static string GetSegment(string source, string defaultValue, string startDelimiter, params string[] endSegmentDelimiters)
        {
            if (!string.IsNullOrEmpty(source))
            {
                int expectedStart = source.IndexOf(startDelimiter) + startDelimiter.Length;
                if (expectedStart >= startDelimiter.Length)
                {
                    foreach (string endDelimiter in endSegmentDelimiters)
                    {
                        int expectedEnd = source.IndexOf(endDelimiter, expectedStart) - expectedStart;
                        if (expectedEnd > 0)
                        {
                            return source.Substring(expectedStart, expectedEnd);
                        }
                    }
                }
            }
            return defaultValue;
        }

    }
}
