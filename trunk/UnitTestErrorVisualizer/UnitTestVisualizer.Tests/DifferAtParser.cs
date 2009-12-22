using System;
using Xunit;
using UnitTestErrorVisualizer;

namespace UnitTestVisualizer.Tests
{
	public class DifferAtParser
	{
		[Fact]
		public void TestResultParser_NStringMessageVb_2()
		{
			string message = "Expected string length 3 but was 4. Strings differ at index 2.\n  Expected: \"who\"\n  But was:  \"what\"\n  -------------^\n\n";
			string expected = TestResultParser.Expected(message);
			string actual = TestResultParser.Actual(message);

			int differAt = TestResultParser.DifferAt(message, expected, actual);

			Assert.Equal(2, differAt);
		}

		[Fact]
		public void TestResultParser_XStringMessage_6()
		{
			string message = "Assert.Equal() Failure\nPosition: First difference is at position 6\nExpected: who's there\nActual:   who's where";
			string expected = TestResultParser.Expected(message);
			string actual = TestResultParser.Actual(message);

			int differAt = TestResultParser.DifferAt(message, expected, actual);

			Assert.Equal(6, differAt);
		}

		[Fact]
		public void TestResultParser_MbStringMessage_7()
		{
			string message = "Expected values to be equal.\n\nExpected Value : \"who\'s there\"\nActual Value   : \"who\'s where\"\n\n   at RedGreenPlayground.MbUnitTests.AlwaysFails() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 21\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.InvokeMethod(Object instance, MethodInfo methodInfo, Int32 testTimeout, Object[] parameters)\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.Execute(Object instance, MethodInfo methodInfo, List`1 tasks, Int32 testTimeout, Object[] parameters, Object expectedResult)\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.Execute(Object instance, TestMethod test, MethodInfo methodInfo, List`1 tasks, Int32 testTimeout)\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.Execute(TestMethodCollection tests, Boolean needToFilterByAssembly)\n";
			string expected = TestResultParser.Expected(message);
			string actual = TestResultParser.Actual(message);

			int differAt = TestResultParser.DifferAt(message, expected, actual);

			Assert.Equal(7, differAt);
		}

		[Fact]
		public void TestResultParser_NIntMessage_0()
		{
			string message = "  Expected: 0\n  But was:  1\n\n";
			string expected = TestResultParser.Expected(message);
			string actual = TestResultParser.Actual(message);

			int differAt = TestResultParser.DifferAt(message, expected, actual);

			Assert.Equal(0, differAt);
		}

		[Fact]
		public void TestResultParser_XIntMessage_0()
		{
			string message = "Assert.Equal() Failure\nExpected: 0\nActual:   1\n";
			string expected = TestResultParser.Expected(message);
			string actual = TestResultParser.Actual(message);

			int differAt = TestResultParser.DifferAt(message, expected, actual);

			Assert.Equal(0, differAt);
		}
	}
}
