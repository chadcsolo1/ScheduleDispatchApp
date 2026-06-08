using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Enums
{
    public enum JobStatus
    {
        Created = 0,
        Scheduled = 1,
        Assigned = 2,
        InProgress = 3,
        Completed = 4,
        Canceled = 5
    }
}
