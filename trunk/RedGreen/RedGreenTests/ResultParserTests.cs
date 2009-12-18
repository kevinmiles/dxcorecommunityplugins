using System;
using System.Text;
using Xunit;
using RedGreen;

namespace RedGreenTests
{
    public class Result305ParserTests
    {
        [Fact]
		public void MbUnitFailedTestStringFail()
		{
			string rawResult = "[failed] Test MbUnit v3.0.5.546/RedGreenPlayground/MbUnitTests/AlwaysFails\nExpected values to be equal.\n\nExpected Value : \"who\'s there\"\nActual Value   : \"who\'s where\"\n\n   at RedGreenPlayground.MbUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 21\n";
			ResultParser parser = new ResultParser();

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
			string rawResult = "[passed] Test MbUnit v3.0.5.546/RedGreenPlayground/MbUnitTests/AlwaysPass\n";
			ResultParser parser = new ResultParser();

			RedGreen.TestResult parsed = parser.ParseTest(rawResult);

			Assert.Equal(RedGreen.TestStatus.Passed, parsed.Status);
			Assert.Equal("RedGreenPlayground.MbUnitTests.AlwaysPass", parsed.Location);
		}

        [Fact]
		public void NUnitFaildInt()
		{
			string rawResult = "[failed] Test NUnit v2.4.7.0/RedGreenPlayground/NUnitTests/IntFail\nMessage\n  Expected: 0\n  But was:  1\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual)\n   at RedGreenPlayground.NUnitTests.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 28\n\n";
			ResultParser parser = new ResultParser();

			RedGreen.TestResult parsed = parser.ParseTest(rawResult);

			Assert.Equal(RedGreen.TestStatus.Failed, parsed.Status);
			Assert.Equal(string.Empty, parsed.Duration);
			Assert.Equal("0", parsed.Failure.Expected);
			Assert.Equal("1", parsed.Failure.Actual);
			Assert.Equal(0, parsed.Failure.ActualDiffersAt);
			Assert.Null(parsed.Failure.FailingStatement);
			Assert.Equal("RedGreenPlayground.NUnitTests.IntFail", parsed.Location);
		}

        [Fact]
        public void ResultSplitter()
        {
            string rawResult = "[failed] Test MbUnit v3.0.5.546/RedGreenPlayground/MbUnitTests/AlwaysFails\nExpected values to be equal.\n\nExpected Value : \"who\'s there\"\nActual Value   : \"who\'s where\"\n\n   at RedGreenPlayground.MbUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 21\n[passed] Test MbUnit v3.0.5.546/RedGreenPlayground/MbUnitTests/AlwaysPass\n[failed] Test NUnit v2.4.7.0/RedGreenPlayground/NUnitTests/IntFail\nMessage\n  Expected: 0\n  But was:  1\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual)\n   at RedGreenPlayground.NUnitTests.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 28\n\n[failed] Root \nUnloading the test package.\n* Host stopped at 1/22/2009 9:31:41 AM.\n* Host process exited with code: 0\nDisposing the test runner.\nStop time: 9:31 AM (Total execution time: 4.118 seconds)\n\n15 run, 3 passed, 12 failed (1 error), 0 inconclusive, 2 skipped (1 ignored)\n";
            System.IO.StringReader reader = new System.IO.StringReader(rawResult);
            string line = reader.ReadLine();
            StringBuilder result;
			ResultParser parser = new ResultParser();

            result = new StringBuilder();
			parser.ReadNextTextResult(reader, ref line, result);
            Assert.Equal("[failed] Test MbUnit v3.0.5.546/RedGreenPlayground/MbUnitTests/AlwaysFails\nExpected values to be equal.\n\nExpected Value : \"who\'s there\"\nActual Value   : \"who\'s where\"\n\n   at RedGreenPlayground.MbUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 21\n", result.ToString());

            result = new StringBuilder();
			parser.ReadNextTextResult(reader, ref line, result);
            Assert.Equal("[passed] Test MbUnit v3.0.5.546/RedGreenPlayground/MbUnitTests/AlwaysPass\n", result.ToString());

            result = new StringBuilder();
			parser.ReadNextTextResult(reader, ref line, result);
            Assert.Equal("[failed] Test NUnit v2.4.7.0/RedGreenPlayground/NUnitTests/IntFail\nMessage\n  Expected: 0\n  But was:  1\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual)\n   at RedGreenPlayground.NUnitTests.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 28\n\n", result.ToString());

            result = new StringBuilder();
			parser.ReadNextTextResult(reader, ref line, result);
            Assert.Equal("[failed] Root \nUnloading the test package.\n* Host stopped at 1/22/2009 9:31:41 AM.\n* Host process exited with code: 0\nDisposing the test runner.\nStop time: 9:31 AM (Total execution time: 4.118 seconds)\n\n15 run, 3 passed, 12 failed (1 error), 0 inconclusive, 2 skipped (1 ignored)\n", result.ToString());
        }

        [Fact]
        public void ResultIsTest()
        {
			ResultParser parser = new ResultParser();

            string rawResult = "[passed] Test MbUnit v3.0.5.546/RedGreenPlayground/MbUnitTests/AlwaysPass\n";
            

            Assert.True(parser.IsTestResult(rawResult));
        }

        [Fact]
        public void ResultIsNotTest()
        {
			ResultParser parser = new ResultParser();

			Assert.False(parser.IsTestResult("[failed] Fixture MbUnit v3.0.5.546/RedGreenPlayground/MbUnitTests\n"));
			Assert.False(parser.IsTestResult("[failed] Assembly MbUnit v3.0.5.546/RedGreenPlayground\n"));
			Assert.False(parser.IsTestResult("[failed] Framework MbUnit v3.0.5.546\n"));
        }

        [Fact]
        public void DisectSummary()
        {
			ResultParser parser = new ResultParser();
			string rawResult = "[failed] Root \nUnloading the test package.\n* Host stopped at 1/22/2009 9:31:41 AM.\n* Host process exited with code: 0\nDisposing the test runner.\nStop time: 9:31 AM (Total execution time: 4.118 seconds)\n\n15 run, 3 passed, 12 failed (1 error), 0 inconclusive, 2 skipped (1 ignored)\n";

			SummaryResult result = parser.ParseSummary(rawResult);

            Assert.Equal("4.118", result.Duration);
            Assert.Equal("15", result.TotalCount);
            Assert.Equal("3", result.PassCount);
            Assert.Equal("12", result.FailCount);
            Assert.Equal("2", result.SkipCount);
        }
    }
}
