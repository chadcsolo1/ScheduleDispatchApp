using SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Events
{
    public sealed class JobCompletedEvent : DomainEvent
    {
        public Guid JobId { get; }

        public JobCompletedEvent(Guid jobId)
        {
            JobId = jobId;
        }
    }
}
