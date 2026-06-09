using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Commands.CancelJob
{
    public sealed record CancelJobCommand(Guid JobId, string Reason);
}
