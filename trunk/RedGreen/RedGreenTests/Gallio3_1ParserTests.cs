using System;
using System.Text;
using Xunit;
using RedGreen;

namespace RedGreenTests
{
    class Gallio3_1ParserTests
	{
		[Fact]
		public void MbUnitFailedTestStringFail()
		{
			string rawResult = "[failed] Test RedGreenPlayground/MbUnitTests/AlwaysFails\nExpected values to be equal.\n\nExpected Value : \"who\'s there\"\nActual Value   : \"who\'s where\"\n\n   at RedGreenPlayground.MbUnitTests.AlwaysFails() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 21\n";
			IResultParser parser = new MbUnit3_1ResultParser();

			RedGreen.TestResult parsed = parser.ParseTest(rawResult);

			Assert.Equal(RedGreen.TestStatus.Failed, parsed.Status);
			Assert.Equal(string.Empty, parsed.Duration);
			Assert.Equal("\"who\'s there\"", parsed.Failure.Expected);
			Assert.Equal("\"who\'s where\"", parsed.Failure.Actual);
			Assert.Equal(7, parsed.Failure.ActualDiffersAt);
			Assert.Null(parsed.Failure.FailingStatement);
			Assert.Equal("RedGreenPlayground.MbUnitTests.AlwaysFails", parsed.Location);
		}

		[Fact]
		public void MbUnitPassed()
		{
			string rawResult = "[passed] Test RedGreenPlayground/MbUnitTests/AlwaysPass\n";
			IResultParser parser = new MbUnit3_1ResultParser();

			RedGreen.TestResult parsed = parser.ParseTest(rawResult);

			Assert.Equal(RedGreen.TestStatus.Passed, parsed.Status);
			Assert.Equal("RedGreenPlayground.MbUnitTests.AlwaysPass", parsed.Location);
		}

		[Fact]
		public void MbUnitFaildInt()
		{
			string rawResult = "[failed] Test RedGreenPlayground/MbUnitTests/IntFail\nExpected values to be equal.\n\nExpected Value : 0\nActual Value   : 1\n\n   at RedGreenPlayground.MbUnitTests.IntFail() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 28\n\n";
			IResultParser parser = new MbUnit3_1ResultParser();

			RedGreen.TestResult parsed = parser.ParseTest(rawResult);

			Assert.Equal(RedGreen.TestStatus.Failed, parsed.Status);
			Assert.Equal(string.Empty, parsed.Duration);
			Assert.Equal("0", parsed.Failure.Expected);
			Assert.Equal("1", parsed.Failure.Actual);
			Assert.Equal(0, parsed.Failure.ActualDiffersAt);
			Assert.Null(parsed.Failure.FailingStatement);
			Assert.Equal("RedGreenPlayground.MbUnitTests.IntFail", parsed.Location);
		}

		[Fact]
		public void NoExpectedValue_ReturnsEmptyString()
		{
			string rawResult = "[failed] Test RedGreenPlayground/MbUnitTests/NotNull\nExpected value to be non-null.\n\nActual Value : null\n\n   at RedGreenPlayground.MbUnitTests.NotNull() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 41\n\n\n";
			IResultParser parser = new MbUnit3_1ResultParser();

			RedGreen.TestResult parsed = parser.ParseTest(rawResult);

			Assert.Equal(string.Empty, parsed.Failure.Expected);
		}

		[Fact]
		public void ResultSplitter()
		{
			using (Gallio3_1DataSource reader = new Gallio3_1DataSource())
			{
				MbUnit3_1ResultParser parser = new MbUnit3_1ResultParser();
				string line = reader.ReadLine();
				StringBuilder result;
				while (line != "running the tests.")
				{// eat the preamble
					line = reader.ReadLine().ToLower();
				}
				line = reader.ReadLine();

				string correct;
				result = new StringBuilder();
				parser.ReadNextTextResult(reader, ref line, result);
				correct = "[failed] Test RedGreenPlayground/MbUnitTests/AlwaysFails\nExpected values to be equal.\n\nExpected Value : \"who\'s there\"\nActual Value   : \"who\'s where\"\n\n   at RedGreenPlayground.MbUnitTests.AlwaysFails() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 21\n\n\n";
				Assert.Equal(correct, result.ToString());
	
				result = new StringBuilder();
				parser.ReadNextTextResult(reader, ref line, result);
				correct = "[passed] Test RedGreenPlayground/MbUnitTests/AlwaysPass\n";
				Assert.Equal(correct, result.ToString());
				
				result = new StringBuilder();
				parser.ReadNextTextResult(reader, ref line, result);
				correct = "[failed] Test RedGreenPlayground/MbUnitTests/IntFail\nExpected values to be equal.\n\nExpected Value : 0\nActual Value   : 1\n\n   at RedGreenPlayground.MbUnitTests.IntFail() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 28\n\n\n";
				Assert.Equal(correct, result.ToString());
				
				result = new StringBuilder();
				parser.ReadNextTextResult(reader, ref line, result);
				correct = "[failed] Test RedGreenPlayground/MbUnitTests/NotNull\nExpected value to be non-null.\n\nActual Value : null\n\n   at RedGreenPlayground.MbUnitTests.NotNull() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 41\n\n\n";
				Assert.Equal(correct, result.ToString());
				
				result = new StringBuilder();
				parser.ReadNextTextResult(reader, ref line, result);
				correct = "[failed] Test RedGreenPlayground/MbUnitTests/Skipped\nExpected values to be equal.\n\nExpected Value : 5\nActual Value   : 500\n\n   at RedGreenPlayground.MbUnitTests.Skipped() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 34\n\n\n";
				Assert.Equal(correct, result.ToString());
				
				result = new StringBuilder();
				parser.ReadNextTextResult(reader, ref line, result);
				correct = "[failed] Test RedGreenPlayground/MbUnitTests/Throws\nExecute\nSystem.Exception: misc exception\n   at RedGreenPlayground.MbUnitTests.Throws() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 50\n\nHost stopped at 12/16/2009 7:22:44 AM.\nHost process exited with code: 0\nHost started at 12/16/2009 7:22:45 AM.\nRunning under CLR v2.0.50727 runtime.\nHost stopped at 12/16/2009 7:22:47 AM.\nHost process exited with code: 0\nHost started at 12/16/2009 7:22:48 AM.\nRunning under CLR v2.0.50727 runtime.\nHost stopped at 12/16/2009 7:22:53 AM.\nHost process exited with code: 0\nDisposing the test runner.\nStop time: 7:22 AM (Total execution time: 22.019 seconds)\n\n6 run, 1 passed, 5 failed, 0 inconclusive, 0 skipped\n\n\n";
				Assert.Equal(correct, result.ToString());
			
				Assert.Equal(null, line);
			}
		}

		[Fact]
		public void ResultIsTest()
		{
			IResultParser parser = new MbUnit3_1ResultParser();
			string rawResult = "[passed] Test RedGreenPlayground/MbUnitTests/AlwaysPass\n";


			Assert.True(parser.IsTestResult(rawResult));
		}

		[Fact]
		public void DisectSummary()
		{
			IResultParser parser = new MbUnit3_1ResultParser();
			string rawResult = "[failed] Test RedGreenPlayground/MbUnitTests/Throws\nExecute\nSystem.Exception: misc exception\n   at RedGreenPlayground.MbUnitTests.Throws() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 50\n\nHost stopped at 12/16/2009 7:22:44 AM.\nHost process exited with code: 0\nHost started at 12/16/2009 7:22:45 AM.\nRunning under CLR v2.0.50727 runtime.\nHost stopped at 12/16/2009 7:22:47 AM.\nHost process exited with code: 0\nHost started at 12/16/2009 7:22:48 AM.\nRunning under CLR v2.0.50727 runtime.\nHost stopped at 12/16/2009 7:22:53 AM.\nHost process exited with code: 0\nDisposing the test runner.\nStop time: 7:22 AM (Total execution time: 22.019 seconds)\n\n6 run, 1 passed, 5 failed, 0 inconclusive, 0 skipped\n\n\n";

			SummaryResult result = parser.ParseSummary(rawResult);

			Assert.Equal("22.019", result.Duration);
			Assert.Equal("6", result.TotalCount);
			Assert.Equal("1", result.PassCount);
			Assert.Equal("5", result.FailCount);
			Assert.Equal("0", result.SkipCount);
		}

	}
}
