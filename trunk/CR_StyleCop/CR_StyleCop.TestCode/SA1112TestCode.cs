namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Test code for SA1112 rule - closing paren must be on the same line as opening paren when there are no parameters.
    /// </summary>
    public class SA1112TestCode
    {
        private string JoinStrings(
            )
        {
            return this.JoinStrings(
                );
        }
    }
}
