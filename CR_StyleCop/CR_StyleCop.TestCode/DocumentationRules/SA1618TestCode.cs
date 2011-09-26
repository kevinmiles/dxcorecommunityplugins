// <copyright file="SA1618TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1618 rule - generic parameters must be documented.
    /// </summary>
    public class SA1618TestCode<T>
    {
        /// <summary>
        /// Some method.
        /// </summary>
        public void MethodName<TT>()
        {
        }
    }
}
