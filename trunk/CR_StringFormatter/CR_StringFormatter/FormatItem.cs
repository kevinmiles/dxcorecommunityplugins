using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.StructuralParser;

namespace CR_StringFormatter
{
	public class FormatItem
	{
		private readonly List<FormatItemPos> _Positions = new List<FormatItemPos>();
		public int Id { get; set; }
		public FormatItems Parent { get; private set; }

		public void AddPosition(int offset, int length)
		{
			Positions.Add(new FormatItemPos(this, offset, length));
		}
    public FormatItem(FormatItems parent, int id, Expression argument)
		{
			Argument = argument;
      Parent = parent;
      Id = id;
		}

		public List<FormatItemPos> Positions
		{
			get
			{
				return _Positions;
			}
		}
		public Expression Argument { get; private set; }
	}
}
