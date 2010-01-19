using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_ClosureScopeIssues
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

Do not build and publish this plugin. It is not production ready

        public class LoopFilter : ElementFilterBase
        {
            public override bool Apply(IElement element)
            {
                if (element == null)
                    return false;
                return element.ElementType == LanguageElementType.For 
                    || element.ElementType == LanguageElementType.ForEach;
            }
        }

        public class LambdaFilter : ElementFilterBase
        {
            public override bool Apply(IElement element)
            {
                if (element == null)
                    return false;
                return element.ElementType == LanguageElementType.LambdaExpression;
            }
        }

        private void CheckIssuesForReferencedLocalOrField(CheckCodeIssuesEventArgs ea, IForEachStatement loop, IAnonymousMethodExpression lambda, IElement element)
        {
            if (element.ElementType == LanguageElementType.ElementReferenceExpression)
            {
                IElement declaration = element.GetDeclaration();
                if (declaration.ElementType == LanguageElementType.Variable || declaration.ElementType == LanguageElementType.InitializedVariable)
                {
                    bool isLoopVariable = loop.LoopVariable == declaration;
                    bool declarationIsParentedByLoop = declaration.IsParentedBy(loop);
                    if (isLoopVariable || !declarationIsParentedByLoop)
                    {
                        ea.AddWarning(element.ToLanguageElement().Range, "Possible unintended closure scope misuse");
                    }
                }
                return;
            }

            foreach (IElement child in element.Children)
            {
                CheckIssuesForReferencedLocalOrField(ea, loop, lambda, child);
            }
        }

        private void CheckIssuesForReferencedLocalOrField(CheckCodeIssuesEventArgs ea, IForStatement loop, IElement lambda, IElement element)
        {
            if (element.ElementType == LanguageElementType.ElementReferenceExpression)
            {
                IElement declaration = element.GetDeclaration();
                if (declaration.ElementType == LanguageElementType.Variable || declaration.ElementType == LanguageElementType.InitializedVariable)
                {
                    if (loop.Initializers.Contains(declaration) || !declaration.IsParentedBy(loop))
                    {
                        ea.AddWarning(element.ToLanguageElement().Range, "Possible unintended closure scope misuse");
                    }
                }
                return;
            }

            foreach (IElement child in element.Children)
            {
                CheckIssuesForReferencedLocalOrField(ea, loop, lambda, child);
            }
        }

        private void closureScopeIssueProvider_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
        {
            if (ea.IsSuppressed(ea.Scope))
                return;

            foreach (IElement loop in ea.GetEnumerable(ea.Scope, new LoopFilter()))
            {
                var forEachLoop = loop as IForEachStatement;
                if (forEachLoop != null)
                {
                    foreach (IElement lambda in ea.GetEnumerable(forEachLoop, new LambdaFilter()))
                    {
                        var lambdaElement = lambda as IAnonymousMethodExpression;
                        if (lambdaElement == null)
                            continue;

                        foreach (IElement element in lambdaElement.Children)
                        {
                            CheckIssuesForReferencedLocalOrField(ea, forEachLoop, lambdaElement, element);
                        }
                    }
                }

                var forLoop = loop as IForStatement;
                if (forLoop != null)
                {
                    foreach (IElement lambda in ea.GetEnumerable(forLoop, new LambdaFilter()))
                    {
                        var lambdaElement = lambda as IAnonymousMethodExpression;
                        if (lambdaElement == null)
                            continue;

                        foreach (IElement element in lambdaElement.Children)
                        {
                            CheckIssuesForReferencedLocalOrField(ea, forLoop, lambdaElement, element);
                        }
                    }
                }
            }
        }
    }
}