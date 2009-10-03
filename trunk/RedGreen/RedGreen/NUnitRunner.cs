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
using System.Collections.Generic;

namespace RedGreen
{
	internal class NUnitRunner : BaseTestRunner
	{
		public override void RunTests(string assemblyPath, string assemblyName)
		{
			RunTestsImpl(assemblyPath, string.Empty);
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
				string.Format("/fixture:{0}", className));
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
				string.Format("/run:{0}.{1}", className, methodName));
		}

		/// <summary>
		/// Do the real work of running some tests
		/// </summary>
		/// <param name="assemblyPath">Where the class lives physically on the disk</param>
		/// <param name="filters">A set of filters to narrow the tests that should be run</param>
		private void RunTestsImpl(string assemblyPath, string filter)//params Filter<ITest>[] filters)
		{
			string consoleRunnerPath = GetConsoleRunnerExePath();
			if (string.IsNullOrEmpty(consoleRunnerPath))
			{
				return;
			}

			StreamReader sr = null;
			DateTime start = DateTime.Now;
			using (System.Diagnostics.Process p = new System.Diagnostics.Process())
			{
				string arguments = string.Format(" {1} /xmlConsole \"{0}\"", assemblyPath, filter);
				p.StartInfo = new System.Diagnostics.ProcessStartInfo(consoleRunnerPath, arguments);
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.CreateNoWindow = true;
				p.Start();
				sr = p.StandardOutput;
			}
			string line = sr.ReadLine();
			List<string> rawResults = new List<string>();
			while (line != null)
			{
				rawResults.Add(line);
				line = sr.ReadLine();
			}
			TimeSpan duration = DateTime.Now - start;
			List<TestResult> parsedResults = NUnitParser.ParseTestResults(rawResults);
			SummaryResult summary = NUnitParser.ParseSummary(rawResults);
			summary.Duration = string.Format("{0}.{1}", duration.Seconds, duration.Milliseconds);
			foreach (TestResult testResult in parsedResults)
			{
				RaiseComplete(string.Empty, testResult);
			}
			RaiseAllComplete(summary);
			//while (line != "<!--This file represents the results of running a test suite-->")
			//{// eat the preamble
			//    line = sr.ReadLine();
			//}
			//line = sr.ReadLine();

			//ResultParser parser = new ResultParser();
			////parser.ParseSummary
			//string rawResult = String.Empty;
			//while (line != null)
			//{
			//    StringBuilder result = new StringBuilder();
			//    ResultParser.ReadNextTextResult(sr, ref line, result);
			//    rawResult = result.ToString();
			//    if (ResultParser.IsTestResult(rawResult))
			//    {// Only raise event for tests completed, not fixtures and the like.
			//        RaiseComplete(result.ToString(), parser.ParseTest(rawResult));
			//    }
			//}
			//RaiseAllComplete(ResultParser.ParseSummary(rawResult));
		}

		private static string GetConsoleRunnerExePath()
		{
			string[] posible = Directory.GetDirectories(System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "NUnit*");
			return Path.Combine(posible[0], "bin\\nunit-console.exe");
		}
	}
}
