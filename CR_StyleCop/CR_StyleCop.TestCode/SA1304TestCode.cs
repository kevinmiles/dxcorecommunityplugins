// <copyright file="SA1304TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 414

    /// <summary>
    /// Test code for SA1304 rule - non private readonly fields must be upper cased.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1304 rule.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter", Justification = "This is about SA1304 rule.")]
    public class SA1304TestCode
    {
        public readonly int ultimateAnswer2 = 42;

        internal readonly int ultimateAnswer3 = 42;

        protected internal readonly int ultimateAnswer4 = 42;

        protected readonly int ultimateAnswer5 = 42;

        private readonly int ultimateAnswer = 42;
    }
}
