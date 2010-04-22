using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary2;

namespace ClassLibrary1 
{
  public class Class2
  {
    public Class2(string[] param1, string stringVariable, bool boolVariable, bool boolVariable1, bool boolConstant)
    {
      param1[0] = "a";
      Test();
      Class2.Test();
      Class1.SomeStaticMethod(0);
    }

    public static void Test()
    {
      Class1 class1 = new Class1();
      class1.SomeProperty = "b";

      var x = new Class1();

      var y = new Class1Descendant();
      
      Class1.SomeStaticMethod(20);
      x.SomeMethod(10+1.ToString());
      x.SomeMethod("a"+"b");
      x.SomeMethod(class1);
      x.SomeMethod(new Class1());
      x.SomeMethod(y);

      var c = new Class3();

      Class3 c_ = new Class3();

      var c3 = new Class3("bbb");
      var c5 = new Class3(0);

      var c2 = new Class3("aaa" + "bbb");
      var c4 = new Class3(0.ToString());
      var c6 = new Class3(class1);

      var c7 = new Class3(y);

      var c8 = new Class3(10+20);

    }
  }
}
