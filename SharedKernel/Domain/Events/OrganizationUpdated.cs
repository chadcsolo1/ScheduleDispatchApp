using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Domain.Events
{
    public class OrganizationUpdated : DomainEvent
    {
        public Guid OrganizationId { get; }

        public OrganizationUpdated(Guid organizationId)
        {
            OrganizationId = organizationId;
        }
    }
}
