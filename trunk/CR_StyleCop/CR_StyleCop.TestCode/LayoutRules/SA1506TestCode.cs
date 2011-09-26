// <copyright file="SA1506TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1506 rule - xml doc comments must not be followed by blank line.
    /// </summary>

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1506 rule.")]
    public class SA1506TestCode
    {
        /// <summary>
        /// Nothing important.
        /// </summary>
        
        public static readonly int Field = 42;
        
        private object propertyName;

        /// <summary>
        /// Occurs never.
        /// </summary>

        public event EventHandler EventName;

        /// <summary>
        /// Gets or sets something.
        /// </summary>

        public object PropertyName
        {
            get { return this.propertyName; }
            set { this.propertyName = value; }
        }

        /// <summary>
        /// Gets or sets something else.
        /// </summary>

        public string StringProperty { get; set; }

        /// <summary>
        /// Does nothing.
        /// </summary>
        
        public void MethodName()
        {
            this.EventName(this, EventArgs.Empty);
        }
    }
}
