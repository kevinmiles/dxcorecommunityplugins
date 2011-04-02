namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Test code for SA1125 rule - use shorthand for nullable types.
    /// </summary>
    public class SA1125TestCode : List<Nullable<int>>
    {
        private Nullable<int> intProperty;

        private Nullable<DateTime> Date { get; set; }

        private Nullable<int> IntProperty
        {
            get { return this.intProperty; }
            set { this.intProperty = value; }
        }

        private Type MethodName(Nullable<long> parameter)
        {
            Nullable<long> variable = parameter;
            return typeof(Nullable<>);
        }
    }
}
