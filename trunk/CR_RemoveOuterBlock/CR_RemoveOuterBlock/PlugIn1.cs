using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_RemoveOuterBlock
{
	public partial class PlugIn1 : StandardPlugIn
	{
		private string _NewCode;
		private SourceRange _DeleteRange;
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

		private void cpRemoveOuterBlock_Apply(object sender, ApplyContentEventArgs ea)
		{
			if (_NewCode == null)
				CalculateCode(ea.Element as ParentingStatement);
			ea.TextDocument.QueueDelete(_DeleteRange);
			ea.TextDocument.QueueInsert(_DeleteRange.Top, _NewCode);
			ea.TextDocument.ApplyQueuedEdits("Remove Outer Block", true);
			CodeRush.Caret.MoveTo(_DeleteRange.Top);
			_NewCode = null;
		}

		private void cpRemoveOuterBlock_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			ParentingStatement parentingStatement = ea.Element as ParentingStatement;
			if (parentingStatement == null)
				return;
			if (parentingStatement.ElementType == LanguageElementType.Switch)
				return;
			ea.Available = true;
		}

		private void CalculateCode(ParentingStatement parentingStatement)
		{
			_NewCode = null;
			if (parentingStatement == null)
				return;

			LanguageElement parentToReplace = null;
			ParentToSingleStatement grandparent = parentingStatement.Parent as ParentToSingleStatement;
			if (grandparent != null)
			{
				if (!grandparent.HasBlock)
				{
					ElementCloneOptions cloneOptions = new ElementCloneOptions() { CloneNodes = false };
					parentToReplace = grandparent.Clone(cloneOptions) as LanguageElement;
				}
			}
			LanguageElement endNode;
			LanguageElement startNode;
			parentingStatement.GetFullBlockNodes(out startNode, out endNode);
			LanguageElement elementToCheck = startNode;
			ElementBuilder elementBuilder = new ElementBuilder();
			if (parentToReplace != null)
				elementBuilder.AddNode(null, parentToReplace);

			while (elementToCheck != null)
			{
				ParentingStatement parent = elementToCheck as ParentingStatement;
				if (parent != null && parent.ElementType != LanguageElementType.Catch)
				{
					foreach (IElement element in parent.Nodes)
					{
						LanguageElement child = element.Clone() as LanguageElement;
						if (child != null)
							elementBuilder.AddNode(parentToReplace, child);
					}
				}
				if (elementToCheck == endNode)
					break;

				elementToCheck = elementToCheck.NextCodeSibling;
			}

			if (parentToReplace != null)
				_DeleteRange = parentToReplace.Range;
			else
				_DeleteRange = new SourceRange(startNode.Range.Top, endNode.Range.Bottom);

			_NewCode = elementBuilder.GenerateCode();

			if (_NewCode.EndsWith(Environment.NewLine))
				_NewCode = _NewCode.Remove(_NewCode.Length - Environment.NewLine.Length);
		}

		private void cpRemoveOuterBlock_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
		{
			CalculateCode(ea.Element as ParentingStatement);
			ea.AddCodePreview(_DeleteRange.Top, _NewCode);
			ea.AddStrikethrough(_DeleteRange);
		}
	}
}