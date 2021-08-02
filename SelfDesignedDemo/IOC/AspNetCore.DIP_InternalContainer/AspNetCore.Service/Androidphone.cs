using System;
using AspNetCore.Interface;

namespace AspNetCore.Service
{
    public class Androidphone : IPhone
    {
        public IMicrophone Microphone { get; set; }
        public IHeadphone Headphone { get; set; }
        public IPower Power { get; set; }
        public Androidphone()
        {
            Console.WriteLine($"{this.GetType().Name}构造函数");
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
