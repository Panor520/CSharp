using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.DAL
{
    public class HorourAbstract : AbstractPhone
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
