namespace CR_SmartGenerics
{
    using System;
    using System.Collections;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.PlugInCore;
    using DevExpress.CodeRush.StructuralParser;

    public partial class CR_SmartGenerics : StandardPlugIn
    {
        private static char[] whiteSpaces = new char[] { '\t', ' ' };
        private SmartGenericsSettings settings = new SmartGenericsSettings();

        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            
            this.settings.Load();
        }

        private static bool CanExecuteFeature(string name, string description)
        {
            StandardFeature feature = new StandardFeature(name, description, CodeRush.Options.GetFullName(typeof(SmartGenericsOptions)), FeatureProduct.Unknown);
            return CodeRush.Feature.CanExecuteFeature(feature);
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
            while (caret.RightChar != '>')
            {
                caret.DeleteRight(1);
            }
            while (caret.LeftChar != '<')
            {
                caret.DeleteLeft(1);
            }
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
                while (caret.RightChar != '>')
                {
                    caret.MoveRight(1);
                }
                caret.MoveRight(1);
            }
        }

        private static void InsertClosingCharacter(TextViewCaret caret, bool useTextField, bool addSpace)
        {
            if (addSpace)
            {
                caret.Insert("  >", false);
                caret.MoveRight(1);
            }
            else
            {
                caret.Insert(">", false);
            }

            if (useTextField)
            {
                if (CaretInsideTextField())
                {
                    BreakActiveTextField();
                }
                InsertTextFieldAt(caret);
            }
        }

        private static bool IsInsideGenerics()
        {
            // TODO: implement better heuristic
            return true;
        }

        private static bool NeedGenerics(TextViewCaret caret)
        {
            CategorizedToken leftToken = GetLeftToken(caret);
            return IsPossibleTypeOrMethodName(caret, leftToken);
        }

        private static CategorizedToken GetLeftToken(TextViewCaret caret)
        {
            TextDocument document = caret.TextDocument;
            if (document == null)
            {
                return null;
            }

            TokenCollection tokens = CodeRush.Language.GetTokens(caret.LeftText);
            if (tokens != null && tokens.Count > 1)
            {
                Token token = tokens[tokens.Count - 2];
                return token as CategorizedToken;
            }
            return null;
        }

        private static bool IsPossibleTypeOrMethodName(TextViewCaret caret, CategorizedToken token)
        {
            if (token == null)
            {
                return false;
            }
            if (token.Category != TokenCategory.Identifier)
            {
                return false;
            }
            if (IsFieldOrPropertyOrVariable(token))
            {
                return false;
            }
            if (IsNamespaceOrAlias(caret, token))
            {
                return false;
            }
            return true;
        }

        private static bool IsFieldOrPropertyOrVariable(CategorizedToken token)
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

        private static bool IsNamespaceOrAlias(TextViewCaret caret, CategorizedToken token)
        {
            foreach (DictionaryEntry @namespace in caret.TextDocument.NamespaceReferences)
            {
                if (@namespace.Key.ToString() == token.Value)
                {
                    return true;
                }
            }
            foreach (string alias in caret.TextDocument.Aliases)
            {
                if (alias == token.Value)
                {
                    return true;
                }
            }
            return false;
        }

        private static void InsertTextFieldAt(TextViewCaret caret)
        {
            TextDocument document = caret.TextDocument;
            EditPoint startEditPoint = CodeRush.EditPoints.New(document, caret.SourcePoint);
            startEditPoint.IsAnchorable = true;
            EditPoint endEditPoint = CodeRush.EditPoints.New(document, caret.SourcePoint);
            endEditPoint.IsPushable = true;
            TextField newField = new TextField(startEditPoint, endEditPoint, "Enter generic parameter(s)", TextFieldType.Normal, false);
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

        private static bool IsLeftTextEndedWithOpenGenericOperator(TextViewCaret caret)
        {
            return caret.LeftText.TrimEnd(whiteSpaces).EndsWith("<");
        }

        private static bool IsRightTextStartedWithCloseGenericOperator(TextViewCaret caret)
        {
            return caret.RightText.TrimStart(whiteSpaces).StartsWith(">");
        }

        private void CR_SmartGenerics_OptionsChanged(OptionsChangedEventArgs ea)
        {
            if (ea.OptionsPages.Contains(typeof(SmartGenericsOptions)))
            {
                this.settings.Load();
            }
        }

        private void CR_SmartGenerics_CommandExecuting(CommandExecutingEventArgs ea)
        {
            if (!CodeRush.Language.IsCSharp)
            {
                return;
            }
            if (!ea.CancelDefault && ea.CommandName == "Edit.DeleteBackwards")
            {
                TextViewCaret caret = GetCaretInActiveFocusedView();
                if (caret != null
                    && this.settings.UseSmartGenerics
                    && this.settings.SmartGenericsEasyDelete
                    && IsInsideGenerics()
                    && IsLeftTextEndedWithOpenGenericOperator(caret)
                    && IsRightTextStartedWithCloseGenericOperator(caret)
                    && CanExecuteFeature("Easy delete", "Deletes empty generics characters"))
                {
                    EasyDelete(caret, this.settings.SmartGenericsUseTextFields);
                    return;
                }
            }
        }

        private void CR_SmartGenerics_EditorCharacterTyped(EditorCharacterTypedEventArgs ea)
        {
            if (!CodeRush.Language.IsCSharp)
            {
                return;
            }
            TextViewCaret caret = GetCaretInActiveFocusedView();
            if (caret != null
                && this.settings.UseSmartGenerics
                && this.settings.SmartGenericsAutoComplete
                && ea.Character == '<'
                && CaretInCodeEditor())
            {
                CodeRush.Source.ParseIfNeeded();
                if (NeedGenerics(caret)
                    && CanExecuteFeature("Smart generics", "Auto completes closing generics char (\">\")"))
                {
                    InsertClosingCharacter(caret, this.settings.SmartGenericsUseTextFields, this.settings.SmartGenericsAddSpace);
                    return;
                }
            }
        }

        private void CR_SmartGenerics_EditorCharacterTyping(EditorCharacterTypingEventArgs ea)
        {
            if (!CodeRush.Language.IsCSharp)
            {
                return;
            }
            TextViewCaret caret = GetCaretInActiveFocusedView();
            if (caret != null
                && this.settings.UseSmartGenerics
                && this.settings.SmartGenericsIgnoreClosingGeneric
                && ea.Character == '>'
                && IsRightTextStartedWithCloseGenericOperator(caret)
                && IsInsideGenerics())
            {
                ea.Cancel = true;
                IgnoreClosingCharacter(caret, this.settings.SmartGenericsUseTextFields);
                return;
            }
        }
    }
}