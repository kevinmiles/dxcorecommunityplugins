// <copyright file="SA1645TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Linq;

#pragma warning disable 1589

    /// <summary>
    /// Test code for SA1645 rule - include tag must contain valid path.
    /// </summary>
    public class SA1645TestCode
    {
        /// <include file='Properties\CR_StyleCop.TestCodeXXX.XML' path='[@name=""]'/>
        public SA1645TestCode()
        {
        }
    }
}
