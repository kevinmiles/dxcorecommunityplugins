// <copyright file="SA1504TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1504 rule - all accessors must be single line or multi line.
    /// </summary>
    public class SA1504TestCode
    {
        private EventHandler event1;
        private EventHandler event2;
        private string property1;
        private string property2;

        internal event EventHandler Event1
        {
            add { this.event1 = (EventHandler)Delegate.Combine(this.event1, value); }
            remove
            {
                this.event1 = (EventHandler)Delegate.Remove(this.event1, value);
            }
        }

        internal event EventHandler Event2
        {
            add
            {
                this.event2 = (EventHandler)Delegate.Combine(this.event2, value);
            }

            remove { this.event2 = (EventHandler)Delegate.Remove(this.event2, value); }
        }

        internal string Property1
        {
            get { return this.property1; }
            set
            {
                this.property1 = value;
            }
        }

        internal string Property2
        {
            get
            {
                return this.property2;
            }

            set { this.property2 = value; }
        }
    }
}
