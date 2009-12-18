using System;
using System.Collections.Generic;
using System.IO;

namespace RedGreenTests
{
	public class Gallio3_1DataSource : TextReader
	{
		private List<string> data;
		private int line = 0;
		public Gallio3_1DataSource()
		{
			data = new List<string> {
					"",
					"Gallio Echo - Version 3.1 build 313",
					"Get the latest version at http://www.gallio.org/",
					"",
					"Start time: 7:22 AM",
					"Initializing the runtime and loading plugins.",
					"Verifying test files.",
					"Initializing the test runner.",
					"Running the tests.",
					"Host started at 12/16/2009 7:22:37 AM.",
					"Running under CLR v2.0.50727 runtime.",
					"[failed] Test RedGreenPlayground/MbUnitTests/AlwaysFails",
					"Expected values to be equal.",
					"",
					"Expected Value : \"who\'s there\"",
					"Actual Value   : \"who\'s where\"",
					"",
					"   at RedGreenPlayground.MbUnitTests.AlwaysFails() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 21",
					"",
					"",
					"[passed] Test RedGreenPlayground/MbUnitTests/AlwaysPass",
					"[failed] Test RedGreenPlayground/MbUnitTests/IntFail",
					"Expected values to be equal.",
					"",
					"Expected Value : 0",
					"Actual Value   : 1",
					"",
					"   at RedGreenPlayground.MbUnitTests.IntFail() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 28",
					"",
					"",
					"[failed] Test RedGreenPlayground/MbUnitTests/NotNull",
					"Expected value to be non-null.",
					"",
					"Actual Value : null",
					"",
					"   at RedGreenPlayground.MbUnitTests.NotNull() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 41",
					"",
					"",
					"[failed] Test RedGreenPlayground/MbUnitTests/Skipped",
					"Expected values to be equal.",
					"",
					"Expected Value : 5",
					"Actual Value   : 500",
					"",
					"   at RedGreenPlayground.MbUnitTests.Skipped() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 34",
					"",
					"",
					"[failed] Test RedGreenPlayground/MbUnitTests/Throws",
					"Execute",
					"System.Exception: misc exception",
					"   at RedGreenPlayground.MbUnitTests.Throws() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 50",
					"",
					"Host stopped at 12/16/2009 7:22:44 AM.",
					"Host process exited with code: 0",
					"Host started at 12/16/2009 7:22:45 AM.",
					"Running under CLR v2.0.50727 runtime.",
					"Host stopped at 12/16/2009 7:22:47 AM.",
					"Host process exited with code: 0",
					"Host started at 12/16/2009 7:22:48 AM.",
					"Running under CLR v2.0.50727 runtime.",
					"Host stopped at 12/16/2009 7:22:53 AM.",
					"Host process exited with code: 0",
					"Disposing the test runner.",
					"Stop time: 7:22 AM (Total execution time: 22.019 seconds)",
					"",
					"6 run, 1 passed, 5 failed, 0 inconclusive, 0 skipped",
					"",
					""
					};
		}
		public override int Peek()
		{
			if (line < data.Count)
			{
				if (data[line].Length == 0)
					return 0;
				return (int)data[line][0];
			}
			return -1;
		}
		public override int Read()
		{
			return base.Read();
		}
		public override string ReadLine()
		{
			if (line < data.Count)
				return data[line++];
			return null;
		}
	}
}
