using SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organizations.Domain.Events
{
    public class OrganizationDeactivated : DomainEvent
    {
        public Guid OrganizationId { get; }

        public OrganizationDeactivated(Guid organizationId)
        {
            OrganizationId = organizationId;
        }
    }
}
