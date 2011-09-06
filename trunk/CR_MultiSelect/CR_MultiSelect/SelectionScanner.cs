using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;

namespace DevExpress.CodeRush.Core
{
	public class SelectionScanner
	{
		private int _ElementsFound;
		private LanguageElementType _ElementType;
		private LanguageElement _TopNode;

		public static bool IsComment(LanguageElementType elementType)
		{
			return elementType == LanguageElementType.Comment || elementType == LanguageElementType.XmlDocComment;
		}

		private void NodeIterator(NodeIterationEventArgs ea)
		{
			if (_TopNode == null)
			{
				_TopNode = ea.LanguageElement;
				_ElementType = _TopNode.ElementType;
				_ElementsFound++;
			}
			else if (_TopNode.IsSibling(ea.LanguageElement))
			{
				if (IsComment(_TopNode.ElementType))
				{
					// Prioritize any other element type over comments.
					_TopNode = ea.LanguageElement;
					_ElementType = _TopNode.ElementType;
				}
				else if (_ElementType != ea.LanguageElement.ElementType && !IsComment(ea.LanguageElement.ElementType))
					_ElementType = LanguageElementType.Unknown;		// Different element types within the range.
				_ElementsFound++;
			}
		}

		public void Scan()
		{
			_ElementType = LanguageElementType.Unknown;
			_ElementsFound = 0;
			_TopNode = null;
			CodeRush.Source.IterateNodesInSelection(NodeIterator);
		}
		public SelectionScanner()
		{

		}
		public int ElementsFound
		{
			get
			{
				return _ElementsFound;
			}
		}
		public LanguageElementType ElementType
		{
			get
			{
				return _ElementType;
			}
		}
		public string ElementName
		{
			get
			{
				return _TopNode.Name;
			}
		}
	}
}
