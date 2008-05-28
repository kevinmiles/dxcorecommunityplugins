using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Diagnostics;

namespace Refactor_Generalize
{
    public partial class RefactorGeneralizePlugin : StandardPlugIn
    {
        private LanguageElement _activeElement;
        private Class _childClass;
        private Class _parentClass;
        private string _fullBlockText;

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

        private void action_Execute(ExecuteEventArgs ea)
        {

        }

        private void codeProvider_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            _activeElement = ea.Element;
            if (_activeElement == null) return;

            _childClass = CodeRush.Source.ActiveClass;
            if (_childClass == null) return;
            if (_childClass.PrimaryAncestorType == null) return;

            _parentClass = _childClass.PrimaryAncestorType.GetDeclaration() as Class;
            if (_parentClass == null) return;

            if(IsGeneralizableElement(_activeElement))
            {
                ea.Available = true;
            }
            else
            {
                ea.Available = false;
            }
        }

        private void codeProvider_Apply(object sender, ApplyContentEventArgs ea)
        {
            _activeElement.SelectFullBlock();
            _fullBlockText = Environment.NewLine + CodeRush.Selection.Text;
            CodeRush.Selection.Delete();

            CodeRush.Documents.Activate(CodeRush.Documents.Get(_parentClass.FileNode.Name));
            targetPicker.Start(_parentClass.View as TextView,_parentClass.Nodes[0] as LanguageElement);
        }

        private void targetPicker_TargetSelected(object sender, TargetSelectedEventArgs ea)
        {
            //Debug.Assert(String.IsNullOrEmpty(_fullBlockText), "fooey");

            CodeRush.Documents.ActiveTextDocument.ExpandText(
                ea.Location.InsertionPoint,_fullBlockText);
        }


        private bool IsGeneralizableElement(LanguageElement languageElement)
        {
            return languageElement is Method || languageElement is Property || languageElement is DelegateDefinition;
        }



    }
}