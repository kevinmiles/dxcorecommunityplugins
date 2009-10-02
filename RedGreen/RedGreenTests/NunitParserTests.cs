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
using Xunit;
using RedGreen;

namespace RedGreenTests
{
    public class NunitParserTests
    {
        [Fact]
        public void ExpectedNotPresent()
        {
            Assert.Equal(String.Empty, new NUnitGallioParser().GetExpected(String.Empty));
            Assert.Equal(String.Empty, new NUnitGallioParser().GetExpected(null));
        }

        [Fact]
        public void StringExpected()
        {
            string source = "Message\n  String lengths are both 11. Strings differ at index 6.\n  Expected: \"who's there\"\n  But was:  \"who's where\"\n  -----------------^\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\n   at RedGreenPlayground.NUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 21";
            string expected = "\"who's there\"";
            Assert.Equal(expected, new NUnitGallioParser().GetExpected(source));
        }

        [Fact]
        public void NumberExpected()
        {
            string source = "Message\n  Expected: 0\n  But was:  1\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual)\n   at RedGreenPlayground.NUnitTests.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 28";
            string expected = "0";
            Assert.Equal(expected, new NUnitGallioParser().GetExpected(source));
        }

        [Fact]
        public void ActualNotPresent()
        {
            Assert.Equal(String.Empty, new NUnitGallioParser().GetActual(String.Empty));
            Assert.Equal(String.Empty, new NUnitGallioParser().GetActual(null));
        }

        [Fact]
        public void StringActual()
        {
            string source = "Message\n  String lengths are both 11. Strings differ at index 6.\n  Expected: \"who's there\"\n  But was:  \"who's where\"\n  -----------------^\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\n   at RedGreenPlayground.NUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 21";
            string expected = "\"who's where\"";
            Assert.Equal(expected, new NUnitGallioParser().GetActual(source));
        }

        [Fact]
        public void NumericActual()
        {
            string source = "Message\n  Expected: 0\n  But was:  1\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual)\n   at RedGreenPlayground.NUnitTests.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 28";
            string expected = "1";
            Assert.Equal(expected, new NUnitGallioParser().GetActual(source));
        }

        [Fact]
        public void GetPositionString()
        {
            string source = "Message\n  String lengths are both 11. Strings differ at index 6.\n  Expected: \"who's there\"\n  But was:  \"who's where\"\n  -----------------^\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\n   at RedGreenPlayground.NUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 21";
            Assert.Equal(6, new NUnitGallioParser().GetPosition(source, "who's where", "who's there"));
        }

        [Fact]
        public void GetMethodLocation()
        {
            Assert.Equal("RedGreenPlayground.NUnitTests.AlwaysPass", new NUnitGallioParser().ReformatLocation("RedGreenPlayground/NUnitTests/AlwaysPass"));
        }

        [Fact]
        public void GetLineNumber()
        {
            string source = "Message\n  String lengths are both 11. Strings differ at index 6.\n  Expected: \"who's there\"\n  But was:  \"who's where\"\n  -----------------^\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\n   at RedGreenPlayground.NUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 21";
            Assert.Equal(21, new NUnitGallioParser().GetLineNumber(source));
        }

		[Fact]
		private void ParseSummary()
		{
			List<string> output = LoadOutput();

			SummaryResult result = NUnitParser.ParseSummary(output);

			Assert.Equal("5", result.TotalCount);
			Assert.Equal("1", result.PassCount);
			Assert.Equal("4", result.FailCount);
			Assert.Equal("1", result.SkipCount);
			Assert.Equal(String.Empty, result.Duration);
		}

		[Fact]
		private void ParseResultsCount()
		{
			List<string> output = LoadOutput();

			List<RedGreen.TestResult> results = NUnitParser.ParseTestResults(output);

			Assert.Equal(6, results.Count);
		}

		[Fact]
		private void ValidateAlwaysFail()
		{
			List<string> output = LoadOutput();

			List<RedGreen.TestResult> results = NUnitParser.ParseTestResults(output);
			RedGreen.TestResult alwaysFail = results.Find(r => r.Location == "RedGreenPlayground.NUnitTests.AlwaysFails");

			Assert.Equal("RedGreenPlayground.NUnitTests.AlwaysFails", alwaysFail.Location);
			Assert.Equal(RedGreen.TestStatus.Failed, alwaysFail.Status);
			Assert.Equal("0.019", alwaysFail.Duration);
			Assert.Equal("\"who's there\"", alwaysFail.Failure.Expected);
			Assert.Equal("\"who's where\"", alwaysFail.Failure.Actual);
			Assert.Equal(6, alwaysFail.Failure.ActualDiffersAt);
		}

		[Fact]
		private void ValidateAlwaysPass()
		{
			List<string> output = LoadOutput();

			List<RedGreen.TestResult> results = NUnitParser.ParseTestResults(output);
			RedGreen.TestResult alwaysPass = results.Find(r => r.Location == "RedGreenPlayground.NUnitTests.AlwaysPass");

			Assert.Equal("RedGreenPlayground.NUnitTests.AlwaysPass", alwaysPass.Location);
			Assert.Equal(RedGreen.TestStatus.Passed, alwaysPass.Status);
		}

		[Fact]
		private void ValidateIntFail()
		{
			List<string> output = LoadOutput();

			List<RedGreen.TestResult> results = NUnitParser.ParseTestResults(output);
			RedGreen.TestResult alwaysFail = results.Find(r => r.Location == "RedGreenPlayground.NUnitTests.IntFail");

			Assert.Equal("RedGreenPlayground.NUnitTests.IntFail", alwaysFail.Location);
			Assert.Equal(RedGreen.TestStatus.Failed, alwaysFail.Status);
			Assert.Equal("0", alwaysFail.Failure.Expected);
			Assert.Equal("1", alwaysFail.Failure.Actual);
			Assert.Equal(0, alwaysFail.Failure.ActualDiffersAt);
		}

		[Fact]
		private void ValidateSkipped()
		{
			List<string> output = LoadOutput();

			List<RedGreen.TestResult> results = NUnitParser.ParseTestResults(output);
			RedGreen.TestResult alwaysFail = results.Find(r => r.Location == "RedGreenPlayground.NUnitTests.Skipped");

			Assert.Equal("RedGreenPlayground.NUnitTests.Skipped", alwaysFail.Location);
			Assert.Equal(RedGreen.TestStatus.Skipped, alwaysFail.Status);
		}
		private List<string> LoadOutput()
		{
			return new List<string> (new string[]{
								"NUnit version 2.4.8",
								"Copyright (C) 2002-2007 Charlie Poole.",
								"Copyright (C) 2002-2004 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov.",
								"Copyright (C) 2000-2002 Philip Craig.",
								"All Rights Reserved.",
								"",
								"Runtime Environment - ",
								"OS Version: Microsoft Windows NT 6.0.6001 Service Pack 1",
								"CLR Version: 2.0.50727.4013 ( Net 2.0.50727.4013 )",
								"",
								"",
								"<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?>",
								"<!--This file represents the results of running a test suite-->",
								"<test-results name=\"redgreenplayground.dll\" total=\"5\" failures=\"4\" not-run=\"1\" date=\"2009-10-02\" time=\"10:35:46\">",
								"<environment nunit-version=\"2.4.8.0\" clr-version=\"2.0.50727.4013\" os-version=\"Microsoft Windows NT 6.0.6001 Service Pack 1\" platform=\"Win32NT\" cwd=\"C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\bin\\Debug\\\" machine-name=\"JAARGERO0409\" user=\"JAARGERO\" user-domain=\"WRPWI\" />",
								"<culture-info current-culture=\"en-US\" current-uiculture=\"en-US\" />",
								"<test-suite name=\"redgreenplayground.dll\" success=\"False\" time=\"0.055\" asserts=\"0\">",
								"<results>",
								"<test-suite name=\"RedGreenPlayground\" success=\"False\" time=\"0.050\" asserts=\"0\">",
								"<results>",
								"<test-suite name=\"NUnitTests\" success=\"False\" time=\"0.040\" asserts=\"0\">",
								"<results>",
								"<test-case name=\"RedGreenPlayground.NUnitTests.AlwaysFails\" executed=\"True\" success=\"False\" time=\"0.019\" asserts=\"2\">",
								"<failure>",
								"<message><![CDATA[  String lengths are both 11. Strings differ at index 6.",
								"Expected: \"who's there\"",
								"But was:  \"who's where\"",
								"-----------------^",
								"]]></message>",
								"<stack-trace><![CDATA[at RedGreenPlayground.NUnitTests.AlwaysFails() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 21",
								"]]></stack-trace>",
								"</failure>",
								"</test-case>",
								"<test-case name=\"RedGreenPlayground.NUnitTests.AlwaysPass\" executed=\"True\" success=\"True\" time=\"0.000\" asserts=\"1\" />",
								"<test-case name=\"RedGreenPlayground.NUnitTests.IntFail\" executed=\"True\" success=\"False\" time=\"0.003\" asserts=\"1\">",
								"<failure>",
								"<message><![CDATA[  Expected: 0",
								"But was:  1",
								"]]></message>",
								"<stack-trace><![CDATA[at RedGreenPlayground.NUnitTests.IntFail() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 28",
								"]]></stack-trace>",
								"</failure>",
								"</test-case>",
								"<test-case name=\"RedGreenPlayground.NUnitTests.NotNull\" executed=\"True\" success=\"False\" time=\"0.004\" asserts=\"2\">",
								"<failure>",
								"<message><![CDATA[  Expected: not null",
								"But was:  null",
								"]]></message>",
								"<stack-trace><![CDATA[at RedGreenPlayground.NUnitTests.NotNull() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 41",
								"]]></stack-trace>",
								"</failure>",
								"</test-case>",
								"<test-case name=\"RedGreenPlayground.NUnitTests.Skipped\" executed=\"False\">",
								"<reason>",
								"<message><![CDATA[Method Skipped's signature is not correct: it must be a public method.]]></message>",
								"</reason>",
								"</test-case>",
								"<test-case name=\"RedGreenPlayground.NUnitTests.Throws\" executed=\"True\" success=\"False\" time=\"0.003\" asserts=\"0\">",
								"<failure>",
								"<message><![CDATA[System.Exception : misc exception]]></message>",
								"<stack-trace><![CDATA[at RedGreenPlayground.NUnitTests.Throws() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 50",
								"]]></stack-trace>",
								"</failure>",
								"</test-case>",
								"</results>",
								"</test-suite>",
								"</results>",
								"</test-suite>",
								"</results>",
								"</test-suite>",
								"</test-results>"
			});
		}
    }
}
