using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DORA.Objects
{
    public class Klas
    {
        public string Klas { get; private set; }
        public int DocentID { get; private set; }

        public Klas(string klas, int docentID)
        {
            Klas = klas;
            DocentID = docentID;
        }
    }
}
