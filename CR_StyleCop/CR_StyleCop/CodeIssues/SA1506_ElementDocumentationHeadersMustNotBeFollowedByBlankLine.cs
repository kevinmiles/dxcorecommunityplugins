namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1506_ElementDocumentationHeadersMustNotBeFollowedByBlankLine : StyleCopRule
    {
        public SA1506_ElementDocumentationHeadersMustNotBeFollowedByBlankLine()
            : base(new LastXmlDocCommentIssueLocator())
        {
        }
    }
}
