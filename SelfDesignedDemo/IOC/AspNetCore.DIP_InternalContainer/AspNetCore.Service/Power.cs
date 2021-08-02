using System;
using AspNetCore.Interface;

namespace AspNetCore.Service
{
    public class Power:IPower
    {
        public Power(IMicrophone microphone)
        {
            Console.WriteLine($"{this.GetType().Name}构造！");
        }
    }
}
