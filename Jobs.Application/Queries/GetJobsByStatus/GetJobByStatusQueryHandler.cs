using Jobs.Application.Abstractions;
using Jobs.Application.DTOs;
using Jobs.Application.Mappings;
using Jobs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Queries.GetJobsByStatus
{
    public sealed class GetJobsByStatusQueryHandler : IQueryHandler<GetJobsByStatusQuery, IReadOnlyList<JobDto>>
    {
        private readonly IJobReadRepository _jobReadRepository;

        public GetJobsByStatusQueryHandler(IJobReadRepository jobReadRepository)
        {
            _jobReadRepository = jobReadRepository;
        }

        public async Task<IReadOnlyList<JobDto>> Handle(GetJobsByStatusQuery query, CancellationToken cancellationToken)
        {
            var jobs = await _jobReadRepository.GetJobsByStatusAsync(query.Status, cancellationToken);

            return jobs.Select(JobMappings.ToDto).ToList();
        }
    }
}
