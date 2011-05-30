// <copyright file="SA1100TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1100 rule - don't use base prefix unnecessary.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1100 rule.")]
    public class SA1100TestCode
    {
        private string propertyName;
        private string propertyName2;

        public virtual string PropertyName
        {
            get { return this.propertyName; }
            set { this.propertyName = value; }
        }

        public virtual string PropertyName2
        {
            get { return this.propertyName2; }
            set { this.propertyName2 = value; }
        }

        public virtual void BaseMethod()
        {
        }

        public virtual void BaseMethod2()
        {
        }

        public class Derived : SA1100TestCode
        {
            public override string PropertyName
            {
                get { return base.PropertyName; }
                set { base.PropertyName = value; }
            }

            public override void BaseMethod()
            {
                base.BaseMethod();
            }

            private void Test()
            {
                base.BaseMethod();
                base.BaseMethod2();
                base.PropertyName = "a";
                base.PropertyName2 = "b";
            }
        }
    }
}
