// <copyright file="SA1626TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1626 rule - documentation must not use three slashes.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1626 rule")]
    public class SA1626TestCode
    {
        private int propertyName;
        private EventHandler eventName;

        public SA1626TestCode()
        {
            /// Wrong comment.
        }

        ~SA1626TestCode()
        {
            /// Wrong comment
        }

        public event EventHandler EventName
        {
            add
            {
                /// 1. Wrong comment
                this.eventName = (EventHandler)Delegate.Combine(this.eventName, value);
            }

            remove
            {
                /// 2. Wrong comment
                this.eventName = (EventHandler)Delegate.Remove(this.eventName, value);
            }
        }

        public int PropertyName
        {
            get
            {
                /// 3. Wrong comment
                return this.propertyName;
            }

            set
            {
                /// 4. Wrong comment
                this.propertyName = value;
            }
        }

        public int this[int index]
        {
            get
            {
                /// 5. Wrong comment.
                return 42;
            }

            set
            {
                /// 6. Wrong comment.
            }
        }

        public void MethodName()
        {
            /// 7. Wrong comment
        }
    }
}
