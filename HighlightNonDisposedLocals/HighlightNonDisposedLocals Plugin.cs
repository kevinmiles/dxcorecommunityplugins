using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Collections.Generic;
using System.Collections;

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

        private bool _HighlightingEnabled = true;
        private Dictionary<int, object> _LinesChanged = new Dictionary<int, object>();
        private static Dictionary<string, bool> _ImplementsIDisposable = new Dictionary<string, bool>();

        private void PlugIn1_EditorPaintLanguageElement(EditorPaintLanguageElementEventArgs ea) {
            if (!_HighlightingEnabled)
                return;
            LanguageElement element = ea.LanguageElement;
            if (_LinesChanged.ContainsKey(element.StartLine))
                return;

            if (IsUndisposedLocal(element))
                ea.PaintArgs.OverlayText(element.Name, element.NameRange.Start, Color.Red);
        }

        private static bool ImplementsIDisposable(string typeName) {
            if (_ImplementsIDisposable.ContainsKey(typeName))
                return _ImplementsIDisposable[typeName];

            bool implementsIDisposable = CodeRush.Source.Implements(typeName, "System.IDisposable");
            _ImplementsIDisposable[typeName] = implementsIDisposable;

            return implementsIDisposable;
        }

        private static bool ParentIsAssignmentWithCreation(ref string typeName, LanguageElement element) {
            Assignment parentAssignment = element.Parent as Assignment;
            if (parentAssignment == null || parentAssignment.LeftSide != element)
                return false;

            if (!(parentAssignment.Expression is ObjectCreationExpression))
                return false;

            if (parentAssignment.Parent is UsingStatement)
                return false;

            LanguageElement localDeclaration = CodeRush.Refactoring.FindLocalDeclaration(element);
            if (localDeclaration == null)
                return false;


            typeName = localDeclaration.GetTypeName();
            return true;
        }

        private static bool InitializerHasCreation(ref string typeName, InitializedVariable initializedVariable) {
            if (!(initializedVariable.Expression is ObjectCreationExpression))
                return false;

            if (initializedVariable.Parent is UsingStatement)
                return false;

            typeName = initializedVariable.GetTypeName();
            return true;
        }



        private static bool IsUndisposedLocal(LanguageElement element) {
            string typeName = null;
            InitializedVariable initializedVariable = element as InitializedVariable;
            if (initializedVariable != null) {
                if (!InitializerHasCreation(ref typeName, initializedVariable))
                    return false;
            } else {
                if (!ParentIsAssignmentWithCreation(ref typeName, element))
                    return false;
            }
            return ImplementsIDisposable(typeName) && FailsToCallDispose(element);
        }


        private void AddChangedLine(int line)
        {
            if (_LinesChanged.ContainsKey(line))
                return;
            _LinesChanged.Add(line, null);
        }

        private void PlugIn1_TextChanged(TextChangedEventArgs ea)
        {
            if (ea.ChangeType == TextChangeType.None)
                return;
            if (ea.ChangeType == TextChangeType.Insertion ||
                ea.ChangeType == TextChangeType.Replacement)
            {
                AddChangedLine(ea.Insertion.Start.Line);
                if (ea.Insertion.LineCount > 1)
                    AddChangedLine(ea.Insertion.End.Line);
            }
            else  // TextChangeType.Deletion...
                AddChangedLine(ea.Deletion.Start.Line);
        }

        private void PlugIn1_AfterParse(AfterParseEventArgs ea) {
            _LinesChanged.Clear();
        }

        private void PlugIn1_DocumentActivated(DocumentEventArgs ea) {
            _ImplementsIDisposable.Clear();
        }

        private static IEnumerable GetAllStatements(LanguageElement parent) {
            if (parent is Accessor)
                return ((Accessor)parent).AllStatements;
            else if (parent is MemberWithParameters)
                return ((MemberWithParameters)parent).AllStatements;
            return null;
        }

        private static bool FailsToCallDispose(LanguageElement element) {
            LanguageElement parentElement = element.GetParentCodeBlock();
            IEnumerable allStatements = GetAllStatements(parentElement);
            if (allStatements == null)
                return false;
            foreach (Statement statement in allStatements)
                if (statement is MethodCall) {
                    LanguageElement firstChild = statement.FirstChild;
                    if (firstChild is MethodReferenceExpression && firstChild.Name == "Dispose") {
                        LanguageElement nextChild = firstChild.FirstChild;
                        if (nextChild is ElementReferenceExpression && nextChild.Name == element.Name)
                            return false;  // We found a call to Dispose....
                    }
                }
            return true;
        }


        private void actionIDisposableHighlightToggle_Execute(ExecuteEventArgs ea) {
            _HighlightingEnabled = !_HighlightingEnabled;
            CodeRush.TextViews.Refresh();
        }

    }
}