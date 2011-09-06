using System;
using DevExpress.CodeRush.StructuralParser;
using System.Collections;

namespace DevExpress.CodeRush.Core
{
	public class MultiSelectEnumerator : IEnumerator
	{
		#region private fields...
		private readonly LanguageElementType _ElementType;
		private readonly MultiSelect _MultiSelect;
		private PartialSelection _Current;
		private int _Index;
		#endregion

		// constructors...
		#region MultiSelectEnumerator
		public MultiSelectEnumerator(MultiSelect multiSelect, LanguageElementType elementType)
		{
			_MultiSelect = multiSelect;
			_ElementType = elementType;
			Reset();
		}
		#endregion

		// IEnumerator members...
		#region Current
		public object Current
		{
			get
			{
				return _Current;
			}
		}
		#endregion
 		#region MoveNext
		public bool MoveNext()
		{
			while (true)
			{
				_Index++;
				if (_Index >= _MultiSelect.Selections.Count)
				{
					_Current = null;
					return false;
				}
				else
				{
					_Current = _MultiSelect.Selections[_Index];
					if (_ElementType == LanguageElementType.Unknown && _Current.Generated == false)		// Use LanguageElementType.Unknown to bring in everything else.
						return true;
					if (_Current.ElementType == _ElementType)
						return true;
					if (_ElementType == LanguageElementType.Variable && _Current.ElementType == LanguageElementType.InitializedVariable)
						return true;
					if (_ElementType == LanguageElementType.InitializedVariable && _Current.ElementType == LanguageElementType.Variable)
						return true;
				}
			}
		}
		#endregion
		#region Reset
		public void Reset()
		{
			_Current = null;
			_Index = -1;
		}
		#endregion
	}
}
