// <copyright file="SA1409TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1409 rule - report redundant code.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1409 rule.")]
    public class SA1409TestCode
    {
        static SA1409TestCode()
        {
        }
        
        private int MethodName()
        {
            int xdtype = 9;

            unchecked
            {
            }
            
            checked
            {
            }

            lock (this)
            {
            }

            unsafe
            {
            }

            try
            {
            }
            finally
            {
            }

            try
            {
                xdtype = 2;
            }
            finally
            {
            }
            
            try // BUGBUG
            {
            }
            finally
            {
                xdtype = 2;
            }

            try
            {
            }
            catch (Exception)
            {
            }

            try
            {
                xdtype = 3;
            }
            catch (Exception)
            {
            }

            try
            {
            }
            catch (Exception)
            {
                xdtype = 6;
            }

            try
            {
            }
            catch (Exception)
            {
            }
            finally
            {
            }

            try
            {
                xdtype = 9;
            }
            catch (Exception)
            {
            }
            finally
            {
            }

            try
            {
            }
            catch (Exception)
            {
                xdtype = 4;
            }
            finally
            {
            }

            try
            {
            }
            catch (Exception)
            {
            }
            finally
            {
                xdtype = 8;
            }

            try
            {
                xdtype = 8;
            }
            catch (Exception)
            {
                xdtype = 7;
            }
            finally
            {
            }

            try
            {
                xdtype = 7;
            }
            catch (Exception)
            {
            }
            finally
            {
                xdtype = 8;
            }

            try
            {
            }
            catch (Exception)
            {
                xdtype = 7;
            }
            finally
            {
                xdtype = 3;
            }

            return xdtype;
        }
    }
}