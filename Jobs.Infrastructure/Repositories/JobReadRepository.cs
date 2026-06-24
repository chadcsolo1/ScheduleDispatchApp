using Jobs.Domain.Entities;
using Jobs.Domain.Enums;
using Jobs.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Infrastructure.Repositories
{
    internal class JobReadRepository : IJobReadRepository
    {
        private readonly JobsDbContext _context;

        public JobReadRepository(JobsDbContext context)
        {
            _context = context;
        }
        public async Task<Job?> GetByIdAsync(Guid jobId, CancellationToken cancellationToken = default)
        {
            return await _context.Jobs.Include(j => j.Checklist)
                                      .Include(j => j.Attachments)
                                     .FirstOrDefaultAsync(j => j.JobId == jobId, cancellationToken);
        }

        public async Task<IReadOnlyList<Job>> GetJobsByStatusAsync(JobStatus status, CancellationToken cancellationToken = default)
        {
            return await _context.Jobs.Include(j => j.Checklist)
                                      .Include(j => j.Attachments)
                                      .Where(j => j.Status == status)
                                      .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Job>> GetJobsForCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Jobs.Include(j => j.Checklist)
                                      .Include(j => j.Attachments)
                                      .Where(j => j.CustomerId == customerId)
                                      .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Job>> GetJobsScheduledForDayAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            return await _context.Jobs.Include(j => j.Checklist)
                                      .Include(j => j.Attachments)
                                      .Where(j => j.ScheduledFor == date.Date)
                                      .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Job>> GetOpenJobsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Jobs.Include(j => j.Checklist)
                                      .Include(j => j.Attachments)
                                      .Where(j => j.Status != JobStatus.Completed && j.Status != JobStatus.Canceled)
                                      .ToListAsync(cancellationToken);
        }
    }
}
