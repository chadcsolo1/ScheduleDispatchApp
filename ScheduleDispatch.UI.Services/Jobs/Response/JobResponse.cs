using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleDispatch.UI.Services.Jobs.Response
{
    public sealed class JobResponse
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public string Description { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;

        public DateTime CreatedAt { get; init; }
        public DateTime? ScheduledFor { get; init; }
        public Guid? AssignedTechnicianId { get; init; }

        // Location
        public string? AddressLine1 { get; init; }
        public string? AddressLine2 { get; init; }
        public string? City { get; init; }
        public string? State { get; init; }
        public int? ZipCode { get; init; }

        // Job Type
        public string? JobTypeName { get; init; }
        public string? JobTypeCategory { get; init; }
        public TimeSpan? JobTypeEstimatedDuration { get; init; }

        //public IReadOnlyList<ChecklistItemDto> CheckList { get; init; } = [];
        //public IReadOnlyList<AttachmentDto> Attachments { get; init; } = [];
        public IReadOnlyList<string> RequiredSkills { get; init; } = [];
    }
}
