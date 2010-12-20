namespace CR_StyleCop.CodeIssues
{
    using System;
    using DevExpress.CodeRush.StructuralParser;

    internal class SA1406_DebugFailMustProvideMessageText : MethodCallCodeIssue
    {
        public SA1406_DebugFailMustProvideMessageText()
            : base("Fail", QualifyParameters)
        {
        }

        private static bool QualifyParameters(MethodCall methodCall)
        {
            if (methodCall.ArgumentsCount < 1)
                return true;

            PrimitiveExpression doubleQuotesExpression = methodCall.Arguments[0] as PrimitiveExpression;
            if (doubleQuotesExpression != null
                && doubleQuotesExpression.PrimitiveType == PrimitiveType.String
                && doubleQuotesExpression.Name == "\"\"")
                return true;

            PrimitiveExpression nullExpression = methodCall.Arguments[0] as PrimitiveExpression;
            if (nullExpression != null
                && nullExpression.PrimitiveType == PrimitiveType.Void
                && nullExpression.Name == "null")
                return true;

            ElementReferenceExpression stringEmptyExpression = methodCall.Arguments[0] as ElementReferenceExpression;
            if (stringEmptyExpression != null
                && stringEmptyExpression.Name == "Empty"
                && stringEmptyExpression.DetailNodeCount == 1
                && stringEmptyExpression.FirstDetail.Name.ToLowerInvariant() == "string")
                return true;

            return false;
        }
    }
}
