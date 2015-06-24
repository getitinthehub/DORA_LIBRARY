using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DORA.BestandImporteren
{
    /*  Hier worden de codes opgeslagen voor het importeren van bestanden,
     *  en het opslaan van de gegevens in die bestanden.
     *  
     *  Bestanden kunnen worden geïmporteerd mits het bestand een 
     *  tekstbestand/csv bestand is en de waardes gesplit zijn met een ';'.
     *  
     */
    public class ImportBestand
    {
        public string ErrorCode { get; private set; }
        public List<Objects.Student> HaalStudentenGegevensOp(string pad)
        {
            if(pad == "")
            {
                ErrorCode = "Geen valide pad opgegeven.";
                return null;
            }

            List<Objects.Student> opgehaaaldeStudenten = new List<Objects.Student>();
            StreamReader sr = new StreamReader(pad);
            try
            {
                while (sr.EndOfStream)
                {
                    string rij = sr.ReadLine();
                    string[] kolomInhoud = rij.Split(';');
                    bool leerplichtig = false;
                    if (kolomInhoud[16].ToLower() == "leerplichtig" || kolomInhoud[16].ToLower() == "ja")
                    {
                        leerplichtig = true;
                    }
                    opgehaaaldeStudenten.Add(new Objects.Student(kolomInhoud[0], kolomInhoud[1], kolomInhoud[2], kolomInhoud[3], kolomInhoud[4],
                        kolomInhoud[5], kolomInhoud[6], kolomInhoud[7], kolomInhoud[9], kolomInhoud[11], kolomInhoud[12], kolomInhoud[13],
                        kolomInhoud[14], kolomInhoud[15], leerplichtig, kolomInhoud[17], kolomInhoud[18]));
                }

            }
            catch(Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sr.Dispose();
            }
            return opgehaaaldeStudenten;
        }

        public bool ImporteerNaarDatabase(List<Object> objectsOpteSlaan)
        {
            Database.DBConnector dbConnectie = new Database.DBConnector();
            return dbConnectie.ImporteerObjecten(objectsOpteSlaan);
        }
    }
}
