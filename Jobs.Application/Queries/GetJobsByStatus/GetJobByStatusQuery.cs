using Jobs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Queries.GetJobsByStatus
{
    public sealed record GetJobsByStatusQuery(JobStatus Status);
}
