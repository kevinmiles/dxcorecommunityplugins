namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class QueryExpressionCodeIssue : KeywordCodeIssue
    {
        public QueryExpressionCodeIssue(Underline underline)
            : base(underline, "from", "where", "select", "let", "group", "orderby", "join")
        {
        }    
    }
}
