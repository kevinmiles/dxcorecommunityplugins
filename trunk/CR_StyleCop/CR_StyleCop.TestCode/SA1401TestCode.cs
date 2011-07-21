// <copyright file="SA1401TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 649
#pragma warning disable 169
#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1401 rule - fields must be private.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1401 rule.")]
    public class SA1401TestCode
    {
        public static readonly string PublicStaticReadonlyField;
        public readonly string PublicReadonlyField;
        public static string PublicStaticField;
        public string PublicField;

        internal static readonly string InternalStaticReadonlyField;
        internal readonly string InternalReadonlyField;
        internal static string InternalStaticField;
        internal string InternalField;

        protected internal static readonly string ProtectedInternalStaticReadonlyField;
        protected internal readonly string ProtectedInternalReadonlyField;
        protected internal static string ProtectedInternalStaticField;
        protected internal string ProtectedInternalField;

        protected static readonly string ProtectedStaticReadonlyField;
        protected readonly string ProtectedReadonlyField;
        protected static string protectedStaticField;
        protected string protectedField;

        private static readonly string privateStaticReadonlyField;
        private readonly string privateReadonlyField;
        private static string privateStaticField;
        private string privateField;

        public struct Inner
        {
            public static readonly string PublicStaticReadonlyField;
            public readonly string PublicReadonlyField;
            public static string PublicStaticField;
            public string PublicField;

            internal static readonly string InternalStaticReadonlyField;
            internal readonly string InternalReadonlyField;
            internal static string InternalStaticField;
            internal string InternalField;

            private static readonly string privateStaticReadonlyField;
            private readonly string privateReadonlyField;
            private static string privateStaticField;
            private string privateField;
        }
    }
}