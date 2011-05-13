// <copyright file="SA1605TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <typeparam name="T">Generic parameter.</typeparam>
    public partial class SA1605TestCode<T>
    {
        // BUGBUG: SA1604 is reported instead of SA1605

        /// <param name="paramName">The parameter for method.</param>
        partial void Method(int paramName);
    }

    /// <typeparam name="T">Generic parameter.</typeparam>
    public partial class SA1605TestCode<T>
    {
        /// <param name="paramName">The parameter for method.</param>
        partial void Method(int paramName)
        {
        }
    }
}
