using System;
using MediatR;

namespace DddWorkshops.Common.ModelFramework
{
    public abstract class BaseDomainEvent : INotification
    {
        protected BaseDomainEvent()
        {
            Guid = Guid.NewGuid();
            OccuredOn = DateTimeProvider.UtcNow;
        }

        /// <summary>
        ///     Date of event occurence.
        /// </summary>
        public DateTime OccuredOn { get; }

        /// <summary>
        ///     Unique identifier of a domain event.
        /// </summary>
        public Guid Guid { get; protected set; }
    }
}