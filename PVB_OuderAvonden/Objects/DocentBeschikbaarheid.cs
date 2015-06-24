using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DORA.Objects
{
    public class DocentBeschikbaarheid
    {
        public string DocentAfkorting { get; private set; }
        public string DatumBeschikbaar { get; private set; }
        public string TijdBeschikbaar { get; private set; }

        public DocentBeschikbaarheid(string docentAfkorting, string datumBeschikbaar, string tijdBeschikbaar)
        {
            DocentAfkorting = docentAfkorting;
            DatumBeschikbaar = datumBeschikbaar;
            TijdBeschikbaar = tijdBeschikbaar;
        }
    }
}
