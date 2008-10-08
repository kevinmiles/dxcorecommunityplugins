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
    /// The hook into Gallio after each test is complete. 
    /// There is a double indirection going on here. 
    /// GallioLogExtension emits an event, which is caught and then re-emitted 
    /// by the runner. That gives one central place to have users subscribe to 
    /// events from.
    /// </summary>
    class GallioLogExtension : LogExtension
    {
        public event TestCompleteEventHandler TestComplete;
        private List<IGallioResultParser> _delimiterFactories = new List<IGallioResultParser>();

        public GallioLogExtension()
        {
            _delimiterFactories.Add(new NUnitGallioParser());
            _delimiterFactories.Add(new MbUnitGallioParser());
            _delimiterFactories.Add(new XunitGallioParser());
        }

        /// <summary>
        /// Respond to the test complete data
        /// </summary>
        /// <param name="e">source of data</param>
        protected override void LogTestStepFinished(Gallio.Runner.Events.TestStepFinishedEventArgs e)
        {
            base.LogTestStepFinished(e);


            TestOutcome outcome = e.TestStepRun.Result.Outcome;

            if (e.GetStepKind() == "Test")
            {
                TestResult testResult = new TestResult();
                IGallioResultParser helper = GetResultParser(e);

                string rawResult = String.Empty;
                testResult.Location = helper.ReformatLocation(e.Test.FullName.Substring(e.Test.FullName.IndexOf("/") + 1));

                switch (outcome.DisplayName.ToLower())
                {
                    case "failed":
                    case "error":
                        testResult.Status = TestStatus.Failed;
                        testResult.Durration = GetTestDuration(e.TestStepRun);
                        rawResult = FormatFailureMessage(e.TestStepRun);
                        testResult.Failure.Expected = helper.GetExpected(rawResult);
                        testResult.Failure.Actual = helper.GetActual(rawResult);
                        testResult.Failure.FailingStatement = DxCoreUtil.GetStatement(testResult.Location, helper.GetLineNumber(rawResult, testResult.Location));
                        testResult.Failure.ActualDiffersAt = helper.GetPosition(rawResult, testResult.Failure.Expected, testResult.Failure.Actual);
                        break;

                    case "passed":
                        testResult.Status = TestStatus.Passed;
                        testResult.Durration = GetTestDuration(e.TestStepRun);
                        break;

                    default:
                    case "skipped":
                    case "ignored":
                        testResult.Status = TestStatus.Skipped;
                        break;
                }
                RaiseComplete(rawResult, testResult);
            }
        }

        /// <summary>
        /// Create the correct parser object for given test result
        /// </summary>
        /// <param name="e">Data that needs to be parsed</param>
        /// <returns>A parser tuned to the testing framework used.</returns>
        private IGallioResultParser GetResultParser(Gallio.Runner.Events.TestStepFinishedEventArgs e)
        {
            IGallioResultParser parser = _delimiterFactories.Find(delegate(IGallioResultParser h)
            {
                return e.Test.FullName.ToLower().StartsWith(h.Framwork.ToLower());
            });
            if (parser != null)
            {
                return parser;
            }
            return new NullGallioParser();
        }

        /// <summary>
        /// Emmit the TestComplete event
        /// </summary>
        /// <param name="raw">result of test in text form</param>
        /// <param name="parsed">result of in type form</param>
        protected void RaiseComplete(string raw, TestResult parsed)
        {
            if (TestComplete != null)
            {
                TestComplete(this, new TestCompleteEventArgs(raw, parsed));
            }
        }

        /// <summary>
        /// Parse the amount of time used to run this test
        /// </summary>
        /// <param name="testStepRun">Information source</param>
        /// <returns></returns>
        private static string GetTestDuration(TestStepRun testStepRun)
        {
            return TimeSpan.FromTicks(testStepRun.EndTime.Ticks - testStepRun.StartTime.Ticks).Seconds.ToString();
        }

        /// <summary>
        /// Get the failure message for a failed test
        /// </summary>
        /// <param name="testStepRun">Information Source</param>
        /// <returns></returns>
        private static string FormatFailureMessage(TestStepRun testStepRun)
        {
            string warnings = FormatStream(testStepRun, LogStreamNames.Warnings);
            string failures = FormatStream(testStepRun, LogStreamNames.Failures);
            if (string.IsNullOrEmpty(warnings))
            {
                return failures;
            }
            else
            {
                return String.Format("{0}{1}{2}", warnings, Environment.NewLine, failures);
            }
        }
        
        /// <summary>
        /// Get the text from the source streams
        /// </summary>
        /// <param name="testStepRun">Test we want data for</param>
        /// <param name="streamName">Information source</param>
        /// <returns></returns>
        private static string FormatStream(TestStepRun testStepRun, string streamName)
        {
            ExecutionLogStream stream = testStepRun.ExecutionLog.Streams.Find(delegate(ExecutionLogStream s){return s.Name == streamName;});
            return stream != null ? stream.ToString() : String.Empty;
        }
    }

}
