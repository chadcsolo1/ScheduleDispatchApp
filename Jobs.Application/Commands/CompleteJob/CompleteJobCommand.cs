using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Commands.CompleteJob
{
    public sealed record CompleteJobCommand(Guid JobId);
}
