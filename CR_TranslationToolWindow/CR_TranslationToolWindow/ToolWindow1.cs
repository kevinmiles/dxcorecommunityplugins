using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_TranslatorToolWindow
{
	[Title("Translator")]
	public partial class ToolWindow1 : ToolWindowPlugIn
	{
		private LanguageElement lastMember = null;
		private string source = "Member";
		private string _language = "Basic";
		// DXCore-generated code...


		public DecoupledStorage Storage
		{
			get { return CodeRush.Options.GetStorage("ToolWindows", "TranslationToolWindow"); }
		}

		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();
			LoadSettings();
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

		#region Radio Button Change Events
		private void TranslationLanguageChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			if (radioButton == null)
				return;
			_language = radioButton.Text;

			ShowCode();
			SaveSettings();
		}

		private void SourceChanged(object sender, EventArgs e)
		{
			source = optSourceFile.Checked ? "File" : "Member";
			ShowCode();
			SaveSettings();
		}
		#endregion
		#region Settings
		private void SaveSettings()
		{
			Storage.WriteString("Translation", "Language", _language);
			Storage.WriteString("Translation", "Source", source);
		}
		private void LoadSettings()
		{
			_language = Storage.ReadString("Translation", "Language", "Basic");
			switch (_language)
			{
				case "Basic":
					optLanguageBasic.Checked = true;
					break;
				case "CSharp":
					optLanguageCSharp.Checked = true;
					break;
				case "JavaScript":
					optLanguageJavaScript.Checked = true;
					break;
				case "C/C++":
					optLanguageCpp.Checked = true;
					break;
			}
			source = Storage.ReadString("Translation", "Source", "Member");
			switch (source)
			{
				case "Member":
					optSourceMember.Checked = true;
					break;
				case "File":
					optSourceFile.Checked = true;
					break;
			}
		}
		#endregion

		private void events_LanguageElementActivated(LanguageElementActivatedEventArgs ea)
		{
			LanguageElement activeMember = CodeRush.Source.ActiveMember;
			if (lastMember == activeMember)
				return;
			lastMember = activeMember;
			ShowCode();
		}

		private void ShowCode()
		{
			if (lastMember == null)
				return;
			codeView1.ShowCode(GetCode(_language), _language);
		}
		private string GetCode(string language)
		{
			LanguageElement sourceNode = optSourceFile.Checked ? lastMember.FileNode : lastMember;
			return CodeRush.Language.GenerateElement(sourceNode, language);
		}

		private void cmdCopyTranslation_Click(object sender, EventArgs e)
		{
			TextDocument ActiveDoc = CodeRush.Documents.ActiveTextDocument;
			if (ActiveDoc == null)
				return;
			Clipboard.SetText(GetCode(_language));
		}

		private void cmdCopyOriginal_Click(object sender, EventArgs e)
		{
			TextDocument ActiveDoc = CodeRush.Documents.ActiveTextDocument;
			if (ActiveDoc == null)
				return;
			Clipboard.SetText(GetCode(ActiveDoc.Language));
		}


	}
}