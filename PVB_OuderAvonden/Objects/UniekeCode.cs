using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DORA.Objects
{
    public class UniekeCode
    {
        public string Code { get; private set; }
        public string StudentNummer { get; private set; }
        public bool isKoppelCode { get; private set; }

        public UniekeCode(string code, string studentnummer, bool iskoppelCode)
        {
            Code = code;
            StudentNummer = studentnummer;
            isKoppelCode = iskoppelCode;
        }
    }
}
