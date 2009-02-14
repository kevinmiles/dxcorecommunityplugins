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
using System.IO;
using System.Text;
namespace RedGreen
{
    class AdHocRunner
    {
        /// <summary>
        /// Raised after a test has been run
        /// </summary>
        public event TestCompleteEventHandler TestComplete;

        /// <summary>
        /// Raised after all tests have been run
        /// </summary>
        public event AllTestsCompleteEventHandler AllTestsComplete;

        public void RunTest(string assemblyPath, string assembly, string type, string method)
        {
            const string kAdHocExe = @"C:\Program Files\Developer Express Inc\DXCore for Visual Studio .NET\2.0\Bin\Plugins\RedGreen.AdHoc.exe";
            StringBuilder result = new StringBuilder();
            StreamReader sr = null;
            using (System.Diagnostics.Process p = new System.Diagnostics.Process())
            {
                p.StartInfo = new System.Diagnostics.ProcessStartInfo(kAdHocExe);
                p.StartInfo.Arguments = string.Format("/a:\"{0}\" /t:{1} /m:{2}", assemblyPath, type, method);
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                sr = p.StandardOutput;
            }
            string line = sr.ReadLine();
            while (line != null)
            {
                result.AppendFormat("{0}\n", line);
                line = sr.ReadLine();
            }

            RaiseComplete(result.ToString());
            SummaryResult summary = new SummaryResult();
            summary.PassCount = "1";
            summary.FailCount = "0";
            summary.SkipCount = "0";
            summary.Duration = "0";
            RaiseAllComplete(summary);
        }

        /// <summary>
        /// Emmit the TestComplete event
        /// </summary>
        /// <param name="raw">result of test in text form</param>
        protected void RaiseComplete(string raw)
        {
            if (TestComplete != null)
            {
                TestComplete(this, new TestCompleteEventArgs(raw, null));
            }
        }

        /// <summary>
        /// Emit the AllTestsComplete event
        /// </summary>
        protected void RaiseAllComplete(SummaryResult result)
        {
            if (AllTestsComplete != null)
            {
                AllTestsComplete(this, new AllTestsCompleteEventArgs(result.PassCount, result.FailCount, result.SkipCount, result.Duration));
            }
        }
    }
}
