using Jobs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Interfaces
{
    public interface IJobRepository
    {
        Task<Job?> GetByIdAsync(Guid jobId, CancellationToken cancellationToken = default);

        Task AddAsync(Job job, CancellationToken cancellationToken = default);

        Task UpdateAsync(Job job, CancellationToken cancellationToken = default);

        Task DeleteAsync(Job job, CancellationToken cancellationToken = default);
    }
}
