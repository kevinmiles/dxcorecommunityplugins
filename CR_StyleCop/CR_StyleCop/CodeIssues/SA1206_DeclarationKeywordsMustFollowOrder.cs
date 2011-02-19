namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1206_DeclarationKeywordsMustFollowOrder : KeywordCodeIssue
    {
        private static string[] keywords = new[]
            {
                "public",
                "protected",
                "internal",
                "private",
                "static",
                "virtual",
                "abstract",
                "override",
                "new",
                "sealed",
                "volatile",
                "const",
                "readonly",
                "partial",
                "extern",
                "event",
                "delegate",
                "unsafe",
                "explicit",
                "implicit",
                "operator",
            };

        public SA1206_DeclarationKeywordsMustFollowOrder()
            : base(Underline.SpanFromFirstToLastKeywordOnLine, keywords)
        {
        }
    }
}
