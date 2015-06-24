using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DORA.AppSecurity
{
    public static class AppSecurity
    {
        public static bool SQLInjectionCheck(string value)
        {
            if (value.Contains('{') || value.Contains('}') || value.Contains('<') || value.Contains('>') || value.Contains('-') ||
                value.Contains('='))
            {
                return false;
            }
            return true;
        }
        public static bool SessionCheck(Objects.Gebruiker g)
        {
            if (!SQLInjectionCheck(g.GebruikersNaam) || !SQLInjectionCheck(g.WachtWoord) || g.GebruikersNaam == "" || g.WachtWoord == "" || g.GebruikersNaam.Length < 8 || g.WachtWoord.Length < 8)
            {
                return false;
            }
            return true;
        }
    }
}
