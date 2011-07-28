using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_OptionsInverter
{
	public partial class PlugIn1 : StandardPlugIn
	{
		private bool _ShowPreview = false;
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

		private void actReadWriteInverter_Execute(ExecuteEventArgs ea)
		{
			TextDocument activeTextDocument = CodeRush.Documents.ActiveTextDocument;
			if (activeTextDocument == null)
				return;

			string generatedCode = String.Empty;
			bool madeChanges = false;
			LanguageElement[] selectedNodes = CodeRush.Source.GetSelectedNodes();
			foreach (LanguageElement element in selectedNodes)
			{
				Assignment assignment = element as Assignment;
				if (assignment != null)
				{
					Expression leftSide = assignment.LeftSide;
					Assignment clonedAssign = assignment.Clone(ElementCloneOptions.Default) as Assignment;
					Expression rightSide = clonedAssign.Expression;

					if (rightSide.Name.StartsWith("Read"))
					{
						if (rightSide.NodeCount > 0)
						{
							// Each LanguageElement has three important properties: Name, Nodes, and DetailNodes.
							MethodReferenceExpression methodReferenceExpression = rightSide.Nodes[0] as MethodReferenceExpression;
							if (methodReferenceExpression != null)
							{
								methodReferenceExpression.Name = "Write" + rightSide.Name.Substring(4);
								rightSide.Name = methodReferenceExpression.Name;
							}

						}

						MethodCallExpression methodCallExpression = rightSide as MethodCallExpression;
						if (methodCallExpression.Arguments != null)
							while (methodCallExpression.Arguments.Count > 2)
							{
								methodCallExpression.Arguments.RemoveAt(methodCallExpression.Arguments.Count - 1);
								
							}
						methodCallExpression.Arguments.Add(leftSide);
						string newCode = CodeRush.Language.GenerateElement(methodCallExpression);
						string statementTerminator = CodeRush.Language.StatementTerminator;
						if (!String.IsNullOrEmpty(statementTerminator) && !newCode.EndsWith(statementTerminator))
							newCode += statementTerminator;
						generatedCode += newCode + Environment.NewLine;
						activeTextDocument.QueueReplace(assignment, newCode);
						madeChanges = true;
					}
				}
			}
			if (madeChanges)
			{
				if (_ShowPreview)
					using (FrmCodePreview frmCodePreview = new FrmCodePreview(generatedCode, CodeRush.Language.Active))
						frmCodePreview.ShowDialog();
				activeTextDocument.ApplyQueuedEdits("Convert CR Option Read to Write calls.");
				TextView activeView = activeTextDocument.ActiveView;
				if (activeView != null)
				{
					TextViewSelection selection = activeView.Selection;
					if (selection != null)
						selection.Clear();
				}
			}
		}

		private static void GenerateMethod()
		{
			Method method = new Method();
			method.Name = "MyNewProc";
			method.MethodType = MethodTypeEnum.Void;
			Param newParam = new Param();
			TypeReferenceExpression newTypeReferenceExpression = new TypeReferenceExpression();
			newTypeReferenceExpression.Name = CodeRush.Language.GetSimpleTypeName("System.Int32");
			newParam.MemberTypeReference = newTypeReferenceExpression;
			newParam.Name = "MyKillerParameter";

			method.Parameters.Add(newParam);

			MethodCall statement = new MethodCall();
			statement.Name = "Start";
			//UnaryIncrement newUnaryIncrement = new UnaryIncrement();
			//ElementReferenceExpression elementReferenceExpression = new ElementReferenceExpression(newParam.Name);
			//newUnaryIncrement.Expression = elementReferenceExpression;
			//statement.AddDetailNode(newUnaryIncrement);
			//int MyKillerParameter = 0;
			//MyKillerParameter++;

			method.AddNode(statement);
			string newCode = CodeRush.Language.GenerateElement(method);
			TextDocument activeTextDocument = CodeRush.Documents.ActiveTextDocument;
			if (activeTextDocument == null)
				return;

			activeTextDocument.InsertText(activeTextDocument.ActiveView.Caret.SourcePoint, newCode);
		}

		private void actGenCodeTest_Execute(ExecuteEventArgs ea)
		{
			GenerateMethod();
		}
	}
}