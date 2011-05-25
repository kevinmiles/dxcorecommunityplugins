// <copyright file="SA1201TestCode2.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1201 rule - class elements must be ordered correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1201 rule.")]
    public class SA1201TestCode2
    {
        public class InnerClass
        {
        }

        public struct InnerStruct
        {
        }

        private void MethodName()
        {
            this.EventName(this, EventArgs.Empty);
        }

        public int this[int i]
        {
            get
            {
                return 1;
            }
        }

        public int PropertyName
        {
            get { return this.propertyName; }
            set { this.propertyName = value; }
        }

        public int PropertyName2 { get; set; }

        public interface InnerInterface
        {
        }

        public enum InnerEnum
        {
            FirstElement
        }

        public event EventHandler EventName;

        public delegate void MyEventHandler(object sender, EventArgs ea);

        ~SA1201TestCode2()
        {
        }

        public SA1201TestCode2(int propertyName, int propertyName2)
        {
            this.propertyName = propertyName;
            this.PropertyName2 = propertyName2;
        }

        private int propertyName;
    }
}
