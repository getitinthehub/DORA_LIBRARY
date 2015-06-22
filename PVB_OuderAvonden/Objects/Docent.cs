using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DORA.Objects
{
    public class Docent : Persoon
    {
        public int PersoonID { get; private set; }
        public string VoorLetters { get; private set; }
        public string DocentAfkorting { get; private set; }

        public Docent(string docentAfkorting, bool isBeheerder, int persoonid, string voornaam,
            string achternaam, string tussenvoegsels, string adres, string postcode, string geboortedatum,
            string woonplaats, string geslacht, string telefoon1, string telefoon2, string email)
            : base(persoonid, voornaam, achternaam, tussenvoegsels, adres, postcode, geboortedatum, email, woonplaats, geslacht, telefoon1, telefoon2)
        {
            PersoonID = persoonid;
            DocentAfkorting = docentAfkorting;

        }

    }

}
