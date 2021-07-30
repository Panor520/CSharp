using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.Delegate
{
    /// <summary>
    /// 多播委托
    /// </summary>
    public  class MulticastDelegateTest
    {
        public  void Show()
        {
            Action action = new Action(Method1);//给委托赋值时没括号就给了方法的地址
            action =(Action) MulticastDelegate.Combine(action, new Action(Method2));
            action += Method3;
            action -= Method3;
            //action();
            foreach (Action item in action.GetInvocationList())
            {
                
                Console.WriteLine("MethodName:"+item.Method.Name); 
                item();//Invoke的简写，等价于下面的Invoke调用
                item.Invoke();
            }
        }

        public void Method1()
        {
            Console.WriteLine("Method1");
        }
        public void Method2()
        {
            Console.WriteLine("Method2");
        }
        public void Method3()
        {
            Console.WriteLine("Method3");
        }
        public void Method4()
        {
            Console.WriteLine("Method4");
        }
    }
}
