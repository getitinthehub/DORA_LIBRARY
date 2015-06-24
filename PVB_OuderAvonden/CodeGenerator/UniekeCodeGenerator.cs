using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DORA.Objects;
using System.Configuration;

namespace DORA.CodeGenerator
{
    public class UniekeCodeGenerator
    {
        public LaatstGegenereerdeCode LaatsteCodes { get; private set; }
        public string ErrorCode { get; private set; }
        public UniekeCodeGenerator(string docentAfkorting)
        {
            if(!GetLaatsteUniekeCodes(docentAfkorting))
            {
                ErrorCode = "Kan niet de laatst gegenereerde unieke codes ophalen.";
            }
        }

        public bool GetLaatsteUniekeCodes(string docentAfkorting)
        {
            Database.DBConnector dbConnectie = new Database.DBConnector();
            LaatsteCodes = dbConnectie.GetLaatsteUniekeCodes(docentAfkorting);

            if(LaatsteCodes != null && LaatsteCodes.LaatsteKoppelCode != null && LaatsteCodes.LaatsteRegistratieCode != null)
            {
                return true;
            }
            return false;
        }

        public UniekeCode GetUniekeCode(string studentNummer)
        {
            Database.DBConnector dbInstance = new Database.DBConnector();
            return dbInstance.GetUniekeCode(studentNummer);
        }

        public bool KoppelNieuweCodeAanStudent(UniekeCode studentUniekeCode)
        {
            Database.DBConnector dbInstance = new Database.DBConnector();
            if(dbInstance.KoppelStudentAanUniekeCode(studentUniekeCode))
            {
                return true;
            }
            return false;
        }

        /* Unieke code format:
         * Registratiecode: 000-XXX00-00
         * KoppelCode:      XXX00-000-00
         * 
         * XXX staat voor 3 letters van de docent
         * 00 staat voor 2 cijfers van de docent
         * -> voorbeeld: er zijn 2 docenten met dezelfde afkorting, aldus een cijfer erachter
         *      om deze docenten te kunnen onderscheiden.
         */

        public UniekeCode GenereerNieuweCode(string studentnummer, UniekeCodeType typeCode)
        {
            UniekeCode nieuweUniekeCode = null;
            int LaatsteNummeriekDeel1 = 0;
            int LaatsteNummeriekDeel2 = 0;
            string docentAfkorting = "";
            string[] laatsteCodeDelen = null;

            switch(typeCode)
            {
                case UniekeCodeType.REGISTRATIE:
                    laatsteCodeDelen = LaatsteCodes.LaatsteRegistratieCode.Code.Split('-');
                    LaatsteNummeriekDeel1 = int.Parse(laatsteCodeDelen[0]);
                    docentAfkorting = laatsteCodeDelen[1];
                    LaatsteNummeriekDeel2 = int.Parse(laatsteCodeDelen[2]);
                    break;
                case UniekeCodeType.KOPPEL:
                    laatsteCodeDelen = LaatsteCodes.LaatsteRegistratieCode.Code.Split('-');
                    docentAfkorting = laatsteCodeDelen[0];
                    LaatsteNummeriekDeel1 = int.Parse(laatsteCodeDelen[1]);
                    LaatsteNummeriekDeel2 = int.Parse(laatsteCodeDelen[2]);
                    break;
                default:
                    ErrorCode = "Niet bestaande unieke code type gekozen.";
                    break;
            }

            if(LaatsteNummeriekDeel1 == 999 && LaatsteNummeriekDeel2 == 99)
            {
                // Maximum aantal unieke codes bereikt.
                ErrorCode = "Maximum aantal codes gegenereerd";
            }
            else
            {
                // Maximum nog niet bereikt, dus hoog op met 1
                if (LaatsteNummeriekDeel2 == 99)
                {
                    // maximum van nummeriek deel2 bereikt, hoog nummeriek deel1 op
                    LaatsteNummeriekDeel1++;
                    LaatsteNummeriekDeel2 = 0;
                }
                else
                {
                    // maximum van nummeriek deel2 nog niet bereikt, dus hoog het op met 1
                    LaatsteNummeriekDeel2++;
                }
            }

            switch(typeCode)
            {
                case UniekeCodeType.REGISTRATIE:
                    nieuweUniekeCode = new UniekeCode(FormatUniekeCode(UniekeCodeType.REGISTRATIE, docentAfkorting, LaatsteNummeriekDeel1, LaatsteNummeriekDeel2), studentnummer, false);
                    break;
                case UniekeCodeType.KOPPEL:
                    nieuweUniekeCode = new UniekeCode(FormatUniekeCode(UniekeCodeType.KOPPEL, docentAfkorting, LaatsteNummeriekDeel1, LaatsteNummeriekDeel2), studentnummer, false);
                    break;
                default:
                    ErrorCode = "Niet bestaande unieke code type gekozen.";
                    break;
            }

            return nieuweUniekeCode;
        }

        private string FormatUniekeCode(UniekeCodeType codeType, string docentAfkorting, int nummeriekDeel1, int nummeriekDeel2)
        {
            string nummeriekDeel1Formatted = "", nummeriekDeel2Formatted = "", formattedUniekeCode = "";
            
            if(nummeriekDeel1 < 10)
            {
                nummeriekDeel1Formatted = string.Format("00{0}", nummeriekDeel1);
            }
            else if(nummeriekDeel1 >= 10)
            {
                nummeriekDeel1Formatted = string.Format("0{0}", nummeriekDeel1);
            }

            if(nummeriekDeel2 < 10)
            {
                nummeriekDeel2Formatted = string.Format("0{0}", nummeriekDeel2);
            }
            else if(nummeriekDeel2 >= 10)
            {
                nummeriekDeel2Formatted = string.Format("{0}", nummeriekDeel2);
            }
            switch(codeType)
            {
                case UniekeCodeType.REGISTRATIE:
                    formattedUniekeCode = string.Format("{0}-{1}-{2}",nummeriekDeel1Formatted,docentAfkorting,nummeriekDeel2Formatted);
                    break;
                case UniekeCodeType.KOPPEL:
                    formattedUniekeCode = string.Format("{0}-{1}-{2}", docentAfkorting, nummeriekDeel1Formatted, nummeriekDeel2Formatted);
                    break;
                default:
                    ErrorCode = "Niet bestaande unieke code type gekozen.";
                    break;
            }
            return formattedUniekeCode;
        }
    }
}
