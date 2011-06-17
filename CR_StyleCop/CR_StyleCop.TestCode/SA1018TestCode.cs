// <copyright file="SA1018TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    
#pragma warning disable 169
#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1018 rule - nullable symbol must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1018 rule.")]
    public class SA1018TestCode
    {
        private int ? property;
        private List<int ?> ints;

        public int ? Property
        {
            get { return this.property; }
            set { this.property = value; }
        }

        private int ? Method<T>(T parameter)
        {
            long ? x = null;
            return this.Method<long ?>(x);
        }

        private void MethodName(DateTime ? date)
        {
        }
    }
}
