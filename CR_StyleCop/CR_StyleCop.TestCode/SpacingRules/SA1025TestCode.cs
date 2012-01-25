// <copyright file="SA1025TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace  CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using  System.Linq;

#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1025 rule - multiple spaces are bad.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification  = "This is about SA1025 rule.")] // not reported
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1107:CodeMustNotContainMultipleStatementsOnOneLine", Justification =  "This is about SA1025 rule.")]
    public class SA1025TestCode
    {
        private int x,  y; // not reported

        public SA1025TestCode()
            :  base()
        {
        }

        private void MethodName(int x,  int y, bool condition)
        {
            this.x = x;  this.y = y; // not reported
            x  = y; // not reported
            x =  y;
            y = condition ?  x :  y;
            y = condition  ? x  : y; // not reported
            x = 3       + 6; // not reported
            y = 3 +       6;
        }
    }
}
