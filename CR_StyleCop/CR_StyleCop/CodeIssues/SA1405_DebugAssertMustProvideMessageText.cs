namespace CR_StyleCop.CodeIssues
{
    using System;
    using DevExpress.CodeRush.StructuralParser;

    internal class SA1405_DebugAssertMustProvideMessageText : StyleCopRule
    {
        public SA1405_DebugAssertMustProvideMessageText()
            : base(new MethodCallIssueLocator("Assert", QualifyParameters))
        {
        }

        private static bool QualifyParameters(MethodCall methodCall)
        {
            if (methodCall.ArgumentsCount < 2)
                return true;

            var doubleQuotesExpression = methodCall.Arguments[1] as PrimitiveExpression;
            if (doubleQuotesExpression != null 
                && doubleQuotesExpression.PrimitiveType == PrimitiveType.String
                && doubleQuotesExpression.Name == "\"\"")
                return true;

            var nullExpression = methodCall.Arguments[1] as PrimitiveExpression;
            if (nullExpression != null
                && nullExpression.PrimitiveType == PrimitiveType.Void
                && nullExpression.Name == "null")
                return true;

            var stringEmptyExpression = methodCall.Arguments[1] as ElementReferenceExpression;
            if (stringEmptyExpression != null
                && stringEmptyExpression.Name == "Empty"
                && stringEmptyExpression.DetailNodeCount == 1
                && stringEmptyExpression.FirstDetail.Name.ToLowerInvariant() == "string")
                return true;

            return false;
        }
    }
}
