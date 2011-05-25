// <copyright file="SA1204TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1204 rule - static members must come first.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1207 rule.")]
    public class SA1204TestCode
    {
        private string regularProperty;
        private static string staticProperty;

        public SA1204TestCode()
        {
        }

        static SA1204TestCode()
        {
            staticProperty = "Foo";
        }

        public event EventHandler RegularEvent;

        public static event EventHandler StaticEvent;

        public string RegularProperty
        {
            get { return this.regularProperty; }
            set { this.regularProperty = value; }
        }

        public static string StaticProperty
        {
            get { return staticProperty; }
            set { staticProperty = value; }
        }

        public virtual void OnRegularEvent(object sender, EventArgs e)
        {
            EventHandler handler = this.RegularEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public void MethodName()
        {
        }

        public static void MethodName(string x)
        {
        }

        public static void OnStaticEvent(object sender, EventArgs e)
        {
            EventHandler handler = StaticEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
