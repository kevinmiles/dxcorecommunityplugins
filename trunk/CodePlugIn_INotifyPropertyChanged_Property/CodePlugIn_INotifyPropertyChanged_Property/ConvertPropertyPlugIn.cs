using System;
using System.Drawing;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;

namespace Refactor_ConvertProperty
{
    /// <summary>
    /// DxCore Code Extension Plug In that implements the INotifyPropertyChanged interface pattern for properties.
    /// It can convert auto-implemented or normal properties to include a call to fire the PropertyChanged event.
    /// It will implement the INPC interface if it is not already implemented on the containing type.
    /// This class contains two plug ins:
    /// The first will simply fire the INPC.PropertyChanged event directly in the property set block after expansion
    /// The second assumes you have base class support for firing PropertyChanged through an OnPropertyChanged method that 
    /// takes a lambda expression. For the base class support, you can use the Prism 4 NotificationObject base class: http://prism.codeplex.com.
    /// 
    /// This plug in currently only supports C# because of some of the hard coded C# syntax. This was done because equivalent VB language features were not
    /// available or the DXCore APIs could not be located to generate the equivalent VB code, and VB support was not needed for the project this
    /// was written for.
    /// </summary>
    public partial class ConvertPropertyPlugIn : StandardPlugIn
    {
        #region Event handlers
        private void ApplyINPCBaseClassOnPropertyChanged(object sender, ApplyContentEventArgs ea)
        {
            Property property = GetActiveProperty(ea.Element);
            ConvertProperty(ea.TextDocument, property, true, ea.ClassInterfaceOrStruct);
            ImplementInterfaceIfNotPresent(ea.TextDocument, ea.ClassInterfaceOrStruct, false);
        }

        private void ApplyINPCBasic(object sender, ApplyContentEventArgs ea)
        {
            Property property = GetActiveProperty(ea.Element);
            ConvertProperty(ea.TextDocument, property, false, ea.ClassInterfaceOrStruct);
            ImplementInterfaceIfNotPresent(ea.TextDocument, ea.ClassInterfaceOrStruct, true);

        }

        private void CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            ea.Available = IsAvailable(ea);
        }
        #endregion


        #region Implementation Details
        private Property GetActiveProperty(LanguageElement element)
        {
            if (element == null)
                return null;

            Property property = element as Property;
            if (property == null)
                property = element.GetParent(LanguageElementType.Property) as Property;

            return property;
        }

        private void ConvertProperty(TextDocument textDocument, Property property, bool baseClassVersion, LanguageElement classExpression)
        {
            if (textDocument == null || property == null)
                return;

            Property propertyClone = (Property)property.Clone();
            string newPropertyCode = ChangeProperty(propertyClone, baseClassVersion, classExpression);

            SourceRange propertyRange = property.Range.Clone();
            textDocument.DeleteText(propertyRange);
            SourceRange newPropertyRange = textDocument.InsertText(propertyRange.Start, newPropertyCode);
            textDocument.Format(newPropertyRange);

        }

        private void ImplementInterfaceIfNotPresent(TextDocument classFile, LanguageElement classExpression, bool redeclareInterface)
        {
            if (classFile == null || classExpression == null)
                return;

            bool interfaceImplemented = CodeRush.Source.Implements(classExpression as ITypeElement, "System.ComponentModel.INotifyPropertyChanged");
            ITypeElement[] baseTypes = CodeRush.Source.GetBaseTypes(classExpression as ITypeElement);
            bool interfaceImplementedOnType = (from bt in baseTypes where bt.Name.Contains("INotifyPropertyChanged") select bt).FirstOrDefault() != null;
            bool propChangedImplementedOnType =
                CodeRush.Source.GetMember(classExpression as ITypeElement, "PropertyChanged", false) != null;
            bool propChangedImplementedOnBaseType = CodeRush.Source.GetMember(classExpression as ITypeElement, "PropertyChanged", true) != null;

            if ((!interfaceImplemented && !interfaceImplementedOnType) || (redeclareInterface && !interfaceImplementedOnType))
            {
                CodeRush.Source.DeclareNamespaceReference("System.ComponentModel");
                CodeRush.Source.ImplementInterface(classExpression as TypeDeclaration, new Interface("INotifyPropertyChanged"));
            }
            if ((!propChangedImplementedOnType && !propChangedImplementedOnBaseType) || (!propChangedImplementedOnType && redeclareInterface))
            {
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
                statement = new SnippetCodeStatement("Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged", false);
            }
            else
            {
                statement = new Statement(string.Empty);
            }
            SourceRange newEventRange = classFile.InsertText(new SourcePoint(classExpression.EndLine, 1), "\n" + CodeRush.Language.GenerateElement(statement));
            classFile.Format(newEventRange);
        }

        private bool IsAvailable(CheckContentAvailabilityEventArgs ea)
        {
            Property activeProperty = GetActiveProperty(ea.Element);
            if (activeProperty == null)
                return false;

            // Uncomment this is you want to prevent it from killing a property that has implementation in its get or set blocks
            //if (!IsSimpleProperty(activeProperty))
            //    return false;

            return true;
        }

        /// <summary>
        /// Checks to see if there is any implementation in the get or set blocks beyond just the member variable get/set
        /// </summary>

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

        private Set GetOnPropertyChangedLambdaSetter(ElementBuilder eb, Property propertyClone, string varName)
        {
            Set setter = eb.BuildSetter();
            If ifStatement = eb.BuildIf(eb.OpNotEquals("value", varName));
            ifStatement.AddNode(eb.BuildAssignment(varName, "value"));
            ExpressionCollection args = new ExpressionCollection();
            LambdaExpression lambda = new LambdaExpression();
            var propAccess = new ElementReferenceExpression(propertyClone.Name);
            lambda.AddNode(propAccess);
            args.Add(lambda);
            var propChangedCall = eb.BuildMethodCall("RaisePropertyChanged", args, null);
            ifStatement.AddNode(propChangedCall);
            setter.AddNode(ifStatement);
            return setter;
        }

        private string ChangeProperty(Property propertyClone, bool baseClassVersion, LanguageElement classExpression)
        {
            if (propertyClone == null)
                return String.Empty;

            ElementBuilder eb = CodeRush.Language.GetActiveElementBuilder();

            // so it's no longer an auto-implemented property....
            propertyClone.IsAutoImplemented = false;
            // remove all nodes and create new ones...
            propertyClone.RemoveAllNodes();

            // create a field variable if it doesn't already exist...
            string varName = CodeRush.Strings.Get("FormatFieldName", propertyClone.Name);
            Variable var = eb.BuildVariable(propertyClone.MemberTypeReference, varName);

            // add property getter...
            //Get getter = GetGetter(eb, varName);
            //propertyClone.AddNode(getter);
            string propDecl = String.Format("public {0} {1}\n{{\n", propertyClone.MemberTypeReference, propertyClone.Name);
            string getter = String.Format("get {{ return {0}; }}\n", varName);

            // create property setter...

            Set setter;
            if (!baseClassVersion)
                setter = GetSetter(eb, propertyClone, varName);
            else
                setter = GetOnPropertyChangedLambdaSetter(eb, propertyClone, varName);
            //propertyClone.AddNode(setter);
            string setterStr = CodeRush.Language.GenerateElement(setter);
            string propBlock = String.Format("{0}{1}{2}}}", propDecl, getter, setterStr);

            // See if member is needed
            bool createField = true;
            foreach (LanguageElement member in classExpression)
            {
                if (member is IFieldElement && member.Name == varName) { createField = false; break; }
            }
            if (createField)
                return CodeRush.Language.GenerateElement(var) + propBlock;//CodeRush.Language.GenerateElement(propertyClone);
            else
                return propBlock;// CodeRush.Language.GenerateElement(propertyClone);
        }
        #endregion
    }
}