// <copyright file="SA1407TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1407 rule - arithmetic expressions must declare precedence.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1407 rule.")]
    public class SA1407TestCode
    {
        private int Invalid()
        {
            int i = 3 + 5 * 8;
            i = 3 + 5 >> 2;
            i = 30 % 4 >> 3;
            i = 6 * 7 % 4;
            return i;
        }

        private int Valid()
        {
            int i = 3 + 6 - 4;
            i = 6 * 7 / 5;
            i = 4 >> 2 << 4;
            return i;
        }
    }
}