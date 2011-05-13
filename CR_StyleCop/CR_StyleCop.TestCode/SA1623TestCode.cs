// <copyright file="" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA623 rule - property documentation must match accessors.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1609:PropertyDocumentationMustHaveValue", Justification = "This is about SA1623 rule")]
    public class SA1623TestCode
    {
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1623 rule")]
        private int readOnlyProperty;
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1623 rule")]
        private int readOnlyProperty2;
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1623 rule")]
        private int writeOnlyProperty;
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1623 rule")]
        private int writeOnlyProperty2;
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1623 rule")]
        private int writeOnlyProperty3;
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1623 rule")]
        private bool booleanReadOnlyProperty;
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1623 rule")]
        private bool booleanReadOnlyProperty2;
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1623 rule")]
        private bool booleanWriteOnlyProperty;
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1623 rule")]
        private bool booleanWriteOnlyProperty2;
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1623 rule")]
        private bool booleanWriteOnlyProperty3;

        /// <summary>
        /// Gets read only property.
        /// </summary>
        public int ReadOnlyProperty
        {
            get { return this.readOnlyProperty; }
        }

        /// <summary>
        /// Sets read only property.
        /// </summary>
        public int ReadOnlyProperty2
        {
            get { return this.readOnlyProperty2; }
        }

        /// <summary>
        /// Gets write only property.
        /// </summary>
        public int WriteOnlyProperty
        {
            set { this.writeOnlyProperty = value; }
        }

        /// <summary>
        /// Sets write only property.
        /// </summary>
        public int WriteOnlyProperty2
        {
            set { this.writeOnlyProperty2 = value; }
        }

        /// <summary>
        /// Gets or sets write only property.
        /// </summary>
        public int WriteOnlyProperty3
        {
            set { this.writeOnlyProperty3 = value; }
        }

        /// <summary>
        /// Gets a value indicating whether read only boolean property is true.
        /// </summary>
        public bool BooleanReadOnlyProperty
        {
            get { return this.booleanReadOnlyProperty; }
        }

        /// <summary>
        /// Sets a value indicating whether read only boolean property is true.
        /// </summary>
        public bool BooleanReadOnlyProperty2
        {
            get { return this.booleanReadOnlyProperty2; }
        }

        /// <summary>
        /// Gets a value indicating whether write only boolean property is true.
        /// </summary>
        public bool BooleanWriteOnlyProperty
        {
            set { this.booleanWriteOnlyProperty = value; }
        }

        /// <summary>
        /// Sets a value indicating whether write only boolean property is true.
        /// </summary>
        public bool BooleanWriteOnlyProperty2
        {
            set { this.booleanWriteOnlyProperty2 = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether write only boolean property is true.
        /// </summary>
        public bool BooleanWriteOnlyProperty3
        {
            set { this.booleanWriteOnlyProperty3 = value; }
        }
        
        /// <summary>
        /// Gets regular property.
        /// </summary>
        public int RegularProperty { get; set; }

        /// <summary>
        /// Sets regular property.
        /// </summary>
        public int RegularProperty2 { get; set; }

        /// <summary>
        /// Gets or sets regular property.
        /// </summary>
        public int RegularProperty3 { get; set; }

        /// <summary>
        /// Gets regular property.
        /// </summary>
        public int RegularProperty4 { get; private set; }

        /// <summary>
        /// Sets regular property.
        /// </summary>
        public int RegularProperty5 { get; private set; }

        /// <summary>
        /// Gets regular property.
        /// </summary>
        public int RegularProperty7 { private get; set; }

        /// <summary>
        /// Sets regular property.
        /// </summary>
        public int RegularProperty8 { private get; set; }

        /// <summary>
        /// Gets or sets regular property.
        /// </summary>
        public int RegularProperty9 { private get; set; }

        /// <summary>
        /// Gets a value indicating whether it is good comment.
        /// </summary>
        public bool BoolProperty { get; set; }

        /// <summary>
        /// Sets a value indicating whether it is good comment.
        /// </summary>
        public bool BoolProperty2 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is good comment.
        /// </summary>
        public bool BoolProperty3 { get; set; }

        /// <summary>
        /// Gets a value indicating whether it is good comment.
        /// </summary>
        public bool BoolProperty4 { get; private set; }

        /// <summary>
        /// Sets a value indicating whether it is good comment.
        /// </summary>
        public bool BoolProperty5 { get; private set; }

        /// <summary>
        /// Gets a value indicating whether it is good comment.
        /// </summary>
        public bool BoolProperty7 { private get; set; }

        /// <summary>
        /// Sets a value indicating whether it is good comment.
        /// </summary>
        public bool BoolProperty8 { private get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is good comment.
        /// </summary>
        public bool BoolProperty9 { private get; set; }
    }
}
