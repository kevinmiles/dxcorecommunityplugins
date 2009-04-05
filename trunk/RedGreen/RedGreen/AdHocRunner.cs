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
using System.Diagnostics;
namespace RedGreen
{
    class AdHocRunner : BaseTestRunner
    {
        public void wildcardMain()
        {
            try
            {
                // Only get subdirectories that begin with the letter "p."
                string[] dirs = Directory.GetDirectories(@"c:\", "p*");
                Console.WriteLine("The number of directories starting with p is {0}.", dirs.Length);
                foreach (string dir in dirs)
                {
                    Console.WriteLine(dir);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        public override void RunTests(string assemblyPath, string assembly, string type, string method)
        {
            RaiseTestsStarting(string.Format("Running Ad-Hoc test for Assembly: {0}, Type: {1}, Method: {2}\r\n", Path.GetFileName(assemblyPath), type, method));

            StringBuilder result = new StringBuilder();
            StreamReader sr = null;
            Stopwatch chrono = new Stopwatch();
            chrono.Start();
            using (Process p = new Process())
            {
                p.StartInfo = new ProcessStartInfo(LocateAdHocExe());
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
            chrono.Stop();
            TimeSpan thelta = chrono.Elapsed;

            RaiseComplete(result.ToString(), null);
            SummaryResult summary = new SummaryResult();
            summary.PassCount = "1";
            summary.FailCount = "0";
            summary.SkipCount = "0";
            summary.Duration = string.Format("{0}.{1}", thelta.Seconds, thelta.Milliseconds);
            RaiseAllComplete(summary);
        }

        private static string LocateAdHocExe()
        {
            string adHocExePath = string.Empty;
            try
            {
                foreach (string devExpFolder in System.IO.Directory.GetDirectories(@"C:\Program Files (x86)", "*"))
                {
                    if (System.IO.Path.GetFileName(devExpFolder).StartsWith("DevExpress"))
                    {
                        string potentialPath = System.IO.Path.Combine(devExpFolder, @"IDETools\Community\PlugIns\RedGreen.Adhoc.exe");
                        if (System.IO.File.Exists(potentialPath))
                        {
                            adHocExePath = potentialPath;
                        }
                    }
                }
            }
            finally
            {
                if (string.IsNullOrEmpty(adHocExePath))
                {
                    const string kOldAdHocExe = @"C:\Program Files\Developer Express Inc\DXCore for Visual Studio .NET\2.0\Bin\Plugins\RedGreen.AdHoc.exe";
                    adHocExePath = kOldAdHocExe;
                }
            }
            return adHocExePath;
        }
        public override void RunTests(string assemblyPath, string assembly)
        {
            //No op. Here for API compatibility
        }

        public override void RunTests(string assemblyPath, string assembly, string type)
        {
            //No op. Here for API compatibility
        }
    }
}
