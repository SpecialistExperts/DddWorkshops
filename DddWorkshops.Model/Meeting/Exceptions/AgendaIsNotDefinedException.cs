using System;

namespace DddWorkshops.Model.Meeting.Exceptions
{
    public class AgendaIsNotDefinedException : Exception
    {
        public AgendaIsNotDefinedException(Meeting meeting)
            : base($"Agenda for meeting {meeting} was not defined!")
        {
        }
    }
}