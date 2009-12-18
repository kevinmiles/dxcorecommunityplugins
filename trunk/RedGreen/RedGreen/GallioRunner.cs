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
using System.IO;
using System.Text;

namespace RedGreen
{
    /// <summary>
    /// Runs tests for Gallio supported testing frameworks. 
    /// </summary>
    class GallioRunner : BaseTestRunner 
    {
		private const string kVersionDelimiter = " - version";
        public override void RunTests(string assemblyPath, string assemblyName)
        {
            RunTestsImpl(assemblyPath,
                string.Format("/f:Assembly:{0}", assemblyName));
        }

        /// <summary>
        /// Run all the tests in a class
        /// </summary>
        /// <param name="assemblyPath">Where the class lives physically on the disk</param>
        /// <param name="assemblyName">The full name of the assembly that contains the class.</param>
        /// <param name="className">Full name of the class that has the tests to run.</param>
        public override void RunTests(string assemblyPath, string assemblyName, string className)
        {
            RunTestsImpl(assemblyPath,
                string.Format("/f:ExactType:{0}", className));
        }

        /// <summary>
        /// Run a test
        /// </summary>
        /// <param name="assemblyPath">Where the class lives physically on the disk</param>
        /// <param name="assemblyName">The full name of the assembly that contains the class.</param>
        /// <param name="className">Full name of the class that has the tests to run.</param>
        /// <param name="methodName">The specific method name of the test to run.</param>
        public override void RunTests(string assemblyPath, string assemblyName, string className, string methodName)
        {
            RunTestsImpl(assemblyPath, 
                string.Format("/f:(ExactType:{0})and(Member:{1})", className, methodName));
        }

        /// <summary>
        /// Do the real work of running some tests
        /// </summary>
        /// <param name="assemblyPath">Where the class lives physically on the disk</param>
        /// <param name="filters">A set of filters to narrow the tests that should be run</param>
		private void RunTestsImpl(string assemblyPath, string filter)//params Filter<ITest>[] filters)
		{
			string gallioPath = GetGallioInstalledFolder();
			if (string.IsNullOrEmpty(gallioPath))
			{
				return;
			}

			StreamReader sr = null;
			using (System.Diagnostics.Process p = new System.Diagnostics.Process())
			{
				string parameters = string.Format("\"{0}\" /v:verbose {1}", assemblyPath, filter);
				p.StartInfo = new System.Diagnostics.ProcessStartInfo(gallioPath, parameters);
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.CreateNoWindow = true;
				p.Start();
				sr = p.StandardOutput;
			}
			string line = sr.ReadLine().ToLower();
			string version = String.Empty;
			while (line != "running the tests.")
			{// eat the preamble
				if (line.Contains(kVersionDelimiter))
				{
					version = ParseVersion(line);
				}
				line = sr.ReadLine().ToLower();
			}
			line = sr.ReadLine();

			IResultParser parser = ParserFactory(version);
			string rawResult = String.Empty;
			while (line != null)
			{
				StringBuilder result = new StringBuilder();
				parser.ReadNextTextResult(sr, ref line, result);
				rawResult = result.ToString();
				if (parser.IsTestResult(rawResult))
				{// Only raise event for tests completed, not fixtures and the like.
					RaiseComplete(result.ToString(), parser.ParseTest(rawResult));
				}
			}
			RaiseAllComplete(parser.ParseSummary(rawResult));
		}
		private static IResultParser ParserFactory(string version)
		{
			switch (version)
			{
				case "3.1 build 313":
					return new MbUnit3_1ResultParser();
				default:
					return new ResultParser();
					break;
			}
		}

		private string ParseVersion(string line)
		{
			return line.Substring(line.IndexOf(kVersionDelimiter)+kVersionDelimiter.Length).Trim();
		}

		private static string GetGallioInstalledFolder()
		{
			Microsoft.Win32.RegistryKey gallioKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Gallio.org\\Gallio");
			if (gallioKey == null)
			{
				System.Windows.Forms.MessageBox.Show(
					@"Gallio not installed (registry key HKLM\SOFTWARE\Gallio.org\Gallio not found).\r\n"
					+ "Please download from http://www.gallio.org/",
					"RedGreen Run Tests",
					System.Windows.Forms.MessageBoxButtons.OK,
					System.Windows.Forms.MessageBoxIcon.Error);
				return string.Empty;
			}
			string[] subKeyNames = gallioKey.GetSubKeyNames();
			string installFolder = string.Empty;
			foreach (string keyName in subKeyNames)
			{
				Microsoft.Win32.RegistryKey versionKey = gallioKey.OpenSubKey(keyName);
				object keyValue = versionKey.GetValue("InstallationFolder");
				if (keyValue != null)
				{
					installFolder = keyValue.ToString();
					break;
				}
			}
			if (!string.IsNullOrEmpty(installFolder))
			{
				string echoPath = Path.Combine(installFolder, @"bin\gallio.echo.exe");
				if (File.Exists(echoPath))
				{
					return echoPath;
				}
			}
			return string.Empty;
		}
    }
}
