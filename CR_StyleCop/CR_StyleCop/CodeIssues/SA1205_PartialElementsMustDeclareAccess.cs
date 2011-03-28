namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1205_PartialElementsMustDeclareAccess : StyleCopRule
    {
        public SA1205_PartialElementsMustDeclareAccess()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}
