using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.ExtendMethod
{
    static class ExtendMethodClass
    {
        public static void ExtendShow(this SealClass seal)
        {
            seal.Age = 18;
            seal.name = "lp";
            seal.Show();
        }
    }
}
