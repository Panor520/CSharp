using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.Delegate
{
    delegate void Delegate1();
    class EventMyClass
    {
        public static void Invoke()
        {
            EventDelegate eventDelegate = new EventDelegate();
            eventDelegate.DelegateEvent += method1;
            eventDelegate.DelegateEvent += method2;
            eventDelegate.Invokke();
        }
        public static void method1()
        {
            Console.WriteLine("1");
        }
        public static  void method2()
        {
            Console.WriteLine("2");
        }
    }
    /// <summary>
    /// 事件必须声明在类中，且事件的调用必须和声明的事件在同一个类中
    /// </summary>
    class EventDelegate
    {
        public event Delegate1 DelegateEvent;//事件必须声明在类中

        public void Invokke()
        {
            this.DelegateEvent?.Invoke();
        }
    }
}
