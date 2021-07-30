using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.Attribute
{
    [Define("自定义类上的特性",1)]
    public class UseAttribute
    {
        public void Show ()
        {
            Console.WriteLine("Use");
        }
        [Define("自定义字段的特性", 1)]
        public string cc;
    }

    public class UseAttribute1
    {
        public void Show()
        {
            Console.WriteLine("Use");
        }
    }
}
