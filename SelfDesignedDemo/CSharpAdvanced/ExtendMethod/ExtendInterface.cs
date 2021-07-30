using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.ExtendMethod
{
    public static class ExtendInterface
    {
        public static int Sub(this IInterface @interface,int a,int b)
        {
            return a - b;
        }
        public static int Multipy(this IInterface @interface, int a, int b)
        {
            return a * b;
        }
        public static int Divide(this IInterface @interface, int a, int b)
        {
            return a / b;
        }
    }
}
