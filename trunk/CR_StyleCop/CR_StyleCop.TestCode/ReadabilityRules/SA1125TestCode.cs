// <copyright file="SA1125TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1125 rule - use shorthand for nullable types.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1121 rule.")]
    public class SA1125TestCode : List<Nullable<int>>
    {
        private Nullable<int> intProperty;

        private Nullable<DateTime> Date { get; set; }

        private List<Nullable<DateTime>> Dates { get; set; }

        private Nullable<int> IntProperty
        {
            get { return this.intProperty; }
            set { this.intProperty = value; }
        }

        private Type MethodName(Nullable<long> parameter)
        {
            Nullable<long> variable = parameter;
            return typeof(Nullable<>);
        }
    }
}
