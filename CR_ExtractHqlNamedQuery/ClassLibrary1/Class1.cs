using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary2;
using System.Collections;

namespace ClassLibrary1
{
  public class Class1
  {
    public IList DoSomething(ISession session)
    {


        var query = session.CreateQuery("from Order o where o.Amount > :amount");
      query.SetParameter("amount", 100);
      return query.List<Order>();
















    }
  }
}
