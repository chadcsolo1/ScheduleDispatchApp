using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.DTOs
{
    public sealed class JobDto
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public string Description { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public DateTime? ScheduledFor { get; init; }
        public Guid? AssignedTechnicianId { get; init; }

        public IReadOnlyList<ChecklistItemDto> Checklist { get; init; } = [];
        public IReadOnlyList<AttachmentDto> Attachments { get; init; } = [];
        public IReadOnlyList<string> RequiredSkills { get; init; } = [];
    }
}
