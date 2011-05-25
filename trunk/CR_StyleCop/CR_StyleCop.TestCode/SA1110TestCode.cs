// <copyright file="SA1110TestCode.cs" company="ACME">
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
    /// Test code for SA1110 rule - opening paren must be on declaration line.
    /// </summary>
    public class SA1110TestCode
    {
        private int this
            [int x]
        {
            get 
            { 
                return this
                    [x]; 
            }
        }

        private string JoinStrings
            (string first, string last)
        {
            return this.JoinStrings
                (first, last);
        }
    }
}