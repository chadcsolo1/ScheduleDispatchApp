using SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Events
{
    public sealed class JobUpdatedEvent : DomainEvent
    {
        public Guid JobId { get; }

        public JobUpdatedEvent(Guid jobId)
        {
            JobId = jobId;
        }
    }
}
