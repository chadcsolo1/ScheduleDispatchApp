using Jobs.Domain.Entities;
using Jobs.Domain.Enums;
using Jobs.Domain.Interfaces;
using Jobs.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Infrastructure.Repositories
{
    internal class JobReadRepository : IJobReadRepository
    {
        private readonly JobsDbContext _context;

        public JobReadRepository(JobsDbContext context)
        {
            _context = context;
        }

        public async Task<PaginationResult<Job>> GetAllAsync(IJobQuerySpecification query, CancellationToken cancellationToken = default)
        {
            int totalJobsCount = 0;

            IQueryable<Job> jobQuery = _context.Jobs
                .Include(j => j.Checklist)
                .Include(j => j.Attachments);

            foreach (var job in jobQuery)
            {
                Console.WriteLine($"Job: {job.Description}, Location: {job.Location}, JobType: {job.JobType}");
            }

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                jobQuery = jobQuery.Where(j => j.Description.ToLower().Contains(query.SearchTerm) ||
                                         j.Location.AddressLine1.ToLower().Contains(query.SearchTerm) ||
                                         j.Location.AddressLine2.ToLower().Contains(query.SearchTerm) ||
                                         j.Location.City.ToLower().Contains(query.SearchTerm) ||
                                         j.Location.State.ToLower().Contains(query.SearchTerm) ||
                                         j.JobType.JobTypeName.ToLower().Contains(query.SearchTerm) ||
                                         j.JobType.JobTypeCategory.ToLower().Contains(query.SearchTerm) ||
                                         j.RequiredSkills.Any(rs => rs.Name.ToLower().Contains(query.SearchTerm)) ||
                                         j.Status.ToString().ToLower().Contains(query.SearchTerm));

            }

            if (query.City != null)
            {
                    //if (!string.IsNullOrWhiteSpace(query.Location.AddressLine1))
                    //queryable = queryable.Where(j => j.Location.AddressLine1.Trim().ToLower() == query.Location.AddressLine1.Trim().ToLower());
    
                    //if (!string.IsNullOrWhiteSpace(query.Location.AddressLine2))
                    //    queryable = queryable.Where(j => j.Location.AddressLine2.Trim().ToLower() == query.Location.AddressLine2.Trim().ToLower());
    
                    if (!string.IsNullOrWhiteSpace(query.City))
                        jobQuery = jobQuery.Where(j => j.Location.City.Trim().ToLower() == query.City.Trim().ToLower());
    
                    
            }

            if (query.State != null) 
            {
                if (!string.IsNullOrWhiteSpace(query.State))
                        jobQuery = jobQuery.Where(j => j.Location.State.Trim().ToLower().Equals(query.State.Trim().ToLower()));
            }

            if (query.JobTypeName != null)
            {
                if (!string.IsNullOrWhiteSpace(query.JobTypeName))
                    jobQuery = jobQuery.Where(j => j.JobType.JobTypeName.ToLower().Contains(query.JobTypeName));
                //if (!string.IsNullOrWhiteSpace(query.JobTypeName))
                //    jobQuery = jobQuery.Where(j => j.JobType.JobTypeName.Trim().ToLower() == query.JobTypeName.Trim().ToLower());

            }

            if (query.JobTypeCategory != null)
            {
                if (!string.IsNullOrWhiteSpace(query.JobTypeCategory))
                    jobQuery = jobQuery.Where(j => j.JobType.JobTypeCategory.Trim().ToLower() == query.JobTypeCategory.Trim().ToLower());

            }

            totalJobsCount = await jobQuery.CountAsync(cancellationToken);

            if (query.SortBy != null)
            {
                switch (query.SortBy?.Trim().ToLower())
                {
                    case "createdate":
                    jobQuery = query.SortDirection?.Trim().ToLower() == "desc" ? jobQuery.OrderByDescending(j => j.CreatedAt) : jobQuery.OrderBy(j => j.CreatedAt);
                    break;
                    case "scheduledfor":
                    jobQuery = query.SortDirection?.Trim().ToLower() == "desc" ? jobQuery.OrderByDescending(j => j.ScheduledFor) : jobQuery.OrderBy(j => j.ScheduledFor);
                    break;
                    case "status":
                    jobQuery = query.SortDirection?.Trim().ToLower() == "desc" ? jobQuery.OrderByDescending(j => j.Status) : jobQuery.OrderBy(j => j.Status);
                    break;
                    default:
                    // Default sorting by CreatedAt if no valid sort field is provided
                    jobQuery = jobQuery.OrderBy(j => j.CreatedAt);
                    break;
                }
            }

            var items = await jobQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken); //Calling .ToListAsync to execute the query and retrieve the results

            return PaginationResult<Job>.Create(items, query.Page, query.PageSize, totalJobsCount);
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
