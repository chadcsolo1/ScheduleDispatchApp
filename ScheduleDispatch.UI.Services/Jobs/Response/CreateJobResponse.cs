using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleDispatch.UI.Services.Jobs.Response
{
    public sealed class CreateJobResponse
    {
        public Guid JobId { get; init; }
        public DateTime CreatedAt { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
