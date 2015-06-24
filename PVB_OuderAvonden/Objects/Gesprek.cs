using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DORA.Objects.Agenda
{
    public class Gesprek
    {
        public int ID { get; private set; }
        public string StudentNummer { get; private set; }
        public string DocentAfkorting { get; private set; }
        public int OudersOfVerzorgersID { get; private set; }
        public string ThuisSituatie { get; private set; }
        public string StudieKeuze { get; private set; }
        public string VoortGang { get; private set; }
        public string Presentie { get; private set; }
        public string Motivatie { get; private set; }
        public string AlgemeenWelbevinden { get; private set; }
        public string VerwachtingsPatroon { get; private set; }
        public int GespreksDuur { get; private set; }

        public Gesprek(int id, string studentnummer, string docentAfkorting, int oudersofVerzorgersid,
            string thuisSituatie, string studieKeuze, string voortGang, string presentie,
            string motivatie, string algemeenWelbevinden, string verwachtingsPersoon, int gespreksDuur)
        {
            ID = id;
            StudentNummer = studentnummer;
            DocentAfkorting = docentAfkorting;
            OudersOfVerzorgersID = oudersofVerzorgersid;
            ThuisSituatie = thuisSituatie;
            StudieKeuze = studieKeuze;
            VoortGang = voortGang;
            Presentie = presentie;
            Motivatie = motivatie;
            AlgemeenWelbevinden = algemeenWelbevinden;
            VerwachtingsPatroon = verwachtingsPersoon;
            GespreksDuur = gespreksDuur;
        }
        
        public Gesprek(int id, string studentnummer, string docentAfkorting, int oudersofVerzorgersid)
        {
            ID = id;
            StudentNummer = studentnummer;
            DocentAfkorting = docentAfkorting;
            OudersOfVerzorgersID = oudersofVerzorgersid;
        }
    }

}
