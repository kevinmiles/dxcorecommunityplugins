// <copyright file="SA1116TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Test code for SA1116 rule - split parameters must begin on line after declaration.
    /// </summary>
    public class SA1116TestCode
    {
        private int this[int x,
            int y]
        {
            get
            {
                return this[x,
                    y];
            }
        }

        private string JoinStrings(string first,
            string last)
        {
            return this.JoinStrings(first,
                last);
        }
    }
}
