using Jobs.Application.Abstractions;
using Jobs.Application.DTOs;
using Jobs.Application.Mappings;
using Jobs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Queries.GetAllJobs
{
    public sealed class GetAllJobsQueryHandler : IQueryHandler<GetAllJobsQuery, IEnumerable<JobDto>>
    {
        private readonly IJobReadRepository _jobReadRepository;

        public GetAllJobsQueryHandler(IJobReadRepository jobReadRepository)
        {
            _jobReadRepository = jobReadRepository;
        }

        public async Task<IEnumerable<JobDto>> Handle(GetAllJobsQuery query, CancellationToken cancellationToken)
        {
            var jobs = await _jobReadRepository.GetAllAsync(cancellationToken);
            return jobs.Select(JobMappings.ToDto);
        }
    }
}
