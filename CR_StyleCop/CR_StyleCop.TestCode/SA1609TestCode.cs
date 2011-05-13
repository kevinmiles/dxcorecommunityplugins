// <copyright file="SA1609TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1609 rule - property documentation must have value tag.
    /// </summary>
    public class SA1609TestCode
    {
        private int propertyName2;
        private int propertyName3 = 42;
        private int propertyName4;

        /// <summary>
        /// Gets or sets something.
        /// </summary>
        public int PropertyName { get; set; }

        /// <summary>
        /// Gets or sets something else.
        /// </summary>
        public int PropertyName2
        {
            get { return this.propertyName2; }
            set { this.propertyName2 = value; }
        }

        /// <summary>
        /// Gets anything.
        /// </summary>
        public int PropertyName3
        {
            get { return this.propertyName3; }
        }

        /// <summary>
        /// Sets anything.
        /// </summary>
        public int PropertyName4
        {
            set { this.propertyName4 = value; }
        }

        // BUGBUG: SA1609 not reported for internal properties.

        /// <summary>
        /// Gets or sets internal property.
        /// </summary>
        internal int PropertyNameX { get; set; }

        /// <summary>
        /// Gets or sets protected property.
        /// </summary>
        protected internal int PropertyNameZ { get; set; }

        /// <summary>
        /// Gets or sets protected property.
        /// </summary>
        protected int PropertyNameY { get; set; }

        /// <summary>
        /// Gets or sets private property.
        /// </summary>
        private int PropertyNameZZ { get; set; }
    }
}
