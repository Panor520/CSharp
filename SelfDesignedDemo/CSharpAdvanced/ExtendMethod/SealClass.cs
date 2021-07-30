using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.ExtendMethod
{
    public sealed class SealClass
    {
        public string name;
        public int Age { get; set; }
        public  void    Show()
        {
            Console.WriteLine($"{this.name}||{this.Age}");
        }
    }
}
