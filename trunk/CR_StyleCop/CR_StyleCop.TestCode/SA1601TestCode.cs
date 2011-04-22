namespace CR_StyleCop.TestCode
{
    using System;

    public partial class SA1601TestCode
    {
        partial void MethodName();
    }

    public partial class SA1601TestCode
    {
        // BUGBUG: SA1600 reported instead of SA1601 for partial methods.
        partial void MethodName()
        {
        }
    }
}
