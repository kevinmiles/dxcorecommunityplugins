namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1502 rule - elements with curly brackets must not be on single line.
    /// </summary>
    public abstract class SA1502TestCode
    {
        private bool propertyName = true;

        internal interface InnerInterface { }

        internal bool PropertyName { get { return this.propertyName; } }

        internal abstract bool PropertyName2 { get; set; }

        private void MethodName() { }

        internal struct InnerStruct { }

        internal class InnerClass { }
    }
}
