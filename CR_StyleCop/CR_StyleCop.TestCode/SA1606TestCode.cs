// <copyright file="SA1606TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "This is about SA1606 rule.")]
    public class SA1606TestCode
    {
        /// <summary>
        /// </summary>
        public int PropertyName { get; set; }

        /// <summary>
        /// </summary>
        public void MethodName()
        {
        }
    }
}
