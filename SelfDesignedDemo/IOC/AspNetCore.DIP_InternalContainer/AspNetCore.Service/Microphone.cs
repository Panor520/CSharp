using System;
using AspNetCore.Interface;

namespace AspNetCore.Service
{
    public class Microphone : IMicrophone
    {
        public Microphone()
        {
            Console.WriteLine($"{this.GetType().Name}被构造！");
        }
    }
}
