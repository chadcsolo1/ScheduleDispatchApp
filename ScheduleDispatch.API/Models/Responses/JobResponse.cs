using Jobs.Application.DTOs;

namespace ScheduleDispatch.API.Models.Responses
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

        public IReadOnlyList<ChecklistItemDto> CheckList { get; init; } = [];
        public IReadOnlyList<AttachmentDto> Attachments { get; init; } = [];
        public IReadOnlyList<string> RequiredSkills { get; init; } = [];
    }
}
