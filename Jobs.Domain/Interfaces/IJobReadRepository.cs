using Jobs.Domain.Entities;
using Jobs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Interfaces
{
    public interface IJobReadRepository
    {
        Task<IReadOnlyList<Job>> GetAllAsync(CancellationToken cancellationToken = default);
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
