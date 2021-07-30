using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.Attribute
{
    public class DefineAttribute:System.Attribute
    {
        public string Name;
        public int Age { get; set; }

        public  DefineAttribute(string name,int age)
        {
            this.Name = name;
            this.Age = age;
        }
        public void Show()
        {
            Console.WriteLine(this.Name+this.Age);
        }
    }

}
