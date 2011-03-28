namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1203_ConstantsMustAppearBeforeFields : StyleCopRule
    {
        public SA1203_ConstantsMustAppearBeforeFields()
            : base(new WrongElementOrderIssueLocator())
        {
        }
    }
}
