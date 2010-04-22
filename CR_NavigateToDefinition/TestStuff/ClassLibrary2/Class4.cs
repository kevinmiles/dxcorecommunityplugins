using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary2
{
  public class Class4
  {
    public const string StringConst = "a";
    public static string StaticString = "b";
    public static string StaticStringProp { get; set; }

    public Class4()
    {
    }

    public class Class4Nested
    {
      public Class4Nested()
      {
        if (StringConst != null)
          ;

        if (Class4.StaticString != null)
          ;

        const string localConst = "a";
      }
    }
  }

  public class TestCtor
  {
    public void SomeMethod()
    {
      Class4 c4 = new Class4();

      Class4.Class4Nested c4a = new Class4.Class4Nested();
    }
  }
}
