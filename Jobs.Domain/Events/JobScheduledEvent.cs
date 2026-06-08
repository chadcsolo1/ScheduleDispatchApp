using SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Events
{
    public sealed class JobScheduledEvent : DomainEvent
    {
        public Guid JobId { get; }
        public DateTime ScheduledFor { get; }

        public JobScheduledEvent(Guid jobId, DateTime scheduledFor)
        {
            JobId = jobId;
            ScheduledFor = scheduledFor;
        }
    }
}
