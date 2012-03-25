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

namespace CR_CreatePartialClasses
{
	/// <summary>
	/// TODO Add summary
	/// </summary>
	public partial class CPCPlugIn : StandardPlugIn
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
		private void createPartialClassesCodeProvider_Apply(object sender, ApplyContentEventArgs ea)
		{
			SourceFile sourceFile = CodeRush.Source.ActiveSourceFile;

			// Get all types
			var allTypes = sourceFile.AllTypes;

			// Filter to classes
			var allPartialClasses = allTypes.Cast<TypeDeclaration>()
				.Where(td => td.ElementType == LanguageElementType.Class && td.IsPartial);

			// Generate path
			string filePath = Path.Combine(Path.GetDirectoryName(sourceFile.FilePath), "PartialClasses.cs");
			using (StreamWriter sourceFileStream = File.CreateText(filePath))
			{
				string namespaceName = allPartialClasses.First().GetNamespace().FullName;
				sourceFileStream.WriteLine("namespace " + namespaceName);
				sourceFileStream.WriteLine("{");
				sourceFileStream.WriteLine("\tusing System;");
				sourceFileStream.WriteLine();

				// Generate partial classes
				var lastType = allPartialClasses.Last();
				foreach (TypeDeclaration type in allPartialClasses)
				{
					string className = type.Name;

					// TODO: Change string builder into code generator
					sourceFileStream.WriteLine("\t/// <summary>");
					sourceFileStream.WriteLine("\t/// Your comment here");
					sourceFileStream.WriteLine("\t/// </summary>");
					sourceFileStream.WriteLine("\tpublic partial class " + className);
					sourceFileStream.WriteLine("\t{");

					var keySearch = type.AllMembers.Cast<Member>()
						.Where(m => (m.ElementType == LanguageElementType.Property || m.ElementType == LanguageElementType.Variable) &&
							m.Attributes.Cast<DevExpress.CodeRush.StructuralParser.Attribute>().Where(a => a.Name == "Key").Any());
					string keyName = (keySearch.Any()) ? keySearch.First().Name : String.Empty;
					if (!string.IsNullOrEmpty(keyName))
					{
						Member keyMember = keySearch.First();
						sourceFileStream.WriteLine();
						sourceFileStream.WriteLine("\t\t/// <summary>");
						sourceFileStream.WriteLine("\t\t/// Your comment here");
						sourceFileStream.WriteLine("\t\t/// </summary>");
						sourceFileStream.WriteLine("\t\tpublic static " + className + " GetByKey(" + keyMember.MemberType + " key, Session session) {");
						sourceFileStream.WriteLine("\t\t\t" + "return session.FindObject<" + className + ">(" + className + ".Fields." + keyName + ");");
						sourceFileStream.WriteLine("\t\t}");
					}

					sourceFileStream.WriteLine("\t} // " + className);
					if (type != lastType)
						sourceFileStream.WriteLine();
				}
				sourceFileStream.WriteLine("} // " + namespaceName);
			}

			// Add file to solution
			CodeRush.Solution.AddFileToProject(CodeRush.Source.ActiveProject.Name, filePath);
		}

		/// <summary>
		/// TODO Add summary
		/// </summary>
		private void createPartialClassesCodeProvider_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			ea.Available = ea.CodeActive.ElementType == LanguageElementType.Namespace ||
				ea.CodeActive.ElementType == LanguageElementType.Class;
		}

		/// <summary>
		/// TODO Add summary
		/// </summary>
		private void createPartialClassesCodeProvider_LanguageSupported(LanguageSupportedEventArgs ea)
		{
			// Using string builder so it's only prepared for CSharp
			ea.Handled = ea.LanguageID == "CSharp";
		}
	}
}