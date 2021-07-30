using System;
using AspNetCore.DAL;
using AspNetCore.IBLL;

namespace AspNetCore.BLL
{
    public class StudentService: IStudentService
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
    }
}
