using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_StackOverflowIssues
{
    public partial class StackOverflowIssuesPlugin : StandardPlugIn
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
        
        private static bool ExpressionReferencesProperty(ElementReferenceExpression expression, Property property)
        {
            return expression != null
                && expression.Name == property.Name
                && (expression.LastChild == null
                    || (!property.IsStatic && expression.LastChild is ThisReferenceExpression)
                    || (property.IsStatic && (expression.LastChild.Name == property.GetParentClassInterfaceOrStruct().Name)));
            // TODO: check aliases to parent class, check class name qualified with namespace or aliased namespace
        }

        private void StackOverflowInGetterIssueProvider_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
        {
            if (ea.IsSuppressed(ea.Scope))
                return;

            foreach (IElement element in ea.GetEnumerable(ea.Scope, ElementFilters.Property))
            {
                var propertyElement = element as IPropertyElement;
                if (propertyElement == null)
                    continue;

                Property property = (Property)propertyElement.ToLanguageElement();
                if (property.Getter != null)
                {
                    foreach (LanguageElement getterElement in property.Getter.Nodes)
                    {
                        var ret = getterElement as Return;
                        if (ret != null && ExpressionReferencesProperty(ret.Expression as ElementReferenceExpression, property))
                        {
                            ea.AddError(ret.Range, "StackOverflowException in runtime");
                        }
                    }
                }
                if (property.Setter != null)
                {
                    foreach (LanguageElement setterElement in property.Setter.Nodes)
                    {
                        var assignment = setterElement as Assignment;
                        if (assignment != null && ExpressionReferencesProperty(assignment.LeftSide as ElementReferenceExpression, property))
                        {
                            ea.AddError(assignment.Range, "StackOverflowException in runtime");
                        }
                    }
                }
            }
        }
    }
}