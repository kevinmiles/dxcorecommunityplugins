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
        private TextViewSelection _activeBlock; 
        private Class _childClass;
        private Class _parentClass;
        //private string _fullBlockText;

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
            //make sure that the language element is not null
            _activeElement = ea.Element;
            if (_activeElement == null) return;

            //get the current class that this language element belongs to.
            _childClass = CodeRush.Source.ActiveClass;
            if (_childClass == null) return;
            //make sure that the class also has a parent class.
            if (_childClass.PrimaryAncestorType == null) return;
            _parentClass = _childClass.PrimaryAncestorType.GetDeclaration() as Class;
            if (_parentClass == null) return;

            //make sure that the element selected is also generalizable.
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
            //select the entire block that belongs to the active element.
            _activeElement.SelectFullBlock();
            //_fullBlockText = Environment.NewLine + CodeRush.Selection.Text;
            //CodeRush.Selection.Delete();
            _activeBlock = CodeRush.TextViews.Active.Selection;

            //get the document for the parent class and activate it.
            Document parentDocument = CodeRush.Documents.Get(_parentClass.FileNode.Name);
            CodeRush.Documents.Activate(parentDocument);

            //now select the target location in the parent class' document.
            if(_parentClass.FirstChild == null)
            {
                //this is temporary. i would like to use the "cc" command in the dxcore but dont know how...
                actionHint.Color = Color.Orange;
                actionHint.Text = "Add a constructor first..";
                actionHint.PointTo(_parentClass.NameRange.End.Line, _parentClass.NameRange.End.Offset);
                _activeBlock.Clear();
            }
            else 
            {
                targetPicker.Start(_parentClass.View as TextView,_parentClass.FirstChild);
            }
        }

        private void targetPicker_TargetSelected(object sender, TargetSelectedEventArgs ea)
        {
            CodeRush.UndoStack.BeginUpdate("Generalize");
            //Debug.Assert(String.IsNullOrEmpty(_fullBlockText), "fooey");
            SourcePoint targetPoint = ea.Location.EndInsertionPoint;
            CodeRush.Documents.ActiveTextDocument.ExpandText(targetPoint, "\r\n" + _activeBlock.Text);
            _activeBlock.Delete();
            //CodeRush.Documents.ActiveTextDocument.ExpandText(
            //    ea.Location.InsertionPoint,_fullBlockText);
            CodeRush.UndoStack.EndUpdate();
        }


        private bool IsGeneralizableElement(LanguageElement languageElement)
        {
            return languageElement is Method || languageElement is Property || languageElement is DelegateDefinition;
        }

        private void targetPicker_Canceled(object sender, EventArgs e)
        {
            _activeBlock.Clear();
        }



    }
}