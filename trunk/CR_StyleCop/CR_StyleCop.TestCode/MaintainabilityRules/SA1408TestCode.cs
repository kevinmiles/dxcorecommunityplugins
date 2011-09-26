// <copyright file="SA1408TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1408 rule - conditional expressions must declare precedence.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1408 rule.")]
    public class SA1408TestCode
    {
        private bool Invalid()
        {
            bool i = true && true || false;
            i = true || true && false;
            return i;
        }

        private bool Valid()
        {
            bool i = true && true && false;
            i = true || true || false;
            return i;
        }
    }
}