using System;

namespace ClassLibrary1
{
  public class Class1
  {
    public static void SomeStaticMethod(int aa)
    {
      aa = aa + 1;
      aa++;

    }

    public string SomeProperty { get; set; }

    public void SomeMethod(string aa)
    {
      string someVariable;

      someVariable = "a";

      SomeProperty = "a";
    }

    public void SomeMethod(Class1 aa)
    {
      
    }

  }

  public class Class1Descendant : Class1
  {
    public Class1Descendant()
    {
    }
  }

}
