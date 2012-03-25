// <copyright file="CPCLPlugIn.cs" company="VirgoTech Krzysztof Blacha">
// Project: CR_CreatePersistentClassList
// File: CPCLPlugIn.cs
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
using System.IO;

namespace CR_CreatePersistentClassList
{
	/// <summary>
	/// TODO Add summary
	/// </summary>
	public partial class CPCLPlugIn : StandardPlugIn
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
		private void createPersistentClassCodeProvider_Apply(object sender, ApplyContentEventArgs ea)
		{
			SourceFile sourceFile = CodeRush.Source.ActiveSourceFile;

			// Get all types
			var allTypes = sourceFile.AllTypes;

			// Filter to classes
			var allPersistentClasses = allTypes.Cast<TypeDeclaration>()
				.Where(td => td.ElementType == LanguageElementType.Class && 
					td.Attributes.Cast<DevExpress.CodeRush.StructuralParser.Attribute>()
						.Where(a => a.Name == "Persistent").Any());

			// Generate path
			string filePath = Path.Combine(Path.GetDirectoryName(sourceFile.FilePath), "PersistentClassHelper.cs");
			using (StreamWriter sourceFileStream = File.CreateText(filePath))
			{
				string firstClassNamespace = allPersistentClasses.First().GetNamespace().FullName;
				sourceFileStream.WriteLine("namespace " + firstClassNamespace);
				sourceFileStream.WriteLine("{");
				sourceFileStream.WriteLine("\tusing System;");
				sourceFileStream.WriteLine();
				sourceFileStream.WriteLine("\tpublic class PersistentClassHelper");
				sourceFileStream.WriteLine("\t{");
				sourceFileStream.WriteLine("\t\tpublic Type[] GetPersistentTypes()");
				sourceFileStream.WriteLine("\t\t{");
				sourceFileStream.WriteLine("\t\t\tType[] persistentTypes = new Type[]");
				sourceFileStream.WriteLine("\t\t\t{");

				// Generate partial classes
				var lastClass = allPersistentClasses.Last();
				foreach (TypeDeclaration type in allPersistentClasses)
				{
					sourceFileStream.WriteLine("\t\t\t\ttypeof(" + type.GetNamespace() + "." + type.Name + ")" +
						((lastClass != type) ? "," : String.Empty));
				}
				sourceFileStream.WriteLine("\t\t\t};");
				sourceFileStream.WriteLine();
				sourceFileStream.WriteLine("\t\t\treturn persistentTypes;");
				sourceFileStream.WriteLine("\t\t} // GetPersistentTypes");
				sourceFileStream.WriteLine("\t} // PersistentClassHelper");
				sourceFileStream.WriteLine("} // " + firstClassNamespace);
			}

			// Add generated file to project
			CodeRush.Solution.AddFileToProject(CodeRush.Source.ActiveProject.Name, filePath);
		}

		/// <summary>
		/// TODO Add summary
		/// </summary>
		private void createPersistentClassCodeProvider_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			ea.Available = ea.CodeActive.ElementType == LanguageElementType.Namespace ||
				ea.CodeActive.ElementType == LanguageElementType.Class;
		}

		/// <summary>
		/// TODO Add summary
		/// </summary>
		private void createPersistentClassCodeProvider_LanguageSupported(LanguageSupportedEventArgs ea)
		{
			// Using string builder so it's only prepared for CSharp
			ea.Handled = ea.LanguageID == "CSharp";
		}
	}
}