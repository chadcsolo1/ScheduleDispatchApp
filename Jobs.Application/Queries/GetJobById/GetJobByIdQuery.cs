using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Queries.GetJobById
{
    public sealed record GetJobByIdQuery(Guid JobId);
}
