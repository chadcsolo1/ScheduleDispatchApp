using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.DTOs
{
    public sealed class ChecklistItemDto
    {
        public Guid Id { get; init; }
        public string Description { get; init; } = string.Empty;
        public bool IsRequired { get; init; }
        public bool IsCompleted { get; init; }
        public DateTime? CompletedAt { get; init; }
    }
}
