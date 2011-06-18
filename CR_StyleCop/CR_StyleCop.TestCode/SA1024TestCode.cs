// <copyright file="SA1024TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1024 rule - colons must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1024 rule.")]
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1107:CodeMustNotContainMultipleStatementsOnOneLine", Justification = "This is about SA1024 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1003:SymbolsMustBeSpacedCorrectly", Justification = "This is about SA1024 rule.")]
    public class SA1024TestCode
    {
        private void MethodName<T>(T param, int x)
            where T: class
        {
            switch (x)
            {
                case 0:
                    break;
                case 1: break;
                case 2:break;
                case 3 : break;
                case 4 :break;
                case 5 :
                    break;
            }
        }

        private void MethodName<T>(T param)
            where T :class
        {
        myLabel1:
            goto myLabel2;
        myLabel2 :
            goto myLabel3;
        myLabel3: goto myLabel4;
        myLabel4 : goto myLabel5;
        myLabel5:goto myLabel6;
        myLabel6 :goto myLabel1;
        }

        private void MethodName<T>(T par, bool condition)
            where T : class
        {
            par = condition ? null : par;
            par = condition ? null: par;
            par = condition ? null :par;
            par = condition ? null 
                : par;
            par = condition ? null 
                :par;
            par = condition ? null : 
                par;
            par = condition ? null:
                par;
        }

        public class Inner1<T>: SA1024TestCode
            where T: class
        {
        }

        public class Inner2<T> :SA1024TestCode
            where T :class
        {
        }

        public class Inner3<T> : SA1024TestCode 
            where T : class
        {
        }
    }
}
