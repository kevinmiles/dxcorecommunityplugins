using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_INotifyPropChangeRename
{
	public partial class PlugIn1 : StandardPlugIn
	{
		// DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();

			//
			// TODO: Add your initialization code here.
			//
		}
		#endregion
		#region FinalizePlugIn
		public override void FinalizePlugIn()
		{
			//
			// TODO: Add your finalization code here.
			//

			base.FinalizePlugIn();
		}
		#endregion

		#region GetAllPropertyReferences(LanguageElement startElement)
		private static FileSourceRangeCollection GetAllPropertyReferences(LanguageElement startElement)
		{
			FileSourceRangeCollection allReferences = new FileSourceRangeCollection();

			Property property;
			PrimitiveExpression primitiveExpression;
			if (!GetPrimitiveExpressionAndProperty(startElement, out primitiveExpression, out property))
				return allReferences;

			// Add references found...
			IElementCollection allPropertyReferences = property.FindAllReferences();
			foreach (IElement element in allPropertyReferences)
				allReferences.Add(new FileSourceRange(element.FirstFile as SourceFile, element.FirstNameRange));

			// Add the contents of the string primitive...
			SourceRange primitiveRange = primitiveExpression.NameRange;
			int nameStart = primitiveExpression.Name.IndexOf(property.Name);
			int nameEndMargin = primitiveExpression.Name.Length - property.Name.Length - nameStart;
			primitiveRange.Start.Offset += nameStart;
			primitiveRange.End.Offset -= nameEndMargin;
			allReferences.Add(new FileSourceRange(primitiveExpression.FileNode, primitiveRange));

			// Add the NameRange of the property declaration itself....
			allReferences.Add(new FileSourceRange(property.FileNode, property.NameRange));

			return allReferences;
		}
		#endregion
		#region GetPrimitiveExpressionAndProperty(LanguageElement element)
		/// <summary>
		/// Gets a PrimitiveExpression and a parenting property provided the PrimitiveExpression is of type System.String and the caret is inside that string.
		/// Returns true if both values are found and the contents of the string matches the name of the property.
		/// </summary>
		private static bool GetPrimitiveExpressionAndProperty(LanguageElement element)
		{
			PrimitiveExpression primitiveExpression;
			Property property;
			return GetPrimitiveExpressionAndProperty(element, out primitiveExpression, out property);
		}
		#endregion
		#region GetPrimitiveExpressionAndProperty(LanguageElement element, out PrimitiveExpression primitiveExpression, out Property property)
		/// <summary>
		/// Gets a PrimitiveExpression and a parenting property provided the PrimitiveExpression is of type System.String and the caret is inside that string.
		/// Returns true if both values are found and the contents of the string matches the name of the property.
		/// </summary>
		private static bool GetPrimitiveExpressionAndProperty(LanguageElement element, out PrimitiveExpression primitiveExpression, out Property property)
		{
			property = null;
			primitiveExpression = element as PrimitiveExpression;
			if (primitiveExpression == null)
				return false;

			if (primitiveExpression.ExpressionTypeName != "System.String")
				return false;

			property = primitiveExpression.GetParentProperty();
			if (property == null)
				return false;

			return property.Name == (string)primitiveExpression.PrimitiveValue;
		}
		#endregion

		private void spINotifyPropertyChangedRename_CheckAvailability(object sender, CheckSearchAvailabilityEventArgs ea)
		{
			ea.Available = GetPrimitiveExpressionAndProperty(ea.Element);
		}

		private void spINotifyPropertyChangedRename_SearchReferences(object sender, SearchEventArgs ea)
		{
			FileSourceRangeCollection allReferences = GetAllPropertyReferences(ea.Element);
			ea.AddRanges(allReferences);
		}

		private void spINotifyPropertyChangedRename_SearchPreviewReferences(object sender, SearchForPreviewEventArgs ea)
		{
			FileSourceRangeCollection allReferences = GetAllPropertyReferences(ea.Element);
			foreach (FileSourceRange fileSourceRange in allReferences)
				ea.AddRange(fileSourceRange.Range);
		}
	}
}