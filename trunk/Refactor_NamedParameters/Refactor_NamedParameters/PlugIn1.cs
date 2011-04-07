using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace Refactor_NamedParameters
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

		private static string GetGeneratedCode(MethodCall originalCall)
		{
			if (originalCall == null)
				return null;
			ExpressionCollection originalArguments = originalCall.Arguments;
			if (originalArguments == null)
				return null;
			MethodCall replacementCall = originalCall.Clone(ElementCloneOptions.Default) as MethodCall;
			replacementCall.Arguments.Clear();
			IElement declaration = originalCall.GetDeclaration();
			Method methodDeclaration = declaration as Method;
			
			if (methodDeclaration == null || methodDeclaration.ParameterCount != originalArguments.Count)
				return null;

			for (int i = 0; i < originalArguments.Count; i++)
			{
				ElementReferenceExpression leftSide = new ElementReferenceExpression(methodDeclaration.Parameters[i].Name);
				AttributeVariableInitializer namedArgument = new AttributeVariableInitializer();
				namedArgument.LeftSide = leftSide;
				namedArgument.RightSide = originalArguments[i];
				replacementCall.Arguments.Add(namedArgument);
			}
			string generatedCode = CodeRush.Language.GenerateElement(replacementCall);
			generatedCode = generatedCode.TrimEnd('\n', '\r');
			return generatedCode;
		}
		private void rpUseNamedParameters_Apply(object sender, ApplyContentEventArgs ea)
		{
			MethodCall originalCall = GetMethodCall(ea.Element);
			string generatedCode = GetGeneratedCode(originalCall);
			if (String.IsNullOrEmpty(generatedCode))
				return;

			ea.TextDocument.SetText(originalCall.Range, generatedCode);
			//originalCall.ReplaceWith(leadingWhiteSpace + generatedCode, "Use Named Parameters");
		}

		private static MethodCall GetMethodCall(LanguageElement element)
		{
			if (element is MethodReferenceExpression && element.Parent is MethodCall)
				return (MethodCall)element.Parent;
			return null;
		}
		private void rpUseNamedParameters_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			MethodCall methodCall = GetMethodCall(ea.Element);
			if (methodCall == null)
				return;
			if (methodCall.Arguments != null && methodCall.Arguments.Count > 0)
				ea.Available = !(methodCall.Arguments[0] is AttributeVariableInitializer);
		}

		private void rpUseNamedParameters_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
		{
			MethodCall originalCall = GetMethodCall(ea.Element);
			string generatedCode = GetGeneratedCode(originalCall);
			if (String.IsNullOrEmpty(generatedCode))
				return;
			ea.AddCodePreview(originalCall.Range.Start, generatedCode);
		}
	}
}