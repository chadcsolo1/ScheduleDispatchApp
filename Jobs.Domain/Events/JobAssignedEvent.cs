using SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Events
{
    public sealed class JobAssignedEvent : DomainEvent
    {
        public Guid JobId { get; }
        public Guid TechnicianId { get; }

        public JobAssignedEvent(Guid jobId, Guid technicianId)
        {
            JobId = jobId;
            TechnicianId = technicianId;
        }
    }
}
