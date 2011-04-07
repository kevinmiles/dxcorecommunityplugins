namespace CR_StyleCop.TestCode
{
    using System;

#pragma warning disable 169

    /// <summary>
    /// Test code for SA1310 rule - field name must not contain _.
    /// </summary>
    public class SA1310TestCode
    {
        private int field_name;

        // BUGBUG: SA1310 is not fired for parameters and local variables.
        private int MethodName(int param_name)
        { 
            int var_name = param_name;
            return var_name;
        }
    }
}
