using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Interface
{
    public interface IPhone
    {
        void Call();

        void Init1234567890(IPower iPower);
        IMicrophone Microphone { get; set; }
        IHeadphone Headphone { get;set; }
        IPower Power { get; set; }
    }
}
