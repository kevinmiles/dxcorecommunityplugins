namespace CR_StyleCop
{
    using System;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    
    internal class StyleCopCodeIssue
    {
        private readonly CodeIssueType issueType;
        private readonly SourceRange range;

        public StyleCopCodeIssue(CodeIssueType issueType, SourceRange range)
        {
            this.issueType = issueType;
            this.range = range;
        }

        public CodeIssueType IssueType
        {
            get { return this.issueType; }
        }

        public SourceRange Range
        {
            get { return this.range; }
        }
    }
}
