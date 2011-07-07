// <copyright file="SA1502TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1502 rule - elements with curly brackets must not be on single line.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1502 rule.")]
    public abstract class SA1502TestCode
    {
        private bool propertyName = true;

        internal interface InnerInterface { }

        internal bool PropertyName { get { return this.propertyName; } }

        internal abstract bool PropertyName2 { get; set; }

        private void MethodName() { }

        internal struct InnerStruct { }

        internal class InnerClass { }
    }
}
