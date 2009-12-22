using System;
using Xunit;
using UnitTestErrorVisualizer;

namespace UnitTestVisualizer.Tests
{
	public class LineNumberParserTests
	{
		[Fact]
		public void LineNumberParser_XStringFailStack_CorrectLineNumber()
		{
			string stack = "Void Equal[T](T, T, System.Collections.Generic.IComparer`1[T])\nVoid Equal[T](T, T)\nC:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\xUnitTests.cs(39, 13) : Void AlwaysFails()\n\n";

			int lineNumber = TestResultParser.LineNumber(stack);

			Assert.Equal(39, lineNumber);
		}

		[Fact]
		public void LineNumberParser_MbStringFailStack_CorrectLineNumber()
		{
			string stack = "c:\\RelEng\\Projects\\MbUnit v3.1\\Work\\src\\Gallio\\Gallio\\Framework\\Assertions\\AssertionContext.cs(286, 21) : Void ThrowFailureAccordingToBehavior(Gallio.Framework.Assertions.AssertionFailure)\nc:\\RelEng\\Projects\\MbUnit v3.1\\Work\\src\\Gallio\\Gallio\\Framework\\Assertions\\AssertionContext.cs(243, 21) : Void SubmitFailure(Gallio.Framework.Assertions.AssertionFailure, Boolean)\nc:\\RelEng\\Projects\\MbUnit v3.1\\Work\\src\\Gallio\\Gallio\\Framework\\Assertions\\AssertionContext.cs(105, 13) : Void SubmitFailure(Gallio.Framework.Assertions.AssertionFailure)\nc:\\RelEng\\Projects\\MbUnit v3.1\\Work\\src\\Gallio\\Gallio\\Framework\\Assertions\\AssertionHelper.cs(100, 17) : Void Fail(Gallio.Framework.Assertions.AssertionFailure)\nc:\\RelEng\\Projects\\MbUnit v3.1\\Work\\src\\Gallio\\Gallio\\Framework\\Assertions\\AssertionHelper.cs(90, 13) : Void Verify(Gallio.Common.Func`1[Gallio.Framework.Assertions.AssertionFailure])\nc:\\RelEng\\Projects\\MbUnit v3.1\\Work\\src\\MbUnit\\MbUnit\\Framework\\Assert.Comparisons.cs(108, 13) : Void AreEqual[T](T, T, Gallio.Common.EqualityComparison`1[T], System.String, System.Object[])\nc:\\RelEng\\Projects\\MbUnit v3.1\\Work\\src\\MbUnit\\MbUnit\\Framework\\Assert.Comparisons.cs(52, 13) : Void AreEqual[T](T, T, System.String, System.Object[])\nc:\\RelEng\\Projects\\MbUnit v3.1\\Work\\src\\MbUnit\\MbUnit\\Framework\\Assert.Comparisons.cs(38, 13) : Void AreEqual[T](T, T)\nC:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs(21, 13) : Void AlwaysFails()";

			int lineNumber = TestResultParser.LineNumber(stack);

			Assert.Equal(21, lineNumber);
		}

		[Fact]
		public void LineNumberParser_NStringFailStack_CorrectLineNumber()
		{
			string stack = "Void That(System.Object, NUnit.Framework.Constraints.Constraint, System.String, System.Object[])\nVoid AreEqual(System.Object, System.Object, System.String, System.Object[])\nVoid AreEqual(System.Object, System.Object)\nC:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs(410, 4) : Void AlwaysFails9()\n";

			int lineNumber = TestResultParser.LineNumber(stack);

			Assert.Equal(410, lineNumber);
		}

		[Fact]
		public void LineNumberParser_XStringFailStackVb_CorrectLineNumber()
		{
			string stack = "Void That(System.Object, NUnit.Framework.Constraints.Constraint, System.String, System.Object[])\nVoid AreEqual(System.Object, System.Object, System.String, System.Object[])\nVoid AreEqual(System.Object, System.Object)\nC:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\VBPlayground\\Class1.vb(11, 9) : Void DoSomething()";

			int lineNumber = TestResultParser.LineNumber(stack);

			Assert.Equal(11, lineNumber);
		}
	}
}