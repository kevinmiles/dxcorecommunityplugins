// <copyright file="SA1000TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

#pragma warning disable 219

    /// <summary>
    /// Test code for SA1000 rule - keywords must be spaces correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1119:StatementMustNotUseUnnecessaryParenthesis", Justification = "This is about SA1000 rule.")]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "This is about SA1000 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:OpeningParenthesisMustBeSpacedCorrectly", Justification = "This is about SA1000 rule.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1000 rule.")]
    public class SA1000TestCode
    {
        private unsafe int MethodName<T>()
        {
            for(int i = 0; i < 10; i++)
            {
            }
            
            int[] values = new[] { 1 };
            var values2 = from x in(values) 
                          where(x < 2)
                          group(x) by(x) into g
                          orderby(g)
                          let y = g
                          select(y);
            var values3 = from x in values
                          join p in(values2) on(x.ToString()) equals(p.ToString())
                          select Tuple.Create(x, p);
            foreach(var value in values2)
            {
            }

            while(values == null)
            {
            }

            if(values == null)
            {
            }

            lock(this)
            {
                using(new StreamReader(string.Empty))
                {
                }

                Point pt = new Point();
                fixed(int* p = &pt.X)
                {
                    *p = 1;
                }

                int* fib = stackalloc int[11];

                switch(pt.X)
                {
                    case 1:
                        break;
                }
            }

            try
            {
                throw new Exception();
            }
            catch (ArgumentException)
            {
            }
            catch (NullReferenceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw(ex);
            }

            T t = default (T);
            Type type = typeof (T);
            int size = sizeof (int);
            int ten = 10;
            int checkedVar = checked (2147483647 + ten);
            int uncheckedVar = unchecked (2147483647 + ten);
            return(11);
        }

        private IEnumerable<int> Yielder()
        {
            yield return(11);
        }

        private class Point
        {
            public int X = 1;
            public int Y = 1;
        }
    }
}
