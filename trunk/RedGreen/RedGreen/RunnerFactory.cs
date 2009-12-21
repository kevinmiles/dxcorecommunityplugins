//using System;
using System.Collections.Generic;
using DevExpress.CodeRush.StructuralParser;

namespace RedGreen
{
    internal static class RunnerFactory
	{
		static readonly List<string> supportedAttributes = new List<string>(new string[] { "Test", "Fact", "Theory", "TestMethod" });

		internal static bool IsTest(Attribute attribute)
		{
			return attribute.TargetNode.ElementType == LanguageElementType.Method && supportedAttributes.Contains(attribute.ToString());
		}

		internal static BaseTestRunner CreateRunnerFromTestAttribute(string testAttributeName)
		{
			switch (testAttributeName)
			{
				case "Xunit.Extensions.TheoryAttribute":
				case "Xunit.FactAttribute":
					return new XunitRunner();
				case "NUnit.Framework.TestAttribute":
					return new NUnitRunner();
				//case "Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute":
				//    return new MsTestRunner();
				case "MbUnit.Framework.TestAttribute":
				default:
					return new GallioRunner();
			}
		}

		internal static BaseTestRunner CreateRunnerFromFixtureAttribute(string testAttributeName)
		{
			switch (testAttributeName)
			{
				case "":
					return new XunitRunner();
				case "NUnit.Framework.TestFixtureAttribute":
					return new NUnitRunner();
				//case "Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute":
				//    return new MsTestRunner();
				case "MbUnit.Framework.TestFixtureAttribute":
				default:
					return new GallioRunner();
			}
		}
	}
}
