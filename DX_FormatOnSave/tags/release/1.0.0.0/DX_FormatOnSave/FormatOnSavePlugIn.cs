using System;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;

namespace DX_FormatOnSave
{
	/// <summary>
	/// DXCore plugin to format documents when they are saved.
	/// </summary>
	public partial class FormatOnSavePlugIn : StandardPlugIn
	{
		private RunningDocumentTableEventProvider _docEvents = null;

		/// <summary>
		/// Gets or sets the options for the plugin.
		/// </summary>
		/// <value>
		/// The <see cref="DX_FormatOnSave.OptionSet"/> that the current plugin
		/// is using.
		/// </value>
		public OptionSet Options { get; set; }

		private void DocumentSaving(object sender, DocumentEventArgs e)
		{
			this.FormatDocument(e.Document);
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
		/// Formats a document.
		/// </summary>
		/// <param name="doc">The document to format.</param>
		public void FormatDocument(Document doc)
		{
			// If the user disabled the plugin, bail.
			if (!this.Options.Enabled)
			{
				return;
			}

			// If the document isn't text or an enabled language, bail.
			TextDocument textDoc = doc as TextDocument;
			if (textDoc == null || !this.LanguageSelectedForFormatting(textDoc.Language))
			{
				return;
			}

			// You can only format the active document, so we have to temporarily
			// activate each document that needs formatting.
			Document active = CodeRush.Documents.Active;
			if (textDoc != active)
			{
				textDoc.Activate();
			}
			CodeRush.Documents.Format();
			if (textDoc != active)
			{
				active.Activate();
			}
		}

		private void FormatOnSavePlugIn_OptionsChanged(OptionsChangedEventArgs ea)
		{
			this.RefreshOptions();
		}

		/// <summary>
		/// Initializes the plug in.
		/// </summary>
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();
			this.OptionsChanged += new OptionsChangedEventHandler(FormatOnSavePlugIn_OptionsChanged);
			this._docEvents = new RunningDocumentTableEventProvider();
			this._docEvents.Initialize();
			this._docEvents.Saving += new EventHandler<DocumentEventArgs>(DocumentSaving);
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
			this.Options = OptionSet.Load(PlugInOptionsPage.Storage);
		}
	}
}