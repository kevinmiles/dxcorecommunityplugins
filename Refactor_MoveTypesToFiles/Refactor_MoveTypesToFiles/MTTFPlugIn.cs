// <copyright file="MTTFPlugIn.cs" company="VirgoTech Krzysztof Blacha">
// Project: Refactor_MoveTypesToFiles
// File: MTTFPlugIn.cs
// Creation date: 2012-03-25
// Last modified: 2012-03-25
// </copyright>

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace Refactor_MoveTypesToFiles
{
	/// <summary>
	/// TODO Add summary
	/// </summary>
	public partial class MTTFPlugIn : StandardPlugIn
	{
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

		/// <summary>
		/// TODO Add summary
		/// </summary>
		private void moveTypesToFilesRefactoring_Apply(object sender, ApplyContentEventArgs ea)
		{
			SourceFile sourceFile = CodeRush.Source.ActiveSourceFile;

			// Get all types
			var allTypes = sourceFile.AllTypes;

			// Filter to classes
			var allClassesDesc = allTypes.Cast<TypeDeclaration>()
				.Where(td => td.ElementType == LanguageElementType.Class)
				.OrderByDescending(td => td.NameRange.Start);

			// Move to files
			RefactoringProviderBase moveRefactoring = CodeRush.Refactoring.Get("Move Type to File");
			foreach (TypeDeclaration classDecl in allClassesDesc)
			{
				// Skip last class
				if (allClassesDesc.Count() == 1)
					continue;

				// Move to files
				CodeRush.Caret.MoveTo(classDecl.NameRange.Start);
				CodeRush.SmartTags.UpdateContext();
				moveRefactoring.Execute();
				CodeRush.Documents.Activate((Document)sourceFile.Document);
			}

			// Rename the file to match last type
			RefactoringProviderBase renameRefactoring = CodeRush.Refactoring.Get("Rename File to Match Type");
			CodeRush.SmartTags.UpdateContext();
			renameRefactoring.Execute();
		}

		/// <summary>
		/// TODO Add summary
		/// </summary>
		private void moveTypesToFilesRefactoring_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			RefactoringProviderBase moveRefactoring = CodeRush.Refactoring.Get("Move Type to File");
			RefactoringProviderBase renameRefactoring = CodeRush.Refactoring.Get("Rename File to Match Type");
			ea.Available = moveRefactoring != null && renameRefactoring != null && 
				(ea.CodeActive.ElementType == LanguageElementType.Namespace || ea.CodeActive.ElementType == LanguageElementType.Class);
		}

		/// <summary>
		/// TODO Add summary
		/// </summary>
		private void moveTypesToFilesRefactoring_LanguageSupported(LanguageSupportedEventArgs ea)
		{
			RefactoringProviderBase moveRefactoring = CodeRush.Refactoring.Get("Move Type to File");
			RefactoringProviderBase renameRefactoring = CodeRush.Refactoring.Get("Rename File to Match Type");
			ea.Handled = moveRefactoring != null && renameRefactoring != null &&
				moveRefactoring.IsLanguageSupported(ea.LanguageID);
		}
	}
}