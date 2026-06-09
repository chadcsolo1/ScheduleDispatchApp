using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.DTOs
{
    public sealed class CreateJobResponseDto
    {
        public Guid JobId { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
