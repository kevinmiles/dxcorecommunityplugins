namespace CR_StyleCop.CodeIssues
{
    using System;
    using DevExpress.CodeRush.StructuralParser;

    internal class SA1406_DebugFailMustProvideMessageText : StyleCopRule
    {
        public SA1406_DebugFailMustProvideMessageText()
            : base(new MethodCallIssueLocator("Fail", QualifyParameters))
        {
        }

        private static bool QualifyParameters(MethodCall methodCall)
        {
            if (methodCall.ArgumentsCount < 1)
                return true;

            var doubleQuotesExpression = methodCall.Arguments[0] as PrimitiveExpression;
            if (doubleQuotesExpression != null
                && doubleQuotesExpression.PrimitiveType == PrimitiveType.String
                && (doubleQuotesExpression.Name == "\"\"" || doubleQuotesExpression.Name == "@\"\""))
                return true;

            var nullExpression = methodCall.Arguments[0] as PrimitiveExpression;
            if (nullExpression != null
                && nullExpression.PrimitiveType == PrimitiveType.Void
                && nullExpression.Name == "null")
                return true;

            var stringEmptyExpression = methodCall.Arguments[0] as ElementReferenceExpression;
            if (stringEmptyExpression != null
                && stringEmptyExpression.Name == "Empty"
                && stringEmptyExpression.Qualifier != null
                && stringEmptyExpression.Qualifier.Name.ToLowerInvariant() == "string")
                return true;

            return false;
        }
    }
}
