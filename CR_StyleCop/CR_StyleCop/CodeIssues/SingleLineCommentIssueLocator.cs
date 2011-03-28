namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;
    
    internal class SingleLineCommentIssueLocator : FirstTokenByTypeIssueLocator
    {
        public SingleLineCommentIssueLocator()
            : base(CsTokenType.SingleLineComment)
        {
        }
    }
}
