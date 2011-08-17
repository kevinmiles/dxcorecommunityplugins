using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.StructuralParser;

namespace CR_MultiSelect
{
	public static class Extensions
	{
		/// <summary>
		/// Returns true if the specified range overlaps this range by at least one position. Adjacent ranges will return false.
		/// </summary>
		public static bool Overlaps(this SourceRange range1, SourceRange range2)
		{
			if (range1.Holds(range2.Top) || range1.Holds(range2.Bottom) || range2.Holds(range1.Top))
				return true;
			return false;
		}

		/// <summary>
		/// Returns true if the specified SourcePoint is inside the specified range (but not at either of the ends of the SourceRange).
		/// </summary>
		public static bool Holds(this SourceRange range, SourcePoint testPoint)
		{
			return testPoint > range.Top && testPoint < range.Bottom;
		}
	}
}
