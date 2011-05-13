// <copyright file="SA1621TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1621 rule - typeparam tag must specify parameter name.
    /// </summary>
    /// <typeparam>Generic parameter.</typeparam>
    public class SA1621TestCode<T>
    {
        /// <summary>
        /// Some method.
        /// </summary>
        /// <typeparam>Generic parameter.</typeparam>
        public void MethodName<TM>()
        {
        }
    }
}
