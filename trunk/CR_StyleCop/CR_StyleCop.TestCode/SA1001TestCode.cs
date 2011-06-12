// <copyright file="SA1001TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1001 rule - commas must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules","*" ,Justification = "This is about SA1001 rule.")]
    public class SA1001TestCode<T1,T2>
    {
        private int field1,field2;

        private void MethodName(int paramName , int paramName2)
        {
            this.MethodName(1,1);
            this.MethodName(1 ,1);
            this.MethodName(1 , 1);
            this.MethodName(1, 1);
        }
    }
}
