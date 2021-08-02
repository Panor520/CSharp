using System;
using AspNetCore.Interface;

namespace AspNetCore.Service
{
    public class Applephone:IPhone
    {
        public IMicrophone Microphone { get; set; }
        public IHeadphone Headphone { get; set; }
        public IPower Power { get; set; }
        public Applephone(IHeadphone iHeadphone)
        {
            this.Headphone = iHeadphone;
            Console.WriteLine($"{this.GetType().Name}带参数构造函数");
        }

        public void Call()
        {
            Console.WriteLine($"{this.GetType().Name}打电话");
        }
        public void Init1234567890(IPower iPower)
        {

        }
    }
}
