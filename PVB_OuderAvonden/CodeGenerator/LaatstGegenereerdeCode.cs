using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DORA.CodeGenerator
{
    public class LaatstGegenereerdeCode
    {
        public DORA.Objects.UniekeCode LaatsteRegistratieCode { get; set; }
        public DORA.Objects.UniekeCode LaatsteKoppelCode { get; set; }
        public LaatstGegenereerdeCode() { }
        public LaatstGegenereerdeCode(DORA.Objects.UniekeCode laatsteRegistratiecode, DORA.Objects.UniekeCode laatsteKoppelcode)
        {
            LaatsteRegistratieCode = laatsteRegistratiecode;
            LaatsteKoppelCode = laatsteKoppelcode;
        }
    }

}
