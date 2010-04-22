using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary1;

namespace ClassLibrary2
{
  class Class5
  {
    void SomeMethod()
    {
      string stringVariable = "a";
      bool boolVariable = true;
      const bool boolConstant = false;

      Class2 instanceX = new Class2(
                new string[] {Class4.StringConst,Class4.StaticString, Class4.StaticStringProp},
                stringVariable,
                boolVariable,
                boolVariable,
                boolConstant
                );

      Class2.Test();
    }
  }
}
