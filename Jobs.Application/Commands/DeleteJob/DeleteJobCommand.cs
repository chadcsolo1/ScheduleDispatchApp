using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Commands.DeleteJob
{
    public sealed record DeleteJobCommand
    (
       Guid JobId     
    );
}
