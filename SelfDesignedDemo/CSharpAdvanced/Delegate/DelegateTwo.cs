using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.Delegate
{
    public  class DelegateTwo
    {
        public void Add(int a,int b)
        {
            Console.WriteLine(a + b);
        }

        public void Show()
        {
            Action<int, int> action = new Action<int, int>(Add);

            action += ((a, b) => { Console.WriteLine(a-b); });

            action += ((a, b) => { Console.WriteLine(a*b); });

            action(1,2);
        }
    }
}
