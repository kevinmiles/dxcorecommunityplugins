// <copyright file="SA1600TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

#pragma warning disable 414
#pragma warning disable 67

    public class SA1600TestCode
    {
        private static readonly int Field = 42;
        private int propertyName;

        public SA1600TestCode()
        {
        }

        ~SA1600TestCode()
        {
        }

        public delegate void MyEventHandler(object sender, EventArgs ea);

        public event EventHandler EventName;

        public enum MyEnum
        {
        }

        public interface IMyInterface
        {
        }

        public int PropertyName { get; set; }

        public int PropertyName2
        {
            get { return this.propertyName; }
            set { this.propertyName = value; }
        }

        public int this[int index]
        {
            get { return index; }
            set { }
        }

        public void MethodName()
        {
        }

        public struct MyStruct
        {
        }
    }
}
