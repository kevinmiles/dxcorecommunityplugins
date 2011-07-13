using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_EasierIdentiers
{
	public partial class PlugIn1 : StandardPlugIn
	{
		private bool _ConvertingSpacesToCamelCase;
		private bool _EnableInParameters;
		private bool _EnableLocalVariables;
		private bool _IsEnabled = true;

		// Ctrl+B == Paste/Replace
		private bool _NextCharIsUpper;
		// DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();
			EventNexus.TextFieldActivated += new TextFieldEventHandler(EventNexus_TextFieldActivated);
			EventNexus.TextFieldDeactivated += new TextFieldEventHandler(EventNexus_TextFieldDeactivated);
			EventNexus.TextFieldCommitted += new TextFieldEventHandler(EventNexus_TextFieldCommitted);
			LoadSettings();
		}
		#endregion
		#region FinalizePlugIn
		public override void FinalizePlugIn()
		{
			EventNexus.TextFieldActivated -= new TextFieldEventHandler(EventNexus_TextFieldActivated);
			EventNexus.TextFieldDeactivated -= new TextFieldEventHandler(EventNexus_TextFieldDeactivated);
			EventNexus.TextFieldCommitted -= new TextFieldEventHandler(EventNexus_TextFieldCommitted);

			base.FinalizePlugIn();
		}
		#endregion

		private bool FoundValidPointToConvertToCamelCase()
		{
			// Add code to verify that we're in a good place to convert spaces to camel case.
			CodeRush.Source.ParseIfTextChanged();
			LanguageElement activeMember = CodeRush.Source.ActiveMember;
			if (activeMember != null && activeMember.NameRange.Contains(CodeRush.Caret.SourcePoint))
				return true;
			if (_EnableInParameters)
			{
				Param param = CodeRush.Source.Active as Param;
				if (param != null && param.NameRange.Contains(CodeRush.Caret.SourcePoint))
				{
					_NextCharIsUpper = false;
					return true;
				}
			}
			if (_EnableLocalVariables)
			{
				Variable variable = CodeRush.Source.Active as Variable;
				if (variable != null && variable.NameRange.Contains(CodeRush.Caret.SourcePoint))
				{
					_NextCharIsUpper = false;
					return true;
				}
			}
			return false;
		}
		private void ConvertSpacesToCamelCase(bool enable)
		{
			if (enable == _ConvertingSpacesToCamelCase)
				return;
			_NextCharIsUpper = enable;
			if (enable)
				enable = FoundValidPointToConvertToCamelCase();
			_ConvertingSpacesToCamelCase = enable;

			if (enable)
				CodeRush.ApplicationObject.StatusBar.Text = "Space to Camel Case";
			else
				CodeRush.ApplicationObject.StatusBar.Text = "";
		}

		void EventNexus_TextFieldCommitted(TextFieldEventArgs ea)
		{
			ConvertSpacesToCamelCase(false);
		}

		void EventNexus_TextFieldDeactivated(TextFieldEventArgs ea)
		{
			ConvertSpacesToCamelCase(false);
		}

		void EventNexus_TextFieldActivated(TextFieldEventArgs ea)
		{
			if (_IsEnabled)
				ConvertSpacesToCamelCase(true);
		}
		
		private void PlugIn1_EditorCharacterTyping(EditorCharacterTypingEventArgs ea)
		{
			bool inTextField = CodeRush.Context.Satisfied("System\\In TextField") == ContextResult.Satisfied;
			if (_ConvertingSpacesToCamelCase)
			{
				if (!inTextField)		// State issue -- maybe we didn't get the TextFieldDeactivated event?
					ConvertSpacesToCamelCase(false);
				if (ea.Character == ' ')
				{
					ea.Cancel = true;
					_NextCharIsUpper = true;
				}
				else if (_NextCharIsUpper)
				{
					ea.Cancel = true;
					_NextCharIsUpper = false;
					CodeRush.Test.SendKey(CodeRush.IDE.Handle, char.ToUpper(ea.Character));
				}
			}
		}

		private void LoadSettings()
		{
			DecoupledStorage storage = OptConvertSpacesToCamelCase.Storage;
			_IsEnabled = storage.ReadBoolean("SpacesToCamelCase", "Enabled", true);
			_EnableInParameters = storage.ReadBoolean("SpacesToCamelCase", "Parameters", false);
			_EnableLocalVariables = storage.ReadBoolean("SpacesToCamelCase", "Locals", false);
		}
		private void PlugIn1_OptionsChanged(OptionsChangedEventArgs ea)
		{
			if (ea.OptionsPages.Contains(typeof(OptConvertSpacesToCamelCase)))
			{
				LoadSettings();
			}
		}

		private void PlugIn1_KeyPressed(KeyPressedEventArgs ea)
		{
			const int KEY_Backspaces = 8;
      if (_ConvertingSpacesToCamelCase && ea.KeyCode == KEY_Backspaces && ea.NoShiftKeys)
			{
				char leftChar = CodeRush.Caret.LeftChar;
				if (leftChar == char.ToUpper(leftChar))
					_NextCharIsUpper = true;
			}
		}

		private void ctxConvertingSpacesToCamelCase_ContextSatisfied(ContextSatisfiedEventArgs ea)
		{
			ea.Satisfied = _ConvertingSpacesToCamelCase;
		}

		private void PlugIn1_CommandExecuting(CommandExecutingEventArgs ea)
		{
			// @SurlyDev: This is for you -- convert spaces to camel case on a paste...
			if (_ConvertingSpacesToCamelCase && ea.CommandName == "Edit.Paste")
				if (Clipboard.ContainsText())
				{
					string text = Clipboard.GetText();
					if (text.Length > 255)
						return;
					char[] punctuation = { ' ', '/', '@', ':', '.' };
					string[] tokens = text.Split(punctuation, 255, StringSplitOptions.RemoveEmptyEntries);
					string newText = String.Empty;
					foreach (string token in tokens)
					{
						if (token.Length > 1)
							newText += char.ToUpper(token[0]).ToString() + token.Substring(1);
						else if (token.Length > 0)
							newText += char.ToUpper(token[0]).ToString();
					}
					Clipboard.SetText(newText);
				}
		}
	}
}