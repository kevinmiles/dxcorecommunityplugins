// <copyright file="SA1410TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1410 rule - remove redundant parens from anonymous methods.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1410 rule.")]
    internal class SA1410TestCode
    {
        private bool MethodName(Func<bool> predicate)
        {
            return predicate();
        }

        private bool Invalid()
        {
            return this.MethodName(delegate() { return true; });
        }

        private bool Valid()
        {
            return this.MethodName(delegate { return true; });
        }

        private bool Valid2()
        {
            return this.MethodName(() => true);
        }
    }
}