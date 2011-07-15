using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
//using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.DXCore.Constants;

namespace CR_PrimitiveTab
{
  public partial class PlugIn1 : StandardPlugIn
	{
		// DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();
			/* Need to modify settings programmatically. For example, merge templates, 
			 * shortcuts, other settings. - This may come in a future release of DXCore. */
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

		private void spFindPrimitives_CheckAvailability(object sender, CheckSearchAvailabilityEventArgs ea)
		{
			if (ea.Element is PrimitiveExpression)
			{
				ContextProviderBase inFormatItemProvider = CodeRush.Context.GetContextProvider("Editor\\Code\\InFormatItem");
				if (inFormatItemProvider != null)
					if (inFormatItemProvider.IsContextSatisfied(""))
						return;

				//PrimitiveExpression primitiveExpression = (PrimitiveExpression)ea.Element;
				//if (primitiveExpression.PrimitiveType == PrimitiveType.String)
				//{
				//	if (CodeRush.Caret.SourcePoint == primitiveExpression.NameRange.Start)
				//		ea.Available = true;
				//	return;
				//}
				ea.Available = true;
			}
				
		}

		private void spFindPrimitives_LanguageSupported(LanguageSupportedEventArgs ea)
		{
			if (ea.LanguageID == Str.Language.CSharp || ea.LanguageID == Str.Language.CPlusPlus || ea.LanguageID == Str.Language.VisualBasic)
				ea.Handled = true;
		}

		private IEnumerable<PrimitiveExpression> FindMatching(LanguageElement scope, PrimitiveExpression primitiveExpression)
		{
			if (scope == null || primitiveExpression == null)
				yield break;

			ElementEnumerable primitivesEnumerable = new ElementEnumerable(scope, new PrimitiveFilter(primitiveExpression), true);
			if (primitivesEnumerable == null)
				yield break;

			foreach (object element in primitivesEnumerable)
				if (element is PrimitiveExpression)
					yield return (PrimitiveExpression)element;
		}
    private void spFindPrimitives_SearchReferences(object sender, SearchEventArgs ea)
		{
			// Rory says: Exhaust the events args first, before looking elsewhere (e.g., the CodeRush.Xxxx properties).
			PrimitiveExpression primitiveExpression = ea.Element as PrimitiveExpression;
			if (primitiveExpression == null)
				return;
			// markm@devexpress.com -- before 11.2 -- ask to get in on the beta. NDA is currently required.

			FileSourceRangeCollection ranges = new FileSourceRangeCollection();
			LanguageElement scope = CodeRush.Source.ActiveSourceFile;
			foreach (PrimitiveExpression primitive in FindMatching(scope, primitiveExpression))
				ranges.Add(new FileSourceRange(primitive.FileNode, primitive.Range));
      ea.AddRanges(ranges);
		}

		private void ctxInPrimitive_ContextSatisfied(ContextSatisfiedEventArgs ea)
		{
			if (CodeRush.Source.Active is PrimitiveExpression)
				ea.Satisfied = true;
		}

		//private void PlugIn1_KeyPressed(KeyPressedEventArgs ea)
		//{
		//	if (ea.CtrlKeyDown && ea.KeyCode == 32)
		//	{
		//		ea.EatKey();
		//	}
		//	
		//}
	}
}