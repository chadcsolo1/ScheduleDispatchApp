using Jobs.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Infrastructure.Persistence
{
    internal class EfUnitOfWork : IUnitOfWork
    {
        private readonly JobsDbContext _context;

        public EfUnitOfWork(JobsDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
