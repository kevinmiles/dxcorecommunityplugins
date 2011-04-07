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

    private static string GetGeneratedCode(LanguageElement originalCall)
		{
      if (!(originalCall is IHasArguments))
				return null;
      ExpressionCollection originalArguments = (originalCall as IHasArguments).Arguments;
			if (originalArguments == null)
				return null;
      LanguageElement replacementCall = originalCall.Clone() as LanguageElement;
      (replacementCall as IHasArguments).Arguments.Clear();
      IMethodElement methodDeclaration = originalCall.GetDeclaration(false) as IMethodElement;
			
			if (methodDeclaration == null || methodDeclaration.Parameters.Count != originalArguments.Count)
				return null;

			for (int i = 0; i < originalArguments.Count; i++)
			{
				ElementReferenceExpression leftSide = new ElementReferenceExpression(methodDeclaration.Parameters[i].Name);
				AttributeVariableInitializer namedArgument = new AttributeVariableInitializer();
				namedArgument.LeftSide = leftSide;
				namedArgument.RightSide = originalArguments[i];
        (replacementCall as IHasArguments).Arguments.Add(namedArgument);
			}
			return CodeRush.CodeMod.GenerateCode(replacementCall, true);
		}
		private void rpUseNamedParameters_Apply(object sender, ApplyContentEventArgs ea)
		{
			LanguageElement originalCall = GetMethodCall(ea.Element);
			string generatedCode = GetGeneratedCode(originalCall);
			if (String.IsNullOrEmpty(generatedCode))
				return;

			ea.TextDocument.SetText(originalCall.Range, generatedCode);
			//originalCall.ReplaceWith(leadingWhiteSpace + generatedCode, "Use Named Parameters");
		}

    private LanguageElement GetMethodCall(LanguageElement element)
    {
      if (element is MethodReferenceExpression)
      {
        LanguageElement elementParent = element.Parent;
        if (elementParent is MethodCall || elementParent is MethodCallExpression)
          return elementParent;
      }
      return null;
    }
		private void rpUseNamedParameters_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
      LanguageElement methodCall = GetMethodCall(ea.Element);
			if (methodCall == null)
				return;

      IHasArguments hasArguments = methodCall as IHasArguments;
      if (hasArguments == null)
        return;

      if (methodCall.GetDeclaration(false) == null)
        return;

      ExpressionCollection arguments = hasArguments.Arguments;
      if (arguments != null && arguments.Count > 0)
        ea.Available = !(arguments[0] is AttributeVariableInitializer);
		}

		private void rpUseNamedParameters_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
		{
      LanguageElement originalCall = GetMethodCall(ea.Element);
			string generatedCode = GetGeneratedCode(originalCall);
			if (String.IsNullOrEmpty(generatedCode))
				return;
			ea.AddCodePreview(originalCall.Range.Start, generatedCode);
		}

    private void rpUseNamedParameters_VisualStudioSupported(VisualStudioSupportedEventArgs ea)
    {
      ea.Handled = CodeRush.VSSettings.VersionAtLeast(VisualStudioVersion.VS2010);
    }
	}
}