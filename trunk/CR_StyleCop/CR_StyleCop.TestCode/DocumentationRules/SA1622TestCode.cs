// <copyright file="SA1622TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1622 rule - generic parameter documentation must have text.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SA1622TestCode<T>
    {
        /// <summary>
        /// Some method.
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        public void MethodName<TM>()
        {
        }
    }
}
