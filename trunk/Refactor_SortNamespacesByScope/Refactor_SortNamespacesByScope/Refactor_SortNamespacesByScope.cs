using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace Refactor_SortNamespacesByScope
{
	public partial class Refactor_SortNamespacesByScopePlugIn : StandardPlugIn
	{
		// DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();

			refactoringProvider.Apply += refactoringProvider_Apply;
			refactoringProvider.CheckAvailability += refactoringProvider_CheckAvailability;
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

		void refactoringProvider_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			var el = ea.Element;
			var isAvalable = false;

			while (!isAvalable && !(el is SourceFile))
			{
				isAvalable = el is NamespaceReference;
				el = el.Parent;
			}

			ea.Available = isAvalable;
		}

		void refactoringProvider_Apply(object sender, ApplyContentEventArgs ea)
		{
			var doc = CodeRush.Documents.ActiveTextDocument;
			var oldRefs = doc.GetNamespaceReferences();
			var oldRange = new SourceRange(oldRefs.First().Range.Start, 
										   oldRefs.Last().Range.End);

			var newRefs = SortReferencesByScope(oldRefs);
			var newCode = newRefs.OfType<LanguageElement>().GenerateCode();
			doc.Replace(oldRange, newCode, "Sorted Namespace References by Scope");
		}

		private IEnumerable<NamespaceReference> SortReferencesByScope(IEnumerable<NamespaceReference> src)
		{
			// parse the usings into logical groups according to some basic heuristics
			var systemRefs = src.Where(n => n.Name.Equals("System") || n.Name.StartsWith("System.")).OrderBy(n => n.Name);
			var msRefs = src.Where(n => n.Name.StartsWith("Microsoft.")).OrderBy(n => n.Name);
			var slnRefs = src.WhereReferencesAreInternalToTheSolution().OrderBy(n => n.Name);
			var libRefs = src.Except(systemRefs).Except(msRefs).Except(slnRefs).OrderBy(n => n.Name);

			// combine the groups in order of logical "scope"
			var result = systemRefs.ToList();
			result.AddRange(msRefs);
			result.AddRange(libRefs);
			result.AddRange(slnRefs);

			return result;
		}
	}

	public static class TextDocumentExtensions
	{
		public static IEnumerable<NamespaceReference> GetNamespaceReferences(this TextDocument sourceDocument)
		{
			return new ElementEnumerable(sourceDocument.FileNode,
										 typeof(NamespaceReference),
										 false).Cast<NamespaceReference>();
		}
	}

	public static class NamespaceReferenceExtensions
	{
		public static bool IsPartOfTheActiveSolution(this NamespaceReference nref)
		{
			var c = nref.Solution.AllProjects.Cast<ProjectElement>().Where(p => nref.Name.StartsWith(p.Name)).Count();
			return c > 0;
		}

		public static IEnumerable<NamespaceReference> WhereReferencesAreInternalToTheSolution(this IEnumerable<NamespaceReference> src)
		{
			var list = CodeRush.Project.Cast<Project>().ToList();
			return from n in src
				   where list.Where(p => n.Name.StartsWith(p.Name)).Count() > 0
				   select n;
		}
	}

	public static class LanguageElementExtensions
	{
		public static string GenerateCode(this IEnumerable<LanguageElement> source)
		{
			var result = new List<string>();
			foreach (var item in source)
			{
				result.Add(item.GenerateCode());
			}
			return string.Join(Environment.NewLine, result.ToArray());
		}

		public static string GenerateCode(this LanguageElement source)
		{
			return CodeRush.CodeMod.GenerateCode(source);
		}
	}
}