using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.Diagnostics.Commands;

namespace DX_FormatOnSave
{
	/// <summary>
	/// DXCore plugin to format documents when they are saved.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The general algorithm is:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <term>User clicks the save button.</term>
	/// </item>
	/// <item>
	/// <term>One or more document saved events get raised.</term>
	/// </item>
	/// <item>
	/// <term>After the VS undo manager has done all its work to save the document, we start the format process:</term>
	/// </item>
	/// <item>
	/// <term>If the document is already in the process of getting formatted, exit or we get in an endless format/save/format/save loop.</term>
	/// </item>
	/// <item>
	/// <term>The document gets added to a list of documents being formatted.</term>
	/// </item>
	/// <item>
	/// <term>The document gets formatted and saved (which will raise the event again, hence the need for tracking).</term>
	/// </item>
	/// <item>
	/// <term>The document gets removed from the list of documents being formatted.</term>
	/// </item>
	/// </list>
	/// </remarks>
	public partial class FormatOnSavePlugin : StandardPlugIn
	{
		/// <summary>
		/// Keeps track of which documents are currently being formatted so
		/// we don't end up in an endless loop of format/save.
		/// </summary>
		private List<Document> _docsBeingFormatted = new List<Document>();

		/// <summary>
		/// Synchronizes access to the list of docs being formatted.
		/// </summary>
		private object _listSync = new object();

		/// <summary>
		/// Raises events for documents being saved.
		/// </summary>
		private RunningDocumentTableEventProvider _docEvents = null;

		/// <summary>
		/// Gets or sets the options for the plugin.
		/// </summary>
		/// <value>
		/// The <see cref="DX_FormatOnSave.OptionSet"/> that the current plugin
		/// is using.
		/// </value>
		public OptionSet Options { get; set; }

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Catching any problems that go on during post-save formatting.")]
		private void DocumentSaving(object sender, DocumentEventArgs e)
		{
			var doc = e.Document;
			if (!this.DocumentShouldBeFormatted(doc))
			{
				return;
			}

			lock (this._listSync)
			{
				// If we're already in the process of formatting the doc, bail.
				if (this._docsBeingFormatted.Contains(doc))
				{
					return;
				}

				// We're not already formatting the doc, so add it to the list.
				this._docsBeingFormatted.Add(doc);
			}

			// Execute document formatting after the VsLinkedUndoTransactionManager.CloseLinkedUndo
			// method has finished so we don't mess up IntelliSense or Undo.
			// http://www.devexpress.com/Support/Center/Question/Details/B223163
			SynchronizationContext.Current.Post(state =>
				{
					try
					{
						FormatDocument(e.Document);
					}
					catch (Exception ex)
					{
						// Issue #147: Unhandled exception while attempting to format the document.
						// This happens if the user closes the document and has
						// unsaved changes - they elect to save on close and this
						// will run AFTER the doc is already closed so we can't
						// cause the document to focus and can't format.
						Log.SendException("Error formatting document on save.", ex);
					}
					finally
					{
						lock (this._listSync)
						{
							// Formatting is done; remove the marker.
							this._docsBeingFormatted.Remove(doc);
						}
					}
				}, null);


		}

		/// <summary>
		/// Determines whether a given document should have formatting executed on it.
		/// </summary>
		/// <param name="doc">The document to check.</param>
		/// <returns>
		/// <see langword="true" /> if formatting is enabled and the document
		/// is one of the selected languages to format; <see langword="false" />
		/// otherwise.
		/// </returns>
		private bool DocumentShouldBeFormatted(Document doc)
		{
			// If the user disabled the plugin, bail.
			if (!this.Options.Enabled)
			{
				return false;
			}

			// If the document isn't text or an enabled language, bail.
			TextDocument textDoc = doc as TextDocument;
			if (textDoc == null || !this.LanguageSelectedForFormatting(textDoc.Language))
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Finalizes the plug in.
		/// </summary>
		public override void FinalizePlugIn()
		{
			this._docEvents.Dispose();
			this._docEvents = null;
			base.FinalizePlugIn();
		}

		/// <summary>
		/// Formats and re-saves a document.
		/// </summary>
		/// <param name="doc">The document to format.</param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="doc" /> is <see langword="null" />.
		/// </exception>
		public static void FormatDocument(Document doc)
		{
			if (doc == null)
			{
				throw new ArgumentNullException("doc");
			}

			// You can only format the active document, so we have to temporarily
			// activate each document that needs formatting. This is a limitation
			// because if the document is ALSO closing we can't make it active
			// so we can't format it.
			Document active = CodeRush.Documents.Active;
			if (doc != active)
			{
				doc.Activate();
			}
			CodeRush.Documents.Format();
			doc.Save();
			if (doc != active)
			{
				active.Activate();
			}
		}

		private void FormatOnSavePlugin_OptionsChanged(OptionsChangedEventArgs ea)
		{
			this.RefreshOptions();
		}

		/// <summary>
		/// Initializes the plug in.
		/// </summary>
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();
			this.OptionsChanged += FormatOnSavePlugin_OptionsChanged;
			this._docEvents = new RunningDocumentTableEventProvider();
			this._docEvents.Initialize();
			this._docEvents.Saving += DocumentSaving;
			this.RefreshOptions();
		}

		/// <summary>
		/// Determines whether a document should be formatted based on the provided
		/// language ID and the user's selected options.
		/// </summary>
		/// <param name="language">The language ID for the document in question.</param>
		/// <returns>
		/// <see langword="true" /> if the user elected to format documents of the
		/// given language; <see langword="false" /> if not.
		/// </returns>
		public bool LanguageSelectedForFormatting(string language)
		{
			if (String.IsNullOrEmpty(language))
			{
				return false;
			}
			switch (language)
			{
				case DevExpress.DXCore.Constants.Str.Language.CPlusPlus:
					return (this.Options.LanguagesToFormat & DocumentLanguages.CPlusPlus) == DocumentLanguages.CPlusPlus;
				case DevExpress.DXCore.Constants.Str.Language.CSharp:
					return (this.Options.LanguagesToFormat & DocumentLanguages.CSharp) == DocumentLanguages.CSharp;
				case DevExpress.DXCore.Constants.Str.Language.CSS:
					return (this.Options.LanguagesToFormat & DocumentLanguages.Css) == DocumentLanguages.Css;
				case DevExpress.DXCore.Constants.Str.Language.HTML:
					return (this.Options.LanguagesToFormat & DocumentLanguages.Html) == DocumentLanguages.Html;
				case DevExpress.DXCore.Constants.Str.Language.JavaScript:
					return (this.Options.LanguagesToFormat & DocumentLanguages.JavaScript) == DocumentLanguages.JavaScript;
				case DevExpress.DXCore.Constants.Str.Language.VisualBasic:
					return (this.Options.LanguagesToFormat & DocumentLanguages.VisualBasic) == DocumentLanguages.VisualBasic;
				case DevExpress.DXCore.Constants.Str.Language.XAML:
					return (this.Options.LanguagesToFormat & DocumentLanguages.Xaml) == DocumentLanguages.Xaml;
				case DevExpress.DXCore.Constants.Str.Language.XML:
				case DevExpress.DXCore.Constants.Str.Language.XMLOnly:
					return (this.Options.LanguagesToFormat & DocumentLanguages.Xml) == DocumentLanguages.Xml;
				default:
					return false;
			}
		}

		/// <summary>
		/// Refreshes the set of options being used by this plugin.
		/// </summary>
		public void RefreshOptions()
		{
			this.Options = OptionSet.Load(PluginOptionsPage.Storage);
		}
	}
}