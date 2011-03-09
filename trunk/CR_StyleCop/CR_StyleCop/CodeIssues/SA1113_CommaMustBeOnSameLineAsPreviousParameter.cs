namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1113_CommaMustBeOnSameLineAsPreviousParameter : KeywordCodeIssue
    {
        public SA1113_CommaMustBeOnSameLineAsPreviousParameter()
            : base(Underline.FirstKeywordOnLine, ",")
        {
        }
    }
}
