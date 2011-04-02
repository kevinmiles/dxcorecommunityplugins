namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1411 rule - redundant parens from attributes.
    /// </summary>
    [SA1411TestCode.My()]
    [SA1411TestCode.MyAttribute()]
    internal class SA1411TestCode
    {
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
        internal class MyAttribute : Attribute
        {
        }
    }
}