namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1401_FieldsMustBePrivate : StyleCopRule
    {
        public SA1401_FieldsMustBePrivate()
            : base(new FirstToLastTokenByTypeIssueLocator(CsTokenType.Public, CsTokenType.Internal, CsTokenType.Protected))
        {
        }
    }
}
