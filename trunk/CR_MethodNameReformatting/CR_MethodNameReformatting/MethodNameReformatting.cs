using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_MethodNameReformatting
{
    public partial class MethodNameReformatting : StandardPlugIn
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

        private void MethodNameReformatting_EditorCharacterTyped(EditorCharacterTypedEventArgs ea)
        {

        }

        private void MethodNameReformatting_EditorCharacterTyping(EditorCharacterTypingEventArgs ea)
        {
            if (ea.Character == ' ' && !ea.IsAltKeyDown && !ea.IsCtrlKeyDown && !ea.IsShiftKeyDown && IAmInAMethodDeclaration())
            {
                ea.Cancel = true;
                ea.TextView.TextDocument.InsertText(CodeRush.Caret.SourcePoint, "_");
            }
        }
        private bool IAmInAMethodDeclaration()
        {
            if (CodeRush.Caret.OnMethodName)
                return true;

            if (CodeRush.Caret.OnMethod)
                return false;
            var statement = CodeRush.Caret.LeftText.Trim() + "()";
            var element = CodeRush.Language.GetActiveExpressionParser().Parser.ParseString(statement);
            return element.LastChild != null && element.LastChild is Method && !string.IsNullOrEmpty(element.LastChild.Name) && element.LastChild.FirstDetail is TypeReferenceExpression;
        }
    }
}