using Jobs.Domain.Entities;
using Jobs.Domain.Enums;
using Jobs.Domain.Interfaces;
using Jobs.Domain.ValueObjects;
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

        public async Task<IReadOnlyList<Job>> GetAllAsync(string? search, JobType? jobType = null, Location? location = null, CancellationToken cancellationToken = default)
        {
            search ??= search?.Trim().ToLower();


            IQueryable<Job> query = _context.Jobs;

            if (!string.IsNullOrEmpty(search) || jobType != null || location != null)
            {
                query = query.Where(j => j.Description.ToLower().Contains(search) ||
                                         j.Location.AddressLine1.ToLower().Contains(search) ||
                                         j.Location.AddressLine2.ToLower().Contains(search) ||
                                         j.Location.City.ToLower().Contains(search) ||
                                         j.Location.State.ToLower().Contains(search) ||
                                         j.JobType.JobTypeName.ToLower().Contains(search) ||
                                         j.JobType.JobTypeCategory.ToLower().Contains(search) ||
                                         j.RequiredSkills.Any(rs => rs.Name.ToLower().Contains(search)) ||
                                         j.Status.ToString().ToLower().Contains(search));
                //.Where(j => location.AddressLine1 == null || j.Location.AddressLine1.Trim().ToLower() == location.AddressLine1.Trim().ToLower())
                //.Where(j => location.AddressLine2 == null || j.Location.AddressLine2.Trim().ToLower() == location.AddressLine2.Trim().ToLower())
                //.Where(j => location.City == null || j.Location.City.Trim().ToLower() == location.City.Trim().ToLower())
                //.Where(j => location.State == null || j.Location.State.Trim().ToLower() == location.State.Trim().ToLower())
                //.Where(j => location.ZipCode == null || j.Location.ZipCode == location.ZipCode);
                //.Where(j => jobType == null || j.JobType == jobType)
                //.Where(j => location.AddressLine1 == null || j.Location.AddressLine1.Trim().ToLower() == location.AddressLine1.Trim().ToLower() ||
                //       j => location.AddressLine2 == null || j.Location.AddressLine2.Trim().ToLower() == location.AddressLine2.Trim().ToLower() ||
                //       location.City == null || j.Location.City.Trim().ToLower() == location.City.Trim().ToLower() ||
                //       location.State == null || j.Location.State.Trim().ToLower() == location.State.Trim().ToLower() ||
                //       location.ZipCode == null || j.Location.ZipCode == location.ZipCode);

                query = query.Where(j => location.AddressLine1 == null || j.Location.AddressLine1.Trim().ToLower() == location.AddressLine1.Trim().ToLower() ||
                                   location.AddressLine2 == null || j.Location.AddressLine2.Trim().ToLower() == location.AddressLine2.Trim().ToLower() ||
                                   location.City == null || j.Location.City.Trim().ToLower() == location.City.Trim().ToLower() ||
                                   location.State == null || j.Location.State.Trim().ToLower() == location.State.Trim().ToLower() ||
                                   location.ZipCode == null || j.Location.ZipCode == location.ZipCode);

            }

            return await query.Include(j => j.Checklist)
                              .Include(j => j.Attachments)
                              .ToListAsync(cancellationToken);
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
