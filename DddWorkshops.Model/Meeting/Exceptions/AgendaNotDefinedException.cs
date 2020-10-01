using System;

namespace DddWorkshops.Model.Meeting.Exceptions
{
    public class AgendaNotDefinedException : Exception
    {
        public AgendaNotDefinedException(Meeting meeting)
            : base($"Agenda for meeting {meeting} was not defined!")
        {
        }
    }
}