using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace HighlightNonDisposedLocals {
    public partial class PlugIn1 : StandardPlugIn {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn() {
            base.InitializePlugIn();

            //
            // TODO: Add your initialization code here.
            //
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn() {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion

        private void PlugIn1_EditorPaintLanguageElement(EditorPaintLanguageElementEventArgs ea) {
            LanguageElement element = ea.LanguageElement;
            if (!LocalIsDisposable(element))
                return;

            // Now we have a local that implements IDisposable....
            ea.PaintArgs.OverlayText(element.Name, element.StartLine, element.StartOffset, Color.Red);
        }

        private static bool LocalIsDisposable(LanguageElement element) {
            string typeName = null;
            if (element is Variable)
                typeName = element.GetTypeName();
            else {
                LanguageElement localDeclaration = CodeRush.Refactoring.FindLocalDeclaration(element);
                if (localDeclaration == null)
                    return false;
                typeName = localDeclaration.GetTypeName();
            }

            return CodeRush.Source.Implements(typeName, "System.IDisposable");
        }


    }
}