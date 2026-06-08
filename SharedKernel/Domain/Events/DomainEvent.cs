using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Domain.Events
{
    public abstract class DomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
