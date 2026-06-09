using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
