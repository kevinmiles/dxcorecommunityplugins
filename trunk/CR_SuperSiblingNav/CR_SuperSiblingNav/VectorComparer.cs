using System;
using System.Collections.Generic;
using System.Linq;

namespace CR_SuperSiblingNav
{
	public class VectorComparer : IComparer<CaretVector>
	{
		public int Compare(CaretVector x, CaretVector y)
		{
			int absXLineDelta = Math.Abs(x.LineDelta);
			int absYLineDelta = Math.Abs(y.LineDelta);
			if (absXLineDelta == absYLineDelta)
			{
				int absXOffsetDelta = Math.Abs(x.OffsetDelta);
				int absYOffsetDelta = Math.Abs(y.OffsetDelta);
				return Math.Sign(absXOffsetDelta - absYOffsetDelta);
			}
			else
				return Math.Sign(absXLineDelta - absYLineDelta);
		}
	}
}
