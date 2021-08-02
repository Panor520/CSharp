using System;

namespace AspNetCore.DAL
{
    public class IphoneAbstract : AbstractPhone
    {
        public override void Call()
        {
            Console.WriteLine("使用 {0} Call", this.GetType().Name);
        }

        public override void Text()
        {
            Console.WriteLine("使用 {0} Text", this.GetType().Name);
        }
    }
}