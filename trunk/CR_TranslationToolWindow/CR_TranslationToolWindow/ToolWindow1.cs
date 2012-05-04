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
		private string language = "Basic";
		private LanguageElement lastMember = null;
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

		private void ShowCode()
		{
			LanguageElement sourceNode;
			sourceNode = optSourceFile.Checked ? lastMember.FileNode : lastMember;
			string code = CodeRush.Language.GenerateElement(sourceNode, language);
			codeView1.ShowCode(code, language);
		}
		private void TranslationLanguageChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			if (radioButton == null)
				return;
			language = radioButton.Text;
			ShowCode();
		}

		private void SourceChanged(object sender, EventArgs e)
		{
			ShowCode();
		}

		private void events_LanguageElementActivated(LanguageElementActivatedEventArgs ea)
		{
			LanguageElement activeMember = CodeRush.Source.ActiveMember;
			if (lastMember == activeMember)
				return;
			lastMember = activeMember;
			ShowCode();
		}


	}
}