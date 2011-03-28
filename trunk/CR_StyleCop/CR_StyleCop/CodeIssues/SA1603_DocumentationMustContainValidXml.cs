namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1603_DocumentationMustContainValidXml : StyleCopRule
    {
        public SA1603_DocumentationMustContainValidXml()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}
