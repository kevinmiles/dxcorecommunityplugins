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
    /// Define the work that each Gallio Parser must do
    /// </summary>
    public interface IGallioResultParser
    {
        /// <summary>
        /// The name of the framework this parser deals with
        /// </summary>
        string Framwork { get;}

        /// <summary>
        /// Parses out the expected results. Designed for the Assert.AreEqual() method
        /// </summary>
        string GetExpected(string source);

        /// <summary>
        /// Parses out the actual results. Designed for the Assert.AreEqual() method
        /// </summary>
        string GetActual(string source);

        /// <summary>
        /// Parses out the offset into the strings where expected varies from the actual. Designed for the Assert.AreEqual() method
        /// </summary>
        int GetPosition(string source, string expected, string actual);

        /// <summary>
        /// Get the line number of the failure. If a line number is specified.
        /// </summary>
        int GetLineNumber(string source);

        /// <summary>
        /// Get the method location where the failure took place. THe assert location would be preferred, but Gallio doesn't supply that data.
        /// </summary>
        string ReformatLocation(string source);
    }
}
