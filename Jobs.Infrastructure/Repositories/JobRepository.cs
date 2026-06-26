using Jobs.Domain.Entities;
using Jobs.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Infrastructure.Repositories
{
    internal class JobRepository : IJobRepository
    {
        private readonly JobsDbContext _context;

        public JobRepository(JobsDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Job job, CancellationToken cancellationToken = default)
        {
            await _context.Jobs.AddAsync(job, cancellationToken);
        }

        public async Task DeleteAsync(Job job, CancellationToken cancellationToken = default)
        {
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task UpdateAsync(Job job, CancellationToken cancellationToken = default)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Job?> GetByIdAsync(Guid jobId, CancellationToken cancellationToken = default)
        {
            return await _context.Jobs.Include(j => j.Checklist)
                                      .Include(j => j.Attachments)
                                     .FirstOrDefaultAsync(j => j.JobId == jobId, cancellationToken);
        }
    }
}
