using System;
using System.ComponentModel;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.DXCore.Constants;

namespace Refactor_NamedParameters
{
	public partial class NamedParametersPlugIn : StandardPlugIn
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
      
      IWithParameters declaration = GetValidDeclaration(originalCall);

      if (declaration == null ||
        !(declaration.Parameters.Count == originalArguments.Count || 
        (declaration.Parameters.Count == originalArguments.Count + 1 && HasParamArray(declaration))
        || (declaration.Parameters.Count == originalArguments.Count + 1 && IsExtensionMethod(declaration))
        ))
				return null;
      int startParamIndex = 0;
      if (IsExtensionMethod(declaration))
        startParamIndex = 1;

			for (int i = 0; i < originalArguments.Count; i++)
			{
        Expression originalArgument = originalArguments[i];
        AttributeVariableInitializer namedArgument = originalArgument as AttributeVariableInitializer;
        if (namedArgument == null)
        {
          ElementReferenceExpression leftSide = new ElementReferenceExpression(declaration.Parameters[startParamIndex + i].Name);
          namedArgument = new AttributeVariableInitializer();
          namedArgument.LeftSide = leftSide;
          namedArgument.RightSide = originalArgument.Clone() as Expression;
        }
        (replacementCall as IHasArguments).Arguments.Add(namedArgument);
			}
			return CodeRush.CodeMod.GenerateCode(replacementCall, true);
		}

    private static bool IsExtensionMethod(IWithParameters declaration)
    {
      if (declaration == null || declaration.Parameters == null || declaration.Parameters.Count == 0)
        return false;

      IMethodElement methodElement = declaration as IMethodElement;
      if (methodElement == null)
        return false;

         
      return methodElement.IsExtensionMethod();
    }

    private static bool HasParamArray(IWithParameters declaration)
    {
      if (declaration == null)
        return false;

      IParameterElementCollection parameters = declaration.Parameters;
      if (parameters == null || parameters.Count == 0)
        return false;

      return parameters[parameters.Count - 1].IsParamArray;
    }

    private static IWithParameters GetValidDeclaration(LanguageElement originalCall)
    {
      if (originalCall == null)
        return null;

      IElement declaration = originalCall.GetDeclaration(false);
      if (declaration is IMethodElement)
        return declaration as IWithParameters;
      IEventElement eventElement = declaration as IEventElement;
      if (eventElement != null && eventElement.Type != null)
      {
        return eventElement.Type.Resolve(ParserServices.SourceTreeResolver) as IWithParameters;
      }
      return null;
    }
		private void rpUseNamedParameters_Apply(object sender, ApplyContentEventArgs ea)
		{
			LanguageElement originalCall = GetMethodCall(ea.Element);
			string generatedCode = GetGeneratedCode(originalCall);
			if (String.IsNullOrEmpty(generatedCode))
				return;

			ea.TextDocument.SetText(originalCall.Range, generatedCode);
		}

    private LanguageElement GetMethodCall(LanguageElement element)
    {
      if (element is MethodReferenceExpression)
      {
        LanguageElement elementParent = element.Parent;
        if (elementParent is MethodCall || elementParent is MethodCallExpression)
          return elementParent;
      }
      if (element is ObjectCreationExpression)
        return element;
      if (element is TypeReferenceExpression && element.Parent is ObjectCreationExpression)
        return element.Parent;

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

      IWithParameters declaration = GetValidDeclaration(methodCall);
      if (declaration == null)
        return;

      ExpressionCollection arguments = hasArguments.Arguments;
      if (arguments == null || arguments.Count <= 0)
        return;

      bool hasAttributeVarInitializer = false;
      foreach (Expression arg in arguments)
      {
        if (arg is AttributeVariableInitializer)
        {
          hasAttributeVarInitializer = true;
          break;
        }
        if (arg is AnonymousMethodExpression)
          return;
      }

      if (!hasAttributeVarInitializer)
        ea.Available = true;
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

    private void rpUseNamedParameters_LanguageSupported(LanguageSupportedEventArgs ea)
    {
      // TODO: add CodeRush.Language.SupportsNamedArguments call.
      ea.Handled = ea.LanguageID == Str.Language.CSharp || ea.LanguageID == Str.Language.VisualBasic;
    }
	}
}