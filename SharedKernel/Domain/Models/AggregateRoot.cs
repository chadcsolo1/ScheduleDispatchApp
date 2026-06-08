using SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Domain.Models
{
    public abstract class AggregateRoot
    {
        private readonly List<DomainEvent> _domainEvents = new();

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
