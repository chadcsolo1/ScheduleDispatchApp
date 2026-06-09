using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Queries.GetJobsForCustomer
{
    public sealed record GetJobsForCustomerQuery(Guid CustomerId);
}
