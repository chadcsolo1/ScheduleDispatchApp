using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.ValueObjects
{
    public record JobType(
        string JobTypeName,
        string JobTypeCategory,
        TimeSpan JobTypeEstimatedDuration
        );
}
