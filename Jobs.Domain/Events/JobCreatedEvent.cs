using SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Events
{
    public sealed class JobCreatedEvent : DomainEvent
    {
        public Guid JobId { get; }

        public JobCreatedEvent(Guid jobId)
        {
            JobId = jobId;
        }
    }
}
