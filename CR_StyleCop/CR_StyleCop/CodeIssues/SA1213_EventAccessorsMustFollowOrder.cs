namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1213_EventAccessorsMustFollowOrder : StyleCopRule
    {
        public SA1213_EventAccessorsMustFollowOrder()
            : base(new FirstTokenByTypeIssueLocator(CsTokenType.Remove))
        {
        }
    }
}
