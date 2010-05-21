using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ClassLibrary2
{
  public interface IQuery
  {
    void SetParameter(string a, int b);
    IList List<T>();
  }

  public interface ISession
  {
    IQuery CreateQuery(string param);
  }

  public class Order
  {
  }
}
