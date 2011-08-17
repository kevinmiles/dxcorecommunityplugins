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

namespace CR_MultiSelect
{
	public partial class PlugIn1 : StandardPlugIn
	{
		private string _ThisAssemblyName;
    private const string STR_MultiSelectList = "MultiSelectList";
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

		private MultiSelect GetMultiSelect(TextView activeView)
		{
			return activeView.Storage.GetObject(STR_MultiSelectList) as MultiSelect;
		}

		private void SetMultiSelect(TextView activeView, MultiSelect multiSelect)
		{
			activeView.Storage.AttachObject(STR_MultiSelectList, multiSelect);
		}

    /* 
		 We'll see:
			• BlockHighlighter
			• Attached Storage for views
			• Intercepting commands
			• Clipboard magic .NET stuff here.
		 */
		private void actMultiSelectAdd_Execute(ExecuteEventArgs ea)
		{
			TextView activeView = CodeRush.TextViews.Active;
			if (activeView == null)
				return;
			MultiSelect multiSelect = GetMultiSelect(activeView);
			if (multiSelect == null)
			{
				multiSelect = new MultiSelect();
				multiSelect.FileName = activeView.FileNode.Name;
				multiSelect.Language = activeView.TextDocument.Language;
        multiSelect.ClassName = CodeRush.Source.ActiveClassName;
				SetMultiSelect(activeView, multiSelect);
			}

			multiSelect.AddSelection(activeView);
			
		}

		private void actMultiSelectClear_Execute(ExecuteEventArgs ea)
		{
			MultiSelect multiSelect = GetMultiSelect(CodeRush.TextViews.Active);
			if (multiSelect == null)
				return;
			multiSelect.Clear();
			RemoveMultiSelect();
		}

		private void actMultiSelectRedo_Execute(ExecuteEventArgs ea)
		{

		}

		private void actMultiSelectUndo_Execute(ExecuteEventArgs ea)
		{

		}

		private void ctxMultiSelectExists_ContextSatisfied(ContextSatisfiedEventArgs ea)
		{
			ea.Satisfied = MultiSelectionExists(CodeRush.TextViews.Active);
		}

		private bool MultiSelectionExists(TextView textView)
		{
			MultiSelect multiSelect = GetMultiSelect(textView);
			if (multiSelect == null)
				return false;
			return multiSelect.Selections.Count > 0;
		}
		private MultiSelect Copy()
		{
			MultiSelect multiSelect = GetMultiSelect(CodeRush.TextViews.Active);
			if (multiSelect != null)
				multiSelect.CopyToClipboard();
			return multiSelect;
		}
		private static void RemoveMultiSelect(TextView textView)
		{
			textView.Storage.RemoveObject(STR_MultiSelectList);
		}
		private void RemoveMultiSelect()
		{
			TextView activeTextView = CodeRush.TextViews.Active;
			if (activeTextView == null)
				return;
			RemoveMultiSelect(activeTextView);
		}
    private MultiSelect Cut()
		{
			MultiSelect multiSelect = Copy();
			if (multiSelect != null)
			{
				multiSelect.Delete("MultiSelect - Cut");
				multiSelect.Clear();
				RemoveMultiSelect();
			}
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
				if (MultiSelectionExists(CodeRush.TextViews.Active))
				{
					Cut();
					ea.CancelDefault = true;
					ea.CancelEvent = true;
				}
			}
			else if (ea.CommandName == "Edit.Copy")
			{
				// Skorkin: Is there a way to determine the order in which an event is listened to? IOW priority.
				if (MultiSelectionExists(CodeRush.TextViews.Active))
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

		private void PlugIn1_DocumentRenamed(DocumentRenamedEventArgs ea)
		{
			TextDocument textDocument = ea.Document as TextDocument;
			if (textDocument == null)
				return;
			TextView[] textViews = textDocument.GetTextViews(false);
			foreach (TextView textView in textViews)
			{
				MultiSelect multiSelect = GetMultiSelect(textView);
				if (multiSelect != null)
					multiSelect.FileName = ea.NewName;
			}
		}

    private void PlugIn1_TextChanged(TextChangedEventArgs ea)
		{
			foreach (TextView textView in ea.TextDocument.GetTextViews())
			{
				MultiSelect multiSelect = GetMultiSelect(textView);
				if (multiSelect != null)
				{
					multiSelect.Clear();
					RemoveMultiSelect(textView);
				}
			}
		}
	}
}