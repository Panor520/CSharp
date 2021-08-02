using System;
using AspNetCore.DAL;
using AspNetCore.IBLL;

namespace AspNetCore.BLL
{
    public class StudentServiceGeneric: IStudentServiceGeneric
    {
        public void PlayPhoneGeneric(AbstractPhone phone)
        {
            Console.WriteLine("用 {0}", phone.GetType().Name);
            phone.Call();
            phone.Text();
        }

        public void PlayPhoneGeneric<T>(T phone)where T:AbstractPhone
        {
            Console.WriteLine("用 {0}", phone.GetType().Name);
            phone.Call();
            phone.Text();
        }
    }
}
