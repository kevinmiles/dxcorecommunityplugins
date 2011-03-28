namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1006_PreprocessorKeywordsMustNotBePrecededBySpace : StyleCopRule
    {
        public SA1006_PreprocessorKeywordsMustNotBePrecededBySpace()
            : base(new PreprocessorDirectiveIssueLocator())
        {
        }
    }
}
