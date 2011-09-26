// <copyright file="SA1108TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1108 rule - block statements must not contain embedded comments.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1107 rule.")]
    public class SA1108TestCode
    {
        private void MethodName(int x, int y)
        {
            if (x != y)
            //// Make sure x does not equal y
            {
            }
            else
            //// Another wrongly placed comment
            {
                x++;
            }

            while (x != y)
            //// Execute while x does not equal y
            {
            }
        }
    }
}
