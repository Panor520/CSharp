using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.Delegate
{
    public static class GenericDelegate
    {
        public static void Method()
        {
            //Action参数最多16个
            Action action1 = new Action(Method1);//Action是官方“无返回值”委托，里面的类型为参数类型
            Action<string> action2 = new Action<string>(Method2);//Action是官方“无返回值”委托，里面的类型为参数类型
            Action<string,int> action3 = new Action<string,int>(Method3);//Action是官方“无返回值”委托，里面的类型为参数类型
            Action<string, int,string> action4 = new Action<string, int, string>(Method4);//Action是官方“无返回值”委托，里面的类型为参数类型

            //func参数最多16个
            Func<string> func1 = new Func<string>(Method5);//Func是官方定义的有返回类型的委托，只有一个参数时，这个参数代表返回类型
            Func<int, string> func2 = new Func<int, string>(Method6);//Func是官方定义的有返回类型的委托，第一个参数为方法的参数类型，最后一个参数为方法的返回类型
            Func<int, string, int> func3 = new Func<int, string, int>(Method6);//Func是官方定义的有返回类型的委托，前面的参数依次为方法的参数类型，最后一个参数为方法的返回类型
        }

        public static void Method1()
        {
            
        }

        public static void Method2(string s)
        {

        }
        public static void Method3(string s,int i)
        {

        }
        public static void Method4(string s, int i, string st)
        {

        }
        public static string Method5()
        {
            return "";
        }
        public static string Method6(int s)
        {
            return "";
        }

        public static int Method6(int s,string ss)
        {
            return 0;
        }
    }
}
