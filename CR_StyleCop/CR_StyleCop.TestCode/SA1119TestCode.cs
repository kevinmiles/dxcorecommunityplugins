namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Test code for SA1119 rule - line has redundant parenthesis.
    /// </summary>
    public class SA1119TestCode
    {
        private readonly int y = (2 + 3);

        /// <summary>
        /// Some method documentation.
        /// </summary>
        /// <returns>Something returned.</returns>
        public int Method()
        {
            int f = (2 + 5);
            var x = (1 + 2);
            int? y = this.y;
            x = (2 + 3);
            string z = (this.Method()).ToString();
            string r = ((3 + 6)).ToString();
            x = checked((f + 2));
            x = unchecked((1 + 2));
            return (y ?? x);
        }

        /// <summary>
        /// This should also report SA1119.
        /// </summary>
        /// <returns>The number of 13.</returns>
        public IEnumerable<int> Yielder()
        {
            yield return (11 + 2);
        }

        /// <summary>
        /// Statements below should not report SA1119 violations.
        /// </summary>
        public void ValidParenthesis()
        {
            var list = new List<int>();
            (list as IEnumerable<int>).GetEnumerator();
            (list is IEnumerable<int>).ToString();

            int? ni = null;
            (ni ?? 5).ToString();

            bool b = true;
            (b || false).ToString();
            (b && false).ToString();
            (!b).ToString();
            (b |= false).ToString();
            (b &= false).ToString();
            (b ? 1 : -1).ToString();

            (from u in list select u).ToString();
            var e = from r in Enumerable.Range(0, 15) select (r + 3);

            var i = 10;
            (i + 8).ToString();
            (i - 8).ToString();
            (i * 2).ToString();
            (i / 2).ToString();
            (i >> 2).ToString();
            (i << 2).ToString();
            (i++).ToString();
            (i--).ToString();
            (++i).ToString();
            (--i).ToString();
            (+i).ToString();
            (-i).ToString();
            (i += 2).ToString();
            (i -= 2).ToString();
            (i *= 2).ToString();
            (i /= 2).ToString();
            (i | 5).ToString();
            (i & 5).ToString();
            (i ^ 5).ToString();
            (i |= 5).ToString();
            (i &= 5).ToString();
            (i ^= 5).ToString();

            (i = 3).ToString();
            ((long)i).ToString();

            (new List<int>()).Add(5);
            (new int[] { 5 })[0].ToString();

            (1 == 2).ToString();
            (1 != 2).ToString();
            (1 > 2).ToString();
            (1 >= 2).ToString();
            (1 < 2).ToString();
            (1 <= 2).ToString();

            Func<Func<int, int>, int> func = x => x(1);
            func((x => x + 1));
        }
    }
}
