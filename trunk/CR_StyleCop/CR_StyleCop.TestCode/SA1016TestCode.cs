// <copyright file="SA1016TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Test code for SA1016 rule - opening attribute bracket must be spaced correctly.
    /// </summary>
    [ SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1016 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:OpeningParenthesisMustBeSpacedCorrectly", Justification = "This is about SA1016 rule.")]
    public class SA1016TestCode
    {
        [ return: MarshalAs(UnmanagedType.Error)]
        private int MethodName([ Optional]int x)
        {
            return 42;
        }
    }
}
