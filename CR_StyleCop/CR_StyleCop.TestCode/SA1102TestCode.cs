// <copyright file="SA1102TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Linq;

    /// <summary>
    /// Test code for SA1102Test rule - query clauses must not be separated by blank lines.
    /// </summary>
    public class SA1102TestCode
    {
        private void MethodName()
        {
            var ints = Enumerable.Range(1, 10);
            var odd = from i in ints
                      
                      where i % 2 == 0
                      
                      select i;

            var odd2 = from i in ints

                      where i % 2 == 0 select i;
        }
    }
}
