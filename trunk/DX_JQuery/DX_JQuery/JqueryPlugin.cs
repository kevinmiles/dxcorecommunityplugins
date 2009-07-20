using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace DX_JQuery
{
    public partial class JqueryPlugin : StandardPlugIn
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

        private void cpConvertToJQuery_LanguageSupported(LanguageSupportedEventArgs ea)
        {
            ea.Handled = ea.LanguageID == "JavaScript";
        }

        private void cpConvertToJQuery_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            ea.Available = IsAvailable(ea.Element);
        }
        internal bool IsAvailable(LanguageElement element)
        {
            return (element.ElementType == LanguageElementType.MethodCallExpression
                   && element.Name == "getElementById")
                   || (element.ElementType == LanguageElementType.ElementReferenceExpression
                   && (element.Name == "value"
                   ||  element.Name == "checked")
                   && element.NextNode.ElementType == LanguageElementType.MethodCallExpression
                   && element.NextNode.Name == "getElementById");
                    
        }

        private void cpConvertToJQuery_Apply(object sender, ApplyContentEventArgs ea)
        {
            if (!IsAvailable(ea.Element))
                return;
            if (ea.Element is MethodCallExpression)
                ReplaceGetElementById(ea);
            else if (ea.Element is ElementReferenceExpression)
                ReplaceGetElementProperty(ea);
        }

        private void ReplaceGetElementById(ApplyContentEventArgs ea)
        {
            MethodCallExpression ex = ea.Element as MethodCallExpression;
            string source = GetSource(ex);
            ea.TextDocument.Replace(ex.Range, source, "Convert to jQuery", true);
        }
        internal void ReplaceGetElementProperty(ApplyContentEventArgs ea)
        {
            ElementReferenceExpression ex = ea.Element as ElementReferenceExpression;
            var elementName = ex.Name;
            string source = CodeRush.Language.GenerateElement(ex);
            
            if(elementName == "value")
                source = GetSource(ex.FirstNode as MethodCallExpression).Replace("get(0)","val()");
            if (elementName == "checked")
                source = GetSource(ex.FirstNode as MethodCallExpression).Replace("get(0)", "is(\":checked\")");
            ea.TextDocument.Replace(ex.Range, source, "Convert to jQuery");
            
        }
        private void cpConvertToJQuery_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
        {
            if (!IsAvailable(ea.Element))
                return;
            ea.AddPreviewRange(ea.Element.Range);
            ea.AddStrikethrough(ea.Element.Range);
            ea.AddCodePreview(ea.Element.Range.Start, GetSource(ea.Element as MethodCallExpression));
        }

        private string GetSource(LanguageElement ex)
        {
            MethodCallExpression expression;
            if (ex is MethodCallExpression)
                expression = ex as MethodCallExpression;
            else
                expression = ((ElementReferenceExpression)ex).FirstNode as MethodCallExpression;

            PrimitiveExpression argumentNode = expression.DetailNodes[0] as PrimitiveExpression;
            string source = String.Format("$(\"#{0}\").get(0)", argumentNode.PrimitiveValue);
            return source;
        }
        private void cpConvertToJQuery_HidePreview(object sender, HideContentPreviewEventArgs ea)
        {
        }
    }
}