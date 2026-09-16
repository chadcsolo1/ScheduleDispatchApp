using Jobs.Domain.Interfaces;
using Jobs.Domain.Models;

namespace ScheduleDispatch.API.Models.Responses
{
    public sealed class CreateJobResponse : ILinksResponse
    {
        public Guid JobId { get; init; }
        public DateTime CreatedAt { get; init; }
        public string Status { get; init; } = string.Empty;
        public List<LinkDto>? Links { get; set; }

    }
}
