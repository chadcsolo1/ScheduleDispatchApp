using Jobs.Domain.Entities;
using Jobs.Domain.Enums;
using Jobs.Domain.Interfaces;
using Jobs.Domain.Models;
using Jobs.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<IReadOnlyList<Job>> GetAllAsync(string? search, JobType? jobType = null, Location? location = null, Sort? sort = null, CancellationToken cancellationToken = default)
        {
            search ??= search?.Trim().ToLower();

            Console.WriteLine($"Search: {search}, JobType: {jobType}, Location: {location}");
            IQueryable<Job> query = _context.Jobs;

            foreach (var job in query)
            {
                Console.WriteLine($"Job: {job.Description}, Location: {job.Location}, JobType: {job.JobType}");
            }

            if (!string.IsNullOrEmpty(search))
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

            }

            if (location != null)
            {
                    if (!string.IsNullOrWhiteSpace(location.AddressLine1))
                    query = query.Where(j => j.Location.AddressLine1.Trim().ToLower() == location.AddressLine1.Trim().ToLower());
    
                    if (!string.IsNullOrWhiteSpace(location.AddressLine2))
                        query = query.Where(j => j.Location.AddressLine2.Trim().ToLower() == location.AddressLine2.Trim().ToLower());
    
                    if (!string.IsNullOrWhiteSpace(location.City))
                        query = query.Where(j => j.Location.City.Trim().ToLower() == location.City.Trim().ToLower());
    
                    if (!string.IsNullOrWhiteSpace(location.State))
                        query = query.Where(j => j.Location.State.Trim().ToLower().Equals(location.State.Trim().ToLower()));
            }

            if (jobType != null)
            {
                if (!string.IsNullOrWhiteSpace(jobType.JobTypeName))
                    query = query.Where(j => j.JobType.JobTypeName.Trim().ToLower() == jobType.JobTypeName.Trim().ToLower());

                if (!string.IsNullOrWhiteSpace(jobType.JobTypeCategory))
                    query = query.Where(j => j.JobType.JobTypeCategory.Trim().ToLower() == jobType.JobTypeCategory.Trim().ToLower());

                //if (!string.IsNullOrWhiteSpace(jobType.JobTypeEstimatedDuration.ToString()))
                //    query = query.Where(j => j.JobType.JobTypeEstimatedDuration == jobType.JobTypeEstimatedDuration);
            }

            if (sort != null)
            {
                switch (sort.SortBy?.Trim().ToLower())
                {
                    case "createdat":
                    query = sort.SortDirection?.Trim().ToLower() == "desc" ? query.OrderByDescending(j => j.CreatedAt) : query.OrderBy(j => j.CreatedAt);
                    break;
                    case "scheduledfor":
                    query = sort.SortDirection?.Trim().ToLower() == "desc" ? query.OrderByDescending(j => j.ScheduledFor) : query.OrderBy(j => j.ScheduledFor);
                    break;
                    case "status":
                    query = sort.SortDirection?.Trim().ToLower() == "desc" ? query.OrderByDescending(j => j.Status) : query.OrderBy(j => j.Status);
                    break;
                    default:
                    // Default sorting by CreatedAt if no valid sort field is provided
                    query = query.OrderBy(j => j.CreatedAt);
                    break;
                }
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
