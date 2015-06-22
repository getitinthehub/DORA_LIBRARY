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
        public int AgendaItemID { get; private set; }
        public Gesprek AgendaItemGesprek { get; private set; }
        public int GesprekID { get; private set; }
        public string Datum { get; private set; }
        public string Tijd { get; private set; }
        public string Locatie { get; private set; }
        public string Lokaal { get; private set; }

        public AgendaItem(int agendaItemID, int gesprekID, string datum, string tijd, string locatie, string lokaal)
        {
            AgendaItemID = agendaItemID;
            GesprekID = gesprekID;
            Datum = datum;
            Tijd = tijd;
            Locatie = locatie;
            Lokaal = lokaal;
        }
        public AgendaItem(int agendaItemID, Gesprek gesprek, string datum, string tijd, string locatie, string lokaal)
        {
            AgendaItemID = agendaItemID;
            AgendaItemGesprek = gesprek;
            GesprekID = gesprek.ID;
            Datum = datum;
            Tijd = tijd;
            Locatie = locatie;
            Lokaal = lokaal;
        }
    }
}
