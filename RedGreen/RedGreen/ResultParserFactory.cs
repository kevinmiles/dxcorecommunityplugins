/*
 * Software License Agreement for RedGreen
 * 
 * Copyright (c) 2009 Renaissance Learning, Inc. and James Argeropoulos
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

namespace RedGreen
{
    /// <summary>
    /// One stop shop to get your Gallio results parser. 
    /// </summary>
    class ResultParserFactory
    {
        private List<IGallioResultParser> _parsers = new List<IGallioResultParser>();

        public ResultParserFactory()
        {
            _parsers.Add(new NUnitGallioParser());
            _parsers.Add(new MbUnitGallioParser());
            _parsers.Add(new XunitGallioParser());
        }

        /// <summary>
        /// Obtain a result parser for the framework in use.
        /// </summary>
        /// <param name="frameworkName"></param>
        /// <returns>A matching parser or the NullGallioParser if none exist in the collection</returns>
        public IGallioResultParser GetParser(string frameworkName)
        {
            IGallioResultParser parser = _parsers.Find(
                delegate(IGallioResultParser p)
                {
                    return frameworkName.ToLower().StartsWith(p.Framwork.ToLower());
                });
            return parser != null ? parser : new NullGallioParser();
        }
    }
}
