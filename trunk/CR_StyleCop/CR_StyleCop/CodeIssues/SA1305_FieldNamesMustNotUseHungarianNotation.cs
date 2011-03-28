namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1305_FieldNamesMustNotUseHungarianNotation : StyleCopRule
    {
        public SA1305_FieldNamesMustNotUseHungarianNotation()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}
