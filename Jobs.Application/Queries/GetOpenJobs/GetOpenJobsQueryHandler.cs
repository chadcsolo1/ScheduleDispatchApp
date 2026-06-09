using Jobs.Application.DTOs;
using Jobs.Application.Mappings;
using Jobs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Queries.GetOpenJobs
{
    public sealed class GetOpenJobsQueryHandler
    {
        private readonly IJobReadRepository _jobReadRepository;

        public GetOpenJobsQueryHandler(IJobReadRepository jobReadRepository)
        {
            _jobReadRepository = jobReadRepository;
        }

        public async Task<IReadOnlyList<JobDto>> Handle(GetOpenJobsQuery query, CancellationToken cancellationToken)
        {
            var jobs = await _jobReadRepository.GetOpenJobsAsync(cancellationToken);

            return jobs.Select(JobMappings.ToDto).ToList();
        }
    }
}
