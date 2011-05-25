// <copyright file="SA1307TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1307 rule.")]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "*", Justification = "This is about SA1307 rule.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1303:ConstFieldNamesMustBeginWithUpperCaseLetter", Justification = "This is about SA1307 rule.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "This is about SA1307 rule.")]
    public class SA1307TestCode
    {
        public const int invalid1 = 1;
        public readonly int invalid7 = 1;
        public static int invalid4 = 1;
        public int invalid10 = 1;

        internal const int invalid2 = 1;
        internal readonly int invalid8 = 1;
        internal static int invalid5 = 1;
        internal int invalid11 = 1;

        protected internal const int invalid3 = 1;
        protected internal readonly int invalid9 = 1;
        protected internal static int invalid6 = 1;
        protected internal int invalid12 = 1;
    }
}
