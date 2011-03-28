namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1207_ProtectedMustComeBeforeInternal : StyleCopRule
    {
        public SA1207_ProtectedMustComeBeforeInternal()
            : base(new FirstToLastTokenByTypeIssueLocator(CsTokenType.Protected, CsTokenType.Internal))
        {
        }
    }
}
