using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Queries.GetAllJobs
{
    public sealed record GetAllJobsQuery
    (string? SearchTerm = null);
}
