// <copyright file="SA1647TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Linq;

#pragma warning disable 1590

    /// <summary>
    /// Test code for SA1647 rule - include tag must contain valid xpath.
    /// </summary>
    public class SA1647TestCode
    {
        /// <include />
        public SA1647TestCode()
        {
        }

        /// <include file='Properties\CR_StyleCop.TestCode.XML' />
        public SA1647TestCode(int paramName)
        {
        }

        /// <include path='[@name=""]'/>
        public SA1647TestCode(string paramName)
        {
        }
    }
}
