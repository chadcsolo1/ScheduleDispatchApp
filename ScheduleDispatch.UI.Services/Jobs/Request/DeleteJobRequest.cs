using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleDispatch.UI.Services.Jobs.Request
{
    public sealed class DeleteJobRequest
    {
        public Guid JobId { get; init; }
    }
}
