using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example_CSharp
{
    public class Class1
    {
        #region Utility
        private void SomeOtherFunction(int param1)
        {
            throw new NotImplementedException();
        }
        #endregion
        
        #region Example1
        private void Example1_Before()
        {
            int Parsed = Int32.Parse("23");
        }
        private void Example1_After()
        {
            Int32 Parsed;
            bool Success = Int32.TryParse("23", out Parsed);

        }
        #endregion

        #region Example2
        private void Example2_Before()
        {
            SomeOtherFunction(Int32.Parse("23"));
        }
        private void Example2_After()
        {
            int Parsed;
            bool success = Int32.TryParse("23", out Parsed);
            SomeOtherFunction(Parsed);
        }
        #endregion
    }
}
