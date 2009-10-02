using System;
using System.Collections.Generic;

namespace RedGreen
{
    internal static class RunnerFactory
	{
		internal static BaseTestRunner CreateRunnerFromTestAttribute(string testAttributeName)
		{
			switch (testAttributeName)
			{
				case "Xunit.FactAttribute":
					return new XunitRunner();
				case "NUnit.Framework.TestAttribute":
					return new NUnitRunner();
				case "MbUnit.Framework.TestAttribute":
				case "Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute":
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
				case "MbUnit.Framework.TestFixtureAttribute":
				case "Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute":
				default:
					return new GallioRunner();
			}
		}
	}
}
