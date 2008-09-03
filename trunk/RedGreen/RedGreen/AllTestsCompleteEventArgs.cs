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

namespace RedGreen
{
    /// <summary>
    /// The argument object passed when the AllTestsComplete event is raised. 
    /// <remarks>All data is in string form because it comes from strings and is generally just used to create more strings.</remarks>
    /// </summary>
    public class AllTestsCompleteEventArgs : EventArgs
    {
        public readonly string PassCount;
        public readonly string FailCount;
        public readonly string SkipCount;
        public readonly string Duration;

        /// <param name="passed">The number of tests passed</param>
        /// <param name="failed">The number of tests failed</param>
        /// <param name="skipped">The number of tests skipped</param>
        /// <param name="duration">The amount of time used to run all the tests</param>
        public AllTestsCompleteEventArgs(string passed, string failed, string skipped, string duration)
        {
            PassCount = passed;
            FailCount = failed;
            SkipCount = skipped;
            Duration = duration;
        }
    }
}
