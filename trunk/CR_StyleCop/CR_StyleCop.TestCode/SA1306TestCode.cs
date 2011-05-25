// <copyright file="SA1306TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 169
#pragma warning disable 414
#pragma warning disable 649

    /// <summary>
    /// Test code for SA1306 rule - field names must begin with lower case.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1306 rule.")]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "*", Justification = "This is about SA1306 rule.")]
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "*", Justification = "This is about SA1306 rule.")]
    public class SA1306TestCode
    {
        public const int Valid1 = 1;
        internal const int Valid2 = 1;
        protected internal const int Valid3 = 1;
        protected const int Valid4 = 1;
        private const int Valid5 = 1;

        public readonly int Valid9 = 1;
        internal readonly int Valid10 = 1;
        protected internal readonly int Valid11 = 1;
        protected readonly int Valid12 = 1;
        private readonly int Valid13 = 1;

        public int Valid6;
        internal int Valid7;
        protected internal int Valid8;

        protected int Invalid1;
        private int Invalid2;
    }
}
