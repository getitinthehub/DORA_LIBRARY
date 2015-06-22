using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DORA.Objects
{
    public class Gebruiker
    {
        public int GebruikerID { get; private set; }
        public int PersoonID { get; private set; }
        public string GebruikersNaam { get; private set; }
        public string WachtWoord { get; set; }
        public bool isBeheerder { get; private set; }

        public Gebruiker(int id, int persoonID, string gebruikersnaam,
            string wachtwoord, bool isbeheerder)
            
        {
            GebruikerID = id;
            PersoonID = persoonID;
            GebruikersNaam = gebruikersnaam;
            WachtWoord = wachtwoord;
            isBeheerder = isbeheerder;
        }
    }

}
