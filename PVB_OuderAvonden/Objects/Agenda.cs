using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DORA.Objects.Agenda
{
    public class Agenda
    {
        public Dictionary<string, AgendaItem> AgendaItems { get; private set; }

        public Agenda() { AgendaItems = new Dictionary<string, AgendaItem>(); }
        public Agenda(Dictionary<string, AgendaItem> agendaItems)
        {
            AgendaItems = agendaItems;
        }

        public bool PlanAgendaItem(AgendaItem agendaItem)
        {
            if (!AgendaItems.ContainsKey(agendaItem.Datum))
            {
                AgendaItems.Add(agendaItem.Datum, agendaItem);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EditAgendaItem(AgendaItem oudItem, AgendaItem nieuwItem)
        {
            if (!AgendaItems.ContainsKey(oudItem.Datum) && !AgendaItems.ContainsKey(nieuwItem.Datum))
            {
                AgendaItems[oudItem.Datum] = nieuwItem;
                return true;
            }
            else
            {
                return false;
            }
        }


    }

    public class AgendaItem
    {
        public Gesprek AgendaItemGesprek { get; private set; }
        public int GesprekID { get; private set; }
        public string Datum { get; private set; }
        public string BeginTijd { get; private set; }
        public string EindTijd { get; private set; }
        public string Locatie { get; private set; }
        public string Lokaal { get; private set; }

        public AgendaItem(Gesprek agendaItemGesprek, string datum, string begintijd, string eindTijd, string locatie, string lokaal)
        {
            AgendaItemGesprek = agendaItemGesprek;
            GesprekID = agendaItemGesprek.ID;
            Datum = datum;
            BeginTijd = begintijd;
            EindTijd = eindTijd;
            Locatie = locatie;
            Lokaal = lokaal;
        }
        public AgendaItem(int gesprekID, string datum, string begintijd, string eindTijd, string locatie, string lokaal)
        {
            AgendaItemGesprek = null;
            GesprekID = gesprekID;
            Datum = datum;
            BeginTijd = begintijd;
            EindTijd = eindTijd;
            Locatie = locatie;
            Lokaal = lokaal;
        }
    }
}
