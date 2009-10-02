///*
// * Software License Agreement for RedGreen
// * 
// * Copyright (c) 2008 Renaissance Learning, Inc. and James Argeropoulos
// * 
// * Permission is hereby granted, free of charge, to any person obtaining a copy
// * of this software and associated documentation files (the "Software"), to deal
// * in the Software without restriction, including without limitation the rights
// * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// * copies of the Software, and to permit persons to whom the Software is
// * furnished to do so, subject to the following conditions:
// * 
// * The above copyright notice and this permission notice shall be included in
// * all copies or substantial portions of the Software.
// * 
// * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// * THE SOFTWARE.
// */

using System;
using Xunit;

namespace RedGreen
{
    /// <summary>
    /// Test runer for the Xunit unit testing framework
    /// </summary>
    internal class XunitRunner : BaseTestRunner//, ITestRunner
    {
        #region Public Interface
        public XunitRunner()
        {
			//AddAttributes("Fact", "Theory");
			//AddFrameworks("Xunit");
        }

        /// <summary>
        /// Run a single unit test
        /// </summary>
        /// <param name="assemblyPath">The assembly that contains the unit test.</param>
        /// <param name="className">The full name of the class that contains the unit test.</param>
        /// <param name="methodName">The name of the unit test method</param>
		override public void RunTests(string assemblyPath, string assemblyName, string className, string methodName)
        {
            System.Xml.XmlNode returnValue = null;

            using (ExecutorWrapper wrapper = new ExecutorWrapper(assemblyPath, null, true))
            {
				returnValue = wrapper.RunTest(className, methodName, node => true);
            }

            ParseResults(returnValue);
        }

        /// <summary>
        /// Run all the unit tests in the class
        /// </summary>
        /// <param name="assemblyPath">The assembly that contains the unit test.</param>
        /// <param name="className">The full name of the class that contains the unit test.</param>
		override public void RunTests(string assemblyPath, string assemblyName, string className)
        {
            System.Xml.XmlNode returnValue = null;

            using (ExecutorWrapper wrapper = new ExecutorWrapper(assemblyPath, null, true))
            {
				returnValue = wrapper.RunClass(className, node => true);
            }

            ParseResults(returnValue);
        }

		/// <summary>
		/// Run all the unit tests in the class
		/// </summary>
		/// <param name="assemblyPath">The assembly that contains the unit test.</param>
		/// <param name="className">The full name of the class that contains the unit test.</param>
		override public void RunTests(string assemblyPath, string assemblyName)
		{
			System.Xml.XmlNode returnValue = null;

			using (ExecutorWrapper wrapper = new ExecutorWrapper(assemblyPath, null, true))
			{
				returnValue = wrapper.RunAssembly(node => true);
			}

			ParseResults(returnValue);
		}
        #endregion

        #region Implementation
        /// <summary>
        /// Break out results and raise appropriate events
        /// </summary>
        /// <param name="resultSource"></param>
        private void ParseResults(System.Xml.XmlNode resultSource)
        {
			SummaryResult result = new SummaryResult();
			if (resultSource == null)
			{
				result.Duration = "0";
				result.TotalCount = "0";
				result.PassCount = "0";
				result.FailCount = "0";
				result.SkipCount = "0";
			}
			else
			{
				for (int i = 0; i < resultSource.ChildNodes.Count; i++)
				{
					ProcessTest(XunitResultXmlUtility.GetResult(resultSource, i));
				}
				result.Duration = GetAttribute(resultSource, "time");
				result.TotalCount = "0";
				result.PassCount = GetAttribute(resultSource, "passed");
				result.FailCount = GetAttribute(resultSource, "failed");
				result.SkipCount = GetAttribute(resultSource, "skipped");
			}
            RaiseAllComplete(result);
        }

        /// <summary>
        /// Helper to get an attribute value as a string
        /// </summary>
        /// <param name="source">XmlNode that contains the attributes</param>
        /// <param name="key">Which attribute to return</param>
        /// <returns></returns>
        private static string GetAttribute(System.Xml.XmlNode source, string key)
        {
            return source.Attributes[key].Value.ToString();
        }

        /// <summary>
        /// Break out results for a single test and raise event
        /// </summary>
        /// <param name="xmlResult">source of result data</param>
        private void ProcessTest(System.Xml.XmlNode xmlResult)
        {
			TestResult testResult = new TestResult();
			testResult.Location = GetAttribute(xmlResult, "name");
			string rawResult = string.Empty;

			switch (GetAttribute(xmlResult, "result"))
			{
				case "Fail":
					rawResult = xmlResult.InnerText;

					testResult.Status = TestStatus.Failed;
					testResult.Duration = GetAttribute(xmlResult, "time");
					testResult.Failure.Expected = GetExpected(rawResult);
					testResult.Failure.Actual = GetActual(rawResult);
					testResult.Failure.FailingStatement = DxCoreUtil.GetStatement(testResult.Location, GetLineNumber(rawResult));
					testResult.Failure.ActualDiffersAt = GetPosition(rawResult);
					break;

				case "Pass":
					testResult.Status = TestStatus.Passed;
					testResult.Duration = GetAttribute(xmlResult, "time");
					break;

				case "Skip":
					testResult.Status = TestStatus.Skipped;
					break;
			}
			RaiseComplete(rawResult, testResult);
        }

        /// <summary>
        /// For locating the failures and putting in some kind of interactive UI
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int GetLineNumber(string source)
        {
            string trimmed = source.Trim();
			int lineNumber = 0;
			try
			{
			    int lineNumberStart = trimmed.LastIndexOf(" ");
			    lineNumber = int.Parse(trimmed.Substring(lineNumberStart));
			}
			catch (FormatException ex)
			{
				int lineNumberEnd = trimmed.LastIndexOf(",0): at");
				int lineNumberStart = trimmed.LastIndexOf('(', lineNumberEnd) + 1;
				lineNumber = int.Parse(trimmed.Substring(lineNumberStart, lineNumberEnd - lineNumberStart));
			}
			return lineNumber;
        }

        public static int GetPosition(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return -1;
            }
            string positionStartExpression = "\r\nPosition: ";
            int positionStart = source.IndexOf(positionStartExpression) + positionStartExpression.Length;
            if (positionStart >= positionStartExpression.Length)
            {
                int positionLength = source.IndexOf("\r\nExpected:") - positionStart;
                string positionText = source.Substring(positionStart, positionLength);
                return int.Parse(positionText.Substring(positionText.LastIndexOf(" ")));
            }
            return -1;
        }

        public static string GetExpected(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }
            string expectedStartExpression = "\r\nExpected: ";
            int expectedStart = source.IndexOf(expectedStartExpression) + expectedStartExpression.Length;
            if (expectedStart >= expectedStartExpression.Length)
            {
                int expectedLength = source.IndexOf("\r\nActual:") - expectedStart;
                return source.Substring(expectedStart, expectedLength);
            }
            return String.Empty;
        }

		public static string GetActual(string source)
		{
			if (string.IsNullOrEmpty(source))
			{
				return string.Empty;
			}
			string actualStartExpression = "\r\nActual:   ";
			int actualStart = source.IndexOf(actualStartExpression) + actualStartExpression.Length;
			if (actualStart >= actualStartExpression.Length)
			{
				int filePath = source.IndexOf(":\\", actualStart); //Find three spaces in front of a file path. Can't find a drive letter cause that might chagne.
				int actualEnd = source.LastIndexOf("   ", filePath);
				return source.Substring(actualStart, actualEnd - actualStart);
			}
			return String.Empty;
		}
        #endregion
    }
}
