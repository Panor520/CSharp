using System;
using AspNetCore.IBLL;
using AspNetCore.DAL;
using AspNetCore.BLL;

namespace AspNetCore.DIP
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Welcome to Pan's IOC Demo!");

            //依赖倒置
            //1.在抽象中不存在元素，在调用时无法调用到 例如下面func PlayPhone的 Iphone参数
            //2.查看定义不方便，如下面的PlayPhone
            {
                IStudentService service = new StudentService();
                Iphone iphone = new Iphone();//加了引用后才引进来Iphone类
                service.PlayPhone(iphone);//F12查看定义直接跳转到接口该函数的定义的地方了
            }

            Console.ReadLine();
        }
    }
}
