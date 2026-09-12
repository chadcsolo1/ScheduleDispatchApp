using Jobs.Domain.Entities;
using Jobs.Domain.Enums;
using Jobs.Domain.Models;
using Jobs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Jobs.Domain.Interfaces
{
    public interface IJobReadRepository
    {
        //Task<IReadOnlyList<Job>> GetAllAsync(string? search, JobType? jobType = null, Location? location = null, Sort? sort = null, CancellationToken cancellationToken = default);
        Task<PaginationResult<ExpandoObject>> GetAllAsync(IJobQuerySpecification queryParameters, CancellationToken cancellationToken = default);
        
        Task<Job?> GetByIdAsync(Guid jobId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Job>> GetJobsForCustomerAsync(
            Guid customerId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Job>> GetJobsByStatusAsync(
            JobStatus status,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Job>> GetOpenJobsAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Job>> GetJobsScheduledForDayAsync(
            DateTime date,
            CancellationToken cancellationToken = default);
    }
}
