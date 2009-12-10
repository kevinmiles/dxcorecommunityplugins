using System;

namespace RedGreen
{
    internal static class RunnerFactory
	{
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
