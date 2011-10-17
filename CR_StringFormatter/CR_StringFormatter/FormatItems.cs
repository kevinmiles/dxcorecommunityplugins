using System;
using System.Collections.Generic;
using DevExpress.CodeRush.StructuralParser;

namespace CR_StringFormatter
{
	public class FormatItems : Dictionary<int, FormatItem>
	{
    private IPrimitiveExpression _PrimitiveExpression;
    public IPrimitiveExpression PrimitiveExpression
    {
      get
      {
        return _PrimitiveExpression;
      }
      set
      {
        _PrimitiveExpression = value;
      }
    }
    private IMethodCallExpression _ParentMethodCall;
    public IMethodCallExpression ParentMethodCall
    {
      get
      {
        return _ParentMethodCall;
      }
      set
      {
        _ParentMethodCall = value;
      }
    }
		public ISourceFile SourceFile
		{
			get
			{
				if (ParentMethodCall == null)
					return null;
				return ParentMethodCall.FirstFile;
			}
		}
    public bool HasFormatItem(int number)
		{
			return ContainsKey(number);
		}
		public FormatItem GetFormatItemAtPos(int line, int offset)
		{
			FormatItemPos formatItemPos = GetFormatItemPosAtPos(line, offset);
			if (formatItemPos != null)
				return formatItemPos.Parent;
			return null;
		}
    public FormatItemPos GetFormatItemPosAtPos(int caretLine, int caretOffset)
		{
			foreach (FormatItem formatItem in Values)
				foreach (FormatItemPos position in formatItem.Positions)
				{
					SourceRange sourceRange = position.GetSourceRange(caretLine);
					if (sourceRange.Contains(caretLine, caretOffset))
						return position;
				}
			return null;
		}
		public void AddFormatItem(int number, IExpression argument)
		{
			Add(number, new FormatItem(this, number, argument));
		}
	}
}
