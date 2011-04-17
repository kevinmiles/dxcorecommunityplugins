namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1510 rule - chained elements must not be separated with blank line.
    /// </summary>
    public class SA1510TestCode
    {
        private int MethodName(bool parameter)
        {
            int varName = 1;
            if (parameter)
            {
                varName++;
            }

            else
            {
                varName--;
            }

            try
            {
                varName++;
            }

            catch (NullReferenceException)
            {
                return varName;
            }

            catch (ArgumentNullException)
            {
                return varName;
            }

            finally
            {
                varName++;
            }

            return varName;
        }
    }
}
