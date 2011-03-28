namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1212_PropertyAccessorsMustFollowOrder : StyleCopRule
    {
        public SA1212_PropertyAccessorsMustFollowOrder()
            : base(new FirstTokenByTypeIssueLocator(CsTokenType.Set))
        {
        }
    }
}
