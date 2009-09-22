namespace CR_SmartGenerics
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.PlugInCore;
    using DevExpress.CodeRush.StructuralParser;

    public partial class CR_SmartGenerics : StandardPlugIn
    {
        private SmartGenericsSettings settings = new SmartGenericsSettings();
        private List<TextField> textFields = new List<TextField>();
        private char[] whiteSpaces = new char[] { '\t', ' ' };

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

        private void CR_SmartGenerics_EditorCharacterTyped(EditorCharacterTypedEventArgs ea)
        {
            if (!CodeRush.Language.IsCSharp)
            {
                return;
            }
            if (this.settings.UseSmartGenerics && this.settings.SmartGenericsAutoComplete
                && ea.Character == '<')
            {
                CodeRush.Source.ParseIfNeeded();
                if (this.NeedGenerics())
                {
                    if (this.settings.SmartGenericsAddSpace)
                    {
                        CodeRush.Caret.Insert("  >", false);
                        CodeRush.Caret.MoveRight(1);
                    }
                    else
                    {
                        CodeRush.Caret.Insert(">", false);
                    }
                    if (this.settings.SmartGenericsUseTextFields && !this.CaretInsideTextField())
                    {
                        this.InsertTextFieldAtCaretPosition();
                    }
                }
            }
        }

        private void CR_SmartGenerics_EditorCharacterTyping(EditorCharacterTypingEventArgs ea)
        {
            if (!CodeRush.Language.IsCSharp)
            {
                return;
            }
            if (this.settings.UseSmartGenerics && this.settings.SmartGenericsIgnoreClosingGeneric
                && ea.Character == '>'
                && this.IsRightTextStartedWithCloseGenericOperator()
                && this.IsInsideGenerics())
            {
                ea.Cancel = true;
                if (this.CaretInsideTextField())
                {
                    this.CloseActiveTextField();
                }
                else
                {
                    while (this.IsRightTextStartedWithCloseGenericOperator())
                    {
                        CodeRush.Caret.MoveRight(1);
                    }
                }
                return;
            }
        }

        private void CR_SmartGenerics_KeyPressed(KeyPressedEventArgs ea)
        {
            if (ea.AlreadyHandled() || !CodeRush.Language.IsCSharp)
            {
                return;
            }
            if (ea.IsBackspace && this.IsInsideGenerics())
            {
                if (this.settings.UseSmartGenerics && this.settings.SmartGenericsEasyDelete
                    && this.IsLeftTextEndedWithOpenGenericOperator()
                    && this.IsRightTextStartedWithCloseGenericOperator())
                {
                    CodeRush.Caret.DeleteRight(1);
                    return;
                }
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

        private bool IsInsideGenerics()
        {
            // TODO: implement better heuristic
            return true;
        }

        private bool NeedGenerics()
        {
            CategorizedToken leftToken = this.GetLeftToken();
            return this.IsPossibleTypeOrMethodName(leftToken);
        }

        private CategorizedToken GetLeftToken()
        {
            TextDocument document = CodeRush.Documents.ActiveTextDocument;
            if (document == null)
            {
                return null;
            }

            TokenCollection tokens = CodeRush.Language.GetTokens(CodeRush.Caret.LeftText);
            if (tokens != null && tokens.Count > 1)
            {
                Token token = tokens[tokens.Count - 2];
                return token as CategorizedToken;
            }
            return null;
        }

        private bool IsPossibleTypeOrMethodName(CategorizedToken token)
        {
            if (token == null)
            {
                return false;
            }
            if (token.Category != TokenCategory.Identifier)
            {
                return false;
            }
            if (this.IsFieldOrPropertyOrVariable(token))
            {
                return false;
            }
            if (this.IsNamespaceOrAlias(token))
            {
                return false;
            }
            return true;
        }

        private bool IsFieldOrPropertyOrVariable(CategorizedToken token)
        {
            if (CodeRush.Source.DeclaresLocal(token.Value))
            {
                // local variable or parameter
                return true;
            }
            IMemberElement member = CodeRush.Source.GetMember(CodeRush.Source.ActiveClassInterfaceStructOrModule, token.Value);
            if (member != null && (member is Property || member is Variable || member is Event))
            {
                // is declared property or field or event
                return true;
            }
            return false;
        }

        private bool IsNamespaceOrAlias(CategorizedToken token)
        {
            foreach (DictionaryEntry @namespace in CodeRush.Source.NamespaceReferences)
            {
                if (@namespace.Key.ToString() == token.Value)
                {
                    return true;
                }
            }
            foreach (string alias in CodeRush.Documents.ActiveTextDocument.Aliases)
            {
                if (alias == token.Value)
                {
                    return true;
                }
            }
            return false;
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
            TextField newField = new TextField(startEditPoint, endEditPoint, "Enter generic parameter", TextFieldType.Normal, false);
            document.TextFields.Add(newField);
            this.textFields.Add(newField);
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

        private bool IsLeftTextEndedWithOpenGenericOperator()
        {
            return CodeRush.Caret.LeftText.TrimEnd(this.whiteSpaces).EndsWith("<");
        }

        private bool IsRightTextStartedWithCloseGenericOperator()
        {
            return CodeRush.Caret.RightText.TrimStart(this.whiteSpaces).StartsWith(">");
        }

        private void CR_SmartGenerics_OptionsChanged(OptionsChangedEventArgs ea)
        {
            if (ea.OptionsPages.Contains(typeof(SmartGenericsOptions)))
            {
                this.settings.Load();
            }
        }
    }
}