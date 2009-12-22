using System;
using Xunit;
using UnitTestErrorVisualizer;

namespace UnitTestVisualizer.Tests
{
	public class ActualValueParser
	{
		[Fact]
		public void TestResultParser_NStringMessageVb_Who()
		{
			string message = "Expected string length 3 but was 4. Strings differ at index 2.\n  Expected: \"who\"\n  But was:  \"what\"\n  -------------^\n\n";

			string actual = TestResultParser.Actual(message);

			Assert.Equal("\"what\"", actual);
		}

		[Fact]
		public void TestResultParser_XStringMessage_Who()
		{
			string message = "Assert.Equal() Failure\nPosition: First difference is at position 6\nExpected: who's there\nActual:   who's where";

			string actual = TestResultParser.Actual(message);

			Assert.Equal("who's where", actual);
		}

		[Fact]
		public void TestResultParser_MbStringMessage_Who()
		{
			string message = "Expected values to be equal.\n\nExpected Value : \"who\'s there\"\nActual Value   : \"who\'s where\"\n\n   at RedGreenPlayground.MbUnitTests.AlwaysFails() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 21\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.InvokeMethod(Object instance, MethodInfo methodInfo, Int32 testTimeout, Object[] parameters)\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.Execute(Object instance, MethodInfo methodInfo, List`1 tasks, Int32 testTimeout, Object[] parameters, Object expectedResult)\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.Execute(Object instance, TestMethod test, MethodInfo methodInfo, List`1 tasks, Int32 testTimeout)\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.Execute(TestMethodCollection tests, Boolean needToFilterByAssembly)\n";

			string actual = TestResultParser.Actual(message);

			Assert.Equal("\"who's where\"", actual);
		}

		[Fact]
		public void TestResultParser_NIntMessage_One()
		{
			string message = "Expected: 0\n  But was:  1\n\nVoid That(System.Object, NUnit.Framework.Constraints.Constraint, System.String, System.Object[])\nVoid AreEqual(Int32, Int32, System.String, System.Object[])\nVoid AreEqual(Int32, Int32)\nC:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs(417, 4) : Void IntFail9()\n\n";

			string actual = TestResultParser.Actual(message);

			Assert.Equal("1", actual);
		}

		[Fact]
		public void TestResultParser_MbIntMessage_One()
		{
			string message = "Expected values to be equal.\n\nExpected Value : 0\nActual Value   : 1\n\n   at RedGreenPlayground.MbUnitTests.IntFail() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 28\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.InvokeMethod(Object instance, MethodInfo methodInfo, Int32 testTimeout, Object[] parameters)\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.Execute(Object instance, MethodInfo methodInfo, List`1 tasks, Int32 testTimeout, Object[] parameters, Object expectedResult)\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.Execute(Object instance, TestMethod test, MethodInfo methodInfo, List`1 tasks, Int32 testTimeout)\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.Execute(TestMethodCollection tests, Boolean needToFilterByAssembly)\n";

			string actual = TestResultParser.Actual(message);

			Assert.Equal("1", actual);
		}

		[Fact]
		public void TestResultParser_XIntMessage_One()
		{
			string message = "Assert.Equal() Failure\nExpected: 0\nActual:   1";

			string actual = TestResultParser.Actual(message);

			Assert.Equal("1", actual);
		}

		[Fact]
		public void TestResultParser_NNotNullMessage_Null()
		{
			string message = "  Expected: not null\n  But was:  null\n\n";

			string actual = TestResultParser.Actual(message);

			Assert.Equal("null", actual);
		}

		[Fact]
		public void TestResultParser_MbNotNullMessage_Null()
		{
			string message = "Expected value to be non-null.\n\nActual Value : null\n\n   at RedGreenPlayground.MbUnitTests.NotNull() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 41\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.InvokeMethod(Object instance, MethodInfo methodInfo, Int32 testTimeout, Object[] parameters)\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.Execute(Object instance, MethodInfo methodInfo, List`1 tasks, Int32 testTimeout, Object[] parameters, Object expectedResult)\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.Execute(Object instance, TestMethod test, MethodInfo methodInfo, List`1 tasks, Int32 testTimeout)\n   at DevExpress.CodeRush.Core.Testing.UnitTestDomain.Execute(TestMethodCollection tests, Boolean needToFilterByAssembly)\n";

			string actual = TestResultParser.Actual(message);

			Assert.Equal("null", actual);
		}

		[Fact]
		public void TestResultParser_XNullMessage_Empty()
		{
			string message = "Test has failed.\nAssert.NotNull() Failure\n";

			string actual = TestResultParser.Actual(message);

			Assert.Equal(String.Empty, actual);
		}
	}
}
