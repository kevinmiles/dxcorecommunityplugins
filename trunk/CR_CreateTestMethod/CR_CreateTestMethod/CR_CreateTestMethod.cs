using System;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.Core;
using System.Collections.Generic;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Drawing;
using DevExpress.CodeRush.Extensions;
using CR_CreateTestMethod.LanguageElementChecks;

namespace CR_CreateTestMethod
{
    public partial class CR_CreateTestMethod : StandardPlugIn
    {
        private Class _currentClass;
        private Method _setupMethod;
        private Assignment _parentAssignment;

        private List<string> _testClassAttributes = new List<string> { "TestFixture", "TestClass" };
        private List<string> _testMethodAttributes = new List<string> { "Test", "TestMethod" };

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

        private void acnCreateTestMethod_Execute(ExecuteEventArgs ea)
        {
            if (!IsAvailable())
                return;
            var text = string.Empty;
            if (CodeRush.Selection.Exists)
                text = CodeRush.Selection.Text;
            else if (CodeRush.Caret.InsideComment)
                text = CodeRush.Source.ActiveComment.Name;
            else
                return;
            CreateTestMethod(text);
        }
        
        private string CreateMethodName(string text)
        {
            var remove = new List<string> { ",","'",".","?","!","-","/","\\","(",")",";","[","]","{","}"};

            remove.ForEach(s => text = text.Replace(s,string.Empty));
            return text.Trim().Replace(" ","_");
        }

        private void CreateTestMethod(string text)
        {
            string source = GetSource(text);
            var currentRange = CodeRush.Source.Active.Range;
            TextDocument.Active.Replace(currentRange, source, string.Empty, true);
        }

        private string GetSource(string text)
        {
            var methodName = CreateMethodName(text);
            ElementBuilder eb = new ElementBuilder();
            var method = eb.AddMethod(CodeRush.Source.ActiveClass, "void", methodName);
            method.Visibility = MemberVisibility.Public;
            var testSection = eb.AddAttributeSection(method);
            eb.AddAttribute(testSection, "Test");
            method.AddSupportElement(testSection);
            eb.AddMethodCall(method, "Assert.Fail", new[] { "\"Not Implemented\"" });
            string source = CodeRush.Language.GenerateElement(testSection) + CodeRush.Language.GenerateElement(method);
            return source;
        }
        private void spCreateTestMethods_GetSmartPasteSuggestions(object aSender, GetSmartPasteSuggestionsEventArgs ea)
        {
            if (!IsAvailable())
                return;            

            StringBuilder sb = new StringBuilder();
            
            foreach (var line in ea.ClipboardText.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (line.Trim().StartsWith("//"))
                    sb.AppendLine(GetSource(line.Trim()).Trim());
                else
                    sb.AppendLine(line.Trim());
            }
            ea.AddSuggestion(sb.ToString(), true, "Add Test Methods");
        }

        private void acnCreateTestMethod_CheckAvailability(CheckActionAvailabilityEventArgs ea)
        {
            ea.Available = IsAvailable();
        }

        private bool IsAvailable()
        {
            return cpInsideTestClass.IsContextSatisfied(string.Empty) && !(cpInsideTestMethod.IsContextSatisfied(string.Empty));
        }

        internal void cpInsideTestClass_ContextSatisfied(ContextSatisfiedEventArgs ea)
        {
            ea.Satisfied = IsInTestClass();
        }

        internal void cpInsideTestMethod_ContextSatisfied(ContextSatisfiedEventArgs ea)
        {
            ea.Satisfied = IsInTestMethod();
        }
        private bool IsInTestMethod()
        {
            return IsInTestClass() && (CodeRush.Source.ActiveMethod != null && CodeRush.Source.ActiveMethod.Attributes.OfType<LanguageElement>().Count(a => _testMethodAttributes.Contains(a.Name)) > 0);
        }
        internal bool IsInTestClass()
        {
            return CodeRush.Source.ActiveClass != null && CodeRush.Source.ActiveClass.IsTestClass();
        }

        internal bool HasSetUp()
        {
            return CodeRush.Source.ActiveClass.Nodes.OfType<LanguageElement>().Count(n => n.ElementType == LanguageElementType.AttributeSection && n.FirstDetail.Name == "SetUp") > 0;
        }

        private void cpMoveToSetup_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            _currentClass = ea.Element.GetClass();
            _setupMethod = GetSetupMethod(_currentClass);
            if(_setupMethod == null)
            {
                ea.Available = false;
                return;
            }
            _parentAssignment = ea.Element.FindParentByElementType(LanguageElementType.Assignment) as Assignment;
            if (_parentAssignment == null)
            {
                ea.Available = false;
                return;
            }
            var referencedVariable = ((Assignment)_parentAssignment).LeftSide as ElementReferenceExpression;
            
            ea.Available = _parentAssignment != null 
                        && _parentAssignment.LeftSide.ElementType == LanguageElementType.ElementReferenceExpression
                        && referencedVariable.GetDeclaringType() == _currentClass
                        && IsInTestClass() 
                        && HasSetUp();
        }

        private void cpMoveToSetup_Apply(object sender, ApplyContentEventArgs ea)
        {
            var containingMethod = _parentAssignment.Parent;
            containingMethod.RemoveNode(_parentAssignment);
            
            _setupMethod.AddDetailNode(_parentAssignment);

            ea.TextDocument.Replace(_parentAssignment.Range, string.Empty, string.Empty, true);
            var insertionLine = _setupMethod.BlockEnd.Bottom.Line;
            ea.TextDocument.InsertText(insertionLine, _setupMethod.BlockEnd.Bottom.Offset - 1, CodeRush.Language.GenerateElement(_parentAssignment));
            ea.TextDocument.Format(_setupMethod.Range);

        }

        private void cpMoveToSetup_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
        {
            _parentAssignment = ea.Element.FindParentByElementType(LanguageElementType.Assignment) as Assignment;
            ea.AddStrikethrough(_parentAssignment.Range);
            ea.AddInsertSymbol(new SourcePoint(_setupMethod.BlockEnd.Bottom.Line - 1, _setupMethod.BlockEnd.Bottom.Offset));
        }
        private Method GetSetupMethod(Class currentClass)
        {
            if (currentClass != null && currentClass.Nodes.Count > 0 && currentClass.Nodes.OfType<AttributeSection>().Count() > 0)
            {
                var setupAttribute = currentClass.Nodes.OfType<AttributeSection>().FirstOrDefault(section => section.FirstDetail != null && section.FirstDetail.Name == "SetUp");
                if(setupAttribute != null)
                    return (setupAttribute.TargetNode as Method);
            }
            return null;
        }

        private void cipNoAssertsInTest_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
        {
            new UnitTestShouldAssertCodeIssueFinder().FindCodeIssues(ea);
        }
    }

    public class UnitTestShouldAssertCodeIssueFinder 
    {
        private CodeIssueType IssueType = CodeIssueType.CodeSmell;
        private string IssueMessage = "A Unit Test should have at least one implicit or explicit assertion";
        private List<string> _expectedExceptionAttributes = new List<string> { "ExpectedException", "ExpectedExceptionAttribute" };
        
        public void FindCodeIssues(CheckCodeIssuesEventArgs ea)
        {
            var resolveScope = ea.ResolveScope();
            foreach (IMethodElement element in resolveScope.GetElementEnumerator(ea.Scope,new ElementTypeFilter(LanguageElementType.Method)))
            {
                if (MethodHasIssue(element))
                {
                    foreach (TextRange range in element.Ranges)
                    {
                        ea.AddIssue(IssueType, (SourceRange)range, IssueMessage);
                    }
                }
            }
        }
        
        private bool MethodHasIssue(IMethodElement method)
        {
            if (!method.IsTestMethod())
                return false;

            var assert = GetAssert(method);
            var exceptionAttribute = GetExpectedExceptionAttribute(method);
            return assert == null && exceptionAttribute == null;
        }

        private static IElementReferenceExpression GetAssert(IMethodElement method)
        {
            return method.FindChildByName("Assert") as IElementReferenceExpression;
        }
        private IAttributeElement GetExpectedExceptionAttribute(IMethodElement method)
        {
            return method.Attributes.OfType<IAttributeElement>().FirstOrDefault(attr => _expectedExceptionAttributes.Contains(attr.Name)); 
        }
    }

    public static class LanguageElementExtensions
    {
        public static LanguageElement FindParentByElementType(this LanguageElement current, LanguageElementType parentType)
        {
            return current.GetParents().FirstOrDefault(p => p.ElementType == parentType);            
        }

        public static IEnumerable<LanguageElement> GetParents(this LanguageElement current)
        {
            var active = current;
            while (active.Parent != null)
            {
                active = active.Parent;
                yield return active;
            }
        }
    }
}

namespace CR_CreateTestMethod.LanguageElementChecks
{
    
    public static class LanguageElementCheckExtensions
    {
        private static List<string> _testClassAttributes = new List<string> { "TestFixture", "TestClass" };
        private static List<string> _testMethodAttributes = new List<string> { "Test", "TestMethod" };
        private static List<string> _setupMethodAttributes = new List<string> { "SetUp", "TestInitialize" };

        public static bool IsTestClass(this IClassElement currentClass)
        {
            return currentClass.Attributes.OfType<LanguageElement>().Count(a => _testClassAttributes.Contains(a.Name)) > 0;
        }

        public static bool HasSetupMethod(this Class currentClass)
        {
            return currentClass.Nodes.OfType<LanguageElement>().Count(n => n.ElementType == LanguageElementType.AttributeSection && _setupMethodAttributes.Contains(n.FirstDetail.Name)) > 0;
        }

        public static Method GetSetupMethod(this IClassElement currentClass)
        {
            if (!currentClass.IsTestClass())
                return null;
            var attributeSection = (AttributeSection)(((Class)currentClass).Nodes.OfType<LanguageElement>().First(n => n.ElementType == LanguageElementType.AttributeSection && _setupMethodAttributes.Contains(n.FirstDetail.Name)));
            return attributeSection.TargetNode as Method;
        }

        public static bool IsTestMethod(this IMethodElement currentMethod)
        {
            return currentMethod.Attributes.Cast<IAttributeElement>().Count(a => _testMethodAttributes.Contains(a.Name)) > 0;
        }
    }
}