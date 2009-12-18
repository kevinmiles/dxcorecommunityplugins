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
    public class ResultParser : BaseResultParser, IResultParser
    {

        private ResultParserFactory _parserFactory = new ResultParserFactory();

		public void ReadNextTextResult(TextReader reader, ref string line, StringBuilder result)
		{
			base.ReadNextTextResult(reader, ref line, result);
		}

        public bool IsTestResult(string rawResult)
        {
			return IsTestResultImpl(rawResult);
        }

        public TestResult ParseTest(string rawResult)
        {
            int statusEnd = rawResult.IndexOf(' ');
            int kindEnd = rawResult.IndexOf(' ', statusEnd + 1);
            int engineEnd = rawResult.IndexOf(' ', kindEnd + 1);
            int versionEnd = rawResult.IndexOf('/', engineEnd + 1);
            int locationEnd = rawResult.IndexOf('\n', versionEnd + 1);
            string status = rawResult.Substring(1, statusEnd - 2);
            string kind = rawResult.Substring(statusEnd + 1, kindEnd - statusEnd - 1);
			string engine = rawResult.Substring(kindEnd + 1, engineEnd - kindEnd - 1);
            string location = rawResult.Substring(versionEnd + 1, locationEnd - versionEnd - 1);

            TestResult testResult = new TestResult();

            if (kind == ResultKind.Test)
            {
                IGallioResultParser helper = _parserFactory.GetParser(engine);

                testResult.Location = helper.ReformatLocation(location);

                switch (status.ToLower())
                {
                    case Status.Failed:
                    case Status.Error:
                        testResult.Status = TestStatus.Failed;
                        testResult.Duration = string.Empty;
                        string content = rawResult.Substring(locationEnd);
                        testResult.Failure.Expected = helper.GetExpected(content);
                        testResult.Failure.Actual = helper.GetActual(content);
                        testResult.Failure.FailingStatement = DxCoreUtil.GetStatement(testResult.Location, helper.GetLineNumber(content));
                        testResult.Failure.ActualDiffersAt = helper.GetPosition(content, testResult.Failure.Expected, testResult.Failure.Actual);
                        break;

                    case Status.Passed:
                        testResult.Status = TestStatus.Passed;
                        testResult.Duration = string.Empty;
                        break;

                    default:
                    case Status.Skipped:
                    case Status.Ignored:
                        testResult.Status = TestStatus.Skipped;
                        break;
                }
            }
            return testResult;
        }

        public SummaryResult ParseSummary(string rawResult)
        {
            const string kDurationStart = "(Total execution time: ";
            int durationStart = rawResult.IndexOf(kDurationStart) + kDurationStart.Length;
            int durationEnd = rawResult.IndexOf(" sec", durationStart);

            int totalEnd = rawResult.IndexOf(" run", durationEnd);
            int totalStart = rawResult.IndexOf("\n\n", durationEnd) + 2;

            int passedEnd = rawResult.IndexOf(" passed", durationEnd);
            int passedStart = rawResult.LastIndexOf(' ', passedEnd - 1) + 1;

            int failedEnd = rawResult.IndexOf(" failed", durationEnd);
            int failedStart = rawResult.LastIndexOf(' ', failedEnd - 1) + 1;

            int skippedEnd = rawResult.IndexOf(" skipped", durationEnd);
            int skippedStart = rawResult.LastIndexOf(' ', skippedEnd - 1) + 1;

            SummaryResult result = new SummaryResult();
            result.Duration = rawResult.Substring(durationStart, durationEnd - durationStart);
            result.TotalCount = rawResult.Substring(totalStart, totalEnd - totalStart);
            result.PassCount = rawResult.Substring(passedStart, passedEnd - passedStart);
            result.FailCount = rawResult.Substring(failedStart, failedEnd - failedStart);
            result.SkipCount = rawResult.Substring(skippedStart, skippedEnd - skippedStart);

            return result;
        }
    }
}
