using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_AssignmentExpressionToEqualityTest
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

		//public bool AreEqual(bool p1, bool p2)
		//{
		//	if (p1 = p2)
		//		return true;
		//	p1 = p2;
		//	return p2 = p1;
		//}

		private void ipAssignmentIntended_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
		{
			// Not inside the UI thread anymore.
			// Don't use CodeRush.Source.Xxxxx, in fact, don't use any CodeRush.Xxxxxx is my recommendation.
			ElementTypeFilter assignmentExpressionFilter = new ElementTypeFilter(LanguageElementType.AssignmentExpression);
			IEnumerable<IElement> enumerable = ea.GetEnumerable(ea.Scope, assignmentExpressionFilter);
			foreach (IElement element in enumerable)
			{
				// For every LanguageElement type, there is a corresponding interface, that starts with an I.
				IAssignmentExpression assignmentExpression = element as IAssignmentExpression;
				if (assignmentExpression == null)
					continue;
				TextPoint start = assignmentExpression.LeftSide.FirstNameRange.Start;
				TextPoint end = assignmentExpression.RightSide.FirstNameRange.End;
				SourceRange range = new SourceRange(start, end);
				ea.AddIssue(CodeIssueType.Warning, range, cpAssignmentExpressionToEqualityCheck.CodeIssueMessage);

				// Other code we were playing with...
				//AssignmentExpression assignmentExpressionReal = LanguageElementRestorer.ConvertToLanguageElement(element) as AssignmentExpression;
				//assignmentExpressionReal.OperatorRange
				//ea.AddIssue(CodeIssueType.Warning, assignmentExpression.Range, cpAssignmentExpressionToEqualityCheck.CodeIssueMessage);
			}
		}

		private static AssignmentExpression GetAssignmentExpression(LanguageElement element)
		{
			// Rory is right. ea.Element will not always be equal to CodeRush.Source.Active.
			// Also, CodeProviders can be called programmatically, targeting other places.
			// Rory recommends always exhausting the ea.Xxxxxxx methods and properties, before looking elsewhere.
			AssignmentExpression assignmentExpression = element as AssignmentExpression;
			if (assignmentExpression == null)
				assignmentExpression = element.GetParent(LanguageElementType.AssignmentExpression) as AssignmentExpression;
			return assignmentExpression;
		}
		private void cpAssignmentExpressionToEqualityCheck_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			AssignmentExpression assignmentExpression = GetAssignmentExpression(ea.Element);
			ea.Available = assignmentExpression != null;
		}

		private static string GetNewEqualityTestCode(AssignmentExpression assignmentExpression)
		{
			RelationalOperation relationalOperation = new RelationalOperation(assignmentExpression.LeftSide.Clone() as Expression,
																												RelationalOperator.Equality,
																												assignmentExpression.RightSide.Clone() as Expression);
			string newEqualityTest = CodeRush.Language.GenerateElement(relationalOperation);
			return newEqualityTest;
		}
		private void cpAssignmentExpressionToEqualityCheck_Apply(object sender, ApplyContentEventArgs ea)
		{
			AssignmentExpression assignmentExpression = GetAssignmentExpression(ea.Element);
			if (assignmentExpression == null)
				return;
			ea.TextDocument.Replace(assignmentExpression.Range, 
															GetNewEqualityTestCode(assignmentExpression), 
															"Convert assignment to equality check.", 
															true);
		}

		private void cpAssignmentExpressionToEqualityCheck_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
		{
			AssignmentExpression assignmentExpression = GetAssignmentExpression(ea.Element);
			if (assignmentExpression == null)
				return;
			string newEqualityTestCode = GetNewEqualityTestCode(assignmentExpression);
			ea.AddCodePreview(assignmentExpression.Range.Start, newEqualityTestCode);
			ea.AddStrikethrough(assignmentExpression.Range);
		}
	}
}