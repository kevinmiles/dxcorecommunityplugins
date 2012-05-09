using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using DevExpress.CodeRush.Core.MultiSelect;
using System.Linq;
using System;

namespace CR_MultiSelectEdit
{
	public partial class PlugIn1 : StandardPlugIn
	{
		//DXCore-generated code...
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();
			RegisterMultiSelectEdit();
		}

		public override void FinalizePlugIn()
		{
			//TODO: Add your finalization code here.
			base.FinalizePlugIn();
		}

		public void RegisterMultiSelectEdit()
		{
			DevExpress.CodeRush.Core.Action MultiSelectEdit = new DevExpress.CodeRush.Core.Action(this.components);
			((System.ComponentModel.ISupportInitialize)MultiSelectEdit).BeginInit();
			MultiSelectEdit.ActionName = "MultiSelectEdit";
			MultiSelectEdit.ButtonText = "MultiSelectEdit"; // Used if button is placed on a menu.
			MultiSelectEdit.RegisterInCR = true;
			MultiSelectEdit.Execute += MultiSelectEdit_Execute;
			((System.ComponentModel.ISupportInitialize)MultiSelectEdit).EndInit();
		}

		private void MultiSelectEdit_Execute(ExecuteEventArgs ea)
		{
			TextView TheView = CodeRush.TextViews.Active;
			var TheMultiSelect = CodeRush.MultiSelect.Get(TheView);
			if (TheMultiSelect == null || TheMultiSelect.Selections.Count == 0)
			{
				return;
			}
			var StartingSelectionRange = TheView.Selection.Range;
			LinkSourceRanges(TheView, StartingSelectionRange, TheMultiSelect.Selections.ToRanges());
			LinkOriginalRange(TheView, StartingSelectionRange, TheMultiSelect.Selections.ToRanges());
			TheMultiSelect.RefreshHighlighting(TheView);
		}

		private static void LinkOriginalRange(TextView TheView, SourceRange CurrentSelection, IEnumerable<SourceRange> ExistingSelections)
		{
			SourceRange FirstRange;
			if (CurrentSelection.IsPoint)
			{
				FirstRange = ExistingSelections.FirstOrDefault();
			}
			else
			{
				FirstRange = CurrentSelection;
			}
			TheView.Selection.Set(FirstRange);
		}

		private static void LinkSourceRanges(TextView TheView, SourceRange CurrentSelection, IEnumerable<SourceRange> Ranges)
		{
			CodeRush.LinkedIdentifiers.BreakAllLinks();
			var List = CodeRush.LinkedIdentifiers.GetStorage(TheView.TextDocument).NewList();
			if (!CurrentSelection.IsPoint)
			{
				List.Add(CurrentSelection);
			}
			foreach (var Selection in Ranges)
			{
				List.Add(Selection);
			}
		}
	}
	public static class MultiSelectEditExt
	{
		public static IEnumerable<SourceRange> ToRanges(this IEnumerable<PartialSelection> Source)
		{
			return (from P in Source select P.Range).ToList();
		}
	}

}