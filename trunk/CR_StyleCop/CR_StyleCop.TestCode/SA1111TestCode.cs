// <copyright file="SA1111TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Test code for SA1111 rule - closing paren must be on last parameter line.
    /// </summary>
    public class SA1111TestCode
    {
        private int this[int x
            ]
        {
            get
            {
                return this[x
                    ];
            }
        }

        private string JoinStrings(string first, string last
            )
        {
            return this.JoinStrings(first, last
                );
        }
    }
}
