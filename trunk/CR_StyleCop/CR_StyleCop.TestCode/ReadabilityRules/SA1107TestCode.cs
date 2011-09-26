// <copyright file="SA1107TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 219

    /// <summary>
    /// Test code for SA1107 rule - code must not contain multiple statements on single line.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1107 rule.")]
    public class SA1107TestCode
    {
        private int MethodName()
        {
            int x = 3; int y = 4;
            int xx = 7, yy = 33;
            x = xx; return yy;
        }
    }
}
