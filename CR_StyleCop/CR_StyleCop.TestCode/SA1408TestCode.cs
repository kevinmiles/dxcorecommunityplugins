namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1408 rule - conditional expressions must declare precedence.
    /// </summary>
    public class SA1408TestCode
    {
        private bool Invalid()
        {
            bool i = true && true || false;
            i = true || true && false;
            return i;
        }

        private bool Valid()
        {
            bool i = true && true && false;
            i = true || true || false;
            return i;
        }
    }
}