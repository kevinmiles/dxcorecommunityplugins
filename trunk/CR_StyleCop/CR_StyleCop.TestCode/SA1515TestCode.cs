namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1515 rule - single line comments must be preceded by blank line.
    /// </summary>
    public class SA1515TestCode
    {
        // Valid comment.
        // Another valid comment.
        private int MethodName()
        {
            int varName = 42;
            // Invalid comment.
            return varName;
        }
        // Another invalid comment.
    }
}
