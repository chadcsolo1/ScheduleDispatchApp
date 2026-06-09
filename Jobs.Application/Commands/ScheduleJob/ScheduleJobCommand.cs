using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Commands.ScheduleJob
{
    public sealed record ScheduleJobCommand(
        Guid JobId,
        DateTime ScheduledFor);
}
