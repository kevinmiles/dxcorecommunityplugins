// <copyright file="SA1014TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

#pragma warning disable 168
#pragma warning disable 169
#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1014 rule - opening generic bracket must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1014 rule.")]
    public class SA1014TestCode< T>
    {
        private List <int> ints1;
        private List< int> ints2;

        private void MethodName <T1>(T1 x)
        {
            List <int> ints11;
            this.MethodName <T1>(x);
        }

        private void MethodName2< T1>(T1 x)
        {
            List< int> ints22;
            this.MethodName2< T1>(x);
        }

        public struct MyStruct <T1>
        {
        }
    }
}
