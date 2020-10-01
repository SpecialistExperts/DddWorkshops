using System;
using DddWorkshops.Common.Guard;
using DddWorkshops.Common.ModelFramework;

namespace DddWorkshops.Model.Meeting.ValueObjects
{
    internal class Term : ValueObject<Term>
    {
        private Term(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public static Term Set(DateTime startTime, DateTime endTime)
        {
            Guard.With<ArgumentException>().Against(
                startTime > endTime,
                $"{nameof(startTime)}, {nameof(endTime)}",
                "Start time should occur earlier than end time!");

            return new Term(startTime, endTime);
        }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        protected override bool InternalEquals(Term valueObject) => 
            valueObject.StartDate == StartDate && valueObject.EndDate == EndDate;

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), StartDate, EndDate);
    }
}