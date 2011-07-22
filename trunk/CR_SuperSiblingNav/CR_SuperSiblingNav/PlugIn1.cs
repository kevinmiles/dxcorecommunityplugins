using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_SuperSiblingNav
{
	public partial class PlugIn1 : StandardPlugIn
	{
		ElementLocation _Caret;
		ElementLocation _Anchor;
		private bool _ChangingInternally;
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

		private void SiblingNavAvailable(object sender, CheckContentAvailabilityEventArgs ea)
		{
			LanguageElement activeMethodOrProperty = ea.MethodOrProperty;
			ea.Available = activeMethodOrProperty != null;
		}

		private void Restore(LanguageElement element, TextView textView)
		{
			_ChangingInternally = true;
			try
			{
				SourcePoint caret = _Caret.GetBestLocation(element);

				if (_Anchor != null)
				{
					SourcePoint anchor = _Anchor.GetBestLocation(element);
					textView.Selection.Set(anchor, caret);
				}
				else 
					textView.Caret.MoveTo(caret);
			}
			finally
			{
				_ChangingInternally = false;
			}
		}
		private void NavToSibling(ApplyContentEventArgs ea, SiblingDirection siblingDirection)
		{
			LanguageElement methodOrProperty = CodeRush.Source.ActiveMethodOrProperty;

			if (methodOrProperty == null)
				return;
			LanguageElement sibling = methodOrProperty;
			while (sibling != null)
			{
				if (siblingDirection == SiblingDirection.Next)
					sibling = sibling.NextCodeSibling;
				else
					sibling = sibling.PreviousCodeSibling;
				if (sibling is Method || sibling is Property)
					break;
			}

			if (sibling == null)
			{
				string msg;
				string searchItem;
					
				if (methodOrProperty is Property)
					searchItem = "properties";
				else
					searchItem = "methods";

				if (siblingDirection == SiblingDirection.Next)
					msg = String.Format("No more {0} below this location.", searchItem);	// Translate: {0} is the item we're searching for (e.g., "properties" or "methods").
				else
					msg = String.Format("No more {0} above this location.", searchItem);	// Translate: {0} is the item we're searching for (e.g., "properties" or "methods").
				CodeRush.ApplicationObject.StatusBar.Text = msg;
				return;
			}
			if (_Caret == null)
				_Caret = ElementLocation.From(methodOrProperty, CodeRush.Caret.SourcePoint);
			TextView textView = ea.TextView;
			if (textView == null)
				return;
			if (_Anchor == null)
				if (textView.Selection.Exists)
					_Anchor = ElementLocation.From(methodOrProperty, textView.Selection.AnchorSourcePoint);
			Restore(sibling, textView);
		}
		private void navMemberNext_Apply(object sender, ApplyContentEventArgs ea)
		{
			NavToSibling(ea, SiblingDirection.Next);
		}

		private void navMemberPrevious_Apply(object sender, ApplyContentEventArgs ea)
		{
			NavToSibling(ea, SiblingDirection.Previous);
		}

		private void CaretMovedOrSelectionChanged()
		{
			if (_ChangingInternally)
				return;
			_Caret = null;
			_Anchor = null;
		}
    private void PlugIn1_CaretMoved(CaretMovedEventArgs ea)
		{
			CaretMovedOrSelectionChanged();
		}

		private void PlugIn1_SelectionChanged(SelectionChangedEventArgs ea)
		{
			CaretMovedOrSelectionChanged();
		}
	}
}