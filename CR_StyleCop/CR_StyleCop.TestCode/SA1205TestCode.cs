namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1205 rule - partial elements must declare access.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "This is about SA1207 rule.")]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1400:AccessModifierMustBeDeclared", Justification = "This is about SA1207 rule.")]
    public class SA1205TestCode
    {
        partial class PartialInner
        {
        }

        public class Inner
        {
        }
    }
}
