// <copyright file="SA1105TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1105 rule - query clause must begin on its own line when spans multiple lines.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1105 rule.")]
    public class SA1105TestCode
    {
        private void MethodName()
        {
            var ints = Enumerable.Range(1, 10);
            var odd = from i in ints where i % 2 
                          == 0
                      select i;

            var odd2 = from i in ints where i % 2 == 0 select 
                           i;
        }
    }
}
