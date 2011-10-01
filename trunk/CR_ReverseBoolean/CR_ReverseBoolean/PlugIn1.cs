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
using System.Diagnostics;

namespace CR_ReverseBoolean
{
	public partial class PlugIn1 : StandardPlugIn
	{
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

		private void InvertExpression(FileChangeCollection changes, Expression expressionToInvert)
		{
			if (expressionToInvert == null)
			{
				Debugger.Break();
				return;
			}
			SourceRange replaceRange = expressionToInvert.Range;
			string newCode = String.Empty;
			if (expressionToInvert is PrimitiveExpression)
			{
				PrimitiveExpression primitiveExpression = expressionToInvert.Clone() as PrimitiveExpression;
				primitiveExpression.PrimitiveValue = !(bool)primitiveExpression.PrimitiveValue;
				newCode = CodeRush.Language.GenerateElement(primitiveExpression);
			}
			else
			{
				if (expressionToInvert.Parent is LogicalInversion)
				{
					LogicalInversion parentInversion = (LogicalInversion)expressionToInvert.Parent;
					replaceRange = parentInversion.Range;
					// TODO: Make sure this works for method calls & property references...
					newCode = CodeRush.Language.GenerateElement(expressionToInvert.Clone() as LanguageElement);
				}
				else
				{
					LogicalInversion newLogicalInversion = new LogicalInversion();
					newLogicalInversion.Expression = expressionToInvert.Clone() as Expression;
					newCode = CodeRush.Language.GenerateElement(newLogicalInversion);
				}
			}
			changes.Add(new FileChange(expressionToInvert.FileNode.Name, replaceRange, newCode));
		}
		private static List<LanguageElement> CollectClonedReferences(Assignment assignment, List<LanguageElement> originalReferences, Assignment clonedAssignment)
		{
			List<LanguageElement> clonedReferences = new List<LanguageElement>();
			foreach (LanguageElement reference in originalReferences)
			{
				Stack<Tuple<bool, int>> path = new Stack<Tuple<bool, int>>();
				LanguageElement thisReference = reference;
				// Calculate path to the reference...
				while (thisReference != null && thisReference != assignment && thisReference.Parent != null)
				{
					bool isDetailNode = thisReference.IsDetailNode;
					int index = -1;
					if (isDetailNode)
						index = thisReference.Parent.DetailNodes.IndexOf(thisReference);
					else
						index = thisReference.Parent.Nodes.IndexOf(thisReference);
					path.Push(new Tuple<bool, int>(isDetailNode, index));
					thisReference = thisReference.Parent;
				}
				// Follow the path down in the clone...
				LanguageElement thisElement = clonedAssignment;
				while (path.Count > 0 && thisElement != null)
				{
					Tuple<bool, int> step = path.Pop();
					bool isDetailNode = step.Item1;

					NodeList childNodes;
					if (isDetailNode)
						childNodes = thisElement.DetailNodes;
					else
						childNodes = thisElement.Nodes;

					int index = step.Item2;
					thisElement = childNodes[index] as LanguageElement;
				}
				clonedReferences.Add(thisElement);
			}
			return clonedReferences;
		}
		private void InvertAssignment(FileChangeCollection changes, Assignment assignment, List<LanguageElement> references)
		{
			// TODO: Check for simple assignment boolean toggle (e.g., "a = !a;"). If we have a match, then exit (no changes needed).
			Assignment clonedAssignment = assignment.Clone() as Assignment;
			//clonedAssignment.SetParent(assignment.Parent);		// So GetDeclaration() call will work.

			List<LanguageElement> clonedReferences = CollectClonedReferences(assignment, references, clonedAssignment);

			// At this point, we have a clonedAssignment and a list of clonedReferences.
			// Invert each reference...
			foreach (LanguageElement clonedReference in clonedReferences)
			{
				if (clonedReference.Parent is LogicalInversion)
				{
					LanguageElement logicalInversion = clonedReference.Parent;
					logicalInversion.Parent.ReplaceChild(logicalInversion, clonedReference);
				}
				else
				{
					LogicalInversion newLogicalInversion = new LogicalInversion();
					LanguageElement parent = clonedReference.Parent;

					parent.ReplaceChild(clonedReference, newLogicalInversion);
					newLogicalInversion.Expression = clonedReference as Expression;
				}
			}

			// Invert the expression...
			if (clonedAssignment.Expression is PrimitiveExpression)
			{
				PrimitiveExpression primitiveExpression = clonedAssignment.Expression as PrimitiveExpression;
				primitiveExpression.PrimitiveValue = !(bool)primitiveExpression.PrimitiveValue;
			}
			else if (clonedAssignment.Expression is LogicalInversion)
			{
				LogicalInversion logicalInversion = (LogicalInversion)clonedAssignment.Expression;
				clonedAssignment.Expression = logicalInversion.FirstDetail as Expression;
			}
			else
			{
				LogicalInversion newLogicalInversion = new LogicalInversion();
				newLogicalInversion.Expression = clonedAssignment.Expression;
				clonedAssignment.Expression = newLogicalInversion;
			}
			string newCode = CodeRush.Language.GenerateElement(clonedAssignment);
			newCode = newCode.TrimEnd('\n', '\r');
			changes.Add(new FileChange(assignment.FileNode.Name, assignment.Range, newCode));
		}
		private static void GetAssignmentsAndStandAloneReferences(LanguageElement scope, LanguageElement declaration, out Dictionary<Assignment, List<LanguageElement>> assignments, out List<LanguageElement> standAloneReferences)
		{
			assignments = new Dictionary<Assignment, List<LanguageElement>>();
			standAloneReferences = new List<LanguageElement>();
			
			ReferenceSearcher searcher = new ReferenceSearcher();
			LanguageElementCollection allReferences = searcher.FindReferences(scope, declaration);

			if (allReferences == null || allReferences.Count == 0)
				return;

			foreach (LanguageElement reference in allReferences)
			{
				if (reference.Parent is Assignment)
				{
					Assignment assignment = (Assignment)reference.Parent;
					if (assignment.LeftSide == reference)
					{
						assignments.Add(assignment, new List<LanguageElement>());
						continue;
					}
				}

				Assignment parentAssignment = reference.GetParent(LanguageElementType.Assignment) as Assignment;
				if (parentAssignment != null && assignments.ContainsKey(parentAssignment))
					assignments[parentAssignment].Add(reference);
				else
					standAloneReferences.Add(reference);
			}
		}
		private FileChangeCollection GetChanges(LanguageElement declaration, Dictionary<Assignment, List<LanguageElement>> assignments, List<LanguageElement> standAloneReferences)
		{
			FileChangeCollection changes = new FileChangeCollection();
			if (declaration is InitializedVariable)
			{
				InitializedVariable initializedVariable = (InitializedVariable)declaration;
				InvertExpression(changes, initializedVariable.Expression);
			}

			foreach (KeyValuePair<Assignment, List<LanguageElement>> assignment in assignments)
				InvertAssignment(changes, assignment.Key, assignment.Value);

			foreach (LanguageElement standAloneReference in standAloneReferences)
				InvertExpression(changes, standAloneReference as Expression);
			return changes;
		}
		private void rpReverseBoolean_Apply(object sender, ApplyContentEventArgs ea)
		{
			LanguageElement element = ea.Element;
			if (element == null)
				return;

			LanguageElement declaration = element.GetDeclaration(true) as LanguageElement;

			Dictionary<Assignment, List<LanguageElement>> assignments;
			List<LanguageElement> standAloneReferences;
			GetAssignmentsAndStandAloneReferences(CodeRush.Source.ActiveSolution, declaration, out assignments, out standAloneReferences);

			DevExpress.DXCore.TextBuffers.ICompoundAction newMultiFileCompoundAction = CodeRush.TextBuffers.NewMultiFileCompoundAction("Reverse Boolean");
			try
			{
				FileChangeCollection changes = GetChanges(declaration, assignments, standAloneReferences);
				if (CodeRush.Caret.SourcePoint.Line != declaration.Range.Start.Line)
					CodeRush.Markers.Drop();
				CodeRush.File.ApplyChanges(changes);
			}
			finally
			{
				if (newMultiFileCompoundAction != null)
					newMultiFileCompoundAction.Close();
			}

			CodeRush.Source.ParseIfTextChanged();

			// Now, let's move the caret to the declaration and rename...
			IElement declarationAtCaret = CodeRush.Source.GetDeclarationAtCaret(declaration.Name);
			if (declarationAtCaret != null)
			{
				// TODO: AlexS: Figure out why sometimes GetDeclarationAtCaret returns null (e.g., works for local variables but not for properties).
				CodeRush.Caret.MoveTo(declarationAtCaret.FirstNameRange.Start);
				RefactoringProviderBase renameRefactoring = CodeRush.Refactoring.Get("Rename");
				CodeRush.SmartTags.UpdateContext();
				if (renameRefactoring.IsAvailable)
				{
					try
					{
						renameRefactoring.IsNestedProvider = true;
						renameRefactoring.Execute();
					}
					finally
					{
						renameRefactoring.IsNestedProvider = false;
					}
				}
			}
		}

		private void rpReverseBoolean_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			// TODO: AlexS -- either limit availability so it isn't available with methods, OR make it available with methods.
			ea.Available = ea.ElementTypeIs("System.Boolean");
		}

		private void rpReverseBoolean_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
		{
			LanguageElement element = ea.Element;
			if (element == null)
				return;

			LanguageElement declaration = element.GetDeclaration(true) as LanguageElement;

			Dictionary<Assignment, List<LanguageElement>> assignments;
			List<LanguageElement> standAloneReferences;
			GetAssignmentsAndStandAloneReferences(element.FileNode, declaration, out assignments, out standAloneReferences);
			FileChangeCollection changes = GetChanges(declaration, assignments, standAloneReferences);
			int topLine = ea.TextView.TopLine;
			int bottomLine = ea.TextView.BottomLine;
			SourceRange viewRange = new SourceRange(topLine - 1, 0, bottomLine + 1, 0);
			foreach (FileChange change in changes)
			{
				if (!viewRange.Contains(change.Range))
					continue;
				ea.AddStrikethrough(change.Range);
				ea.AddCodePreview(change.Range.Start, change.Text);
			}
		}
	}
	public static class Extensions
	{
		/// <summary>
		/// Replaces a specified child node with a given new node, in either the Nodes or DetailNodes lists.
		/// </summary>
		/// <param name="originalChild">The original child node to replace.</param>
		/// <param name="newChild">The new child node to insert.</param>
		public static void ReplaceChild(this LanguageElement parent, LanguageElement originalChild, LanguageElement newChild)
		{
			if (originalChild == null || parent == null)
				return;
			if (originalChild.IsDetailNode)
				parent.ReplaceDetailNode(originalChild, newChild);
			else
				parent.ReplaceNode(originalChild, newChild);
		}
		public static bool ElementTypeIs(this CheckContentAvailabilityEventArgs ea, string fullTypeName)
		{
			LanguageElement element = ea.Element;
			if (element == null)
				return false;

			IHasType declaration = element as IHasType;
			if (declaration == null)
			{
				declaration = element.GetDeclaration() as IHasType;
				if (declaration == null)
					return false;
			}
			ITypeReferenceExpression type = declaration.Type;
			if (type == null)
				return false;

			return type.Is(fullTypeName);
		}
	}
}