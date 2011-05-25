// <copyright file="SA1409TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1409 rule - report redundant code.
    /// </summary>
    public class SA1409TestCode
    {
        static SA1409TestCode()
        {
        }
        
        private int MethodName()
        {
            int xdType = 9;

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
                xdType = 2;
            }
            finally
            {
            }
            
            try
            {
            }
            finally
            {
                xdType = 2;
            }

            try
            {
            }
            catch (Exception)
            {
            }

            try
            {
                xdType = 3;
            }
            catch (Exception)
            {
            }

            try
            {
            }
            catch (Exception)
            {
                xdType = 6;
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
                xdType = 9;
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
                xdType = 4;
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
                xdType = 8;
            }

            try
            {
                xdType = 8;
            }
            catch (Exception)
            {
                xdType = 7;
            }
            finally
            {
            }

            try
            {
                xdType = 7;
            }
            catch (Exception)
            {
            }
            finally
            {
                xdType = 8;
            }

            try
            {
            }
            catch (Exception)
            {
                xdType = 7;
            }
            finally
            {
                xdType = 3;
            }

            return xdType;
        }
    }
}