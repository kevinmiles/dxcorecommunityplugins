// <copyright file="SA1646TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Linq;

#pragma warning disable 1589

    /// <summary>
    /// Test code for SA1646 rule - include tag must contain valid xpath.
    /// </summary>
    public class SA1646TestCode
    {
        /// <include file='Properties\CR_StyleCop.TestCode.XML' path='[@name=""]'/>
        public SA1646TestCode()
        {
        }
    }
}
