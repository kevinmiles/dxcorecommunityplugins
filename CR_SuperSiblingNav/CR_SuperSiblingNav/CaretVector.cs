using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.StructuralParser;

namespace CR_SuperSiblingNav
{
	public class CaretVector
	{
		public int LineDelta;
		public int OffsetDelta;
		public ElementPosition ElementPosition;
		public CaretVector(ElementPosition position)
		{
			ElementPosition = position;
		}
    public static CaretVector From(SourcePoint target, SourcePoint source, ElementPosition position)
		{
			CaretVector caretVector = new CaretVector(position);
			caretVector.LineDelta = target.Line - source.Line;
			caretVector.OffsetDelta = target.Offset - source.Offset;
			return caretVector;
		}
	}
}
