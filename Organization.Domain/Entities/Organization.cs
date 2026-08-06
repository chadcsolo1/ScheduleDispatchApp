using Organizations.Domain.Events;
using SharedKernel.Domain.Events;
using SharedKernel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organizations.Domain.Entities
{
    public class Organization : AggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Organization() { } // EF

        public Organization(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;

            AddDomainEvent(new OrganizationCreated(Id));
        }

        public void UpdateName(string name)
        {
            Name = name;
            AddDomainEvent(new OrganizationUpdated(Id));
        }

        public void Activate()
        {
            IsActive = true;
            AddDomainEvent(new OrganizationActivated(Id));
        }

        public void Deactivate()
        {
            IsActive = false;
            AddDomainEvent(new OrganizationDeactivated(Id));
        }
    }
}
