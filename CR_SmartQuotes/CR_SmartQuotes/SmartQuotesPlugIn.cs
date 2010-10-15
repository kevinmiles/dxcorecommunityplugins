namespace CR_SmartQuotes
{
    using System;
    using System.Collections.Generic;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.PlugInCore;
    using DevExpress.CodeRush.StructuralParser;

    public partial class SmartQuotesPlugIn : StandardPlugIn
    {
        private SmartQuoteSettings settings = new SmartQuoteSettings();

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            this.settings.Load();
            EventNexus.IntellisenseDeactivated += this.EventNexusIntellisenseDeactivated;
        }

        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            base.FinalizePlugIn();

            EventNexus.IntellisenseDeactivated -= this.EventNexusIntellisenseDeactivated;
        }
        #endregion

        private void EventNexusIntellisenseDeactivated()
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

        private void SmartQuotesPlugInCommandExecuting(CommandExecutingEventArgs ea)
        {
            if (!ea.CancelDefault && ea.CommandName == "Edit.DeleteBackwards")
            {
                TextViewCaret caret = GetCaretInActiveFocusedView();
                if (caret != null)
                {
                    if (this.settings.UseSmartDoubleQuotes 
                        && this.settings.DoubleQuotesEasyDelete
                        && caret.LeftChar == '\"' 
                        && caret.RightChar == '\"'
                        && CanExecuteFeature("Easy delete", "Deletes empty quotes and double quotes"))
                    {
                        EasyDelete(caret, this.settings.DoubleQuotesUseTextFields);
                        return;
                    }
                    if (this.settings.UseSmartQuotes 
                        && this.settings.QuotesEasyDelete
                        && caret.LeftChar == '\'' 
                        && caret.RightChar == '\''
                        && CanExecuteFeature("Easy delete", "Deletes empty quotes and double quotes"))
                    {
                        EasyDelete(caret, this.settings.QuotesUseTextFields);
                        return;
                    }
                }
            }
        }

        private void SmartQuotesPlugInEditorCharacterTyped(EditorCharacterTypedEventArgs ea)
        {
            TextViewCaret caret = GetCaretInActiveFocusedView();
            if (caret != null)
            {
                if (this.settings.UseSmartDoubleQuotes
                    && this.settings.DoubleQuotesAutoComplete
                    && ea.Character == '\"'
                    && CaretInCodeEditor()
                    && !IsLastCharacterEscaped(caret.LeftText)
                    && CanExecuteFeature("Smart double quotes", "Auto completes closing double quotes"))
                {
                    InsertClosingCharacter(caret, "\"", this.settings.DoubleQuotesUseTextFields);
                    return;
                }
                if (this.settings.UseSmartQuotes
                    && this.settings.QuotesAutoComplete
                    && ea.Character == '\''
                    && CaretInCodeEditor()
                    && !IsLastCharacterEscaped(caret.LeftText)
                    && CanExecuteFeature("Smart quotes", "Auto completes closing quote"))
                {
                    CodeRush.Source.ParseIfTextChanged();
                    if (CaretWithinNaturalLanguage())
                    {
                        // to prevent double apostrophes e.g. in English phrases
                        return;
                    }
                    InsertClosingCharacter(caret, "\'", this.settings.QuotesUseTextFields);
                    return;
                }
            }
        }

        private void SmartQuotesPlugInEditorCharacterTyping(EditorCharacterTypingEventArgs ea)
        {
            TextViewCaret caret = GetCaretInActiveFocusedView();
            if (caret != null)
            {
                if (this.settings.UseSmartDoubleQuotes
                    && this.settings.DoubleQuotesIgnoreClosingQuote
                    && ea.Character == '\"'
                    && CaretBeforeClosingDoubleQuote(caret))
                {
                    ea.Cancel = true;
                    IgnoreClosingCharacter(caret, this.settings.DoubleQuotesUseTextFields);
                    return;
                }
                if (this.settings.UseSmartQuotes
                    && this.settings.QuotesIgnoreClosingQuote
                    && ea.Character == '\''
                    && CaretBeforeClosingQuote(caret))
                {
                    ea.Cancel = true;
                    IgnoreClosingCharacter(caret, this.settings.QuotesUseTextFields);
                    return;
                }
            }
        }

        private static TextViewCaret GetCaretInActiveFocusedView()
        {
            TextView view = CodeRush.Documents.ActiveTextView;
            if (view != null && view.IsFocused)
            {
                return view.Caret;
            }
            return null;
        }

        private static void EasyDelete(TextViewCaret caret, bool useTextField)
        {
            caret.DeleteRight(1);
            if (useTextField && CaretInsideTextField())
            {
                AcceptActiveTextField();
            }
        }

        private static void IgnoreClosingCharacter(TextViewCaret caret, bool useTextField)
        {
            if (useTextField && CaretInsideTextField())
            {
                AcceptActiveTextField();
            }
            else
            {
                caret.MoveRight(1);
            }
        }

        private static void InsertClosingCharacter(TextViewCaret caret, string character, bool useTextField)
        {
            caret.Insert(character, false);
            if (useTextField)
            {
                if (CaretInsideTextField())
                {
                    BreakActiveTextField();
                }
                InsertTextFieldAt(caret);
            }
        }

        private void SmartQuotesPluginOptionsChanged(OptionsChangedEventArgs ea)
        {
            if (ea.OptionsPages.Contains(typeof(SmartQuoteOptions)))
            {
                this.settings.Load();
            }
        }

        private static void InsertTextFieldAt(TextViewCaret caret)
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

        private static void AcceptActiveTextField()
        {
            CodeRush.Command.Execute("FieldAccept");
        }

        private static void BreakActiveTextField()
        {
            CodeRush.Command.Execute("FieldBreak");
        }

        private static bool IsNextCharacterEscaped(string text)
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

        private static bool IsLastCharacterEscaped(string text)
        {
            if (string.IsNullOrEmpty(text) || text.Length < 2)
            {
                return false;
            }
            string withoutLastChar = text.Substring(0, text.Length - 1);
            return IsNextCharacterEscaped(withoutLastChar);
        }

        private static bool CaretBeforeClosingDoubleQuote(TextViewCaret caret)
        {
            return caret.RightChar == '\"' && CodeRush.Caret.InsideString
                && !IsNextCharacterEscaped(caret.LeftText);
        }

        private static bool CaretBeforeClosingQuote(TextViewCaret caret)
        {
            return caret.RightChar == '\'' && CodeRush.Caret.InsideString
                && !IsNextCharacterEscaped(caret.LeftText);
        }

        private static bool CaretWithinNaturalLanguage()
        {
            return CodeRush.Caret.InsideComment
                || CodeRush.Caret.AtCompilerDirective;
        }

        private static bool CaretInCodeEditor()
        {
            return !CodeRush.Refactoring.IsMenuActive
                && !CodeRush.IDE.IsMenuActive
                && !CaretInLinkedIdentifier()
                && !CodeRush.Refactoring.PickerIsActive
                && !CodeRush.Intellassist.Active
                && !CodeIssueFixUIIsActive();
        }

        private static bool CaretInsideTextField()
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

        private static bool CaretInLinkedIdentifier()
        {
            return CodeRush.Context.Satisfied("System\\In Linked Identifier") == ContextResult.Satisfied;
        }

        private static bool CodeIssueFixUIIsActive()
        {
            return CodeRush.Context.Satisfied("System\\CodeFix UI is active") == ContextResult.Satisfied;
        }

        private static bool CanExecuteFeature(string name, string description)
        {
            StandardFeature feature = new StandardFeature(name, description, CodeRush.Options.GetFullName(typeof(SmartQuoteOptions)), FeatureProduct.Unknown);
            return CodeRush.Feature.CanExecuteFeature(feature);
        }
    }
}