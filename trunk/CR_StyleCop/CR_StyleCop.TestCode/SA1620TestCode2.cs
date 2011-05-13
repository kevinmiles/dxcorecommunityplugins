// <copyright file="SA1620TestCode2.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1620 rule - comments must match generic parameter list.
    /// </summary>
    /// <typeparam name="T2">Should be second.</typeparam>
    /// <typeparam name="T1">Should be first.</typeparam>
    public class SA1620TestCode2<T1, T2>
    {
        /// <summary>
        /// Some method.
        /// </summary>
        /// <typeparam name="TM2">Should be second.</typeparam>
        /// <typeparam name="TM1">Should be first.</typeparam>
        public void MethodName<TM1, TM2>()
        {
        }
    }
}
