using Jobs.Application.DTOs;
using Jobs.Application.Mappings;
using Jobs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Queries.GetJobsForCustomer
{
    public sealed class GetJobsForCustomerQueryHandler
    {
        private readonly IJobReadRepository _jobReadRepository;

        public GetJobsForCustomerQueryHandler(IJobReadRepository jobReadRepository)
        {
            _jobReadRepository = jobReadRepository;
        }

        public async Task<IReadOnlyList<JobDto>> Handle(GetJobsForCustomerQuery query, CancellationToken cancellationToken)
        {
            var jobs = await _jobReadRepository.GetJobsForCustomerAsync(query.CustomerId, cancellationToken);

            return jobs.Select(JobMappings.ToDto).ToList();
        }
    }
}
