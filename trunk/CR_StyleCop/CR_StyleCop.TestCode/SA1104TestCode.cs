// <copyright file="SA1104TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Linq;

    /// <summary>
    /// Test code for SA1104 rule - query clause must be on new line when previous clause spans multiple lines.
    /// </summary>
    public class SA1104TestCode
    {
        private void MethodName()
        {
            var ints = Enumerable.Range(1, 10);
            var odd = from i
                          in ints where i % 2 == 0
                      select i;

            var odd2 = from i in ints
                       where i % 2 
                        == 0 select i;
        }
    }
}
