namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1109 rule - block statements must not contain region directive.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:DoNotPlaceRegionsWithinElements", Justification = "This is about SA1109 rule.")]
    public class SA1109TestCode
    {
        private void MethodName(int x, int y)
        {
            if (x != y)
            #region Wrongly placed region
            {
            }
            #endregion
            else
            #region Another wrongly placed region
            {
                x++;
            }
            #endregion

            while (x != y)
            #region Yet another wrongly placed region
            {
            }
            #endregion
        }
    }
}
