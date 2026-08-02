using SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organization.Domain.Events
{
    public class OrganizationActivated : DomainEvent
    {
        public Guid OrganizationId { get; }

        public OrganizationActivated(Guid organizationId)
        {
            OrganizationId = organizationId;
        }
    }
}
