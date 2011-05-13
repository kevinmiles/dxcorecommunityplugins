// <copyright file="SA1300TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.testCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 67

    /// <summary>
    /// Test code for SA1300 rule - elements' names must begin with uppercase letter.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1300 rule.")]
    public class SA1300TestCode
    {
        private int propertyNameBackend;

        public delegate void myEventHandler(object sender, EventArgs ea);

        public event EventHandler eventName;

        public enum myEnum
        {
            FirstElement
        }

        public int propertyName
        {
            get { return this.propertyNameBackend; }
            set { this.propertyNameBackend = value; }
        }

        public int propertyName2 { get; set; }

        private void methodName()
        {
        }

        public struct myStruct
        {
        }

        public class myClass
        {
            public myClass()
            {
            }
        }
    }
}
