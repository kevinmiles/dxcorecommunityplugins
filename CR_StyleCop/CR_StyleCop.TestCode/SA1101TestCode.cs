// <copyright file="SA1101TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1101 rule - use this to indicate instance member access.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1100 rule.")]
    public class SA1101TestCode
    {
        public event EventHandler BasePublicEvent;

        internal event EventHandler BaseInternalEvent;
        
        protected internal event EventHandler BaseProtectedInternalEvent;
        
        protected event EventHandler BaseProtectedEvent;

        public string BasePublicProperty { get; set; }
        
        internal string BaseInternalProperty { get; set; }
        
        protected internal string BaseProtectedInternalProperty { get; set; }
        
        protected string BaseProtectedProperty { get; set; }

        public virtual void OnBasePublicEvent(EventArgs e)
        {
            var handler = this.BasePublicEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal virtual void OnBaseInternalEvent(EventArgs e)
        {
            var handler = this.BaseInternalEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected internal virtual void OnBaseProtectedInternalEvent(EventArgs e)
        {
            var handler = this.BaseProtectedInternalEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnBaseProtectedEvent(EventArgs e)
        {
            var handler = this.BaseProtectedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public class DerivedSA1101Test : SA1101TestCode
        {
            private string privateField;

            public event EventHandler PublicEvent;
        
            internal event EventHandler InternalEvent;
            
            protected internal event EventHandler ProtectedInternalEvent;
            
            protected event EventHandler ProtectedEvent;
            
            private event EventHandler PrivateEvent;

            public string PublicProperty { get; set; }
            
            internal string InternalProperty { get; set; }
            
            protected internal string ProtectedInternalProperty { get; set; }
            
            protected string ProtectedProperty { get; set; }
            
            private string PrivateProperty { get; set; }

            public void OnPublicEvent(EventArgs e)
            {
                var handler = this.PublicEvent;
                if (handler != null)
                {
                    handler(this, e);
                }
            }

            internal void OnInternalEvent(EventArgs e)
            {
                var handler = this.InternalEvent;
                if (handler != null)
                {
                    handler(this, e);
                }
            }

            protected internal void OnProtectedInternalEvent(EventArgs e)
            {
                var handler = this.ProtectedInternalEvent;
                if (handler != null)
                {
                    handler(this, e);
                }
            }

            protected void OnProtectedEvent(EventArgs e)
            {
                var handler = this.ProtectedEvent;
                if (handler != null)
                {
                    handler(this, e);
                }
            }

            private void OnPrivateEvent(EventArgs e)
            {
                var handler = this.PrivateEvent;
                if (handler != null)
                {
                    handler(this, e);
                }
            }

            private string Usage()
            {
                BasePublicEvent += (s, e) => { };
                BaseInternalEvent += (s, e) => { };
                BaseProtectedInternalEvent += (s, e) => { };
                BaseProtectedEvent += (s, e) => { };

                BasePublicProperty = "a";
                BaseInternalProperty = "a";
                BaseProtectedInternalProperty = "a";
                BaseProtectedProperty = "a";

                OnBasePublicEvent(EventArgs.Empty);
                OnBaseInternalEvent(EventArgs.Empty);
                OnBaseProtectedInternalEvent(EventArgs.Empty);
                OnBaseProtectedEvent(EventArgs.Empty);

                PublicEvent += (s, e) => { };
                InternalEvent += (s, e) => { };
                ProtectedInternalEvent += (s, e) => { };
                ProtectedEvent += (s, e) => { };
                PrivateEvent += (s, e) => { };

                privateField = "a";

                PublicProperty = "a";
                InternalProperty = "a";
                ProtectedInternalProperty = "a";
                ProtectedProperty = "a";
                PrivateProperty = "a";

                OnPublicEvent(EventArgs.Empty);
                OnInternalEvent(EventArgs.Empty);
                OnProtectedInternalEvent(EventArgs.Empty);
                OnProtectedEvent(EventArgs.Empty);
                OnPrivateEvent(EventArgs.Empty);

                return this.privateField;
            }
        }
    }
}
