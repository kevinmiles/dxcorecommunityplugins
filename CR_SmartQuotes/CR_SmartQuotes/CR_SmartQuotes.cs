namespace CR_SmartQuotes
{
    using System;
    using System.Collections.Generic;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.PlugInCore;
    using DevExpress.CodeRush.StructuralParser;

    public partial class CR_SmartQuotes : StandardPlugIn
    {
        private SmartQuoteSettings settings = new SmartQuoteSettings();

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            this.settings.Load();
            EventNexus.IntellisenseDeactivated += this.EventNexus_IntellisenseDeactivated;
        }

        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            base.FinalizePlugIn();

            EventNexus.IntellisenseDeactivated -= this.EventNexus_IntellisenseDeactivated;
        }
        #endregion

        private void EventNexus_IntellisenseDeactivated()
        {
            // Deletes auto generated closing " when VS added its own " to close HTML or XML attribute
            if (CodeRush.Language.IsHtmlOrXml
                && !CodeRush.Caret.InsideString
                && CodeRush.Caret.LeftChar == '\"'
                && CodeRush.Caret.RightChar == '\"')
            {
                CodeRush.Caret.DeleteRight(1);
            }
        }

        private void CR_SmartQuotes_CommandExecuting(CommandExecutingEventArgs ea)
        {
            if (!ea.CancelDefault && ea.CommandName == "Edit.DeleteBackwards")
            {
                TextViewCaret caret = GetCaretInActiveFocusedView();
                if (caret != null)
                {
                    if (this.settings.UseSmartDoubleQuotes && this.settings.DoubleQuotesEasyDelete
                        && caret.LeftChar == '\"' && caret.RightChar == '\"')
                    {
                        this.EasyDelete(caret);
                        return;
                    }
                    if (this.settings.UseSmartQuotes && this.settings.QuotesEasyDelete
                        && caret.LeftChar == '\'' && caret.RightChar == '\'')
                    {
                        this.EasyDelete(caret);
                        return;
                    }
                }
            }
        }

        private TextViewCaret GetCaretInActiveFocusedView()
        {
            TextView view = CodeRush.Documents.ActiveTextView;
            if (view != null && view.IsFocused)
            {
                return view.Caret;
            }
            return null;
        }

        private void EasyDelete(TextViewCaret caret)
        {
            caret.DeleteRight(1);
            if (CaretInsideTextField())
            {
                CloseActiveTextField();
            }
        }

        private void IgnoreClosingCharacter(TextViewCaret caret)
        {
            if (this.CaretInsideTextField())
            {
                this.CloseActiveTextField();
            }
            else
            {
                caret.MoveRight(1);
            }
        }

        private void InsertClosingCharacter(TextViewCaret caret, string character, bool useTextField)
        {
            caret.Insert(character, false);
            if (useTextField)
            {
                if (this.CaretInsideTextField())
                {
                    this.CloseActiveTextField();
                }
                this.InsertTextFieldAt(caret);
            }
        }

        private void CR_SmartQuotes_EditorCharacterTyped(EditorCharacterTypedEventArgs ea)
        {
            TextViewCaret caret = GetCaretInActiveFocusedView();
            if (caret != null)
            {
                if (this.settings.UseSmartDoubleQuotes 
                    && this.settings.DoubleQuotesAutoComplete
                    && ea.Character == '\"'
                    && this.CaretInCodeEditor()
                    && !this.IsLastCharacterEscaped(caret.LeftText)
                    && this.CanExecuteFeature("Smart double quotes", "Auto completes closing double quotes"))
                {
                    this.InsertClosingCharacter(caret, "\"", this.settings.DoubleQuotesUseTextFields);
                    return;
                }
                if (this.settings.UseSmartQuotes 
                    && this.settings.QuotesAutoComplete
                    && ea.Character == '\''
                    && this.CaretInCodeEditor()
                    && !this.IsLastCharacterEscaped(caret.LeftText)
                    && this.CanExecuteFeature("Smart quotes", "Auto completes closing quote"))
                {
                    CodeRush.Source.ParseIfTextChanged();
                    if (this.CaretWithinNaturalLanguage())
                    {
                        // to prevent double apostrophes e.g. in English phrases
                        return;
                    }
                    this.InsertClosingCharacter(caret, "\'", this.settings.QuotesUseTextFields);
                    return;
                }
            }
        }

        private void CR_SmartQuotes_EditorCharacterTyping(EditorCharacterTypingEventArgs ea)
        {
            TextViewCaret caret = GetCaretInActiveFocusedView();
            if (caret != null)
            {
                if (this.settings.UseSmartDoubleQuotes 
                    && this.settings.DoubleQuotesIgnoreClosingQuote
                    && ea.Character == '\"'
                    && this.CaretBeforeClosingDoubleQuote(caret))
                {
                    ea.Cancel = true;
                    this.IgnoreClosingCharacter(caret);
                    return;
                }
                if (this.settings.UseSmartQuotes 
                    && this.settings.QuotesIgnoreClosingQuote
                    && ea.Character == '\''
                    && this.CaretBeforeClosingQuote(caret))
                {
                    ea.Cancel = true;
                    this.IgnoreClosingCharacter(caret);
                    return;
                }
            }
        }

        private void CR_SmartQuotes_OptionsChanged(OptionsChangedEventArgs ea)
        {
            if (ea.OptionsPages.Contains(typeof(SmartQuoteOptions)))
            {
                this.settings.Load();
            }
        }

        private void InsertTextFieldAt(TextViewCaret caret)
        {
            TextDocument document = caret.TextDocument;
            EditPoint startEditPoint = CodeRush.EditPoints.New(document, caret.SourcePoint);
            startEditPoint.IsAnchorable = true;
            EditPoint endEditPoint = CodeRush.EditPoints.New(document, caret.SourcePoint);
            endEditPoint.IsPushable = true;
            TextField newField = new TextField(startEditPoint, endEditPoint, "Enter string value", TextFieldType.Normal, false);
            EditPoint targetPoint = endEditPoint.Clone();
            targetPoint.MoveRight(1);
            TextFieldTarget newTarget = new TextFieldTarget(targetPoint, targetPoint);
            document.TextFields.Add(newField);
            document.TextFieldTarget = newTarget;
        }

        private void CloseActiveTextField()
        {
            CodeRush.Command.Execute("FieldAccept");
        }

        private bool IsNextCharacterEscaped(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            string withoutCitationChars = text.TrimEnd('\\');
            int citationCharsCount = text.Length - withoutCitationChars.Length;
            bool isCitation = citationCharsCount % 2 == 1;
            return isCitation;
        }

        private bool IsLastCharacterEscaped(string text)
        {
            if (string.IsNullOrEmpty(text) || text.Length < 2)
            {
                return false;
            }
            string withoutLastChar = text.Substring(0, text.Length - 1);
            return this.IsNextCharacterEscaped(withoutLastChar);
        }

        private bool CaretBeforeClosingDoubleQuote(TextViewCaret caret)
        {
            return caret.RightChar == '\"' && CodeRush.Caret.InsideString
                && !this.IsNextCharacterEscaped(caret.LeftText);
        }

        private bool CaretBeforeClosingQuote(TextViewCaret caret)
        {
            return caret.RightChar == '\'' && CodeRush.Caret.InsideString
                && !this.IsNextCharacterEscaped(caret.LeftText);
        }

        private bool CaretWithinNaturalLanguage()
        {
            return CodeRush.Caret.InsideComment
                || CodeRush.Caret.AtCompilerDirective;
        }

        private bool CaretInCodeEditor()
        {
            return !CodeRush.Refactoring.IsMenuActive
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

        private bool CanExecuteFeature(string name, string description)
        {
            StandardFeature feature = new StandardFeature(name, description, CodeRush.Options.GetFullName(typeof(SmartQuoteOptions)), FeatureProduct.Unknown);
            return CodeRush.Feature.CanExecuteFeature(feature);
        }
    }
}