using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.StructuralParser;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.Core.Replacement;


namespace CR_ObjectInitializerToConstructor
{
	public static class Extensions
	{
		public static ITypeElement GetInitializerType(this MemberInitializerExpression memberInitializerExpression)
		{
			return memberInitializerExpression.Resolve(ParserServices.SourceTreeResolver) as ITypeElement;
		}
		public static LanguageElement GetLanguageElement(this IElement element)
		{
			return LanguageElementRestorer.ConvertToLanguageElement(element);
		}
		public static IElementCollection FindConstructors(this ITypeElement typeElement, ExpressionCollection parameters)
		{
			return ParserServices.SourceTreeResolver.FindConstructors(typeElement, parameters);
		}
		public static bool HasDefaultConstructor(this ITypeElement typeElement)
		{
			IElementCollection existingDefaultConstructors = typeElement.FindConstructors(new ExpressionCollection());
			return existingDefaultConstructors != null && existingDefaultConstructors.Count > 0;
		}
		public static ITypeElement GetTypeDeclaration(this TypeReferenceExpression typeReferenceExpression)
		{
			return typeReferenceExpression.Resolve(ParserServices.SourceTreeResolver) as ITypeElement;
		}
	}
}
