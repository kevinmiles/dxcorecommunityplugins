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
	internal class NUnitParser
	{
		public static List<TestResult> ParseTestResults(List<string> source)
		{
			List<string> testCases = GetTestCases(source);
			List<TestResult> results = ParseTestCases(testCases);
			return results;
		}
		private static List<TestResult> ParseTestCases(List<string> testCases)
		{
			List<TestResult> results = new List<TestResult>();
			foreach (string resultData in testCases)
			{
				results.Add(ParseCase(resultData));
			}
			return results;
		}
		private static TestResult ParseCase(string testCase)
		{
			TestResult result = new TestResult();
			ParseCommonData(testCase, result);
			ParseFailure(testCase, result);
			return result;
		}
		private static void ParseFailure(string testCase, TestResult result)
		{
			if (result.Status == TestStatus.Failed)
			{
				string kExpectedStartDelimiter = "Expected: ";
				string kActualStartDelimiter = "But was:  ";
				int expectedStart = testCase.IndexOf(kExpectedStartDelimiter, testCase.IndexOf("<message"));
				if (expectedStart > 0)
				{
					int actualStart = testCase.IndexOf(kActualStartDelimiter, expectedStart);
					result.Failure.Expected = testCase.Substring(expectedStart + kExpectedStartDelimiter.Length, actualStart - expectedStart - kExpectedStartDelimiter.Length);

					int actualEnd = testCase.IndexOf('\"', actualStart + kActualStartDelimiter.Length + 1) + 1;
					if (actualEnd == 0)
					{
						actualEnd = testCase.IndexOf("]]", actualStart);
					}
					result.Failure.Actual = testCase.Substring(actualStart + kActualStartDelimiter.Length, actualEnd - actualStart - kActualStartDelimiter.Length);

					if (testCase.Contains("CDATA[  String"))
					{

						int differStart = testCase.LastIndexOf(' ', expectedStart) + 1;
						result.Failure.ActualDiffersAt = int.Parse(testCase.Substring(differStart, expectedStart - differStart - 1));
					}

					const string kLineStartDelimiter = ":line ";
					int lineStart = testCase.LastIndexOf(kLineStartDelimiter) + kLineStartDelimiter.Length;
					int lineEnd = testCase.IndexOf("]]", lineStart);
					int lineNumber = int.Parse(testCase.Substring(lineStart, lineEnd - lineStart));
					result.Failure.FailingStatement = DxCoreUtil.GetStatement(result.Location, lineNumber);
				}
			}
		}
		private static void ParseCommonData(string testCase, TestResult testResult)
		{
			string openTag = testCase.Substring(testCase.IndexOf('<'));
			string[] attributes = openTag.Split(' ');
			foreach (string keyValue in attributes)
			{
				if (keyValue.Contains("="))
                {
					int delimiter = keyValue.IndexOf("=");
					string key = keyValue.Substring(0, delimiter);
					string value = keyValue.Substring(delimiter + 2, keyValue.Length - delimiter - 3);
					switch (key)
					{
						case "name":
							testResult.Location = value;
							break;
						case "success":
							if (value == "False")
							{
								testResult.Status = TestStatus.Failed;
							}
							else if (value == "True")
							{
								testResult.Status = TestStatus.Passed;
							}
							else
							{
								testResult.Status = TestStatus.Unknown;
							}
							break;
						case "time":
							testResult.Duration = value;
							break;
						case "executed":
							testResult.Status = TestStatus.Skipped;
							break;
						default:
							break;
					}
				}
			}
		}
		private static List<string> GetTestCases(List<string> source)
		{
			int offset = 0;
			while (offset < source.Count && source[offset].Trim().StartsWith("<test-case") == false)
			{
				offset++;
			}
			StringBuilder testCase = new StringBuilder();
			List<string> testCases = new List<string>();
			while (offset < source.Count)
			{
				string line = source[offset++].Trim();
				if (line.StartsWith("<test-case"))
				{
					if (testCase.Length > 0)
					{
						testCases.Add(testCase.ToString());
					}
					testCase = new StringBuilder();
				}
				testCase.Append(line);
			}
			if (testCase.Length > 0)
			{
				testCases.Add(testCase.ToString());
			}
			return testCases;
		}
        public static SummaryResult ParseSummary(List<string> source)
		{
			SummaryResult result = new SummaryResult();
			string summaryLine = source.Find(line => line.Trim().StartsWith("<test-results name="));
			if (!string.IsNullOrEmpty(summaryLine))
			{
				//<test-results name="redgreenplayground.dll" total="5" failures="4" not-run="1" date="2009-10-02" time="10:35:46">
				string[] attributes = summaryLine.Split(' ');
				foreach (string keyValue in attributes)
				{
					if (keyValue.Contains("="))
					{
						int delimiter = keyValue.IndexOf("=");
						string key = keyValue.Substring(0, delimiter);
						string value = keyValue.Substring(delimiter + 2, keyValue.Length - delimiter - 3);
						switch (key)
						{
							case "total":
								result.TotalCount = value;
								break;
							case "failures":
								result.FailCount = value;
								break;
							case "not-run":
								result.SkipCount = value;
								break;
							default:
								break;
						}
					}
				}
				result.PassCount = (int.Parse(result.TotalCount) - int.Parse(result.FailCount)).ToString();
				result.Duration = String.Empty;
			}
			return result;
		}
	}
}
