using System;
using System.Collections.Generic;
using DevExpress.CodeRush.StructuralParser;

namespace DevExpress.CodeRush.Core
{
	public class StartPointComparer : IComparer<PartialSelection>
	{
		public int Compare(PartialSelection x, PartialSelection y)
		{
			SourcePoint xTop = x.Range.Top;
			SourcePoint yTop = y.Range.Top;
			if (xTop.Line == yTop.Line)
				return xTop.Offset - yTop.Offset;
			else
				return xTop.Line - yTop.Line;
		}
	}
}
