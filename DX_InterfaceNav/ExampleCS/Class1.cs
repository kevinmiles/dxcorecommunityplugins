using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ExampleCS
{

    public interface MyInterface
    {
        void MySub();
        String MyFunc();
        String MyProp { get;  }
    }
    public class Tester
    {
        public void RequiredTests1()
        {
            MyInterface MyVar = new MyImplementor();
            MyInterface Value1 = MyVar; 
            string Value2 = MyVar.MyProp;
            string Value3 = MyVar.MyFunc();
            MyVar.MySub();
        }
        public void RequiredTests2()
        {
            MyImplementor MyVar = new MyImplementor();
            MyInterface Value1 = MyVar; 
            string Value2 = MyVar.MyProp;
            string Value3 = MyVar.MyFunc();
            MyVar.MySub();
        }

        //public void IdealTests1()
        //{
        //    MyInterface MyVar;
        //    // Illegal syntax
        //    string Value4 = MyVar.MyProp();
        //    string Value5 = MyVar.MyFunc; // Not found since this refers to a no-existent member
        //}
        //public void IdealTests2()
        //{
        //    MyImplementor MyVar;
        //    // Illegal syntax
        //    string Value4 = MyVar.MyProp();
        //    string Value5 = MyVar.MyFunc; // Not found since this refers to a no-existent member
        //}
    }
    public class MyImplementor : MyInterface
    {
        public void MySub()
        {
        }
        public String MyFunc()
        {
            throw new NotImplementedException();
        }
        public String MyProp
        {
            get
            {
                return "dummy";
            }
        }
    }
    public class MyImplementor2 : MyInterface
    {
        void MyInterface.MySub()
        {
        }
        String MyInterface.MyFunc()
        {
            throw new NotImplementedException();
        }
        String MyInterface.MyProp
        {
            get
            {
                return "dummy";
            }
        }
    }

}
