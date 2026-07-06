using Jobs.Application.Abstractions;
using Jobs.Application.DTOs;
using Jobs.Application.Mappings;
using Jobs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Queries.GetJobById
{
    public sealed class GetJobByIdQueryHandler : IQueryHandler<GetJobByIdQuery, JobDto?>
    {
        private readonly IJobReadRepository _jobReadRepository;

        public GetJobByIdQueryHandler(IJobReadRepository jobReadRepository)
        {
            _jobReadRepository = jobReadRepository;
        }

        public async Task<JobDto?> Handle(GetJobByIdQuery query, CancellationToken cancellationToken)
        {
            var job = await _jobReadRepository.GetByIdAsync(query.JobId, cancellationToken);

            return job is null ? null : JobMappings.ToDto(job);
        }
    }
}
