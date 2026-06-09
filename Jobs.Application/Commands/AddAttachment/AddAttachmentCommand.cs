using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Commands.AddAttachment
{
    public sealed record AddAttachmentCommand(
    Guid JobId,
    string FileName,
    string Url);
}
