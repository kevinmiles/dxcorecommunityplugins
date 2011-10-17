using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core.Replacement;

namespace CR_EnforceNamingConventions
{
	public partial class PlugIn1 : StandardPlugIn
	{
		private int _NumMembersChanged = 0;
		private int _NumReferencesChanged;
		private int _NumTypesChanged = 0;
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


		public enum FirstLetterStyle
		{
			Lower,
			Upper
		}

		static string GetNewName(IMemberElement element)
		{
			if (element == null)
				return null;

			string name = element.Name;

			if (String.IsNullOrEmpty(name))
				return null;

			name = name.TrimStart('_');

			if (name == String.Empty)
				return name;

			bool allUpper = true;
			foreach (char chr in name)
				if (char.IsLower(chr))
				{
					allUpper = false;
					break;
				}

			if (allUpper)
				name = name.ToLower();

			string firstLetter = name.Substring(0, 1);

			FirstLetterStyle firstLetterStyle;
			if (element is IFieldElement && element.Visibility != MemberVisibility.Public)
				firstLetterStyle = FirstLetterStyle.Lower;
			else
				firstLetterStyle = FirstLetterStyle.Upper;

			if (firstLetterStyle == FirstLetterStyle.Lower)
				firstLetter = firstLetter.ToLower();
			else
				firstLetter = firstLetter.ToUpper();

			return String.Format("{0}{1}", firstLetter, name.Remove(0, 1));
		}
		private void GetChanges(FileChangeCollection result, Member member, string newName)
		{
			result.Add(new FileChange(member.FileNode.Name, member.NameRange, newName));
			CascadeReferenceSearcher cascadeReferenceSearcher = new CascadeReferenceSearcher();
			IElementCollection references = cascadeReferenceSearcher.FindCascadeReferences(CodeRush.Source.ActiveSolution, member);
			foreach (IElement reference in references)
			{
				_NumReferencesChanged++;
				result.Add(new FileChange(reference.FirstFile.Name, reference.FirstNameRange, newName));
			}
		}
		private FileChangeCollection GetChanges()
		{
			_NumTypesChanged = 0;
			_NumMembersChanged = 0;
			_NumReferencesChanged = 0;
			Cursor saveCursor = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				FileChangeCollection result;
				result = new FileChangeCollection();
				SolutionElement activeSolution = CodeRush.Source.ActiveSolution;
				foreach (TypeDeclaration type in activeSolution.AllTypes)
				{
					bool countedThisType = false;
					foreach (Member member in type.AllMembers)
					{
						string newName = GetNewName(member);
						if (!String.IsNullOrEmpty(newName) && member.Name != newName)
						{
							if (!countedThisType)
							{
								countedThisType = true;
								_NumTypesChanged++;
							}
							_NumMembersChanged++;
							GetChanges(result, member, newName);
						}
					}
				}
				return result;
			}
			finally
			{
				Cursor.Current = saveCursor;
			}
		}
		private static void SetStatusBarText(string msg)
		{
			CodeRush.ApplicationObject.StatusBar.Text = msg;
		}
		private void ShowResults()
		{
			string referenceCountMsg;
			if (_NumReferencesChanged > 1)
				referenceCountMsg = String.Format("(with {0:0,0.} references)", _NumReferencesChanged);
			else
				referenceCountMsg = "(with 1 reference)";

			string msg;

			if (_NumMembersChanged > 0 && _NumTypesChanged == 1)
				msg = String.Format("Renamed {0:0,0.} members {1} in 1 type.", _NumMembersChanged, referenceCountMsg);
			else if (_NumMembersChanged == 1 && _NumTypesChanged == 1)
				msg = String.Format("Renamed 1 member {0} in 1 type.", referenceCountMsg);
			else
				msg = String.Format("Renamed {0:0,0.} members {1} in {2:0,0.} types.", _NumMembersChanged, referenceCountMsg, _NumTypesChanged);

			SetStatusBarText(msg);
		}
		private void actEnforceNamingConvention_Execute(ExecuteEventArgs ea)
		{
			FileChangeCollection changes = GetChanges();
			if (changes.Count == 0)
			{
				SetStatusBarText("All members correctly named.");
				return;
			}

			CodeRush.File.ApplyChanges(changes, "Enforce Naming Conventions", true);
			ShowResults();
		}
	}
}