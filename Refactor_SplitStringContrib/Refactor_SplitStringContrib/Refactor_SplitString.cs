namespace Refactor_SplitStringContrib
{
    using System;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.PlugInCore;
    using DevExpress.CodeRush.StructuralParser;

    public partial class Refactor_SplitString : StandardPlugIn
    {
        private SplitStringSettings settings = new SplitStringSettings();

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            
            this.settings.Load();
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            base.FinalizePlugIn();
        }
        #endregion

        private void Refactor_SplitString_KeyPressed(KeyPressedEventArgs ea)
        {
            if (this.settings.SmartEnterSplitString
                && ea.IsEnter && !ea.ShiftKeyDown && !ea.AltKeyDown && !ea.CtrlKeyDown)
            {
                CodeRush.Source.ParseIfTextChanged();
                if (CodeRush.Caret.InsideString
                    && this.CaretInCodeEditor())
                {
                    CodeRush.SmartTags.UpdateContext();
                    RefactoringProviderBase splitString = CodeRush.Refactoring.Get("Split String");
                    if (splitString != null
                        && this.IsRefactoringAvailable(splitString))
                    {
                        splitString.Execute();
                        return;
                    }
                }
            }
        }

        private void Refactor_SplitString_OptionsChanged(OptionsChangedEventArgs ea)
        {
            if (ea.OptionsPages.Contains(typeof(SplitStringOptions)))
            {
                this.settings.Load();
            }
        }

        private bool CaretInCodeEditor()
        {
            return !this.CaretInsideTextField()
                && !CodeRush.Refactoring.IsMenuActive
                && !CodeRush.IDE.IsMenuActive
                && !this.CaretInLinkedIdentifier()
                && !CodeRush.Refactoring.PickerIsActive
                && !CodeRush.Intellassist.Active
                && !this.CodeIssueFixUIIsActive();
        }

        private bool CaretInsideTextField()
        {
            TextDocument doc = CodeRush.Documents.ActiveTextDocument;
            if (doc != null)
            {
                TextField textField = doc.LastActiveField;
                if (textField != null)
                {
                    SourcePoint caretPos = CodeRush.Caret.SourcePoint;
                    return textField.Range.Contains(caretPos);
                }
            }
            return false;
        }

        private bool CaretInLinkedIdentifier()
        {
            return CodeRush.Context.Satisfied("System\\In Linked Identifier") == ContextResult.Satisfied;
        }

        private bool CodeIssueFixUIIsActive()
        {
            return CodeRush.Context.Satisfied("System\\CodeFix UI is active") == ContextResult.Satisfied;
        }

        private bool IsRefactoringAvailable(RefactoringProviderBase refactoring)
        {
            return refactoring.GetAvailability() == RefactoringAvailability.Available;
        }
    }
}