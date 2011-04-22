namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// </summary>
    public partial class SA1607TestCode
    {
        // BUGBUG: SA1606 is reported instead of SA1607

        /// <summary>
        /// </summary>
        partial void Method();
    }

    /// <content>
    /// </content>
    public partial class SA1607TestCode
    {
        /// <content>
        /// </content>
        partial void Method()
        {
        }
    }
}
