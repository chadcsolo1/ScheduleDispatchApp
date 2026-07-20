using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleDispatch.UI.Services.Jobs.Request
{
    public sealed class UpdateJobRequest
    {
        public string Description { get; init; } = string.Empty;

        public string AddressLine1 { get; init; } = string.Empty;
        public string? AddressLine2 { get; init; }
        public string City { get; init; } = string.Empty;
        public string State { get; init; } = string.Empty;
        public int ZipCode { get; init; }

        public string JobTypeName { get; init; } = string.Empty;
        public string JobTypeCategory { get; init; } = string.Empty;
        public TimeSpan JobTypeEstimatedDuration { get; init; }

        public List<string> RequiredSkills { get; init; } = new();
    }
}
