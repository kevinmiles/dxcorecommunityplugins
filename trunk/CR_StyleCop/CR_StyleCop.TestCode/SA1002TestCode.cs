// <copyright file="SA1002TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System
    ;

    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1002 rule - semicolons must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1002 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1013:ClosingCurlyBracketsMustBeSpacedCorrectly", Justification = "This is about SA1002 rule.")]
    public class SA1002TestCode
    {
        private EventHandler handler ;
        private EventHandler handler2;

        public SA1002TestCode()
        {
            this.handler = delegate { this.DoNothing();};
            this.handler2 = delegate { this.DoNothing(); } ;
        }

        private void DoNothing()
        {
        }
    }
}
