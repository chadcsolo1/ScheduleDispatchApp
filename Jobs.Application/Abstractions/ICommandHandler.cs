using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Abstractions
{
    public interface ICommandHandler<TCommand>
    {
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }
}
