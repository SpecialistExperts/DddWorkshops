using System;

namespace DddWorkshops.Model.Meeting.Exceptions
{
    public class AgendaIsAlreadyDefinedException : Exception
    {
        public AgendaIsAlreadyDefinedException(Meeting meeting)
            : base($"Agenda for meeting {meeting} is already defined!")
        {
        }
    }
}