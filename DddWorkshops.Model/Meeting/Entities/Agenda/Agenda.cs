using DddWorkshops.Common.ModelFramework;

namespace DddWorkshops.Model.Meeting.Entities.Agenda
{
    internal class Agenda : Entity
    {
        private string agendaText;

        private Agenda(string agendaText)
        {
            this.agendaText = agendaText;
        }

        protected internal virtual string AgendaText => agendaText;

        public override string ToString() => agendaText;

        public static Agenda Create(string meetingAgenda)
        {
            return new Agenda(meetingAgenda);
        }

        public void Update(string newMeetingAgenda)
        {
            agendaText = newMeetingAgenda;
        }
    }
}