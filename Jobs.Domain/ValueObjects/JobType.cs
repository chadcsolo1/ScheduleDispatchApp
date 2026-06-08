using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.ValueObjects
{
    public record JobType(
        string Name,
        string Category,
        TimeSpan EstimatedDuration
        );
}
