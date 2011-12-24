namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class SA1005_SingleLineCommentsMustBeginWithSingleSpace : StyleCopRule
    {
        public SA1005_SingleLineCommentsMustBeginWithSingleSpace()
            : base(new SingleLineCommentIssueLocator())
        {
        }
    }
}
