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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;
using DevExpress.CodeRush.StructuralParser;

namespace RedGreen
{
    /// <summary>
    /// Test runer for the Xunit unit testing framework
    /// </summary>
    internal class XunitRunner : BaseTestRunner, ITestRunner
    {
        #region Public Interface
        public XunitRunner()
        {
            AddAttributes("Fact", "Theory");
            AddFrameworks("Xunit");
        }

        /// <summary>
        /// Run a single unit test
        /// </summary>
        /// <param name="assemblyPath">The assembly that contains the unit test.</param>
        /// <param name="className">The full name of the class that contains the unit test.</param>
        /// <param name="methodName">The name of the unit test method</param>
        public void RunMethod(string assemblyPath, string assemblyName, string className, string methodName)
        {
            System.Xml.XmlNode returnValue = null;

            using (ExecutorWrapper wrapper = new ExecutorWrapper(assemblyPath, null, true))
            {
                returnValue = wrapper.RunTest(className, methodName, delegate(System.Xml.XmlNode node) { return true; });
            }

            ParseResults(returnValue);
        }

        /// <summary>
        /// Run all the unit tests in the class
        /// </summary>
        /// <param name="assemblyPath">The assembly that contains the unit test.</param>
        /// <param name="className">The full name of the class that contains the unit test.</param>
        public void RunClass(string assemblyPath, string assemblyName, string className)
        {
            System.Xml.XmlNode returnValue = null;

            using (ExecutorWrapper wrapper = new ExecutorWrapper(assemblyPath, null, true))
            {
                returnValue = wrapper.RunClass(className, delegate(System.Xml.XmlNode node) { return true; });
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
            if (resultSource == null)
            {
                RaiseAllComplete("0", "0", "0", "0");
                return;
            }

            for (int i = 0; i < resultSource.ChildNodes.Count; i++)
            {
                ProcessTest(XunitResultXmlUtility.GetResult(resultSource, i));
            }

            string passed = GetAttribute(resultSource, "passed");
            string failed = GetAttribute(resultSource, "failed");
            string skipped = GetAttribute(resultSource, "skipped");
            string duration = GetAttribute(resultSource, "time");
            RaiseAllComplete(passed, failed, skipped, duration);
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
            testResult.MethodLocation = GetAttribute(xmlResult, "name");
            string rawResult = string.Empty;

            switch (GetAttribute(xmlResult, "result"))
            {
                case "Fail":
                    rawResult = FormatFailure(xmlResult.InnerText);

                    testResult.Result = TestStatus.Failed;
                    testResult.Durration = GetAttribute(xmlResult, "time");
                    testResult.FailAtLine = GetLineNumber(xmlResult.InnerText);
                    testResult.Position = GetPosition(xmlResult.InnerText);
                    testResult.Expected = GetExpected(xmlResult.InnerText);
                    testResult.Actual = GetActual(xmlResult.InnerText);
                    break;

                case "Pass":
                    testResult.Result = TestStatus.Passed;
                    testResult.Durration = GetAttribute(xmlResult, "time");
                    break;

                case "Skip":
                    testResult.Result = TestStatus.Skipped;
                    break;
            }
            RaiseComplete(rawResult, testResult);
        }

        /// <summary>
        /// This is to put the location on a line by itself and order it so that if you click on it, VS will take you to the spot
        /// The file path has to be first and the location needs to follow in parens. 
        /// I may be all wet on the requirements, I figured it out by looking at what others did and noticing that other formats don't work.
        /// </summary>
        public static string FormatFailure(string failure)
        {
            Regex locateActual = new Regex(@"\r\nActual:\s");
            Regex locateActualEnd = new Regex(@"\s\s\sat");
            int actualEnd = locateActualEnd.Match(failure, locateActual.Match(failure).Index).Index + 3;

            Regex locateFilePath = new Regex(@"[A-Za-z]\:");

            Regex locateLineNumber = new Regex(":line ");

            int filePathStart = locateFilePath.Match(failure, actualEnd).Index;
            return string.Format("{0}\r\n{1},0): {2}",
                       failure.Substring(0, actualEnd),
                       locateLineNumber.Replace(failure.Substring(filePathStart), "("),
                       failure.Substring(actualEnd, filePathStart - actualEnd - 4));//-4 to remove " in "
        }

        /// <summary>
        /// For locating the failures and putting in some kind of interactive UI
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int GetLineNumber(string source)
        {
            string trimmed = source.Trim();
            return int.Parse(trimmed.Substring(trimmed.LastIndexOf(' ')));
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
                int actualLength = source.IndexOf("   at ", actualStart) - actualStart;
                return source.Substring(actualStart, actualLength);
            }
            return String.Empty;
        }
        #endregion
    }
}
