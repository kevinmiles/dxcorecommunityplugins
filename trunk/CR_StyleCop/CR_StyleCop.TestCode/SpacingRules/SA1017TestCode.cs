// <copyright file="SA1017TestCode.cs" company="ACME">
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
    /// Test code for SA1017 rule - closing attribute bracket must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1017 rule.") ]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "This is about SA1017 rule.")]
    public class SA1017TestCode
    {
        [return: MarshalAs(UnmanagedType.Error) ]
        private int MethodName([Optional ]int x)
        {
            return 42;
        }
    }
}
