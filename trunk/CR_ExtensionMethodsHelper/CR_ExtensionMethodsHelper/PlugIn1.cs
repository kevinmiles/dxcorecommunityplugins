using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Collections.Generic;
using System.Collections;

namespace CR_ExtensionMethodsHelper
{
  public partial class PlugIn1 : StandardPlugIn
  {
    private string _NamespaceOfExtensionMethod;

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

    
    // private methods...
    private bool IsMethodCallReference(LanguageElement element)
    {
      return element != null && (element.ElementType == LanguageElementType.MethodCall || element.ElementType == LanguageElementType.MethodCallExpression);
    }

    private LanguageElement GetActiveMethodReference(LanguageElement element)
    {
      if (element == null)
        return null;

      if (IsMethodCallReference(element))
        return element;

      if (IsMethodCallReference(element.Parent))
        return element.Parent;

      return null;
    }

    private ICollection GetProjectSymbols()
    {
      ProjectElement scope = CodeRush.Source.ActiveProject;
      if (scope == null)
        return null;

      return scope.ProjectSymbols.Values;
    }

    private bool IsValidReferenceAndQualifier(LanguageElement activeRerence, out ITypeElement callerType)
    {
      callerType = null;
      if (!(activeRerence is IHasQualifier))
        return false;

      // should be undeclared....
      IElement declaration = activeRerence.GetDeclaration(false);
      if (declaration != null)
        return false;

      Expression qualifier = (activeRerence as IHasQualifier).Qualifier;
      if (qualifier is MethodReferenceExpression)
        qualifier = (qualifier as MethodReferenceExpression).Qualifier;
      if (qualifier == null)
        return false;

      callerType = qualifier.Resolve(ParserServices.SourceTreeResolver) as ITypeElement;
      if (callerType == null)
        return false;

      return true;
    }

    private bool ExtendsTheCallerType(IMethodElement method, ITypeElement callerType)
    {
      if (method == null || callerType == null)
        return false;

      if (method.Parameters.Count == 0)
        return false;

      IExtensionMethodParam extensionParam = method.Parameters[0] as IExtensionMethodParam;
      if (extensionParam == null)
        return false;

      ITypeReferenceExpression type = extensionParam.Type;
      if (type == null)
        return false;

      return type.Is(callerType);
    }

    private IMethodElement FindExtensionMethodInClass(IClassElement classElement, string methodName, ITypeElement callerType)
    {
      if (classElement == null || callerType == null)
        return null;

      bool isCaseSensitiveLanguage = CodeRush.Language.IsCaseSensitive;

      IMemberElementCollection classElementMembers = classElement.Members;
      foreach (IElement member in classElementMembers)
      {
        IMethodElement method = member as IMethodElement;
        if (method == null)
          continue;

        if (String.Compare(method.Name, methodName, !isCaseSensitiveLanguage) == 0)
          if (method.IsExtensionMethod())
            if (ExtendsTheCallerType(method, callerType))
              return method;
      }
      return null;
    }
    private IMethodElement FindExtensionMethod(ICollection projectSymbols, string methodName, ITypeElement callerType)
    {
      if (projectSymbols == null || callerType == null)
        return null;

      foreach (IElement element in projectSymbols)
      {
        IClassElement classElement = element as IClassElement;
        if (classElement == null)
          continue;

        if (!classElement.IsStatic)
          continue;

        IMethodElement extensionMethod = FindExtensionMethodInClass(classElement, methodName, callerType);
        if (extensionMethod != null)
          return extensionMethod;
      }
      return null;
    }

    // event handlers...
    private void cpAddReference_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
    {
      LanguageElement activeMethodRerence = GetActiveMethodReference(ea.Element);
      if (activeMethodRerence == null)
        return;

      ITypeElement callerType;
      if (!IsValidReferenceAndQualifier(activeMethodRerence, out callerType))
        return;

      ICollection projectSymbols = GetProjectSymbols();
      if (projectSymbols == null)
        return;

      string methodNameToFind = activeMethodRerence.Name;
      IMethodElement extensionMethod = FindExtensionMethod(projectSymbols, methodNameToFind, callerType);
      if (extensionMethod == null || extensionMethod.ParentNamespace == null)
        return;

      _NamespaceOfExtensionMethod = extensionMethod.ParentNamespace.FullName;

      ea.Available = true;
    }

    private void cpAddReference_Apply(object sender, ApplyContentEventArgs ea)
    {
      CodeRush.Source.DeclareNamespaceReference(_NamespaceOfExtensionMethod);
    }
  }
}