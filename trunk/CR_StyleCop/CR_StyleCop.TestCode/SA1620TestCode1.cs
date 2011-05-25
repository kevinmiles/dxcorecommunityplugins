// <copyright file="SA1620TestCode1.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1620 rule - comments must match generic parameter list.
    /// </summary>
    /// <typeparam name="T">T parameter.</typeparam>
    /// <typeparam name="S">Additional parameter.</typeparam>
    public class SA1620TestCode1<T>
    {
        /// <summary>
        /// Some method.
        /// </summary>
        /// <typeparam name="TM">TM parameter.</typeparam>
        /// <typeparam name="TM2">Additional parameter.</typeparam>
        private void MethodName<TM>()
        {
        }
    }
}
