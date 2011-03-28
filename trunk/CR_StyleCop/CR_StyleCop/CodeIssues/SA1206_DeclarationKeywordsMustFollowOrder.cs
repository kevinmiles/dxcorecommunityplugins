namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1206_DeclarationKeywordsMustFollowOrder : StyleCopRule
    {
        private static CsTokenType[] keywords = new[]
            {
                CsTokenType.Public,
                CsTokenType.Protected,
                CsTokenType.Internal,
                CsTokenType.Private,
                CsTokenType.Static,
                CsTokenType.Virtual,
                CsTokenType.Abstract,
                CsTokenType.Override,
                CsTokenType.New,
                CsTokenType.Sealed,
                CsTokenType.Volatile,
                CsTokenType.Const,
                CsTokenType.Readonly,
                CsTokenType.Partial,
                CsTokenType.Extern,
                CsTokenType.Event,
                CsTokenType.Delegate,
                CsTokenType.Unsafe,
                CsTokenType.Explicit,
                CsTokenType.Implicit,
                CsTokenType.Operator,
            };

        public SA1206_DeclarationKeywordsMustFollowOrder()
            : base(new FirstToLastTokenByTypeIssueLocator(keywords))
        {
        }
    }
}
