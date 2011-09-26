// <copyright file="SA1013TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1013 rule - closing curly bracket must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1013 rule.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "*", Justification = "This is about SA1013 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1002:SemicolonsMustBeSpacedCorrectly", Justification = "This is about SA1013 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1012:OpeningCurlyBracketsMustBeSpacedCorrectly", Justification = "This is about SA1013 rule.")]
    public class SA1013TestCode
    {
        public event EventHandler EventName1 = delegate {};

        public event EventHandler EventName2 = delegate { } ;

        private void MethodName()
        {
            this.EventName1 += delegate { throw new Exception();};
            this.EventName2 += delegate { throw new Exception(); } ;
        }}
}
