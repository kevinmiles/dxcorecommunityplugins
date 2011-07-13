﻿// <copyright file="SA1400TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1400 rule - elements should have access modifier.
    /// </summary>
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
            Event(this, EventArgs.Empty);
        }

        struct MyStruct
        {
        }
    }
}