namespace CR_StyleCop.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop;

    internal class ViolationComparer : IEqualityComparer<Violation>
    {
        public bool Equals(Violation x, Violation y)
        {
            return x.Line == y.Line && x.Rule == y.Rule && x.Message == y.Message;
        }

        public int GetHashCode(Violation obj)
        {
            return obj.Key.GetHashCode();
        }
    }
}
