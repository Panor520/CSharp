using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.Lambda
{
    /// <summary>
    /// 声明委托
    /// </summary>
    /// <param name="name">参数1</param>
    /// <param name="age">参数2</param>
    delegate void studentDelegate(string name,int age);
    public class Lambda1
    {
        public void Show()
        {
            DateTime dateTime = DateTime.Now;
            //version1
            {
                studentDelegate studentDelegate = new studentDelegate(
                    Student);//传入方法
                studentDelegate("version1", 1);
            }
            //version2(这样写可以访问局部变量)
            {
                studentDelegate studentDelegate = new studentDelegate(
                delegate (string name, int age)
                {
                    Console.WriteLine(dateTime);
                    Console.WriteLine($"名字：{name} 年龄：{age}");
                });//直接传入方法
                studentDelegate("version2", 2);
            }
            //Version3
            {
                studentDelegate studentDelegate = new studentDelegate(
                (string name, int age) =>
                {
                    Console.WriteLine($"名字：{name} 年龄：{age}");
                });//简化直接传入方法
                studentDelegate("version3", 3);
            }
            //Version4
            {
                studentDelegate studentDelegate = new studentDelegate(
                (name,age) =>
                {
                    Console.WriteLine($"名字：{name} 年龄：{age}");
                });//简化直接传入方法
                studentDelegate("version4", 4);
            }
            {  }
        }
        public void Student(string name, int age)
        {
            Console.WriteLine($"名字：{name} 年龄：{age}");
        }
    }
}
