// <copyright file="SA1114TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Test code for SA1114 rule - parameters must be on the same or on the next line as opening paren.
    /// </summary>
    public class SA1114TestCode
    {
        private int this[

            int x]
        {
            get
            {
                return this[

                    x];
            }
        }

        private string JoinStrings(

            string first)
        {
            return this.JoinStrings(

                first);
        }
    }
}