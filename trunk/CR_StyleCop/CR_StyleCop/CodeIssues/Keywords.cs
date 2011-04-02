namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal static class Keywords
    {
        public static readonly CsTokenType[] QueryExpression = new[] { 
            CsTokenType.From, 
            CsTokenType.Where, 
            CsTokenType.Select, 
            CsTokenType.Let, 
            CsTokenType.Group, 
            CsTokenType.OrderBy, 
            CsTokenType.Join };
    }
}
