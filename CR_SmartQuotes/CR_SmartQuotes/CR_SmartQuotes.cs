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
        private List<TextField> textFields = new List<TextField>();

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            this.settings.Load();
            EventNexus.TextFieldCommitted += this.EventNexus_TextFieldCommitted;
            EventNexus.AllTextFieldsCommitted += this.EventNexus_AllTextFieldsCommitted;
        }

        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            base.FinalizePlugIn();

            EventNexus.TextFieldCommitted -= this.EventNexus_TextFieldCommitted;
            EventNexus.AllTextFieldsCommitted -= this.EventNexus_AllTextFieldsCommitted;
        }
        #endregion

        private void CR_SmartQuotes_EditorCharacterTyped(EditorCharacterTypedEventArgs ea)
        {
            if (this.settings.UseSmartDoubleQuotes && this.settings.DoubleQuotesAutoComplete
                && ea.Character == '\"'
                && !this.IsLastCharacterEscaped(CodeRush.Caret.LeftText))
            {
                CodeRush.Caret.Insert("\"", false);
                if (this.settings.DoubleQuotesUseTextFields && !this.CaretInsideTextField())
                {
                    this.InsertTextFieldAtCaretPosition();
                }
                return;
            }
            if (this.settings.UseSmartQuotes && this.settings.QuotesAutoComplete
                && ea.Character == '\''
                && !this.IsLastCharacterEscaped(CodeRush.Caret.LeftText))
            {
                CodeRush.Source.ParseIfTextChanged();
                if (this.CaretWithinNaturalLanguage())
                {
                    // to prevent double apostrophes e.g. in English phrases
                    return;
                }
                CodeRush.Caret.Insert("\'", false);
                if (this.settings.QuotesUseTextFields && !this.CaretInsideTextField())
                {
                    this.InsertTextFieldAtCaretPosition();
                }
                return;
            }
        }

        private void CR_SmartQuotes_EditorCharacterTyping(EditorCharacterTypingEventArgs ea)
        {
            if (this.settings.UseSmartDoubleQuotes && this.settings.DoubleQuotesIgnoreClosingQuote
                && ea.Character == '\"'
                && CodeRush.Caret.RightChar == '\"' && CodeRush.Caret.InsideString
                && !this.IsNextCharacterEscaped(CodeRush.Caret.LeftText))
            {
                ea.Cancel = true;
                if (this.CaretInsideTextField())
                {
                    this.CloseActiveTextField();
                }
                else
                {
                    CodeRush.Caret.MoveRight(1);
                }
                return;
            }
            if (this.settings.UseSmartQuotes && this.settings.QuotesIgnoreClosingQuote
                && ea.Character == '\''
                && CodeRush.Caret.RightChar == '\'' && CodeRush.Caret.InsideString
                && !this.IsNextCharacterEscaped(CodeRush.Caret.LeftText))
            {
                ea.Cancel = true;
                if (this.CaretInsideTextField())
                {
                    this.CloseActiveTextField();
                }
                else
                {
                    CodeRush.Caret.MoveRight(1);
                }
                return;
            }
        }

        private void CR_SmartQuotes_KeyPressed(KeyPressedEventArgs ea)
        {
            if (ea.IsBackspace && CodeRush.Caret.InsideString)
            {
                if (this.settings.UseSmartDoubleQuotes && this.settings.DoubleQuotesEasyDelete 
                    && CodeRush.Caret.LeftChar == '\"' && CodeRush.Caret.RightChar == '\"')
                {
                    CodeRush.Caret.DeleteRight(1);
                    return;
                }
                if (this.settings.UseSmartQuotes && this.settings.QuotesEasyDelete 
                    && CodeRush.Caret.LeftChar == '\'' && CodeRush.Caret.RightChar == '\'')
                {
                    CodeRush.Caret.DeleteRight(1);
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

        private void EventNexus_TextFieldCommitted(TextFieldEventArgs ea)
        {
            this.RemoveTextField(ea.TextField);
        }

        private void EventNexus_AllTextFieldsCommitted(EventArgs ea)
        {
            this.textFields.Clear();
        }

        private void InsertTextFieldAtCaretPosition()
        {
            TextDocument document = CodeRush.Documents.ActiveTextDocument;
            if (document == null)
            {
                return;
            }
            EditPoint startEditPoint = CodeRush.EditPoints.New(document, CodeRush.Caret.SourcePoint);
            startEditPoint.IsAnchorable = true;
            EditPoint endEditPoint = CodeRush.EditPoints.New(document, CodeRush.Caret.SourcePoint);
            endEditPoint.IsPushable = true;
            TextField newField = new TextField(startEditPoint, endEditPoint, "Enter string value", TextFieldType.Normal, false);
            EditPoint targetPoint = endEditPoint.Clone();
            targetPoint.MoveRight(1);
            TextFieldTarget newTarget = new TextFieldTarget(targetPoint, targetPoint);
            document.TextFields.Add(newField);
            document.TextFieldTarget = newTarget;
            this.textFields.Add(newField);
        }

        private void CloseActiveTextField()
        {
            CodeRush.Command.Execute("FieldAccept");
        }

        private void RemoveTextField(TextField textField)
        {
            if (this.textFields.Contains(textField))
            {
                this.textFields.Remove(textField);
            }
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

        private bool CaretWithinNaturalLanguage()
        {
            return CodeRush.Caret.InsideComment
                || CodeRush.Caret.AtCompilerDirective;
        }
    }
}