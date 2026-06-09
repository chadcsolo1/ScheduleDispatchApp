using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Abstractions
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default);
    }
}
