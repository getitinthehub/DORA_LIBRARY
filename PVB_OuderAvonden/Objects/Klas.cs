using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DORA.Objects
{
    public class Klas
    {
        public string klas { get; private set; }
        public string DocentAfkorting { get; private set; }

        public Klas(string _klas, string docentAfkorting)
        {
            klas = _klas;
            DocentAfkorting = docentAfkorting;
        }
    }
}
