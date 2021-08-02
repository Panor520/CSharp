using System;
using AspNetCore.Interface;

namespace AspNetCore.Service
{
    public  class Headphone:IHeadphone
    {
        public Headphone(IPower power)
        {
            Console.WriteLine($"{this.GetType().Name}被构造!");
        }
    }
}
