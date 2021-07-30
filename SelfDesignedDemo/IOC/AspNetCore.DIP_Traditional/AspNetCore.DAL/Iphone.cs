using System;

namespace AspNetCore.DAL
{
    public class Iphone
    {
        public void Call()
        {
            Console.WriteLine("使用 {0} Call",this.GetType().Name);
        }

        public void Text()
        {
            Console.WriteLine("使用 {0} Text", this.GetType().Name);
        }
    }
}