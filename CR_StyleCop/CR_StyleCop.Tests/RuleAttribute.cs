namespace CR_StyleCop.Tests
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    internal class RuleAttribute : RowAttribute
    {
        public RuleAttribute(string ruleCheck)
            : base(ruleCheck)
        {
        }

        public string CheckId
        {
            get { return (string)this.Values[0]; }
        }
    }
}
