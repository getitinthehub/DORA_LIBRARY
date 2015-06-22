using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DORA.Objects
{
    public class Student : Persoon
    {
        public int StudentNummer { get; private set; }
        public int PersoonID { get; private set; }
        public string Klas { get; private set; }
        public string Opleiding { get; private set; }
        public bool isLeerplichtig { get; private set; }
        public string VorigeSchool { get; private set; }
        public Student(int studentnummer, int persoonid, string klas, string opleiding, bool isleerplichtig, string vorigeschool, string voornaam,
            string achternaam, string tussenvoegsels, string adres, string postcode, string geboortedatum,
            string woonplaats, string geslacht, string telefoon1, string email)
            : base(persoonid, voornaam, achternaam, tussenvoegsels, adres, postcode, geboortedatum, email, woonplaats, geslacht, telefoon1)
        {
            StudentNummer = studentnummer;
            PersoonID = persoonid;
            Klas = klas;
            Opleiding = opleiding;
            isLeerplichtig = isleerplichtig;
            VorigeSchool = vorigeschool;
        }
        public Student(int studentnummer, int persoonid, string klas, string opleiding, bool isleerplichtig, string vorigeschool, string voornaam,
            string achternaam, string tussenvoegsels, string adres, string postcode, string geboortedatum,
            string woonplaats, string geslacht, string telefoon1, string telefoon2, string email)
            : base(persoonid, voornaam, achternaam, tussenvoegsels, adres, postcode, geboortedatum, email, woonplaats, geslacht, telefoon1,telefoon2)
        {
            StudentNummer = studentnummer;
            PersoonID = persoonid;
            Klas = klas;
            Opleiding = opleiding;
            isLeerplichtig = isleerplichtig;
            VorigeSchool = vorigeschool;
        }
    }

}
