using System;
using System.Collections.Generic;
//using System.Linq;
using DevExpress.CodeRush.StructuralParser;

namespace CR_PrimitiveTab
{
	/* Review of LanguageElements and LiteElements and their interfaces.
	 * 
			  LanguageElement descendants -- these are the heavier objects. Easy to work with, but big. 
					These always exist for open files.
			 
			  LiteElements -- lightweight, fewer properties, WAY less memory. 
					Used for files on disk, and referenced assemblies.
			 
			  Interfaces -- Stick an I in front of the LanguageElement name. Implemented by both of these.
			 */

	public class PrimitiveFilter : IElementFilter
	{
		private readonly PrimitiveExpression _Source;
		public PrimitiveFilter(PrimitiveExpression source)
		{
			_Source = source;
		}

		// IElementFilter members...
		public bool Apply(IElement element)
		{
			// Is this an element we want to work with? Return true if so.
			IPrimitiveExpression primitiveExpression = element as IPrimitiveExpression;
			if (primitiveExpression == null)
				return false;

			return primitiveExpression.PrimitiveType == _Source.PrimitiveType &&
						 primitiveExpression.Name == _Source.Name;
		}

		public bool SkipChildren(IElement element)
		{
			return false;
		}
	}
}
