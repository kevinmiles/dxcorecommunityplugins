using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace RedGreen
{
	public class MbUnit3_1ResultParser : BaseResultParser, IResultParser
	{
		public MbUnit3_1ResultParser()
		{

		}

		public void ReadNextTextResult(TextReader reader, ref string line, StringBuilder result)
		{
			while (line.StartsWith("[") == false)
			{
				line = reader.ReadLine();
			}
			base.ReadNextTextResult(reader, ref line, result);
		}

		public TestResult ParseTest(string rawResult)
		{
			string header = rawResult.Substring(0, rawResult.IndexOf('\n'));
			string status = Regex.Match(header, @"\w+").Value;
			string kind = Regex.Match(header, @"\]\s\w+").Value.Substring(2);
			string location = header.Substring(header.LastIndexOf(' ') + 1);

			TestResult testResult = new TestResult();
			if (kind == ResultKind.Test)
			{
				//IGallioResultParser helper = _parserFactory.GetParser(engine);

				testResult.Location = location.Replace('/', '.');

				switch (status.ToLower())
				{
					case Status.Failed:
					case Status.Error:
						testResult.Status = TestStatus.Failed;
						testResult.Duration = string.Empty;
						string content = rawResult.Substring(rawResult.IndexOf('\n') + 1);
						testResult.Failure.Expected = GetExpected(content);
						testResult.Failure.Actual = GetActual(content);
						testResult.Failure.FailingStatement = DxCoreUtil.GetStatement(testResult.Location, GetLineNumber(content));
						testResult.Failure.ActualDiffersAt = GetPosition(content, testResult.Failure.Expected, testResult.Failure.Actual);
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

		private string GetExpected(string content)
		{
			if (content.Contains("\n\nExpected Value :"))
			{
				int startActual = content.IndexOf("\nActual");
				string uptoActual = content.Substring(0, startActual);
				int startExpected = uptoActual.IndexOf(':');
				if (startExpected > 0)
					return uptoActual.Substring(startExpected + 2);
			}
			return string.Empty;
		}

		private string GetActual(string content)
		{
			if (content.Contains("Actual Value :"))
			{
				int start = Regex.Match(content, "\nActual\\s").Index;
				start = content.IndexOf(':', start);
				int end = content.IndexOf("\n\n", start);
				return content.Substring(start + 2, end - start - 2);
			}
			return string.Empty;
		}

		private int GetPosition(string content, string expected, string actual)
		{
			int i = 0;
            for (; i < Math.Min(expected.Length, actual.Length); ++i)
			{
				if (expected[i] != actual[i])
				{
					break;
				}
			}
			return i;
		}

		private int GetLineNumber(string content)
		{
			try
			{
				return int.Parse(Regex.Match(content.Substring(content.LastIndexOf(":line")), "\\d+").Value);
			}
			catch (FormatException ex)
			{
				return 0;
			}
		}

		public bool IsTestResult(string rawResult)
		{
			return IsTestResultImpl(rawResult);
		}

		public SummaryResult ParseSummary(string rawResult)
		{
			//Disposing the test runner.\nStop time: 7:22 AM (Total execution time: 22.019 seconds)\n\n6 run, 1 passed, 5 failed, 0 inconclusive, 0 skipped\n\n\n";
			SummaryResult result = new SummaryResult();
			string footer = rawResult.Substring(rawResult.IndexOf("\nStop time:"));
			result.Duration = Regex.Match(footer, @"\d+\.\d+").Value;
			result.TotalCount = Regex.Match(Regex.Match(footer, @"\d+\srun").Value, @"\d+").Value;
			result.PassCount = Regex.Match(Regex.Match(footer, @"\d+\spassed").Value, @"\d+").Value;
			result.FailCount = Regex.Match(Regex.Match(footer, @"\d+\sfailed").Value, @"\d+").Value;
			result.SkipCount = Regex.Match(Regex.Match(footer, @"\d+\sskipped").Value, @"\d+").Value;
			return result;
		}
	}
}
