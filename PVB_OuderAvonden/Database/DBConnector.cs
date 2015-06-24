using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DORA.Objects;
using DORA.Objects.Agenda;
using System.Configuration;

namespace DORA.Database
{
    public class DBConnector
    {
        private string ConnectionString { get; set; }
        private SqlConnection sqlConn { get; set; }
        public string ErrorCode { get; private set; }

        public DBConnector(string connectionString)
        {
            ConnectionString = connectionString;
            sqlConn = new SqlConnection(ConnectionString);
        }

        public DBConnector()
        {
            sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["DORAConn"].ConnectionString);
        }

        public bool ValideerGebruiker(Gebruiker g, out Persoon p)
        {
            bool flag = false;

            if(g == null)
            {
                ErrorCode = "Null value 'Gebruiker' passed through";
                p = null;
                return false;
            }
            else
            {
                SqlCommand sqlComm = new SqlCommand("spGetPersoon", sqlConn);
                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@GEBRUIKERSNAAM", g.GebruikersNaam);
                sqlComm.Parameters.AddWithValue("@WACHTRWOORD", g.WachtWoord);
                
                Persoon persoonGegevens = null;
                try
                {
                    if (sqlConn.State != System.Data.ConnectionState.Open)
                    {
                        sqlConn.Open();
                        SqlDataReader reader = sqlComm.ExecuteReader();
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                persoonGegevens = new Persoon((int)reader["PersoonID"],
                                    (string)reader["Voornaam"], (string)reader["Achternaam"], (string)reader["Tussenvoegsel"], (string)reader["Adres"],
                                    (string)reader["Postcode"], (string)reader["Geboortedatum"], (string)reader["Woonplaats"], (string)reader["Geslacht"],
                                    (string)reader["Telefoon1"], (string)reader["Telefoon2"], (string)reader["Email"]);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    ErrorCode = ex.Message;
                }
                finally
                {
                    sqlConn.Close();
                }
                p = persoonGegevens;
                if(persoonGegevens == null)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool GetPersoon(Gebruiker g, out Persoon p)
        {
            bool flag = false;
            Persoon pOpgehaald = null;
            if(g == null)
            {
                ErrorCode = "Null value 'Gebruiker' passed through";
                p = null;
                return false;
            }
            else
            {
                SqlCommand sqlComm = new SqlCommand("spGetPersoon", sqlConn);
                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@GEBRUIKERSNAAM", g.GebruikersNaam);
                sqlComm.Parameters.AddWithValue("@WACHTWOORD", g.WachtWoord);

                try
                {
                    sqlConn.Open();
                    SqlDataReader reader = sqlComm.ExecuteReader();

                    if(reader.HasRows)
                    {
                        pOpgehaald = new Persoon((int)reader["ID"], (string)reader["VoorNaam"], (string)reader["AchterNaam"], (string)reader["TussenVoegsels"], (string)reader["Adres"],
                            (string)reader["PostCode"], (string)reader["GeboorteDatum"], (string)reader["Email"], (string)reader["Woonplaats"], (string)reader["Geslacht"], (string)reader["Telefoon1"]);
                    }
                }
                catch(Exception ex)
                {
                    ErrorCode = ex.Message;
                }
                finally
                {
                    sqlConn.Close();
                }
            }

            p = pOpgehaald;
            return flag;
        }

        public List<Klas> GetKlassen()
        {
            List<Klas> klassen = new List<Klas>();

            try
            {
                sqlConn.Open();
                SqlCommand sqlComm = new SqlCommand("spGetKlassen", sqlConn);
                SqlDataReader reader = sqlComm.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        klassen.Add(new Klas((string)reader["Klas"], (string)reader["DocentAfkorting"]));
                    }
                }

            }
            catch(Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return klassen;
        }

        public List<Lokaal> GetLokalen()
        {
            List<Lokaal> Lokalen = new List<Lokaal>();

            try
            {
                sqlConn.Open();
                SqlCommand sqlComm = new SqlCommand("spGetLokalen", sqlConn);
                SqlDataReader reader = sqlComm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Lokalen.Add(new Lokaal((string)reader["Lokaal"]));
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return Lokalen;
        }

        public List<Docent> GetDocenten()
        {
            List<Docent> Docenten = new List<Docent>();

            try
            {
                sqlConn.Open();
                SqlCommand sqlComm = new SqlCommand("spGetDocenten", sqlConn);
                SqlDataReader reader = sqlComm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Docenten.Add(new Docent((string)reader["DocentAfkorting"], (bool)reader["Beheerder"], (int)reader["PersoonID"],
                            (string)reader["Voornaam"], (string)reader["Achternaam"], (string)reader["Tussenvoegsel"], (string)reader["Adres"],
                            (string)reader["Postcode"], (string)reader["Geboortedatum"], (string)reader["Woonplaats"], (string)reader["Geslacht"],
                            (string)reader["Telefoon1"], (string)reader["Telefoon2"], (string)reader["Email"]));
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return Docenten;
        }

        public bool GetDocent(string docentAfkorting, out Docent d)
        {
            bool flag = false;
            Docent dOpgehaald = null;

            try
            {
                sqlConn.Open();
                SqlCommand sqlComm = new SqlCommand("spGetDocent", sqlConn);
                SqlDataReader reader = sqlComm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dOpgehaald = new Docent((string)reader["DocentAfkorting"], (bool)reader["Beheerder"], (int)reader["PersoonID"],
                            (string)reader["Voornaam"], (string)reader["Achternaam"], (string)reader["Tussenvoegsel"], (string)reader["Adres"], 
                            (string)reader["Postcode"], (string)reader["Geboortedatum"], (string)reader["Woonplaats"], (string)reader["Geslacht"], 
                            (string)reader["Telefoon1"], (string)reader["Telefoon2"], (string)reader["Email"]);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            d = dOpgehaald;
            return flag;
        }

        public bool GetStudent(string studentNummer, out Student s)
        {
            bool flag = false;
            Student sOpgehaald = null;

            try
            {
                sqlConn.Open();
                SqlCommand sqlComm = new SqlCommand("spGetStudent", sqlConn);
                SqlDataReader reader = sqlComm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sOpgehaald = new Student((string)reader["StudentNummer"], (int)reader["ID"], (string)reader["Klas"], (string)reader["Opleiding"],
                            (bool)reader["Leerplichtig"], (string)reader["VorigeSchool"], (string)reader["Voornaam"], (string)reader["Achternaam"], 
                            (string)reader["Tussenvoegsel"], (string)reader["Adres"], (string)reader["Postcode"], (string)reader["Geboortedatum"],
                            (string)reader["Woonplaats"], (string)reader["Geslacht"], (string)reader["Telefoon1"], (string)reader["Telefoon2"], 
                            (string)reader["Email"]);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            s = sOpgehaald;
            return flag;
        }

        public List<DocentBeschikbaarheid> GetDocentBeschikbaarheid(string docentAfkorting)
        {
            List<DocentBeschikbaarheid> beschikbaarheden = null;
            SqlCommand sqlComm = new SqlCommand("spGetDocentBeschikbaarheid", sqlConn);
            sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
            sqlComm.Parameters.AddWithValue("@DOCAFKORTING", docentAfkorting);
            try
            {
                sqlConn.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();
                if(reader.HasRows)
                {
                    beschikbaarheden = new List<DocentBeschikbaarheid>();
                    while(reader.Read())
                    {
                        beschikbaarheden.Add(new DocentBeschikbaarheid((string)reader["DocentAfkorting"],(string)reader["DatumBeschikbaar"],(string)reader["TijdBeschikbaar"]));
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return beschikbaarheden;
        }

        public List<DocentBeschikbaarheid> GetDocentenBeschikbaarheid()
        {
            List<DocentBeschikbaarheid> beschikbaarheden = null;

            SqlCommand sqlComm = new SqlCommand("spGetDocentBeschikbaarheid", sqlConn);
            sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                sqlConn.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();
                if (reader.HasRows)
                {
                    beschikbaarheden = new List<DocentBeschikbaarheid>();
                    while (reader.Read())
                    {
                        beschikbaarheden.Add(new DocentBeschikbaarheid((string)reader["DocentAfkorting"], (string)reader["DatumBeschikbaar"], (string)reader["TijdBeschikbaar"]));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return beschikbaarheden;
        }

        public List<AgendaItem> GetAgendaItems()
        {
            List<AgendaItem> agendaItems = null;
            SqlCommand sqlComm = new SqlCommand("spGetAgendaItems", sqlConn);
            sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                sqlConn.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();
                if(reader.HasRows)
                {
                    agendaItems = new List<AgendaItem>();
                    while(reader.Read())
                    {
                        Gesprek g = new Gesprek(int.Parse(reader["GesprekID"].ToString()), (string)reader["StudentNummer"], (string)reader["DocentAfkorting"], int.Parse(reader["PersoonID"].ToString()));
                        agendaItems.Add(new AgendaItem(g, (string)reader["Datum"], (string)reader["BeginTijd"], (string)reader["EindTijd"], (string)reader["Locatie"], (string)reader["Lokaal"]));
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }
            return agendaItems;
        }

        public List<AgendaItem> GetSpecifiekDocentAgendaItems(string docAfkorting)
        {
            List<AgendaItem> agendaItems = null;

            SqlCommand sqlComm = new SqlCommand("spGetDocentAgendaItems", sqlConn);
            sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
            sqlComm.Parameters.AddWithValue("@DOCAFKORTING", docAfkorting);
            try
            {
                sqlConn.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();
                if (reader.HasRows)
                {
                    agendaItems = new List<AgendaItem>();
                    while (reader.Read())
                    {
                        Gesprek g = new Gesprek(int.Parse(reader["GesprekID"].ToString()), (string)reader["StudentNummer"], (string)reader["DocentAfkorting"], int.Parse(reader["PersoonID"].ToString()));
                        agendaItems.Add(new AgendaItem(g, (string)reader["Datum"], (string)reader["BeginTijd"], (string)reader["EindTijd"], (string)reader["Locatie"], (string)reader["Lokaal"]));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return agendaItems;
        }

        public List<Gesprek> GetDocentGesprekken(string docAfkorting)
        {
            List<Gesprek> docentGesprekken = null;
            SqlCommand sqlComm = new SqlCommand("spGetDocentGesprekken", sqlConn);
            sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
            sqlComm.Parameters.AddWithValue("@DOCAFKORTING", docAfkorting);

            try
            {
                sqlConn.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();
                if(reader.HasRows)
                {
                    docentGesprekken = new List<Gesprek>();
                    while(reader.Read())
                    {
                        docentGesprekken.Add(new Gesprek(int.Parse(reader["ID"].ToString()), (string)reader["StudentNumer"], (string)reader["DocentAfkorting"], int.Parse(reader["PersoonID"].ToString()),
                            (string)reader["ThuisSituatie"], (string)reader["StudieKeuze"], (string)reader["Voortgang"], (string)reader["Presentie"], (string)reader["Motivatie"], (string)reader["AlgemeenWelbevinden"], (string)reader["VerwachtingsPatroon"], int.Parse(reader["Gespreksduur"].ToString())));
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return docentGesprekken;
        }

        public List<Gesprek> GetGesprekken()
        {
            List<Gesprek> docentGesprekken = null;
            SqlCommand sqlComm = new SqlCommand("spGetGesprekken", sqlConn);
            sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                sqlConn.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();
                if (reader.HasRows)
                {
                    docentGesprekken = new List<Gesprek>();
                    while (reader.Read())
                    {
                        docentGesprekken.Add(new Gesprek(int.Parse(reader["ID"].ToString()), (string)reader["StudentNumer"], (string)reader["DocentAfkorting"], int.Parse(reader["PersoonID"].ToString()),
                            (string)reader["ThuisSituatie"], (string)reader["StudieKeuze"], (string)reader["Voortgang"], (string)reader["Presentie"], (string)reader["Motivatie"], (string)reader["AlgemeenWelbevinden"], (string)reader["VerwachtingsPatroon"], int.Parse(reader["Gespreksduur"].ToString())));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return docentGesprekken;
        }

        public List<UniekeCode> GetUniekeCodes()
        {
            List<UniekeCode> uniekeCodes = null;

            SqlCommand sqlComm = new SqlCommand("", sqlConn);
            sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                sqlConn.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();
                if(reader.HasRows)
                {
                    uniekeCodes = new List<UniekeCode>();
                    while(reader.Read())
                    {
                        uniekeCodes.Add(new UniekeCode((string)reader["Code"], (string)reader["StudentNummer"], (bool)reader["CodeType"]));
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return uniekeCodes;
        }

        public UniekeCode GetUniekeCode(string studentNummer)
        {
            UniekeCode studentCode = null;
            SqlCommand sqlComm = new SqlCommand("spGetStudentUniekeCode", sqlConn);
            sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
            sqlComm.Parameters.AddWithValue("@STUDENTNUMMER", studentNummer);
            try
            {
                sqlConn.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();
                if(reader.HasRows)
                {
                    studentCode = new UniekeCode((string)reader["Code"], (string)reader["StudentNummer"], (bool)reader["CodeType"]);
                }
            }
            catch(Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return studentCode;
        }

        internal bool KoppelStudentAanUniekeCode(UniekeCode studentUniekeCode)
        {
            SqlCommand sqlComm = new SqlCommand("spKoppelStudentAanUniekeCode", sqlConn);
            sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
            sqlComm.Parameters.AddWithValue("@STUDENTNUMMER",studentUniekeCode.StudentNummer);
            sqlComm.Parameters.AddWithValue("@UNIEKECODE",studentUniekeCode.Code);
            sqlComm.Parameters.AddWithValue("@TYPECODE",studentUniekeCode.isKoppelCode);
            int success = 0;
            try
            {
                sqlConn.Open();
                success = int.Parse(sqlComm.ExecuteScalar().ToString());
            }
            catch(Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            if(success == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal CodeGenerator.LaatstGegenereerdeCode GetLaatsteUniekeCodes(string docentAfkorting)
        {
            CodeGenerator.LaatstGegenereerdeCode laatsteCodes = null;
            SqlCommand sqlComm = new SqlCommand("spGetLaatsteUniekeCodes", sqlConn);
            sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
            sqlComm.Parameters.AddWithValue("@DOCAFKORTING", docentAfkorting);
            try
            {
                sqlConn.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();
                if(reader.HasRows)
                {
                    laatsteCodes = new CodeGenerator.LaatstGegenereerdeCode();
                    while(reader.Read())
                    {
                        UniekeCode uniekeCode = new UniekeCode((string)reader["Code"], (string)reader["StudentNummer"], (bool)reader["CodeType"]);
                        switch(uniekeCode.isKoppelCode)
                        {
                            case true:
                                laatsteCodes.LaatsteKoppelCode = uniekeCode;
                                break;
                            case false:
                                laatsteCodes.LaatsteRegistratieCode = uniekeCode;
                                break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return laatsteCodes;
        }

        internal bool ImporteerObjecten(List<object> objectsOpteSlaan) // out List<object> ObjectenNietOpgeslagen
        {
            bool flag = false;
            List<SqlCommand> sqlCommands = new List<SqlCommand>();
            //List<object> objectenNietOpgeslagen = new List<object>();
            foreach (Object o in objectsOpteSlaan)
            {
                sqlCommands.Add(PrepObjectOmOpTeslaan(o));
            }

            int success = 0;
            try
            {
                sqlConn.Open();
                foreach(SqlCommand sqlComm in sqlCommands)
                {
                    success = int.Parse(sqlComm.ExecuteScalar().ToString());
                    if(success == 0)
                    {
                        // TODO: return objects that are not saved
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            if (success == 1)
            {
                flag = true;
            }

            return flag;
        }

        public SqlCommand PrepObjectOmOpTeslaan(Object objOpteSlaan)
        {
            SqlCommand sqlComm = null;
            if (objOpteSlaan.GetType() == typeof(Student))
            {
                Student s = (Student)objOpteSlaan;
                sqlComm = new SqlCommand("spStudentOpslaan", sqlConn);
                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@STUDENTNUMMER", s.StudentNummer);
                sqlComm.Parameters.AddWithValue("@KLAS", s.Klas);
                sqlComm.Parameters.AddWithValue("@OPLEIDING", s.Opleiding);
                sqlComm.Parameters.AddWithValue("@LEERPLICHTIG", s.isLeerplichtig);
                sqlComm.Parameters.AddWithValue("@VORIGESCHOOL", s.VorigeSchool);
                sqlComm.Parameters.AddWithValue("@VOORNAAM", s.VoorNaam);
                sqlComm.Parameters.AddWithValue("@ACHTERNAAM", s.AchterNaam);
                sqlComm.Parameters.AddWithValue("@TUSSENVOEGSEL", s.TussenVoegsels);
                sqlComm.Parameters.AddWithValue("@ADRES", s.Adres);
                sqlComm.Parameters.AddWithValue("@POSTCODE", s.PostCode);
                sqlComm.Parameters.AddWithValue("@GEBOORTEDATUM", s.GeboorteDatum);
                sqlComm.Parameters.AddWithValue("@GEBOORTEPLAATS", s.Geboorteplaats);
                sqlComm.Parameters.AddWithValue("@EMAIL", s.Email);
                sqlComm.Parameters.AddWithValue("@GESLACHT", s.Geslacht);
                sqlComm.Parameters.AddWithValue("@WOONPLAATS", s.Woonplaats);
                sqlComm.Parameters.AddWithValue("@TELEFOON1", s.Telefoon1);
                sqlComm.Parameters.AddWithValue("@TELEFOON2", s.Telefoon2);
                sqlComm.Parameters.AddWithValue("@STATUS", s.Status);
            }
            else if (objOpteSlaan.GetType() == typeof(Docent))
            {
                Docent d = (Docent)objOpteSlaan;
                sqlComm = new SqlCommand("spDocentOpslaan", sqlConn);
                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@DOCAFKORTING", d.DocentAfkorting);
                sqlComm.Parameters.AddWithValue("@ISBEHEERDER", d.IsBeheerder);
                sqlComm.Parameters.AddWithValue("@VOORNAAM", d.VoorNaam);
                sqlComm.Parameters.AddWithValue("@ACHTERNAAM", d.AchterNaam);
                sqlComm.Parameters.AddWithValue("@TUSSENVOEGSEL", d.TussenVoegsels);
                sqlComm.Parameters.AddWithValue("@ADRES", d.Adres);
                sqlComm.Parameters.AddWithValue("@POSTCODE", d.PostCode);
                sqlComm.Parameters.AddWithValue("@GEBOORTEDATUM", d.GeboorteDatum);
                sqlComm.Parameters.AddWithValue("@EMAIL", d.Email);
                sqlComm.Parameters.AddWithValue("@GESLACHT", d.Geslacht);
                sqlComm.Parameters.AddWithValue("@WOONPLAATS", d.Woonplaats);
                sqlComm.Parameters.AddWithValue("@TELEFOON1", d.Telefoon1);
                sqlComm.Parameters.AddWithValue("@TELEFOON2", d.Telefoon2);
                sqlComm.Parameters.AddWithValue("@STATUS", d.Status);

            }
            else if (objOpteSlaan.GetType() == typeof(Klas))
            {
                Klas k = (Klas)objOpteSlaan;
                sqlComm = new SqlCommand("spKlasOpslaan", sqlConn);
                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@DOCAFKORTING", k.DocentAfkorting);
                sqlComm.Parameters.AddWithValue("@KLAS", k.klas);
            }
            return sqlComm;
        }
    }
}
