using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Abstractions
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default);
    }
}
