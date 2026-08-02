using SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organization.Domain.Events
{
    public class OrganizationCreated : DomainEvent
    {
        public Guid OrganizationId { get; }

        public OrganizationCreated(Guid organizationId)
        {
            OrganizationId = organizationId;
        }
    }
}
