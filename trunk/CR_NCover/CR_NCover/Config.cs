using System;
using System.IO;

namespace CR_NCover
{
	public class TestDriven
	{
		public static string NCoverResultPathFor(string solution)
		{
			return String.Format(@"{0}\Mutant Design\TestDriven.NET 2.0\Coverage\{1}", Path.GetTempPath(), solution);
		}
	}
}