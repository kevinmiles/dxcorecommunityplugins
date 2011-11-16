// <copyright file="SA1012TestCode.cs" company="ACME">
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
    /// Test code for SA1012 rule - opening curly bracket must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1012 rule.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "*", Justification = "This is about SA1012 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "This is about SA1012 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1013:ClosingCurlyBracketsMustBeSpacedCorrectly", Justification = "This is about SA1012 rule.")]
    public class SA1012TestCode
    {
        private int propertyName;

        public event EventHandler EventName1 = delegate { };

        public event EventHandler EventName2 = delegate {};

        public event EventHandler EventName
        {
            add{ this.EventName1 += value; }
            remove {this.EventName1 += value; }
        }

        public int PropertyName
        {
            get{ return this.propertyName; }
            set {this.propertyName = value; }
        }

        private void MethodName()
        {
            this.EventName1 += delegate{ throw new Exception(); };
            this.EventName2 += delegate {throw new Exception(); };
        }
    }
}
