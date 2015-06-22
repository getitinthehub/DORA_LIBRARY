using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DORA.Objects
{
    public class DocentBeschikbaarheid
    {
        public int ID { get; private set; }
        public int DocentID { get; private set; }
        public string DatumBeschikbaar { get; private set; }
        public string TijdBeschikbaar { get; private set; }

        public DocentBeschikbaarheid(int id, int docentid, string datumBeschikbaar, string tijdBeschikbaar)
        {
            ID = id;
            DocentID = docentid;
            DatumBeschikbaar = datumBeschikbaar;
            TijdBeschikbaar = tijdBeschikbaar;
        }
    }
}
