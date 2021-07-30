using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.Lambda
{
    public class LambdaOne
    {
        public void Show()
        {
            //输入参数类型必须全部为显式或全部为隐式；
            Action<int, int> action = (a, b) => { Console.WriteLine(a + b); };
            action(2, 3);
            //当使用有返回值的委托时，若只有一个参数，则可以不加括号，不加return关键字，编译器会自动添加。
            Func<int, int> func = a => a * a;
            Console.WriteLine("Func<int, int>:" + func(3));

            //当使用有返回值的委托时，若返回值为布尔类型，也可以不加括号，不加return关键字
            //func1是func2的简写
            Func<int, int, bool> func1 = (x, y) => x == y;
            Func<int, int, bool> func2 = (c, d) => { return c == d; };
            Func<int, int, int, bool> func3 = (e, f, g) => e == f && f == g;
            Console.WriteLine(func3(1, 2, 3));

            //NoReturn and No parameter Delegate case
            Action action1 = () => { Console.WriteLine("No Parameter"); };
            action1();

            Action<int> action2 = (a) =>
            {
                if (a == 1)
                    Console.WriteLine("a=1");
                else
                    Console.WriteLine("a<>1");
            };
            action2(1);

            //Linq示例
            List<string> strList = new List<string>()//声明一个 string 列表，并添加数据
            {
                "ABC",
                "123456",
                "哈哈",
                "1234567890"
            };
            Console.WriteLine(strList.First(a => a.Length > 5));
            List<int> intList = new List<int>()//声明一个 string 列表，并添加数据
            {
                1,2,3,4,5
            };
            Console.WriteLine(intList.First(a => a > 2));

            LambdaTest("lambdaTest", s =>
            {
                if (s.Contains("lambda"))
                    return "include lambda!";
                else
                    return "No " ;
                  });

            Func<string, string> func4 = new Func<string, string>(funcTEST);
            LambdaTest("lambdaTest", func4);
        }

        public void LambdaTest(string str,Func<string,string> func)
        {
            Console.WriteLine(func(str));
        }

        public string funcTEST(string str)
        {
            if (str.Contains("lambda"))
                return "include lambda!";
            else
                return "No ";
        }
    }
}
