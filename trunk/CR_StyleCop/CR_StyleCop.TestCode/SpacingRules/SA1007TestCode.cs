// <copyright file="SA1007TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

#pragma warning disable 660
#pragma warning disable 661
#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1007 rule - operator overloads must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1007 rule.")]
    public class SA1007TestCode
    {
        public static bool operator==(SA1007TestCode left, SA1007TestCode right)
        {
            return true;
        }

        public static bool operator!=(SA1007TestCode left, SA1007TestCode right)
        {
            return false;
        }

        public static bool operator<=(SA1007TestCode left, SA1007TestCode right)
        {
            return false;
        }

        public static bool operator>=(SA1007TestCode left, SA1007TestCode right)
        {
            return false;
        }

        public static bool operator<(SA1007TestCode left, SA1007TestCode right)
        {
            return false;
        }

        public static bool operator>(SA1007TestCode left, SA1007TestCode right)
        {
            return false;
        }

        public static SA1007TestCode operator+(SA1007TestCode left, SA1007TestCode right)
        {
            return null;
        }

        public static SA1007TestCode operator-(SA1007TestCode left, SA1007TestCode right)
        {
            return null;
        }

        public static SA1007TestCode operator*(SA1007TestCode left, SA1007TestCode right)
        {
            return null;
        }

        public static SA1007TestCode operator/(SA1007TestCode left, SA1007TestCode right)
        {
            return null;
        }

        public static SA1007TestCode operator%(SA1007TestCode left, SA1007TestCode right)
        {
            return null;
        }

        public static SA1007TestCode operator&(SA1007TestCode left, SA1007TestCode right)
        {
            return null;
        }

        public static SA1007TestCode operator|(SA1007TestCode left, SA1007TestCode right)
        {
            return null;
        }

        public static SA1007TestCode operator>>(SA1007TestCode left, int right)
        {
            return null;
        }

        public static SA1007TestCode operator<<(SA1007TestCode left, int right)
        {
            return null;
        }

        public static SA1007TestCode operator+(SA1007TestCode left)
        {
            return left;
        }

        public static SA1007TestCode operator-(SA1007TestCode left)
        {
            return left;
        }

        public static SA1007TestCode operator!(SA1007TestCode left)
        {
            return left;
        }

        public static SA1007TestCode operator~(SA1007TestCode left)
        {
            return left;
        }

        public static SA1007TestCode operator++(SA1007TestCode left)
        {
            return left;
        }

        public static SA1007TestCode operator--(SA1007TestCode left)
        {
            return left;
        }

        public static bool operator true(SA1007TestCode left)
        {
            return false;
        }

        public static bool operator false(SA1007TestCode left)
        {
            return true;
        }
    }
}
