using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Reflection;
using System.IO;
using DevExpress.CodeRush.Menus;
using System.Collections;

namespace CR_MultiSelect
{
	public partial class PlugIn1 : StandardPlugIn
	{
		private string _ThisAssemblyName;
		// DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();

			_ThisAssemblyName = Path.GetFileNameWithoutExtension(typeof(PlugIn1).Assembly.CodeBase);
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		}
		#endregion
		#region FinalizePlugIn
		public override void FinalizePlugIn()
		{
			AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;

			base.FinalizePlugIn();
		}
		#endregion

		Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (_ThisAssemblyName != null && args.Name.StartsWith(_ThisAssemblyName))
				return typeof(PlugIn1).Assembly;
			return null;
		}

		private void actMultiSelectAdd_Execute(ExecuteEventArgs ea)
		{
			CodeRushPlaceholder.MultiSelect.Add(CodeRush.TextViews.Active);
		}

		private void actMultiSelectClear_Execute(ExecuteEventArgs ea)
		{
			CodeRushPlaceholder.MultiSelect.Clear(CodeRush.TextViews.Active);
		}

		private void actMultiSelectRedo_Execute(ExecuteEventArgs ea)
		{
			CodeRushPlaceholder.MultiSelect.Redo(CodeRush.TextViews.Active);
		}

		private void actMultiSelectUndo_Execute(ExecuteEventArgs ea)
		{
			CodeRushPlaceholder.MultiSelect.Undo(CodeRush.TextViews.Active);
		}

		private void ctxMultiSelectExists_ContextSatisfied(ContextSatisfiedEventArgs ea)
		{
			ea.Satisfied = CodeRushPlaceholder.MultiSelect.SelectionExists(CodeRush.TextViews.Active);
		}
		private MultiSelect Copy()
		{
			return CodeRushPlaceholder.MultiSelect.CopyToClipboard(CodeRush.TextViews.Active);
		}
    private MultiSelect Cut()
		{
			MultiSelect multiSelect = CodeRushPlaceholder.MultiSelect.CutToClipboard(CodeRush.TextViews.Active);
			if (multiSelect != null)
				multiSelect.Delete("MultiSelect - Cut");
			CodeRushPlaceholder.MultiSelect.Clear(CodeRush.TextViews.Active);
			return multiSelect;
		}
		private void Paste()
		{
			MultiSelect multiSelect = MultiSelect.FromClipboard();
			if (multiSelect == null)
				return;
			TextDocument activeTextDocument = CodeRush.Documents.ActiveTextDocument;
			if (activeTextDocument == null)
				return;
			// TODO: More intelligent pastes go here. For example, we could identify the parts of the selection that were fields, methods, and properties and insert them in appropriate places in the target class.
			SourceRange insertedTextRange = activeTextDocument.InsertText(activeTextDocument.ActiveView.Caret.SourcePoint, multiSelect.ToText());
			activeTextDocument.Format(insertedTextRange);
		}
    private void PlugIn1_CommandExecuting(CommandExecutingEventArgs ea)
		{
			if (ea.CommandName == "Edit.Cut")
			{
				if (CodeRushPlaceholder.MultiSelect.SelectionExists(CodeRush.TextViews.Active))
				{
					Cut();
					ea.CancelDefault = true;
					ea.CancelEvent = true;
				}
			}
			else if (ea.CommandName == "Edit.Copy")
			{
				// Skorkin: Is there a way to determine the order in which an event is listened to? IOW priority.
				if (CodeRushPlaceholder.MultiSelect.SelectionExists(CodeRush.TextViews.Active))
				{
					Copy();
					ea.CancelDefault = true;
					ea.CancelEvent = true;
				}
			}
			else if (ea.CommandName == "Edit.Paste")
			{
				if (MultiSelect.IsOnClipboard())
				{
					Paste();
					ea.CancelDefault = true;
				}
			}
		}

		private void ctxMultiSelectRedoAvailable_ContextSatisfied(ContextSatisfiedEventArgs ea)
		{
			ea.Satisfied = CodeRushPlaceholder.MultiSelect.RedoIsAvailable(CodeRush.TextViews.Active);
		}

		private string GetCode(MultiSelect multiSelect, LanguageElementType elementType)
		{
			string result = String.Empty;
			
			MultiSelectEnumerator allElements = new MultiSelectEnumerator(multiSelect, elementType);
			while (allElements.MoveNext())
			{
				PartialSelection partialSelection = allElements.Current as PartialSelection;
				if (partialSelection != null)
				{
					result += partialSelection.Text;
					partialSelection.Generated = true; // Prevents this from coming in later when LanguageElementType.Unknown is passed in.
				}
			}
			return result;
		}

		public enum RelativeLocation
		{
			Before,
			After
		}

		public enum DefaultLocation
		{
			Top,
			Bottom
		}
		
		private SourcePoint GetInsertionPoint(TypeDeclaration activeType, LanguageElementType elementType, RelativeLocation relativeLocation, DefaultLocation defaultLocation)
		{
			LanguageElementType[] elementTypes;
			if (elementType == LanguageElementType.Variable)
			{
				elementTypes = new LanguageElementType[2];
				elementTypes[0] = LanguageElementType.Variable;
				elementTypes[1] = LanguageElementType.InitializedVariable;
			}
			else
			{
				elementTypes = new LanguageElementType[1];
				elementTypes[0] = elementType;
			}

			ElementEnumerable elementEnumerable = new ElementEnumerable(activeType, elementTypes);
		
			LanguageElement lastElement = null;
			foreach (LanguageElement element in elementEnumerable)
			{
				if (relativeLocation == RelativeLocation.Before)
					return element.GetFullBlockCutRange().Top;
				else	// RelativeLocation.After...
					lastElement = element;
			}
			if (lastElement != null)
				return lastElement.GetFullBlockCutRange().Bottom;
			if (defaultLocation == DefaultLocation.Top)
				return activeType.BlockCodeRange.Top;
			else  // Bottom...
				return activeType.BlockCodeRange.Bottom;
		}
    private void QueueAdd(TextDocument activeTextDocument, TypeDeclaration activeType, MultiSelect multiSelect, LanguageElementType elementType)
		{
			string codeToGenerate = GetCode(multiSelect, elementType);
			if (codeToGenerate == String.Empty)
				return;

			// TODO: Change RelativeLocation.After, DefaultLocation.Bottom parameters to instead reference fields populated from the MultiSelect options.
			SourcePoint insertionPoint = GetInsertionPoint(activeType, elementType, RelativeLocation.After, DefaultLocation.Bottom);
			activeTextDocument.QueueInsert(insertionPoint, codeToGenerate);
		}
		private void actMultiSelectIntegratedPaste_Execute(ExecuteEventArgs ea)
		{
			TextDocument activeTextDocument = CodeRush.Documents.ActiveTextDocument;
			if (activeTextDocument == null)
				return;
			TypeDeclaration activeType = CodeRush.Source.ActiveClassInterfaceOrStruct;
			if (activeType == null)
				return;
			MultiSelect multiSelect = CodeRushPlaceholder.MultiSelect.FromClipboard();
			if (multiSelect == null)
				return;

			multiSelect.PrepareToGenerate();
			QueueAdd(activeTextDocument, activeType, multiSelect, LanguageElementType.Method);
			QueueAdd(activeTextDocument, activeType, multiSelect, LanguageElementType.Property);
			QueueAdd(activeTextDocument, activeType, multiSelect, LanguageElementType.Event);
			QueueAdd(activeTextDocument, activeType, multiSelect, LanguageElementType.Variable);
			QueueAdd(activeTextDocument, activeType, multiSelect, LanguageElementType.Unknown);
			activeTextDocument.ApplyQueuedEdits("Integrated Paste");
		}

		private bool CanPerformIntegratedPaste()
		{
			if (CodeRush.Source.ActiveClassInterfaceOrStruct == null)
				return false;
			MultiSelect multiSelect = CodeRushPlaceholder.MultiSelect.FromClipboard();
			if (multiSelect == null)
				return false;
			return multiSelect.Selections.Count > 0 && multiSelect.ContainsOnlyMembers;
		}

		private static int GetPasteItemIndex(MenuBar contextMenu)
		{
			IMenuControl pasteItem = contextMenu.FindByCommandName("Edit.Paste");
			if (pasteItem != null)
				return pasteItem.Index;
			else
				return -1;
		}
		private void PlugIn1_EditorContextMenuShowing(EditorContextMenuShowingEventArgs ea)
		{
			bool shouldBeVisible = CanPerformIntegratedPaste();
			MenuBar contextMenu = CodeRush.Menus.Bars[VsCommonBar.EditorContext];
			if (contextMenu == null)
				return;
			IMenuControl integratedPasteItem = contextMenu.FindItemByAction(actMultiSelectIntegratedPaste);
			if (integratedPasteItem == null)
				return;
			integratedPasteItem.Visible = shouldBeVisible;
			if (!shouldBeVisible)
				return;
			int pasteItemIndex = GetPasteItemIndex(contextMenu);
			if (pasteItemIndex >= 0)
				integratedPasteItem.MoveTo(pasteItemIndex + 1);
		}
	}
}