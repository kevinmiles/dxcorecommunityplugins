// <copyright file="SA1400TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1400 rule - elements should have access modifier.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1400 rule.")]
    class SA1400TestCode
    {
        bool fieldName;

        SA1400TestCode()
        {
        }

        event EventHandler Event;

        enum MyEnum
        {
            FirstElement,
            NextElement
        }

        bool FieldName
        {
            get { return this.fieldName; }
            set { this.fieldName = value; }
        }

        int PropertyName { get; set; }

        void Test()
        {
            this.Event(this, EventArgs.Empty);
        }

        struct MyStruct
        {
        }
    }
}
