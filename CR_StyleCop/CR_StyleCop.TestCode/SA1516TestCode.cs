// <copyright file="SA1516TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1516 rule - elements must be separated by blank line.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "This is about SA1516 rule.")]
    public class SA1516TestCode
    {
        private int propertyName;
        private SA1516TestCode()
        {
        }
        private event EventHandler EventName
        {
            add
            {
            }
            remove
            {
            }
        }
        private interface IMyInterface
        {
        }
        private int PropertyName { get; set; }
        private int PropertyName2
        {
            get
            {
                return this.propertyName;
            }
            set
            {
                this.propertyName = value;
            }
        }
        private void MethodName()
        {
        }
        private struct MyStruct
        {
        }
        private class MyClass
        {
        }
    }
}
