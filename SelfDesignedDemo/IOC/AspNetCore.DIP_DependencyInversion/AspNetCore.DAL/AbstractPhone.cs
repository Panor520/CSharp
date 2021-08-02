using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.DAL
{
    public abstract class AbstractPhone
    {
        //public int Id { get; set; }
        //public string Branch { get; set; }
        public abstract void Call();

        public abstract void Text();
    }
}
