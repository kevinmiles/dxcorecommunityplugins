// <copyright file="SA1604TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <content>
    /// Test code for SA1604 rule - documentation must have summary tag.
    /// </content>
    public class SA1604TestCode
    {
        /// <value>
        /// Some unknown value.
        /// </value>
        public int PropertyName { get; set; }

        /// <param name="paramName">The parameter.</param>
        public void MethodName(int paramName)
        {
        }
    }
}
