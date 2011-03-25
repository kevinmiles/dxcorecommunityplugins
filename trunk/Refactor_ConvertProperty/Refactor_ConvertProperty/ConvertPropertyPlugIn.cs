using System;
using System.Drawing;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.ComponentModel;

namespace Refactor_ConvertProperty
{
    public partial class ConvertPropertyPlugIn : StandardPlugIn
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

        // Event handlers
        private void Apply(object sender, ApplyContentEventArgs ea)
        {
            Property property = GetActiveProperty(ea.Element);
            ConvertProperty(ea.TextDocument, property);
            ImplementInterfaceIfNotPresent(ea.TextDocument, ea.ClassInterfaceOrStruct);
        }

        private void CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            ea.Available = IsAvailable(ea);
        }

        // Private implementation
        private Property GetActiveProperty(LanguageElement element)
        {
            if (element == null)
                return null;

            Property property = element as Property;
            if (property == null)
                property = element.GetParent(LanguageElementType.Property) as Property;

            return property;
        }

        private void ConvertProperty(TextDocument textDocument, Property property)
        {
            if (textDocument == null || property == null)
                return;

            Property propertyClone = (Property)property.Clone();
            string newPropertyCode = ChangeProperty(propertyClone);

            SourceRange propertyRange = property.Range.Clone();
            textDocument.DeleteText(propertyRange);
            SourceRange newPropertyRange = textDocument.InsertText(propertyRange.Start, newPropertyCode);
            textDocument.Format(newPropertyRange);

        }

        private void ImplementInterfaceIfNotPresent(TextDocument classFile, LanguageElement classExpression)
        {
            if (classFile == null || classExpression == null)
                return;

            bool alreadyImplemented = false;
            foreach (LanguageElement element in classExpression.DetailNodes)
            {
                if (element.Name == "INotifyPropertyChanged" || element.Name == "System.ComponentModel.INotifyPropertyChanged")
                {
                    alreadyImplemented = true;
                    break;
                }
            }
            if (!alreadyImplemented)
            {
                CodeRush.Source.DeclareNamespaceReference("System.ComponentModel");
                CodeRush.Source.ImplementInteface(classExpression as TypeDeclaration, new Interface("INotifyPropertyChanged"));
                AddPropertyChangedMember(classFile, classExpression);

            }
        }

        private void AddPropertyChangedMember(TextDocument classFile, LanguageElement classExpression)
        {
            Statement statement;
            if (CodeRush.Language.IsCSharp)
            {
                statement = new Statement("public event PropertyChangedEventHandler PropertyChanged = delegate {}");
            }
            else if (CodeRush.Language.IsBasic)
            {
                statement = new SnippetCodeStatement("Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged",false);
            }
            else
            {
                statement = new Statement(string.Empty);
            }
            SourceRange newEventRange = classFile.InsertText(new SourcePoint(classExpression.EndLine - 1,1), "\n"+CodeRush.Language.GenerateElement(statement));
            classFile.Format(newEventRange);
        }

        private bool IsAvailable(CheckContentAvailabilityEventArgs ea)
        {
            Property activeProperty = GetActiveProperty(ea.Element);
            if (activeProperty == null)
                return false;

            // TODO: check selection and caret position if needed...

            if (!IsSimpleProperty(activeProperty))
                return false;

            return true;
        }

        private bool IsSimpleProperty(Property activeProperty)
        {
            if (activeProperty == null)
                return false;

            if (activeProperty.IsAutoImplemented)
                return true;

            Set setter = activeProperty.Setter;
            Get getter = activeProperty.Getter;

            if (setter == null || setter.NodeCount != 1)
                return false;
            if (getter == null || getter.NodeCount != 1)
                return false;

            LanguageElement setterNode = (LanguageElement)setter.Nodes[0];
            LanguageElement getterNode = (LanguageElement)getter.Nodes[0];

            return setterNode is Assignment && getterNode is Return;
        }

        private Get GetGetter(ElementBuilder eb, string varName)
        {
            Get getter = eb.BuildGetter();
            getter.AddNode(eb.BuildReturn(varName));
            return getter;
        }

        private Set GetSetter(ElementBuilder eb, Property propertyClone, string varName)
        {
            Set setter = eb.BuildSetter();
            If ifStatement = eb.BuildIf(eb.OpNotEquals("value", varName));
            ifStatement.AddNode(eb.BuildAssignment(varName, "value"));
            ExpressionCollection args = new ExpressionCollection();
            args.Add(new SnippetExpression(CodeRush.StrUtil.AddQuotes(propertyClone.Name)));
            ExpressionCollection arguments = new ExpressionCollection();
            arguments.Add(eb.BuildThisReferenceExpression());
            arguments.Add(eb.BuildObjectCreationExpression("PropertyChangedEventArgs", args));
            if (CodeRush.Language.IsCSharp)
            {
                ifStatement.AddNode(eb.BuildMethodCall("PropertyChanged", arguments, null /* qualifier */));
            }
            else if (CodeRush.Language.IsBasic)
            {
                RaiseEvent raiseEvent = new RaiseEvent(eb.BuildMethodCallExpression("PropertyChanged", arguments));
                ifStatement.AddNode(raiseEvent);
            }
            setter.AddNode(ifStatement);
            return setter;
        }

        private string ChangeProperty(Property propertyClone)
        {
            if (propertyClone == null)
                return String.Empty;

            ElementBuilder eb = CodeRush.Language.GetActiveElementBuilder();

            // convert auto-implemented property...
            if (propertyClone.IsAutoImplemented)
            {
                // so it's no longer an auto-implemented property....
                propertyClone.IsAutoImplemented = false;
                // remove all nodes and create new ones...
                propertyClone.RemoveAllNodes();

                // create a field variable...
                string varName = CodeRush.Strings.Get("FormatFieldName", propertyClone.Name);
                Variable var = eb.BuildVariable(propertyClone.MemberTypeReference, varName);

                // add property getter...
                Get getter = GetGetter(eb, varName);
                propertyClone.AddNode(getter);

                // create property setter...
                Set setter = GetSetter(eb, propertyClone, varName);
                propertyClone.AddNode(setter);

                return CodeRush.Language.GenerateElement(var) + CodeRush.Language.GenerateElement(propertyClone);
            }
            else
            {
                Set setter = propertyClone.Setter;
                if (setter == null)
                    return String.Empty;

                Assignment assignment = setter.Nodes[0] as Assignment;
                if (assignment == null)
                    return String.Empty;

                string varName = assignment.LeftSide.Name;
                propertyClone.RemoveNode(setter);

                // recreate setter...
                Set newSetter = GetSetter(eb, propertyClone, varName);
                propertyClone.AddNode(newSetter);
            }
            return CodeRush.Language.GenerateElement(propertyClone);
        }
    }
}