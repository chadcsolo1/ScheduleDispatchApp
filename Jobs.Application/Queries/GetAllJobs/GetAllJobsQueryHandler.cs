using Jobs.Application.Abstractions;
using Jobs.Application.DTOs;
using Jobs.Application.Mappings;
using Jobs.Domain.Interfaces;
using Jobs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Queries.GetAllJobs
{
    public sealed class GetAllJobsQueryHandler : IQueryHandler<GetAllJobsQuery, PaginationResult<JobDto>>
    {
        private readonly IJobReadRepository _jobReadRepository;

        public GetAllJobsQueryHandler(IJobReadRepository jobReadRepository)
        {
            _jobReadRepository = jobReadRepository;
        }

        public async Task<PaginationResult<JobDto>> Handle(GetAllJobsQuery? query, CancellationToken cancellationToken)
        {
            var pagedJobs = await _jobReadRepository.GetAllAsync(query, cancellationToken);
            return new PaginationResult<JobDto>
            {
                Items = pagedJobs.Items.Select(JobMappings.ToDto).ToList(),
                Page = pagedJobs.Page,
                PageSize = pagedJobs.PageSize,
                TotalCount = pagedJobs.TotalCount,

            };

        }
    }
}
