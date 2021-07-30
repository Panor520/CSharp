using System;
using AspNetCore.DAL;

namespace AspNetCore.BLL
{
    public class StudentService
    {
        public void Study()
        {
            Console.WriteLine("study ioc");
        }

        public void PlayPhone(Iphone phone)//类型Iphone using AspNetCore.DAL;
        {
            Console.WriteLine("用 {0}",phone.GetType().Name);
            phone.Call();
            phone.Text();
        }

        public void PlayPhone(Honour phone)//为了输出Honour 需要新写这个方法
        {
            Console.WriteLine("用 {0}", phone.GetType().Name);
            phone.Call();
            phone.Text();
        }
    }
}
