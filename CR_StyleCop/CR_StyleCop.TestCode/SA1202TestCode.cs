namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1202 rule - elements must be sorted by access correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1202 rule.")]
    public class SA1202TestCode
    {
        private void MethodName1()
        {
        }

        protected void MethodName2()
        {
        }

        protected void MethodName22()
        {
        }

        protected internal void MethodName3()
        {
        }

        internal void MethodName4()
        {
        }

        public void MethodName5()
        {
        }
    }
}
