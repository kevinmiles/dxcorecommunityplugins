using DevExpress.CodeRush.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_CodeBlockContentProviders
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

        private void cpStartOfBlock_ContextSatisfied(ContextSatisfiedEventArgs ea)
        {
            ea.Satisfied = false;
            var activeElement = CodeRush.Source.Active as IHasBlock;            
            if(activeElement == null)
                return;
            if (!((LanguageElement)activeElement).IsCollapsible)
                return;

            ea.Satisfied = activeElement.BlockStart.Contains(CodeRush.Caret.SourcePoint) 
                        || ((LanguageElement)activeElement).Range.Start == CodeRush.Caret.SourcePoint;
        }

        private void cpEndOfBlock_ContextSatisfied(ContextSatisfiedEventArgs ea)
        {
            ea.Satisfied = false;
            var activeElement = CodeRush.Source.Active as IHasBlock;
            if (activeElement == null)
                return;
            if (!((LanguageElement)activeElement).IsCollapsible)
                return;

            ea.Satisfied = activeElement.BlockEnd.Contains(CodeRush.Caret.SourcePoint);
        }
    }
}