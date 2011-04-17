namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1512 rule - Single line comments must not be followed by blank line.
    /// </summary>
    public class SA1512TestCode
    {
        // This is invalid comment.

        private void MethodName()
        {
            // This is valid comment.

            // This is continuation of valid comment.
        }
    }
}
