using SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Events
{
    public sealed class JobCanceledEvent : DomainEvent
    {
        public Guid JobId { get; }
        public string Reason { get; }

        public JobCanceledEvent(Guid jobId, string reason)
        {
            JobId = jobId;
            Reason = reason;
        }
    }
}
