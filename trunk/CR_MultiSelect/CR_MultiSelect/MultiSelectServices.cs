using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.Core;
using System.Windows.Forms;
using DevExpress.DXCore.Platform.Drawing;
using System.ComponentModel;

namespace DevExpress.CodeRush.Core
{
	public sealed class MultiSelectServices : Service
	{
		private const string STR_ClipboardFormatName = "DXCore.MultiCodeSelect";
		private const string STR_MultiSelectList = "MultiSelectList";
		private const string STR_RedoMultiSelectList = "RedoMultiSelectList";
		private Color _HighlightColor;

		
		#region internal constructor...
		internal MultiSelectServices() { }
		#endregion

		#region InitializeService
		protected override void InitializeService(InitializeCause cause)
		{
			base.InitializeService(cause);
			HookEvents();
			SetHighlightColor(OptMultiSelect.DefaultSelectionColor);
		}
		#endregion
		#region FinalizeService
		protected override void FinalizeService(FinalizeCause cause)
		{
			UnhookEvents();
			base.FinalizeService(cause);
		}
		#endregion

		// event handlers...
		#region EventNexus_DocumentRenamed
		void EventNexus_DocumentRenamed(DocumentRenamedEventArgs ea)
		{
			TextDocument textDocument = ea.Document as TextDocument;
			if (textDocument == null)
				return;
			TextView[] textViews = textDocument.GetTextViews(false);
			foreach (TextView textView in textViews)
			{
				MultiSelect multiSelect = CodeRushPlaceholder.MultiSelect.Get(textView);
				if (multiSelect != null)
					multiSelect.FileName = ea.NewName;
			}
		}
		#endregion
		#region EventNexus_OptionsChanged
		void EventNexus_OptionsChanged(OptionsChangedEventArgs ea)
		{
			if (!ea.OptionsPages.Contains(typeof(OptMultiSelect)))
				return;

			SetHighlightColor(OptMultiSelect.Storage.ReadColor("Appearance", "SelectionColor"));
			RefreshAllHighlighting();
		}
		#endregion
		#region EventNexus_TextChanged
		void EventNexus_TextChanged(TextChangedEventArgs ea)
		{
			foreach (TextView textView in ea.TextDocument.GetTextViews())
				Clear(textView);
		}
		#endregion

		// Methods hidden from Intellisense...
		#region HookEvents
		[Browsable(false)]
		internal void HookEvents()
		{
			EventNexus.TextChanged += new TextChangedEventHandler(EventNexus_TextChanged);
			EventNexus.DocumentRenamed += new DocumentRenamedEventHandler(EventNexus_DocumentRenamed);
			EventNexus.OptionsChanged += new OptionsChangedEventHandler(EventNexus_OptionsChanged);
		}
		#endregion
		#region GetRedo
		[Browsable(false)]
		public MultiSelect GetRedo(TextView textView)
		{
			return textView.Storage.GetObject(STR_RedoMultiSelectList) as MultiSelect;
		}
		#endregion
		#region Remove
		/// <summary>
		/// Removes the MultiSelect object from the specified TextView.
		/// </summary>
		/// <param name="textView">The TextView from which to remove the MultiSelect object.</param>
		[Browsable(false)]
		public void Remove(TextView textView)
		{
			textView.Storage.RemoveObject(STR_MultiSelectList);
			textView.Storage.RemoveObject(STR_RedoMultiSelectList);
		}
		#endregion
		#region Set
		/// <summary>
		/// Sets the MultiSelect object for the specified TextView.
		/// </summary>
		/// <param name="textView">The TextView to work with.</param>
		/// <param name="multiSelect">The MultiSelect instance to assign to the TextView.</param>
		[Browsable(false)]
		public void Set(TextView textView, MultiSelect multiSelect)
		{
			textView.Storage.AttachObject(STR_MultiSelectList, multiSelect);
		}
		#endregion
		#region SetRedo
		[Browsable(false)]
		public void SetRedo(TextView textView, MultiSelect redoMultiSelect)
		{
			textView.Storage.AttachObject(STR_RedoMultiSelectList, redoMultiSelect);
		}
		#endregion
		#region UnhookEvents
		[Browsable(false)]
		public void UnhookEvents()
		{
			EventNexus.TextChanged -= new TextChangedEventHandler(EventNexus_TextChanged);
			EventNexus.DocumentRenamed -= new DocumentRenamedEventHandler(EventNexus_DocumentRenamed);
			EventNexus.OptionsChanged -= new OptionsChangedEventHandler(EventNexus_OptionsChanged);
		}
		#endregion

		// protected internal overridden properties...
		#region Name
		protected override string Name
		{
			get
			{
				return "MultiSelect";
			}
		}
		#endregion

		// public properties...
		#region HighlightColor
		public Color HighlightColor
		{
			get
			{
				return _HighlightColor;
			}
			set
			{
				_HighlightColor = value;
			}
		}
		#endregion

		// public methods...
		#region Add
		/// <summary>
		/// Adds the selection in the specified TextView to this view's MultiSelect list.
		/// </summary>
		/// <param name="textView">The TextView containing the MultiSelect list to add to.</param>
		public void Add(TextView textView)
		{
			if (textView == null)
				return;
			MultiSelect multiSelect = Get(textView);
			if (multiSelect == null)
			{
				multiSelect = new MultiSelect();
				multiSelect.FileName = textView.FileNode.Name;
				multiSelect.Language = textView.TextDocument.Language;
				multiSelect.TypeName = CodeRush.Source.ActiveTypeName;
				Set(textView, multiSelect);
			}
			else if (multiSelect.TypeName != CodeRush.Source.ActiveTypeName)		// Inconsistent class names, so let's clear it out.
				multiSelect.TypeName = String.Empty;

			MultiSelect redoMultiSelect = GetRedo(textView);
			if (redoMultiSelect != null)
				redoMultiSelect.Selections.Clear();

			multiSelect.AddSelection(textView);
		}
		#endregion
		#region Clear
		/// <summary>
		/// Clears the multi-selection and removes it from the specified TextView.
		/// </summary>
		/// <param name="textView">The TextView containing the multi-selection to remove.</param>
		public void Clear(TextView textView)
		{
			MultiSelect multiSelect = Get(textView);
			if (multiSelect == null)
				return;
			multiSelect.Clear();
			Remove(textView);
		}
		#endregion
		#region CopyToClipboard(MultiSelect multiSelect)
		/// <summary>
		/// Copies the specified MultiSelect object to the clipboard.
		/// </summary>
		/// <param name="multiSelect">The MultiSelect object to copy to the clipboard.</param>
		public void CopyToClipboard(MultiSelect multiSelect)
		{
			multiSelect.Sort();
			DataFormats.Format format = DataFormats.GetFormat(STR_ClipboardFormatName);
			IDataObject dataObject = new DataObject();
			dataObject.SetData(DataFormats.Text, multiSelect.ToText());
			dataObject.SetData(format.Name, multiSelect);
			Clipboard.SetDataObject(dataObject, true);
		}
		#endregion
		#region CopyToClipboard(TextView textView)
		/// <summary>
		/// Copies the MultiSelect object from the specified TextView to the clipboard.
		/// </summary>
		/// <param name="textView">The TextView containing the MultiSelect object to copy.</param>
		/// <returns>The MultiSelect object from the active TextView.</returns>
		public MultiSelect CopyToClipboard(TextView textView)
		{
			MultiSelect multiSelect = Get(textView);
			if (multiSelect != null)
				multiSelect.CopyToClipboard();
			return multiSelect;
		}
		#endregion
		#region CutToClipboard(TextView textView)
		/// <summary>
		/// Copies the MultiSelect object from the specified TextView to the clipboard and removes all selections.
		/// </summary>
		/// <param name="textView">The TextView containing the MultiSelect object to cut.</param>
		/// <returns>The MultiSelect object from the active TextView.</returns>
		public MultiSelect CutToClipboard(TextView textView)
		{
			MultiSelect multiSelect = Get(textView);
			if (multiSelect != null)
			{
				multiSelect.WasCut = true;
				multiSelect.CopyToClipboard();
			}
			return multiSelect;
		}
		#endregion
		#region Delete
		public void Delete(MultiSelect multiSelect, string operation)
		{
			TextDocument textDocument = CodeRush.Documents.Get(multiSelect.FileName) as TextDocument;
			if (textDocument == null)
				return;
			foreach (PartialSelection selection in multiSelect.Selections)
				textDocument.QueueDelete(selection.Range);

			textDocument.ApplyQueuedEdits(operation);
		}
		#endregion
		#region FromClipboard
		public MultiSelect FromClipboard()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject.GetDataPresent(STR_ClipboardFormatName))
				return dataObject.GetData(STR_ClipboardFormatName) as MultiSelect;
			return null;
		}
		#endregion
		#region Get
		/// <summary>
		/// Gets the MultiSelect object associated with the specified TextView. Returns null if no multi-selection exists.
		/// </summary>
		/// <param name="textView">The TextView to check.</param>
		public MultiSelect Get(TextView textView)
		{
			return textView.Storage.GetObject(STR_MultiSelectList) as MultiSelect;
		}
		#endregion
    #region IsOnClipboard
		public bool IsOnClipboard()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			return dataObject.GetDataPresent(STR_ClipboardFormatName);
		}
		#endregion
		#region Redo
		public void Redo(TextView textView)
		{
			MultiSelect redoMultiSelect = CodeRushPlaceholder.MultiSelect.GetRedo(textView);
			if (redoMultiSelect == null || redoMultiSelect.Selections.Count <= 0)
				return;

			MultiSelect multiSelect = CodeRushPlaceholder.MultiSelect.Get(textView);
			if (multiSelect == null)
				return;

			int lastIndex = redoMultiSelect.Selections.Count - 1;
			multiSelect.AddSelection(textView, redoMultiSelect.Selections[lastIndex]);
			redoMultiSelect.Selections.RemoveAt(lastIndex);
		}
		#endregion
		#region RedoIsAvailable
		/// <summary>
		/// Returns true if a multi-select Redo operation is available for the specified TextView.
		/// </summary>
		/// <param name="textView">The TextView to check.</param>
		public bool RedoIsAvailable(TextView textView)
		{
			MultiSelect redoMultiSelect = GetRedo(textView);
			return redoMultiSelect != null && redoMultiSelect.Selections.Count > 0;
		}
		#endregion
		#region RefreshAllHighlighting
		/// <summary>
		/// Refreshes highlighting for all multi-select instances.
		/// </summary>
		public void RefreshAllHighlighting()
		{
			foreach (TextView textView in CodeRush.TextViews)
			{
				MultiSelect multiSelect = Get(textView);
				if (multiSelect != null)
					multiSelect.RefreshHighlighting(textView);
			}
		}
		#endregion
		#region SelectionExists
		/// <summary>
		/// Returns true if the specified TextView contains a MultiSelect object with at least one selection defined.
		/// </summary>
		/// <param name="textView">The TextView to check.</param>
		public bool SelectionExists(TextView textView)
		{
			MultiSelect multiSelect = Get(textView);
			if (multiSelect == null)
				return false;
			return multiSelect.Selections.Count > 0;
		}
		#endregion
		#region SetHighlightColor
		/// <summary>
		/// Sets the highlight color for multi-select instances.
		/// </summary>
		public void SetHighlightColor(System.Drawing.Color thisColor)
		{
			_HighlightColor = Color.FromRgb(thisColor.R, thisColor.G, thisColor.B);
		}
		#endregion
		#region Undo
		public void Undo(TextView activeTextView)
		{
			MultiSelect multiSelect = Get(activeTextView);
			if (multiSelect == null || multiSelect.Selections.Count == 0)
				return;

			MultiSelect redoMultiSelect = GetRedo(activeTextView);
			if (redoMultiSelect == null)
			{
				redoMultiSelect = new MultiSelect();
				SetRedo(activeTextView, redoMultiSelect);
			}

			int lastIndex = multiSelect.Selections.Count - 1;
			PartialSelection selectionToRemove = multiSelect.GetSelectionAt(lastIndex);
			redoMultiSelect.Selections.Add(selectionToRemove);
			selectionToRemove.RemoveHighlighter();
			activeTextView.Caret.MoveTo(selectionToRemove.CaretPosition);
			multiSelect.RemoveSelectionAt(lastIndex);
		}
		#endregion
		#region UndoIsAvailable
		/// <summary>
		/// Returns true if a multi-select Undo operation is available for the specified TextView.
		/// </summary>
		/// <param name="textView">The TextView to check.</param>
		public bool UndoIsAvailable(TextView textView)
		{
			MultiSelect multiSelect = Get(textView);
			return multiSelect != null && multiSelect.Selections.Count > 0;
		}
		#endregion
	}
}
