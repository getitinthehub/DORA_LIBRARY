using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace DORA.Objects
{
    public class Persoon
    {
        public int ID { get; private set; }
        public string VoorNaam { get; private set; }
        public string AchterNaam { get; private set; }
        public string TussenVoegsels { get; private set; }
        public string Adres { get; private set; }
        public string PostCode { get; private set; }
        public string GeboorteDatum { get; private set; }
        public string Email { get; private set; }
        public string Woonplaats { get; private set; }
        public string Geslacht { get; private set; }
        public string Telefoon1 { get; private set; }
        public string Telefoon2 { get; private set; }
        public bool Status { get; set; }
        public Persoon(string voornaam, string achternaam, string tussenvoegsels,
            string adres, string postcode, string geboortedatum, string email, string woonplaats,
            string geslacht, string telefoon1)
        {
            VoorNaam = voornaam;
            AchterNaam = achternaam;
            TussenVoegsels = tussenvoegsels;
            Adres = adres;
            PostCode = postcode;
            GeboorteDatum = geboortedatum;
            Email = email;
            Woonplaats = woonplaats;
            Geslacht = geslacht;
            Telefoon1 = telefoon1;
        }

        public Persoon(int id, string voornaam, string achternaam, string tussenvoegsels,
            string adres, string postcode, string geboortedatum, string email, string woonplaats,
            string geslacht, string telefoon1)
        {
            ID = id;
            VoorNaam = voornaam;
            AchterNaam = achternaam;
            TussenVoegsels = tussenvoegsels;
            Adres = adres;
            PostCode = postcode;
            GeboorteDatum = geboortedatum;
            Email = email;
            Woonplaats = woonplaats;
            Geslacht = geslacht;
            Telefoon1 = telefoon1;
        }

        public Persoon(int id, string voornaam, string achternaam, string tussenvoegsels,
            string adres, string postcode, string geboortedatum, string email, string woonplaats,
            string geslacht, string telefoon1, string telefoon2)
        {
            ID = id;
            VoorNaam = voornaam;
            AchterNaam = achternaam;
            TussenVoegsels = tussenvoegsels;
            Adres = adres;
            PostCode = postcode;
            GeboorteDatum = geboortedatum;
            Email = email;
            Woonplaats = woonplaats;
            Geslacht = geslacht;
            Telefoon1 = telefoon1;
            Telefoon2 = telefoon2;
        }

    }
}
