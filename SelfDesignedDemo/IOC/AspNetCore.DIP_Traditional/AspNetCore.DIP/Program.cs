using System;
using AspNetCore.BLL;
using AspNetCore.DAL;

namespace AspNetCore.DIP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Pan's IOC Demo!");

            //传统调用
            //程序集间相互依赖，底层改变，高层也可能要变
            //增加新需求，就要修改多层代码，会导致代码不稳定。 比如下面的Playphone 想要输出其他类型手机，不光需要增加其他类型手机，还需要重载PlayPhone方法。
            {
                StudentService service = new StudentService();
                Iphone iphone = new Iphone();
                service.PlayPhone(iphone);

                //增加输出Honour方法
                Honour honour = new Honour();
                service.PlayPhone(honour);

            }

            Console.ReadLine();
        }
    }
}
