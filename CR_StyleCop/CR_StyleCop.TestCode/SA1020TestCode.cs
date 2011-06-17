// <copyright file="SA1020TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1020 rule - ++ and -- operators must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1020 rule.")]
    public class SA1020TestCode
    {
        // BUGBUG: only 2 of violations are reported.
        private int Method(int x)
        {
            x ++;
            ++ x;
            x --;
            -- x;
            return x;
        }

        private void Method()
        {
            int x = 0;
            this.Method(x ++);
            this.Method(++ x);
            this.Method(x --);
            this.Method(-- x);
        }
    }
}
